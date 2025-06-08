using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Extensoes;
using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentos.ConsoleApp.Controllers
{
    [Route("equipamentos")]
    public class ControladorEquipamento : Controller
    {
        private readonly ContextoDados contextoDados;
        private readonly IRepositorioEquipamento repositorioEquipamento;
        private readonly IRepositorioFabricante repositorioFabricante;

        public ControladorEquipamento()
        {
            contextoDados = new ContextoDados(true);
            repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contextoDados);
            repositorioFabricante = new RepositorioFabricanteEmArquivo(contextoDados);
        }

        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var fabricantes = repositorioFabricante.SelecionarRegistros();
            var cadastrarVm = new CadastrarEquipamentoViewModel(fabricantes);
            return View(cadastrarVm);
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(CadastrarEquipamentoViewModel cadastrarVm)
        {
            var fabricantes = repositorioFabricante.SelecionarRegistros();
            var equipamento = cadastrarVm.ParaEntidade(fabricantes);

            repositorioEquipamento.CadastrarRegistro(equipamento);

            ViewBag.Mensagem = $"O registro \"{cadastrarVm.Nome}\" foi registrado com sucesso";
            return View("Notificacao");
        }

        [HttpGet("editar/{id:int}")]
        public IActionResult Editar([FromRoute] int id)
        {
            var equipamentoOriginal = repositorioEquipamento.SelecionarRegistroPorId(id);
            if (equipamentoOriginal == null)
                return NotFound();

            var fabricantes = repositorioFabricante.SelecionarRegistros();

            var editarVm = new EditarEquipamentoViewModel(
                id,
                equipamentoOriginal.Nome,
                equipamentoOriginal.PrecoAquisicao,
                equipamentoOriginal.DataFabricacao,
                equipamentoOriginal.Fabricante.Id,
                fabricantes
            );

            return View(editarVm);
        }

        [HttpPost("editar/{id:int}")]
        public IActionResult Editar([FromRoute] int id, EditarEquipamentoViewModel editarVm)
        {
            var equipamentoOriginal = repositorioEquipamento.SelecionarRegistroPorId(id);
            if (equipamentoOriginal == null)
                return NotFound();

            var fabricantes = repositorioFabricante.SelecionarRegistros();
            var equipamentoEditado = editarVm.ParaEntidade(fabricantes);

           
            if (equipamentoEditado.Fabricante.Id != equipamentoOriginal.Fabricante.Id)
            {
                equipamentoOriginal.Fabricante.RemoverEquipamento(equipamentoOriginal);
                equipamentoOriginal.Fabricante = equipamentoEditado.Fabricante;
            }

            repositorioEquipamento.EditarRegistro(id, equipamentoEditado);

            ViewBag.Mensagem = $"O registro \"{editarVm.Nome}\" foi editado com sucesso";
            return View("Notificacao");
        }

        [HttpGet("excluir/{id:int}")]
        public IActionResult Excluir([FromRoute] int id)
        {
            var equipamento = repositorioEquipamento.SelecionarRegistroPorId(id);
            if (equipamento == null)
                return NotFound();

            var excluirVm = new ExcluirEquipamentoViewModel(id, equipamento.Nome);
            return View(excluirVm);
        }

        [HttpPost("excluir/{id:int}")]
        public IActionResult ExcluirConfirmado([FromRoute] int id)
        {
            repositorioEquipamento.ExcluirRegistro(id);
            ViewBag.Mensagem = "O registro foi excluído com sucesso";
            return View("Notificacao");
        }

        [HttpGet("visualizar")]
        public IActionResult Visualizar()
        {
            var equipamentos = repositorioEquipamento.SelecionarRegistros();
            var visualizarVm = new VizualizarEquipamentoViewModels(equipamentos);
            return View("Visualizar", visualizarVm);
        }
    }
}