using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp;

class Program
{
    static void Main(string[] args)
    {
        TelaFabricante telaFabricante = new TelaFabricante();
        TelaEquipamento telaEquipamento = new TelaEquipamento(telaFabricante);

        TelaChamado telaChamado = new TelaChamado(telaEquipamento.repositorioEquipamento);
        TelaPrincipal telaPrincipal = new TelaPrincipal();

        while (true)
        {
            char opcaoPrincipal = telaPrincipal.ApresentarMenuPrincipal();

            switch (opcaoPrincipal)
            {
                case '1': 
                    GerenciarEquipamentos(telaEquipamento);
                    break;

                case '2': 
                    GerenciarChamados(telaChamado);
                    break;

                case '3': 
                    GerenciarFabricantes(telaFabricante);
                    break;

                case '0': 
                    return;

                default:
                    Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red);
                    break;
            }

            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
        }
    }

    static void GerenciarEquipamentos(TelaEquipamento telaEquipamento)
    {
        char opcao = telaEquipamento.ApresentarMenu();

        switch (opcao)
        {
            case '1': telaEquipamento.CadastrarEquipamento(); break;
            case '2': telaEquipamento.EditarEquipamento(); break;
            case '3': telaEquipamento.ExcluirEquipamento(); break;
            case '4': telaEquipamento.VisualizarEquipamentos(true); break;
            default: Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red); break;
        }
    }

    static void GerenciarChamados(TelaChamado telaChamado)
    {
        char opcao = telaChamado.ApresentarMenu();

        switch (opcao)
        {
            case '1': telaChamado.CadastrarChamado(); break;
            case '2': telaChamado.EditarChamado(); break;
            case '3': telaChamado.ExcluirChamado(); break;
            case '4': telaChamado.VisualizarChamados(true); break;
            default: Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red); break;
        }
    }

    static void GerenciarFabricantes(TelaFabricante telaFabricante)
    {
        char opcao = telaFabricante.ApresentarMenu();

        switch (opcao)
        {
            case '1': telaFabricante.Cadastrar(); break;
            case '2': telaFabricante.Editar(); break;
            case '3': telaFabricante.Excluir(); break;
            case '4': telaFabricante.Visualizar(); break;
            case '0': break;
            default: Notificador.ExibirMensagem("Opção inválida!", ConsoleColor.Red); break;
        }
    }
}
