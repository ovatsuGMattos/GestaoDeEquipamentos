namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado
{
    public interface IRepositorio <T> where T : EntidadeBase<T>
    {
        public void CadastrarRegistro(T novoRegistro);
        public void EditarRegistro(int id, T registroAtualizado);
        public bool ExcluirRegistro(int id);

        public List<T> SelecionarRegistros();

        public T SelecionarRegistroPorId(int idRegistro);
    }
}
