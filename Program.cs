using WebApplication1.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
    obj =>
    {
        obj.AddPolicy(name: "OpenChatSystem", obj =>
        {
            obj.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });

    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("chatSystem", builder => builder
        .WithOrigins("http://localhost:4200", "http://localhost:50785")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();
//app.UseCors("OpenChatSystem");
app.UseCors("chatSystem");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHub<ChatHub>("/chathub");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();



app.MapControllers();

app.Run();
