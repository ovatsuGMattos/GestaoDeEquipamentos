namespace GestaoDeEquipamentos.ConsoleApp.ModuloFabricante;

public class RepositorioFabricante
{
    private Fabricante[] fabricantes = new Fabricante[100];
    private int contadorFabricante = 0;
    private int idAtual = 1;

    public void CadastrarFabricante(Fabricante novo)
    {
        novo.Id = idAtual++;
        fabricantes[contadorFabricante++] = novo;
    }

    public Fabricante[] SelecionarTodos()
    {
        return fabricantes.Where(f => f != null).ToArray();
    }

    public Fabricante SelecionarPorId(int id)
    {
        for (int i = 0; i < fabricantes.Length; i++)
        {
            if (fabricantes[i] != null && fabricantes[i].Id == id)
            {
                return fabricantes[i];
            }
        }

        return null;
    }
    public bool EditarFabricante(int id, Fabricante atualizado)
    {
        for (int i = 0; i < fabricantes.Length; i++)
        {
            if (fabricantes[i] != null && fabricantes[i].Id == id)
            {
                atualizado.Id = id;
                atualizado.QuantidadeEquipamentos = fabricantes[i].QuantidadeEquipamentos;
                fabricantes[i] = atualizado;
                return true;
            }
        }
        return false;
    }

    public bool ExcluirFabricante(int id)
    {
        for (int i = 0; i < fabricantes.Length; i++)
        {
            if (fabricantes[i] != null && fabricantes[i].Id == id)
            {
                fabricantes[i] = null;
                return true;
            }
        }
        return false;
    }
}
