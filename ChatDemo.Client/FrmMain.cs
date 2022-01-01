using Microsoft.AspNetCore.SignalR.Client;

namespace ChatDemo.Client
{
    public partial class FrmMain : Form
    {
        private HubConnection _connection;
        public FrmMain()
        {
            InitializeComponent();
        }

        private async void btnSendMessage_Click(object sender, EventArgs e)
        {
            try
            {
                await _connection.InvokeAsync("SendMessage", txtUser.Text, txtNewMessage.Text);
            }
            catch (Exception ex)
            {
                AppendMessage(ex.Message);
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5025/hub/message")
                .Build();
            _connection.Closed += async (error) =>
            {
                await _connection.StartAsync();
            };
            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Invoke(() =>
                {
                    var newMessage = $"{user}: {message}";
                    AppendMessage(newMessage);
                });
            });
            try
            {
                _connection.StartAsync();
                AppendMessage("已成功连接到消息服务器");
                txtNewMessage.Focus();
            }
            catch (Exception ex)
            {
                AppendMessage(ex.Message);
            }
        }

        private void AppendMessage(string message)
        {
            rtxtMessages.AppendText($"[{DateTime.Now}]==>>{message}{Environment.NewLine}");
        }
    }
}