using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

static void Main(string[] args)
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    WebApplication app = builder.Build();

    ContextoDados contextoDados = new ContextoDados(true);
    IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

    if (repositorioFabricante.SelecionarRegistros().Count < 1)
        repositorioFabricante.CadastrarRegistro(new Fabricante("Dell", "contato@dell.com", "21 3222-3322"));

    app.MapGet("/", PaginaInicial);

    app.MapGet("/fabricantes/cadastrar", FormularioCadastrarFabricante);
    app.MapPost("/fabricantes/cadastrar", CadastrarFabricante);

    app.MapGet("/fabricantes/editar/{id:int}", FormularioEditarFabricante);
    app.MapPost("/fabricantes/editar/{id:int}", EditarFabricante);

    app.MapGet("/fabricantes/excluir/{id:int}", FormularioExcluirFabricante);
    app.MapPost("/fabricantes/excluir/{id:int}", ExcluirFabricante);

    app.MapGet("/fabricantes/visualizar", VisualizarFabricantes);

    app.Run();
}