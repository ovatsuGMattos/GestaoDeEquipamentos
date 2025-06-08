using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Extensoes;
using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;

namespace GestaoDeEquipamentos.ConsoleApp.Controllers
{

    [Route("chamados")]
    public class ControladorChamadoController : Controller
    {
        private ContextoDados contextoDados;
        private IRepositorioChamado repositorioChamado;
        private IRepositorioEquipamento repositorioEquipamento;
        public ControladorChamadoController()
        {
            contextoDados = new ContextoDados(true);
            repositorioChamado = new RepositorioChamadoEmArquivo(contextoDados);
            repositorioEquipamento = new RepositorioEquipamentoEmArquivo(contextoDados);
        }
        [HttpGet("cadastrar")]
        public IActionResult Cadastrar()
        {
            var equipamentos = repositorioEquipamento.SelecionarRegistros();

            var cadastrarVm = new CadastrarChamadoViewModel(equipamentos);

            return View(cadastrarVm);
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar(CadastrarChamadoViewModel cadastrarVm)
        {
            var equipamentos = repositorioEquipamento.SelecionarRegistros();
            var chamado = cadastrarVm.ParaEntidade(equipamentos);

            repositorioChamado.CadastrarRegistro(chamado);
            ViewBag.Mensagem = ($"O registro \"{chamado.Titulo} \" foi registrado com sucesso");

            return View("notificacao");
        }
        [HttpGet("editar/{id:int}")]

        public IActionResult Editar([FromRoute] int id)
        {
            var chamdadoSelecionado = repositorioChamado.SelecionarRegistroPorId(id);

            var equipamentos = repositorioEquipamento.SelecionarRegistros();

            var editarVm = new EditarChamadoViewModel
                (id,
                chamdadoSelecionado.Titulo,
                chamdadoSelecionado.Descricao,
                chamdadoSelecionado.Equipamento.Id,
                equipamentos);

            return View(editarVm);
        }
        [HttpPost("editar/{id:int}")]
        public IActionResult Editar([FromRoute] int id, EditarChamadoViewModel editarVm)
        {
            var equipamentos = repositorioEquipamento.SelecionarRegistros();

            var chamadoOriginal = repositorioChamado.SelecionarRegistroPorId(id);

            var chamadoEditado = editarVm.ParaEntidade(equipamentos);

            if (chamadoEditado.Equipamento != chamadoOriginal.Equipamento)
            {
                chamadoOriginal.Equipamento.RemoverChamado(chamadoOriginal);
                chamadoOriginal.Equipamento = chamadoEditado.Equipamento;
            }
            repositorioChamado.EditarRegistro(id, chamadoEditado);

            ViewBag.Mensagem = ($"O registro \"{editarVm.Titulo} \" foi registrado com sucesso");

            return View("notificacao");
        }
        [HttpGet("excluir/{id:int}")]
        public IActionResult Excluir([FromRoute] int id)
        {
            var chamadoSelecionado = repositorioChamado.SelecionarRegistroPorId(id);

            var excluirVm = new ExcluirChamadoViewModel(id, chamadoSelecionado.Titulo);

            return View(excluirVm);
        }
        [HttpPost("excluir/{id:int}")]
        public IActionResult ExcluirConfirmado([FromRoute] int id)
        {
            repositorioChamado.ExcluirRegistro(id);

            ViewBag.Mensagem = ($"O registro foi Excluido com sucesso");

            return View("Notificacao");
        }

        [HttpGet("visualizar")]
        public IActionResult Vizualizar()
        {
            var chamados = repositorioChamado.SelecionarRegistros();

            var visualizarvm = new VizualizarChamadosViewModel(chamados);
            return View("visualizar", visualizarvm);
        }
    }
}