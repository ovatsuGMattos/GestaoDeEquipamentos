namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

public class Fabricante
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public int QuantidadeEquipamentos { get; set; }

    public Fabricante(string nome, string email, string telefone)
    {
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

    public void Exibir()
    {
        Console.WriteLine($"ID: {Id} | Nome: {Nome} | Email: {Email} | Telefone: {Telefone} | Qtd Equip.: {QuantidadeEquipamentos}");
    }
}