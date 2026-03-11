var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:8000");

var app = builder.Build();

app.MapGet("/", () =>
    10 + 15
);

app.MapGet("/saudacao", () => 
    new {
            mensagem = "Olá, mundo!" 
        }
);

//
app.Run();