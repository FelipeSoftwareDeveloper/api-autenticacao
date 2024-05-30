namespace Blank.Domain.Entities
{
    public class EntidadeBase
    {
        public Guid Id { get; private set; }
        public DateTime CriadoEm { get; private set; }
        public DateTime AtualizadoEm { get; private set; }
        public DateTime? Deletado { get; private set; }
        public Guid UserId { get; private set; }

        public EntidadeBase()
        {

        }
        public EntidadeBase(Guid userId)
        {
            Id = Guid.NewGuid();
            CriadoEm = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            Deletado = null;
            UserId = userId;
        }

        public void Atualizado() => AtualizadoEm = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);

        public void Deletar()
        {
            Deletado = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            Atualizado();
        }
    }
}
