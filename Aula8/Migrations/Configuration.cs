namespace Aula8.Migrations
{
    using Aula8.Models;
    using Microsoft.Ajax.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Aula8.DAL.GerenciamentoDeProjetosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Aula8.DAL.GerenciamentoDeProjetosContext context)
        {
            var usuario1 = new Usuario { Id = 1, Nome = "Antonio", Email = "tom250299@gmail.com", Cargo = "Gerente" };
            var usuario2 = new Usuario { Id = 2, Nome = "Vinicios", Email = "antoniodecarvalhochaaban@gmail.com", Cargo = "Colaborador" };
            var usuario3 = new Usuario { Id = 3, Nome = "Arthur", Email = "antoniocarvalho@unitins.br", Cargo = "Colaborador" };
            var usuarios = new List<Usuario>
            {
                usuario1,
                usuario2,
                usuario3
            };
            usuarios.ForEach(s => context.Usuario.AddOrUpdate(p => p.Email, s));

            context.SaveChanges();

            var projeto1 = new Projeto
            {
                Id = 1,
                NomeDoProjeto = "Topicos3",
                DescrisaoDoProjeto = "A1 para a materia de topicos",
                DataDeInicioDoProjeto = DateTime.Parse("2023-04-05"),
                DataDeTerminoDoProjeto = DateTime.Parse("2023-04-13"),
                EstadoDoProjeto = "Em Andamento",
                Usuarios = usuarios,
            };
            var projeto2 =
                new Projeto
                {
                    Id = 2,
                    NomeDoProjeto = "Topicos2",
                    DescrisaoDoProjeto = "Trabalho para a materia de topicos 2",
                    DataDeInicioDoProjeto = DateTime.Parse("2022-04-05"),
                    DataDeTerminoDoProjeto = DateTime.Parse("2022-04-13"),
                    EstadoDoProjeto = "Finalizado",
                    Usuarios = usuarios,
                };
            var projeto3 =
                 new Projeto
                 {
                     Id = 3,
                     NomeDoProjeto = "Para Excluir",
                     DescrisaoDoProjeto = "Projeto para tentar excluir",
                     DataDeInicioDoProjeto = DateTime.Parse("2023-04-05"),
                     DataDeTerminoDoProjeto = DateTime.Parse("2023-04-13"),
                     EstadoDoProjeto = "Criado",
                     Usuarios = usuarios
                 };
            var projetos = new List<Projeto>
            {
                projeto1,
                projeto2,
                projeto3
            };
            context.InserirEmProjetoUsuarios(projetos);

            var tarefa1 = new Tarefa
            {
                Id = 1,
                NomeDaTarefa = "Finalizar Login com Google e Facebook",
                DescrisaoDaTarefa = "Para essa tarefa é necessario fazer o sistema de Login com Google e Facebook",
                DataDeInicioDaTarefa = DateTime.Parse("2023-04-05"),
                ProjetoAssociado = projeto1,
                StatusDaTarefa = "Criado",
                UsuariosAssociadosATarefa = usuarios
            };
            var tarefa2 = new Tarefa
            {
                Id = 2,
                NomeDaTarefa = "Finalizar Inserção de dados para validar Migrations",
                DescrisaoDaTarefa = "Para essa tarefa é necessario fazer Inserção de dados para validar Migrations",
                DataDeInicioDaTarefa = DateTime.Parse("2023-04-05"),
                ProjetoAssociado = projeto1,
                StatusDaTarefa = "Finalizado",
                UsuariosAssociadosATarefa = usuarios
            };
            var tarefa3 = new Tarefa
            {
                Id = 3,
                NomeDaTarefa = "Finalizar Front do projeto Topicos3",
                DescrisaoDaTarefa = "Para essa tarefa é necessario Finalizar Front do projeto A1 topicos3",
                DataDeInicioDaTarefa = DateTime.Parse("2023-04-05"),
                ProjetoAssociado = projeto1,
                StatusDaTarefa = "Em Andamento",
                UsuariosAssociadosATarefa = usuarios
            };
            var tarefa4 = new Tarefa
            {
                Id = 4,
                NomeDaTarefa = "Finalizar Front do projeto Topicos2",
                DescrisaoDaTarefa = "Para essa tarefa é necessario Finalizar Front do projeto A1 topicos3",
                DataDeInicioDaTarefa = DateTime.Parse("2022-04-05"),
                ProjetoAssociado = projeto2,
                StatusDaTarefa = "Finalizado",
                UsuariosAssociadosATarefa = usuarios
            };
            var tarefa5 = new Tarefa
            {
                Id = 5,
                NomeDaTarefa = "Finalizar Login com Google e Facebook",
                DescrisaoDaTarefa = "Para essa tarefa é necessario fazer o sistema de Login com Google e Facebook",
                DataDeInicioDaTarefa = DateTime.Parse("2022-04-05"),
                ProjetoAssociado = projeto2,
                StatusDaTarefa = "Finalizado",
                UsuariosAssociadosATarefa = usuarios
            };
            var tarefa6 = new Tarefa
            {
                Id = 6,
                NomeDaTarefa = "Finalizar Inserção de dados para validar Migrations",
                DescrisaoDaTarefa = "Para essa tarefa é necessario fazer Inserção de dados para validar Migrations",
                DataDeInicioDaTarefa = DateTime.Parse("2022-04-05"),
                ProjetoAssociado = projeto3,
                StatusDaTarefa = "Finalizado",
                UsuariosAssociadosATarefa = usuarios
            };
            var tarefas = new List<Tarefa>
            {
                tarefa1,
                tarefa2,
                tarefa3,
                tarefa4,
                tarefa5,
                tarefa6
            };
            context.InserirEmTarefaUsuarios(tarefas);

            projetos.ForEach(s => context.Projeto.AddOrUpdate(p => p.DescrisaoDoProjeto, s));
            context.SaveChanges();

            tarefas.ForEach(s => context.Tarefa.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var comentarios = new List<Comentario>
            {
                new Comentario{Id = 1, DescrisaoDoComentario =" Eu fiz a captura de dados pelo login google precisa agora fazer a inserção no banco de dados",DataDoComentario= DateTime.Parse("2023-04-05"),
                ProjetoAssociadoAoComentario = projeto1, TarefaAssociadaAoComentario = tarefas[0],UsuarioAssociadosAoComentario = usuario1}
            };
            comentarios.ForEach(s => context.Comentario.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();

            var milestones = new List<MileStone>
            {

            };
            var relatorios = new List<Relatorio>
            {
                new Relatorio{Id = 1, NomeDoRelatorio = "Criação de tarefas", DescrisaoDoRelatorio= "As tarefas foram criadas e no momento estão em processo de andamento",DataDoRelatorio = DateTime.Parse("2023-04-05"),Projeto = projeto1}
            };
            relatorios.ForEach(s => context.Relatorio.AddOrUpdate(p => p.Id, s));
            context.SaveChanges();
        }
    }
}
