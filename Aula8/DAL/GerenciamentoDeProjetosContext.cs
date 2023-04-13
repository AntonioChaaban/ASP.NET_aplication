using Aula8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Helpers;

namespace Aula8.DAL
{
    public class GerenciamentoDeProjetosContext : DbContext
    {
        public GerenciamentoDeProjetosContext() : base("SchoolContext")
        {

        }
        
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<MileStone> Milestone { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        public DbSet<Relatorio> Relatorio { get; set; }

        public void InserirEmTarefaUsuarios(List<Tarefa> tarefas)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7ODPJT7\\SQLEXPRESS;Initial Catalog=SchoolContext2;Integrated Security=True");
            foreach (Tarefa tarefa in tarefas)
            {
                foreach (Usuario usuario in tarefa.UsuariosAssociadosATarefa)
                {
                    SqlCommand comand = new SqlCommand("insert into TarefaUsuario(Tarefa_Id, Usuario_Id) values(@Tarefa_Id,@Usuario_Id)", conn);
                    comand.Parameters.Add("@Tarefa_Id", SqlDbType.VarChar).Value = tarefa.Id;
                    comand.Parameters.Add("@Usuario_Id", SqlDbType.VarChar).Value = usuario.Id;
                    try
                    {
                        conn.Open();
                        comand.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
        public IEnumerable<int> ObterUsuariosDeTarefas(int tarefaId)
        {
            IEnumerable<int> usuarios = new List<int>();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7ODPJT7\\SQLEXPRESS;Initial Catalog=SchoolContext2;Integrated Security=True");
            SqlCommand comand = new SqlCommand("SELECT Usuario_Id FROM TarefaUsuario WHERE Tarefa_Id = @Id", conn);
            comand.Parameters.Add("@Id", SqlDbType.VarChar).Value = tarefaId;
            try
            {
                conn.Open();
                SqlDataReader resultados = comand.ExecuteReader();

                while (resultados.Read())
                {
                    int id = resultados.GetInt32(0);
                    usuarios.ToList().Add(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return usuarios;
        }
        public void InserirEmProjetoUsuariosPorId(int? projeto,int? usuario)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7ODPJT7\\SQLEXPRESS;Initial Catalog=SchoolContext2;Integrated Security=True");
            SqlCommand comand = new SqlCommand("insert into ProjetoUsuarios(ProjetoId, UsuarioId) values(@ProjetoId,@UsuarioId)", conn);
            comand.Parameters.Add("@ProjetoId", SqlDbType.VarChar).Value = projeto;
            comand.Parameters.Add("@UsuarioId", SqlDbType.VarChar).Value = usuario;
            try
            {
                conn.Open();
                comand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void InserirEmProjetoUsuarios(List<Projeto> projetos) 
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7ODPJT7\\SQLEXPRESS;Initial Catalog=SchoolContext2;Integrated Security=True");
            foreach (Projeto projeto in projetos) 
            {
                foreach(Usuario usuario in projeto.Usuarios)
                {
                    SqlCommand comand = new SqlCommand("insert into ProjetoUsuarios(ProjetoId, UsuarioId) values(@ProjetoId,@UsuarioId)", conn);
                    comand.Parameters.Add("@ProjetoId", SqlDbType.VarChar).Value = projeto.Id;
                    comand.Parameters.Add("@UsuarioId", SqlDbType.VarChar).Value = usuario.Id;
                    try
                    {
                        conn.Open();
                        comand.ExecuteNonQuery();
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            } 
        }
        public List<int> ObterUsuariosDoProjeto(int projetoId)
        {
            List<int> usuarios = new List<int>();
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7ODPJT7\\SQLEXPRESS;Initial Catalog=SchoolContext2;Integrated Security=True");
            SqlCommand comand = new SqlCommand("SELECT UsuarioId FROM ProjetoUsuarios WHERE ProjetoId = @Id", conn);
            comand.Parameters.Add("@Id", SqlDbType.VarChar).Value = projetoId;
            try
            {
                conn.Open();
                SqlDataReader resultados = comand.ExecuteReader();

                while (resultados.Read()) 
                {
                    int id = resultados.GetInt32(0);
                    usuarios.Add(id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            Console.WriteLine(usuarios);
            return usuarios;
        }
        public void DeletarEmProjetoUsuariosPorId(int? projeto, int? usuario)
        {
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-7ODPJT7\\SQLEXPRESS;Initial Catalog=SchoolContext2;Integrated Security=True");
            SqlCommand comand = new SqlCommand("DELETE FROM ProjetoUsuarios WHERE ProjetoId = @ProjetoId AND UsuarioId = @UsuarioId", conn);
            comand.Parameters.Add("@ProjetoId", SqlDbType.VarChar).Value = projeto;
            comand.Parameters.Add("@UsuarioId", SqlDbType.VarChar).Value = usuario;
            try
            {
                conn.Open();
                comand.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Projeto>()
            .HasMany(p => p.Usuarios)
            .WithMany(u => u.Projetos)
            .Map(m =>
            {
                m.ToTable("ProjetoUsuarios");
                m.MapLeftKey("ProjetoId");
                m.MapRightKey("UsuarioId");
            });
            modelBuilder.Entity<Usuario>()
            .HasMany(u => u.Projetos)
            .WithMany(p => p.Usuarios)
            .Map(m =>
            {
                m.ToTable("ProjetoUsuarios");
                m.MapLeftKey("UsuarioId");
                m.MapRightKey("ProjetoId");
            });

            base.OnModelCreating(modelBuilder);

        }
    }
}