using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Entities;
using TaskStatus = TaskManager.Domain.Entities.TaskStatus;

namespace TaskManager.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskItem>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Titulo).IsRequired().HasMaxLength(100);
                b.Property(x => x.Status).IsRequired();
            });

            // ----------------------------------------------------
            // 1. DATA SEEDING: Inserção de Dados Iniciais
            // ----------------------------------------------------
            modelBuilder.Entity<TaskItem>().HasData(
                new TaskItem
                {
                    Id = 1,
                    Titulo = "Teste 01 - Tarefa Pendente",
                    Descricao = "Descrição detalhada da tarefa pendente.",
                    Status = TaskStatus.Pendente,
                    DataCriacao = new DateTime(2025, 10, 15, 22, 25, 31, DateTimeKind.Utc),
                    DataConclusao = null
                },
                new TaskItem
                {
                    Id = 2,
                    Titulo = "Teste 02 - Tarefa Concluída",
                    Descricao = "Descrição detalhada da tarefa concluida.",
                    Status = TaskStatus.Concluida,
                    DataCriacao = new DateTime(2025, 10, 15, 15, 21, 31, DateTimeKind.Utc),
                    DataConclusao = new DateTime(2025, 12, 5)
                },
                new TaskItem
                {
                    Id = 3,
                    Titulo = "Teste 03 - Em Andamento",
                    Descricao = "Descrição detalhada da tarefa em andamento.",
                    Status = TaskStatus.EmProgresso,
                    DataCriacao = new DateTime(2025, 10, 15, 18, 36, 31, DateTimeKind.Utc),
                    DataConclusao = null
                },

                new TaskItem
                {
                    Id = 4,
                    Titulo = "Teste 04 - Prioridade Alta",
                    Descricao = "Descrição detalhada da tarefa pendente.",
                    Status = TaskStatus.Pendente,
                    DataCriacao = new DateTime(2025, 10, 15, 12, 25, 31, DateTimeKind.Utc),
                    DataConclusao = null
                },
                new TaskItem
                {
                    Id = 5,
                    Titulo = "Teste 05 - Concluído Hoje",
                    Descricao = "Descrição detalhada da tarefa concluida.",
                    Status = TaskStatus.Concluida,
                    DataCriacao = new DateTime(2025, 10, 15, 09, 25, 31, DateTimeKind.Utc),
                    DataConclusao = new DateTime(2024, 1, 5)
                },
                new TaskItem
                {
                    Id = 6,
                    Titulo = "Teste 06 - Desenvolvimento",
                    Descricao = "Descrição detalhada da tarefa em andamento.",
                    Status = TaskStatus.EmProgresso,
                    DataCriacao = new DateTime(2025, 10, 15, 10, 25, 31, DateTimeKind.Utc),
                    DataConclusao = null
                },
                new TaskItem
                {
                    Id = 7,
                    Titulo = "Teste 07 - Revisão de Código",
                    Descricao = "Descrição detalhada da tarefa pendente.",
                    Status = TaskStatus.Pendente,
                    DataCriacao = new DateTime(2025, 10, 15, 11, 25, 31, DateTimeKind.Utc),
                    DataConclusao = null
                },
                new TaskItem
                {
                    Id = 8,
                    Titulo = "Teste 08 - Tarefa Urgente",
                    Descricao = "Descrição detalhada da tarefa pendente.",
                    Status = TaskStatus.Pendente,
                    DataCriacao = new DateTime(2025, 10, 15, 11, 34, 31, DateTimeKind.Utc),
                    DataConclusao = null
                },
                new TaskItem
                {
                    Id = 9,
                    Titulo = "Teste 09 - Antiga Concluída",
                    Descricao = "Descrição detalhada da tarefa concluida.",
                    Status = TaskStatus.Concluida,
                    DataCriacao = new DateTime(2025, 10, 15, 22, 11, 31, DateTimeKind.Utc),
                    DataConclusao = new DateTime(2025, 1, 5)
                },
                new TaskItem
                {
                    Id = 10,
                    Titulo = "Teste 10 - Documentação",
                    Descricao = "Descrição detalhada da tarefa em andamento.",
                    Status = TaskStatus.EmProgresso,
                    DataCriacao = new DateTime(2025, 10, 15, 15, 01, 31, DateTimeKind.Utc),
                    DataConclusao = null
                }
            );
        }
    }
}
