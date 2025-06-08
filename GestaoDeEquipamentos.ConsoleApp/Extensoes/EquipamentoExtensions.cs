using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

namespace GestaoDeEquipamentos.ConsoleApp.Extensoes
{
    public static class EquipamentoExtensions
    {


        public static Equipamento ParaEntidade(this FormularioEquipamentoViewModel formularioVm, List<Fabricante> fabricantes)
        {
            Fabricante fabricanteselecionado = null;

            foreach (var f in fabricantes)
            {
                if (f.Id == formularioVm.FabricanteId)

                    fabricanteselecionado = f;

            }


            return new Equipamento(
            formularioVm.Nome,
            formularioVm.PrecoAquisicao,
            formularioVm.DataFabricacao,
            fabricanteselecionado
            );
        }



        public static DetalhesEquipamentoViewModel ParaDetalhesVm(this Equipamento equipamento)
        {
            return new DetalhesEquipamentoViewModel(
                equipamento.Id,
                equipamento.Nome,
                equipamento.PrecoAquisicao,
                equipamento.DataFabricacao,
               equipamento.Fabricante.Nome);
        }


    }
}