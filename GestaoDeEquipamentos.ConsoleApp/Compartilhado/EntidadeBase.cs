namespace GestaoDeEquipamentos.ConsoleApp.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public int id { get; set; }

        public abstract void AtualizarRegistro(T registroAtualizado);
        public abstract string Validar();

    }
}
