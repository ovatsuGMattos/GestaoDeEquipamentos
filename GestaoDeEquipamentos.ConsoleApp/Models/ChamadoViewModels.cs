using GestaoDeEquipamentos.ConsoleApp.Extensoes;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;


namespace GestaoDeEquipamentos.ConsoleApp.Models;

public abstract class FormularioChamadoViewModel
{
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public int EquipamentoId { get; set; }
    public List<SelecionarEquipamentoViewModel> EquipamentosDisponiveis { get; set; }
    public FormularioChamadoViewModel()
    {
        EquipamentosDisponiveis = new List<SelecionarEquipamentoViewModel>();
    }
}
public class SelecionarEquipamentoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public SelecionarEquipamentoViewModel(int id, string nome)
    {
        Id = id;
        Nome = nome;
    }
}

public class CadastrarChamadoViewModel : FormularioChamadoViewModel
{
    public CadastrarChamadoViewModel() { }

    public CadastrarChamadoViewModel(List<Equipamento> equipamentos)
    {
        foreach (var e in equipamentos)
        {
            var selecionarVm = new SelecionarEquipamentoViewModel(e.Id, e.Nome);

            EquipamentosDisponiveis.Add(selecionarVm);
        }
    }

}
public class EditarChamadoViewModel : FormularioChamadoViewModel
{
    public int Id { get; set; }
    public EditarChamadoViewModel() { }

    public EditarChamadoViewModel(int id, string titulo, string descricao, int equipamentoId, List<Equipamento> equipamentos)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        EquipamentoId = equipamentoId;

        foreach (var equipamento in equipamentos)
        {
            var SelecionarVm = new SelecionarEquipamentoViewModel(equipamento.Id, equipamento.Nome);

            EquipamentosDisponiveis.Add(SelecionarVm);
        }
    }
}
public class ExcluirChamadoViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; }

    public ExcluirChamadoViewModel(int id, string titulo)
    {
        Id = id;
        Titulo = titulo;
    }
}

public class VizualizarChamadosViewModel
{
    public List<DetalhesChamadoViewModel> Registros { get; set; }
    public VizualizarChamadosViewModel(List<Chamado> chamados)
    {
        Registros = new List<DetalhesChamadoViewModel>();
        {
            foreach (var c in chamados)
            {
                var detalhesVm = c.ParaDetahesVm();
                Registros.Add(detalhesVm);
            }
        }
    }
}

public class DetalhesChamadoViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataAbertura { get; set; }
    public int TempoDecorrido { get; set; }
    public string NomeEquipamento { get; set; }

    public DetalhesChamadoViewModel(
        int id,
        string titulo,
        string descricao,
        DateTime dataAbertura,
        int tempoDecorrido,
        string nomeEquipamento)
    {
        Id = id;
        Titulo = titulo;
        Descricao = descricao;
        DataAbertura = dataAbertura;
        TempoDecorrido = tempoDecorrido;
        NomeEquipamento = nomeEquipamento;

    }
    public override string ToString()
    {
        return $"Id: {Id} - Equipamento:{NomeEquipamento} - Título: {Titulo} - Descrição: {Descricao} - Data de Abertura : {DataAbertura}- Dias Em Aberto :{TempoDecorrido} - Nome Equipamento : {NomeEquipamento}";
    }

}


