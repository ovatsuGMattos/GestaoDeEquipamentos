using GestaoDeEquipamentos.ConsoleApp.Compartilhado;
using GestaoDeEquipamentos.ConsoleApp.Extensoes;
using GestaoDeEquipamentos.ConsoleApp.Models;
using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using Microsoft.AspNetCore.Mvc;

[Route("chamados")]
public class ControladorChamado : Controller
{
    private ContextoDados contextoDados;
    private IRepositorioChamado repositorioChamado;
    private IRepositorioEquipamento repositorioEquipamento;

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var equipamentos = repositorioEquipamento.SelecionarRegistros();

        var cadastrarVM = new CadastrarChamadoViewModel(equipamentos);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    public IActionResult Cadastrar(CadastrarChamadoViewModel cadastrarVM)
    {
        var equipamentos = repositorioEquipamento.SelecionarRegistros();

        var chamado = cadastrarVM.ParaEntidade(equipamentos);

        repositorioChamado.CadastrarRegistro(chamado);

        var notificacaoVM = new NotificacaoViewModel(
            "Chamado Cadastrado!",
            $"O registro \"{chamado.Titulo}\" foi cadastrado com sucesso!"
        );

        return View("Notificacao", notificacaoVM);
    }
}