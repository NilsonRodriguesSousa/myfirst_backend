using myapi.Models;

// Cria o construtor da aplicação
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Define a URL onde a API será executada
builder.WebHost.UseUrls("http://localhost:8000");

// Variáveis de controle
Funcionario[] funcionarios = new Funcionario[100];
int totalFuncionarios = 0;

// Constrói a aplicação
var app = builder.Build();

app.UseCors("AllowAll");

app.MapGet("/", () =>
{
    return Results.Ok("API EduCalc funcionando com sucesso!");
});

app.MapGet("/operacoes", () =>
{
    return Results.Ok(new
    {
        mensagem = "Operações disponíveis",
        operacoes = new string[] { "soma", "subtracao", "multiplicacao", "divisao" }
    });
});

app.MapGet("/calcular/{operacao}/{a}/{b}", (string operacao, double a, double b) =>
{
    operacao = operacao.ToLower();

    double resultado;

    switch (operacao)
    {
        case "soma":
            resultado = a + b;
            return Results.Ok(new
            {
                operacao = "soma",
                valor1 = a,
                valor2 = b,
                resultado = resultado
            });

        case "subtracao":
            resultado = a - b;
            return Results.Ok(new
            {
                operacao = "subtracao",
                valor1 = a,
                valor2 = b,
                resultado = resultado
            });

        case "multiplicacao":
            resultado = a * b;
            return Results.Ok(new
            {
                operacao = "multiplicacao",
                valor1 = a,
                valor2 = b,
                resultado = resultado
            });

        case "divisao":            
            if (b == 0)
            {
                return Results.BadRequest(new
                {
                    erro = "Não é possível realizar divisão por zero."
                });
            }

            resultado = a / b;
            return Results.Ok(new
            {
                operacao = "divisao",
                valor1 = a,
                valor2 = b,
                resultado = resultado
            });
        
        default:
            return Results.BadRequest(new
            {
                erro = "Operação inválida. Utilize: soma, subtracao, multiplicacao ou divisao."
            });
    }
});

app.MapGet("/exemplo_funcionario", () =>
{
    //using myapi.Models;
    Funcionario funcionario = new ();

    funcionario.Nome = "Fulano"; 
    funcionario.Idade = 19;
    funcionario.Cargo = "Fiscal";

    Console.WriteLine("Nome: " + funcionario.Nome);
    Console.WriteLine("Idade: " + funcionario.Idade);
    Console.WriteLine("Cargo: " + funcionario.Cargo);

    return Results.Ok(new
    {
        nome = funcionario.Nome,
        idade = funcionario.Idade,
        cargo = funcionario.Cargo
    });
});

app.MapGet("/funcionarios/{nome}/{idade}/{cargo}/{departamento}",
    (string nome, int idade, string cargo, string departamento) =>
{
    // Verifica se o vetor está cheio
    if (totalFuncionarios >= funcionarios.Length)
    {
        return Results.BadRequest("Limite de funcionários atingido.");
    }

    Funcionario f = new Funcionario();

    f.Nome = nome;
    f.Idade = idade;
    f.Cargo = cargo;
    f.Departamento = departamento;

    // Adiciona no vetor 
    funcionarios[totalFuncionarios] = f;
    totalFuncionarios++;

    return Results.Ok(new
    {
        nome = f.Nome,
        idade = f.Idade,
        cargo = f.Cargo,
        departamento = f.Departamento
    });
});

app.MapGet("/funcionarios", () =>
{
    var lista = new List<object>();

    for (int i = 0; i < totalFuncionarios; i++)
    {
        var f = funcionarios[i];

        lista.Add(new
        {
            nome = f.Nome,
            idade = f.Idade,
            cargo = f.Cargo,
            departamento = f.Departamento
        });
    }

    return Results.Ok(lista);
});



app.Run();