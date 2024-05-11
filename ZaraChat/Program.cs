public class MyClass
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
        
        

        app.Run();
    }
}