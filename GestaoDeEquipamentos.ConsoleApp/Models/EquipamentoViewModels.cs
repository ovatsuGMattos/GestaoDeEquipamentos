using GestaoDeEquipamentos.ConsoleApp.Extensoes;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.Models
{

    public abstract class FormularioEquipamentoViewModel
    {
        public string Nome { get; set; }
        public decimal PrecoAquisicao { get; set; }
        public DateTime DataFabricacao { get; set; }
        public int FabricanteId { get; set; }
        public List<selecionarFabricanteViewModel> FabricantesDisponiveis { get; set; }
        protected FormularioEquipamentoViewModel()
        {
            FabricantesDisponiveis = new List<selecionarFabricanteViewModel>();
        }

    }
    public class selecionarFabricanteViewModel
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public selecionarFabricanteViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

    }

    public class CadastrarEquipamentoViewModel : FormularioEquipamentoViewModel
    {

        public CadastrarEquipamentoViewModel()
        {

        }

        public int Id { get; set; }
        public CadastrarEquipamentoViewModel(List<Fabricante> fabricantes)
        {
            foreach (var fabricante in fabricantes)
            {
                var selecionarVm = new selecionarFabricanteViewModel(fabricante.Id, fabricante.Nome);
                FabricantesDisponiveis.Add(selecionarVm);

            }
        }
    }


    public class EditarEquipamentoViewModel : FormularioEquipamentoViewModel
    {
        public int Id { get; set; }
        public EditarEquipamentoViewModel() { }
        public EditarEquipamentoViewModel(int id, string nome, decimal precoAquisicao, DateTime dataFabricacao, int fabricanteId, List<Fabricante> fabricantes)
        {
            Id = id;
            Nome = nome;
            PrecoAquisicao = precoAquisicao;
            DataFabricacao = dataFabricacao;
            FabricanteId = fabricanteId;

            foreach (var fabricante in fabricantes)
            {
                var selecionarVm = new selecionarFabricanteViewModel(fabricante.Id, fabricante.Nome);
                FabricantesDisponiveis.Add(selecionarVm);

            }
        }
    }

    public class ExcluirEquipamentoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ExcluirEquipamentoViewModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    public class VizualizarEquipamentoViewModels
    {
        public List<DetalhesEquipamentoViewModel> registros { get; set; }

        public VizualizarEquipamentoViewModels(List<Equipamento> equipamentos)
        {
            registros = new List<DetalhesEquipamentoViewModel>();
            foreach (Equipamento e in equipamentos)
            {
                DetalhesEquipamentoViewModel DetalhesVm = e.ParaDetalhesVm();
                registros.Add(DetalhesVm);
            }

        }

    }
    public class DetalhesEquipamentoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoAquisicao { get; set; }
        public DateTime DataFabricaçao { get; set; }
        public string NomeFabricante { get; set; }

        public DetalhesEquipamentoViewModel(int id, string nome, decimal precoaquisicao, DateTime datafabricacao, string nomefabricante)
        {
            Id = id;
            Nome = nome;
            PrecoAquisicao = precoaquisicao;
            DataFabricaçao = datafabricacao;
            NomeFabricante = nomefabricante;


        }
        public override string ToString()
        {
            return $"id: {Id} - Nome: {Nome} - Fabricante : {NomeFabricante} - Preço De Aquisição -{PrecoAquisicao:C2} -Data De Fabricação {DataFabricaçao:d} ";
        }

    }

}
