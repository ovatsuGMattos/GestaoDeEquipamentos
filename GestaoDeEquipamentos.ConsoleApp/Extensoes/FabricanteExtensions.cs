using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.Extensoes
{
    public static class FabricantesExtensions
    {
        public static Fabricante ParaEntidade(this FormularioFabricanteViewModel FormularioVm)
        {
            return new Fabricante(FormularioVm.Nome, FormularioVm.Telefone, FormularioVm.Email);
        }
        public static DetalhesFabricanteViewModel ParaDetalhesVm(this Fabricante fabricante)
        {
            return new DetalhesFabricanteViewModel(fabricante.Id, fabricante.Nome, fabricante.Email, fabricante.Telefone);
        }
    }
}