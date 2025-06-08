using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using System.Runtime.CompilerServices;

namespace GestaoDeEquipamentos.ConsoleApp.Extensoes
{
    public static class ChamadosExtensions
    {
        public static Chamado ParaEntidade(this FormularioChamadoViewModel formularioVm, List<Equipamento> equipamentos)
        {
            Equipamento equipamentoSelecionado = null;

            foreach (var f in equipamentos)
            {
                if (f.Id == formularioVm.EquipamentoId)
                    equipamentoSelecionado = f;
            }
            return new Chamado(formularioVm.Titulo, formularioVm.Descricao, equipamentoSelecionado);
        }


        public static DetalhesChamadoViewModel ParaDetahesVm(this Chamado chamado)
        {
            return new DetalhesChamadoViewModel(chamado.Id, chamado.Titulo, chamado.Descricao, chamado.DataAbertura, chamado.TempoDecorrido, chamado.Equipamento.Nome);
        }
    }
}