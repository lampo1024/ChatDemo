using ChatDemo.Server.Hubs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<MessageHub>("/hub/message");
app.Run();