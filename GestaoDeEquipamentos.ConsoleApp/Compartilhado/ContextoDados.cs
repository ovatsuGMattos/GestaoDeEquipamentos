using GestaoDeEquipamentos.ConsoleApp.ModuloChamado;
using GestaoDeEquipamentos.ConsoleApp.ModuloEquipamento;
using GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado;

public class ContextoDados
{
    public List<Fabricante> Fabricantes { get; set; }
    public List<Equipamento> Equipamentos { get; set; }
    public List<Chamado> Chamados { get; set; }

    private string pastaArmazenamento = "C:\\temp";
    private string arquivoArmazenamento = "dados.json";

    public ContextoDados()
    {
        Fabricantes = new List<Fabricante>();
        Equipamentos = new List<Equipamento>();
        Chamados = new List<Chamado>();
    }

    public ContextoDados(bool carregarDados) : this()
    {
        if (carregarDados)
            Carregar();
    }

    public void Salvar()
    {
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.WriteIndented = true;
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        string json = JsonSerializer.Serialize(this, jsonOptions);

        if (!Directory.Exists(pastaArmazenamento))
            Directory.CreateDirectory(pastaArmazenamento);

        File.WriteAllText(caminhoCompleto, json);
    }

    public void Carregar()
    {
        string caminhoCompleto = Path.Combine(pastaArmazenamento, arquivoArmazenamento);

        if (!File.Exists(caminhoCompleto)) return;

        string json = File.ReadAllText(caminhoCompleto);

        if (string.IsNullOrWhiteSpace(json)) return;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
        jsonOptions.ReferenceHandler = ReferenceHandler.Preserve;

        ContextoDados contextoArmazenado = JsonSerializer.Deserialize<ContextoDados>(json, jsonOptions)!;

        if (contextoArmazenado == null) return;

        this.Fabricantes = contextoArmazenado.Fabricantes;
        this.Equipamentos = contextoArmazenado.Equipamentos;
        this.Chamados = contextoArmazenado.Chamados;
    }
}