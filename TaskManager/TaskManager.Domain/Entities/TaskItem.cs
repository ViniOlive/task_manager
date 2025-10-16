namespace TaskManager.Domain.Entities
{
    public enum TaskStatus
    {
        Pendente = 0,
        EmProgresso = 1,
        Concluida = 2
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Descricao { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime? DataConclusao { get; set; }
        public TaskStatus Status { get; set; } = TaskStatus.Pendente;
    }
}
