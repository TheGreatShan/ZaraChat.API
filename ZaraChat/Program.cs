using Microsoft.AspNetCore.Mvc;
using ZaraChat.BusinessLogic;
using ZaraChat.Chat;

namespace ZaraChat;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();


        app.MapGet("/version", () => { return "v0.0.1"; })
            .WithName("Version")
            .WithOpenApi();

        // TODO pass the token with the Header
        app.MapPost("/ask", async (HttpContext context, List<ChatMessage> chatMessages) =>
                await ChatService.Ask(new ApiInput(context.Request.Headers.Authorization!, chatMessages)))
            .WithName("Ask")
            .WithOpenApi();


        app.Run();
    }
}