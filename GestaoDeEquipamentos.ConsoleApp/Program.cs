using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using System.Text;

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

static Task PaginaInicial(HttpContext context)
{
    string conteudo = File.ReadAllText("Html/PaginaInicial.html");

    return context.Response.WriteAsync(conteudo);
}

static Task VisualizarFabricantes(HttpContext context)
{
    ContextoDados contextoDados = new ContextoDados(true);
    IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

    List<Fabricante> fabricantes = repositorioFabricante.SelecionarRegistros();

    string conteudo = File.ReadAllText("Html/VisualizarFabricantes.html");

    StringBuilder sb = new StringBuilder(conteudo);

    foreach (Fabricante f in fabricantes)
    {
        string itemLista = $"<li>{f.ToString()} / <a href=\"/fabricantes/editar/{f.Id}\">Editar</a> / <a href=\"/fabricantes/excluir/{f.Id}\">Excluir</a></li> #fabricante#";

        sb.Replace("#fabricante#", itemLista);
    }

    sb.Replace("#fabricante#", "");

    string conteudoString = sb.ToString();

    return context.Response.WriteAsync(conteudoString);
}

static Task FormularioCadastrarFabricante(HttpContext context)
{
    string form = File.ReadAllText("Html/CadastrarFabricante.html");

    return context.Response.WriteAsync(form);
}

static Task CadastrarFabricante(HttpContext context)
{
    ContextoDados contextoDados = new ContextoDados(true);
    IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

    string nome = context.Request.Form["nome"].ToString();
    string email = context.Request.Form["email"].ToString();
    string telefone = context.Request.Form["telefone"].ToString();

    Fabricante novoFabricante = new Fabricante(nome, email, telefone);

    repositorioFabricante.CadastrarRegistro(novoFabricante);

    string conteudo = File.ReadAllText("Html/Notificacao.html");

    StringBuilder sb = new StringBuilder(conteudo);

    sb.Replace("#mensagem#", $"O registro \"{novoFabricante.Nome}\" foi cadastrado com sucesso!");

    string conteudoString = sb.ToString();

    return context.Response.WriteAsync(conteudoString);
}



static Task FormularioEditarFabricante(HttpContext context)
{
    ContextoDados contextoDados = new ContextoDados(true);
    IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

    int id = Convert.ToInt32(context.GetRouteValue("id"));

    Fabricante fabricante = repositorioFabricante.SelecionarRegistroPorId(id);

    string conteudo = File.ReadAllText("Html/EditarFabricante.html");

    StringBuilder sb = new StringBuilder(conteudo);

    sb.Replace("#id#", id.ToString());
    sb.Replace("#nome#", fabricante.Nome);
    sb.Replace("#email#", fabricante.Email);
    sb.Replace("#telefone#", fabricante.Telefone);

    string conteudoString = sb.ToString();

    return context.Response.WriteAsync(conteudoString);
}

static Task EditarFabricante(HttpContext context)
{
    ContextoDados contextoDados = new ContextoDados(true);
    IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

    string nome = context.Request.Form["nome"].ToString();
    string email = context.Request.Form["email"].ToString();
    string telefone = context.Request.Form["telefone"].ToString();

    Fabricante fabricanteAtualizado = new Fabricante(nome, email, telefone);

    int id = Convert.ToInt32(context.GetRouteValue("id"));

    repositorioFabricante.EditarRegistro(id, fabricanteAtualizado);

    string conteudo = File.ReadAllText("Html/Notificacao.html");

    StringBuilder sb = new StringBuilder(conteudo);

    sb.Replace("#mensagem#", $"O registro \"{fabricanteAtualizado.Nome}\" foi editado com sucesso!");

    string conteudoString = sb.ToString();

    return context.Response.WriteAsync(conteudoString);
}




static Task FormularioExcluirFabricante(HttpContext context)
{
    ContextoDados contextoDados = new ContextoDados(true);
    IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

    int id = Convert.ToInt32(context.GetRouteValue("id"));

    Fabricante fabricante = repositorioFabricante.SelecionarRegistroPorId(id);

    string conteudo = File.ReadAllText("Html/ExcluirFabricante.html");

    StringBuilder sb = new StringBuilder(conteudo);

    sb.Replace("#id#", id.ToString());
    sb.Replace("#fabricante#", fabricante.Nome);

    string conteudoString = sb.ToString();

    return context.Response.WriteAsync(conteudoString);
}

static Task ExcluirFabricante(HttpContext context)
{
    ContextoDados contextoDados = new ContextoDados(true);
    IRepositorioFabricante repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);

    int id = Convert.ToInt32(context.GetRouteValue("id"));

    repositorioFabricante.ExcluirRegistro(id);

    string conteudo = File.ReadAllText("Html/Notificacao.html");

    StringBuilder sb = new StringBuilder(conteudo);

    sb.Replace("#mensagem#", $"O registro \"{id}\" foi excluído com sucesso!");

    string conteudoString = sb.ToString();

    return context.Response.WriteAsync(conteudoString);
}