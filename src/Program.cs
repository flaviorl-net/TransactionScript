using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppContext>(x => x.UseInMemoryDatabase("clienteDB"));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/cliente/todos", (AppContext context) =>
{
    var todos = context
        .Cliente
        .Include("EnderecoCliente")
        .ToList();

    return todos is null ? Results.NotFound() : Results.Ok(todos);
});

app.MapPost("/cliente/cadastrar", (AppContext context, ClienteViewModel clienteViewModel, IEmailService emailService) =>
{
    var cliente = new Cliente(Guid.NewGuid(), clienteViewModel.Nome, clienteViewModel.SobreNome, clienteViewModel.DataNascimento);
    
    cliente.AdicionarEndereco(Guid.NewGuid(), clienteViewModel.Logradouro, 
        clienteViewModel.Endereco, clienteViewModel.Numero, clienteViewModel.Complemento, cliente.Id);

    if (!cliente.ValidationResult.IsValid)
    {
        return Results.BadRequest(cliente.ValidationResult.Errors);
    }

    context.Cliente.Add(cliente);

    if (context.SaveChanges() == 0)
    {
        return Results.BadRequest("Cliente não cadastrado, tente novamente mais tarde.");
    }

    if (!emailService.EnviarEmail("contato@empresa.com", new string[] { "ana@empresa.com" }, "Bem vindo(a)", "Seja bem vindo(a)"))
    {
        return Results.Ok("Cliente cadastrado, mas não foi possível comunica-lo.");
    }

    return Results.StatusCode(201);
});

app.Run();
