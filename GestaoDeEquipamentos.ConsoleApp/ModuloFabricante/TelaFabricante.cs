namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;


public class TelaFabricante
{
    private RepositorioFabricante repositorioFabricante = new RepositorioFabricante();

    public char ApresentarMenu()
    {
        Console.Clear();
        Console.WriteLine("Gestão de Fabricantes");
        Console.WriteLine("1 - Cadastrar Fabricante");
        Console.WriteLine("2 - Editar Fabricante");
        Console.WriteLine("3 - Excluir Fabricante");
        Console.WriteLine("4 - Visualizar Fabricantes");
        Console.WriteLine("0 - Voltar");
        Console.Write("Opção: ");
        return Console.ReadLine()![0];
    }

    public void Cadastrar()
    {
        Console.Clear();
        Console.WriteLine("Cadastro de Fabricante");

        Console.Write("Nome: ");
        string nome = Console.ReadLine()!;
        Console.Write("Email: ");
        string email = Console.ReadLine()!;
        Console.Write("Telefone: ");
        string telefone = Console.ReadLine()!;

        Fabricante novo = new Fabricante(nome, email, telefone);
        repositorioFabricante.CadastrarFabricante(novo);

        Console.WriteLine("Fabricante cadastrado com sucesso!");
    }

    public void Editar()
    {
        Visualizar();

        Console.Write("ID para editar: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Fabricante existente = repositorioFabricante.SelecionarPorId(id);
        if (existente == null)
        {
            Console.WriteLine("Fabricante não encontrado.");
            return;
        }

        Console.Write("Novo nome: ");
        string nome = Console.ReadLine()!;
        Console.Write("Novo email: ");
        string email = Console.ReadLine()!;
        Console.Write("Novo telefone: ");
        string telefone = Console.ReadLine()!;

        Fabricante atualizado = new Fabricante(nome, email, telefone);
        repositorioFabricante.EditarFabricante(id, atualizado);

        Console.WriteLine("Fabricante editado com sucesso!");
    }

    public void Excluir()
    {
        Visualizar();

        Console.Write("ID para excluir: ");
        int id = Convert.ToInt32(Console.ReadLine());

        if (repositorioFabricante.ExcluirFabricante(id))
            Console.WriteLine("Fabricante excluído.");
        else
            Console.WriteLine("Fabricante não encontrado.");
    }

    public void Visualizar()
    {
        Console.Clear();
        Console.WriteLine("Lista de Fabricantes:\n");

        var fabricantes = repositorioFabricante.SelecionarTodos();

        foreach (var f in fabricantes)
            f.Exibir();

        Console.WriteLine();
    }

    public Fabricante SelecionarFabricante()
    {
        Visualizar();

        Console.Write("ID do fabricante: ");
        int id = Convert.ToInt32(Console.ReadLine());

        return repositorioFabricante.SelecionarPorId(id);
    }
}
