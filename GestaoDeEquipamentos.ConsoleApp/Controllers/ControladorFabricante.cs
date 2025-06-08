using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Extensoes;
using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using Microsoft.AspNetCore.Mvc;
using System.Text;


namespace GestaoDeEquipamentos.ConsoleApp.Controllers
{
    [Route("fabricantes")]

    public class ControladorFabricante : Controller
    {
        [HttpGet("cadastrar")]
        public IActionResult ExibirFormularioDeCadastroFabricantes()
        {
            CadastrarFabricantesViewModel cadastrarvm = new CadastrarFabricantesViewModel();

            return View("cadastrar", cadastrarvm);
        }
        [HttpPost("cadastrar")]
        public IActionResult CadastrarFabricante(CadastrarFabricantesViewModel CadastrarVM)

        {
            ContextoDados contextodedados = new ContextoDados(true);
            IRepositorioFabricante repositoriofabricante = new RepositorioFabricanteEmArquivo(contextodedados);

            Fabricante NovoFabricante = CadastrarVM.ParaEntidade();

            repositoriofabricante.CadastrarRegistro(NovoFabricante);

            ViewBag.Mensagem = ($"O registro \"{NovoFabricante.Nome} \" foi registrado com sucesso");

            return View("notificacao");
        }


        [HttpGet("editar/{id:int}")]

        public IActionResult Editar([FromRoute] int id)
        {
            ContextoDados contextodedados = new ContextoDados(true);
            IRepositorioFabricante repositoriofabricante = new RepositorioFabricanteEmArquivo(contextodedados);

            Fabricante fabricanteSelecionado = repositoriofabricante.SelecionarRegistroPorId(id);

            EditarFabricantesViewModel EditarVm = new EditarFabricantesViewModel(id, fabricanteSelecionado.Nome, fabricanteSelecionado.Email, fabricanteSelecionado.Telefone);

            return View(EditarVm);
        }


        [HttpPost("editar/{id:int}")]
        public IActionResult Editar([FromRoute] int id, EditarFabricantesViewModel EditarVm)
        {

            ContextoDados contextodedados = new ContextoDados(true);
            IRepositorioFabricante repositoriofabricante = new RepositorioFabricanteEmArquivo(contextodedados);

            Fabricante fabricanteatualizado = new Fabricante(EditarVm.Nome, EditarVm.Email, EditarVm.Telefone);

            repositoriofabricante.EditarRegistro(id, fabricanteatualizado);

            ViewBag.Mensagem = ($"O registro \"{EditarVm.Nome}\" foi editado com sucesso");

            return View("Notificacao");
        }

        [HttpGet("excluir/{id:int}")]
        public IActionResult ExibirFormularioDeExclusaoFabricantes(int id)
        {
            ContextoDados contextodedados = new ContextoDados(true);
            IRepositorioFabricante repositoriofabricante = new RepositorioFabricanteEmArquivo(contextodedados);

            Fabricante fabricanteselecionado = repositoriofabricante.SelecionarRegistroPorId(id);

            ExcluirFabricantesViewModel excluirVm = new ExcluirFabricantesViewModel(fabricanteselecionado.Id, fabricanteselecionado.Nome);

            return View("Excluir", excluirVm);
        }

        [HttpPost("excluir/{id:int}")]
        public IActionResult ExcluirFabricante([FromRoute] int id)
        {
            ContextoDados contextodedados = new ContextoDados(true);
            IRepositorioFabricante repositoriofabricante = new RepositorioFabricanteEmArquivo(contextodedados);

            repositoriofabricante.ExcluirRegistro(id);

            ViewBag.Mensagem = ($"O registro foi Excluido com sucesso");

            return View("Notificacao");

        }

        [HttpGet("visualizar")]
        public IActionResult vizualizarFabricantes()
        {
            ContextoDados contextodedados = new ContextoDados(true);

            IRepositorioFabricante repositoriofabricante = new RepositorioFabricanteEmArquivo(contextodedados);

            List<Fabricante> fabricantes = repositoriofabricante.SelecionarRegistros();

            VizualizarFabricantesViewModel viewModel = new VizualizarFabricantesViewModel(fabricantes);

            return View("Visualizar", viewModel);
        }


    }
}