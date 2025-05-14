using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        // criar um servidor web
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        WebApplication app = builder.Build();

        app.MapGet("/", PaginaInicial);

        //app.MapGet("fabricantes/visualizar", VisualizarFabricantes);
       
        app.Run();
    }

    static Task PaginaInicial(HttpContext context)
    {
        string conteudo = File.ReadAllText("Html/PaginaInicial.html");
        return context.Response.WriteAsync(conteudo);
    }

    //static Task VisualizarFabricantes(HttpContext context)
    //{
    //    ContextoDados contextoDados = new ContextoDados(true);
    //    IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

    //    string conteudo = File.ReadAllText("ModuloFabricante/Html/Visualizar.html");

    //    StringBuilder stringBuilder = new StringBuilder(contextoDados)

    //    foreach (fabricante f in repositorioFabricante.SelecionarRegistro())
    //    {
    //        string itemLista = $"<li>{f.ToString()}</li> #fabricante#";
    //        stringBuilder.Replace("#fabricante#", itemLista);
    //    }


    //}
}
