namespace GestaoDeEquipamentos.ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        // Instancia o construtor do servidor...
        // ...configura as dependências internas (controladores e serviços) e externas (repositórios)
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();

        // Constroi a instância do servidor...
        // ...configura os middlewares executados durante requisições
        WebApplication app = builder.Build();

        // Mapeia controladores e rotas que serão gerenciadas pelo servidor
        app.UseRouting();
        app.MapControllers();

        // Inicia o loop do servidor web, escuta por requisições na porta especificada
        app.Run();
    }
}