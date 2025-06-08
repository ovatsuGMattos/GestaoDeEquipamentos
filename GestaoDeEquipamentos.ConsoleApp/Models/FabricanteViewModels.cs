using GestaoDeEquipamentos.ConsoleApp.Extensoes;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.Models;



public abstract class FormularioFabricanteViewModel
{
    public string Nome { get; set; }

    public string Email { get; set; }
    public string Telefone { get; set; }

}


public class CadastrarFabricantesViewModel : FormularioFabricanteViewModel
{



    public CadastrarFabricantesViewModel() { }


    public CadastrarFabricantesViewModel(string nome, string email, string telefone) : this()
    {

        Nome = nome;
        Email = email;
        Telefone = telefone;

    }

}
public class EditarFabricantesViewModel : FormularioFabricanteViewModel
{

    public int Id { get; set; }


    public EditarFabricantesViewModel()
    {
    }
    public EditarFabricantesViewModel(int id, string nome, string email, string telefone) : this()
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;

    }


}

public class ExcluirFabricantesViewModel
{

    public int Id { get; set; }
    public string Nome { get; set; }

    public ExcluirFabricantesViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;


    }


}

public class VizualizarFabricantesViewModel
{

    public List<DetalhesFabricanteViewModel> registros { get; } = new List<DetalhesFabricanteViewModel>();
    public VizualizarFabricantesViewModel(List<Fabricante> fabricantes)
    {
        foreach (Fabricante f in fabricantes)
        {
            DetalhesFabricanteViewModel DetalhesVm = f.ParaDetalhesVm();
            registros.Add(DetalhesVm);
        }
    }


}
public class DetalhesFabricanteViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public string Email { get; set; }
    public string Telefone { get; set; }

    public DetalhesFabricanteViewModel(int id, string nome, string email, string telefone)
    {
        Id = id;
        Nome = nome;
        Email = email;
        Telefone = telefone;
    }

    public override string ToString()
    {
        return $"id: {Id}, Nome: {Nome} , Email : {Email} , telefone{Telefone}";
    }
}