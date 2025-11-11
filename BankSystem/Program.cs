using Api.Services;
using BankSystem.Api.Repositories;
using BankSystem.Data;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {


        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<BankContext>(
            options => options.UseSqlServer(builder.Configuration.GetConnectionString("BankSystem"))
        );

        builder.Services.AddScoped<IContaRepository, ContaRepository>();
        builder.Services.AddScoped<IContaService, ContaService>();
        builder.Services.AddScoped<IClienteService, ClienteService>();
        builder.Services.AddScoped<IClienteRepository, ClienteRepository>();

        builder.Services.AddControllers();

        var app = builder.Build();


        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.MapGet("/health", async (BankContext context) =>
        {

            bool testeConexao = await context.Database.CanConnectAsync();

            if (testeConexao)
            {
                return Results.Ok("Banco está Funcionando!");
            }
            else
            {
                return Results.Problem("Banco está Fora do Ar!");
            }
        });

        app.Run();
    }


}
