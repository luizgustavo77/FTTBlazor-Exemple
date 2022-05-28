using AlunoAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AlunoAPI.Data
{
    public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
    {
        #region Contructor
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }
        #endregion

        #region Public properties
        public DbSet<Aluno> Alunos { get; set; }
        #endregion

        #region Overidden methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aluno>().HasData(GetProducts());
            base.OnModelCreating(modelBuilder);
        }
        #endregion

        #region Private methods
        private List<Aluno> GetProducts()
        {
            return new List<Aluno>
            {
                new Aluno { Id = 1, Name = "Luiz"},
                new Aluno { Id = 2, Name = "Thiago"},
                new Aluno { Id = 3, Name = "Jhonatan"},
                new Aluno { Id = 4, Name = "Paulo"}
            };
        }
        #endregion
    }
}
