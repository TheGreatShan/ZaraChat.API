using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using ZaraChat.BusinessLogic;
using ZaraChat.Chat;

namespace ZaraChat;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        var app = builder.Build();

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseHttpsRedirection();


        app.MapGet("/version", () => { return "v0.0.1"; })
            .WithName("Version")
            .WithOpenApi();

        app.MapPost("/ask", async (HttpContext context, List<ChatMessage> chatMessages) =>
                await ChatService.Ask(new ApiInput(context.Request.Headers.Authorization!, chatMessages)))
            .WithName("Ask")
            .WithOpenApi();

        app.MapPost("/toText", async (HttpContext context, string fileName) =>
            {
                using var ms = new MemoryStream();
                await context.Request.Body.CopyToAsync(ms);
                var content = ms.ToArray();

                return ChatService.SpeechToText(
                    new SpeechToTextInput(
                        context.Request.Headers.Authorization!,
                        content,
                        fileName));
            })
            .Accepts<byte[]>("application/octet-stream")
            .WithName("SpeechToText")
            .WithOpenApi();


        app.Run();
    }
}