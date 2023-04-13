namespace Aula8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class novaCriasao : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Enrollment", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Enrollment", "StudentID", "dbo.Student");
            DropIndex("dbo.Enrollment", new[] { "CourseID" });
            DropIndex("dbo.Enrollment", new[] { "StudentID" });
            CreateTable(
                "dbo.Comentario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DescrisaoDoComentario = c.String(),
                        DataDoComentario = c.DateTime(nullable: false),
                        ProjetoAssociadoAoComentario_Id = c.Int(),
                        TarefaAssociadaAoComentario_Id = c.Int(),
                        UsuarioAssociadosAoComentario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.ProjetoAssociadoAoComentario_Id)
                .ForeignKey("dbo.Tarefa", t => t.TarefaAssociadaAoComentario_Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioAssociadosAoComentario_Id)
                .Index(t => t.ProjetoAssociadoAoComentario_Id)
                .Index(t => t.TarefaAssociadaAoComentario_Id)
                .Index(t => t.UsuarioAssociadosAoComentario_Id);
            
            CreateTable(
                "dbo.Projeto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeDoProjeto = c.String(),
                        DescrisaoDoProjeto = c.String(),
                        DataDeInicioDoProjeto = c.DateTime(nullable: false),
                        DataDeTerminoDoProjeto = c.DateTime(nullable: false),
                        EstadoDoProjeto = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Email = c.String(),
                        Cargo = c.String(),
                        Projeto_Id = c.Int(),
                        Tarefa_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.Projeto_Id)
                .ForeignKey("dbo.Tarefa", t => t.Tarefa_Id)
                .Index(t => t.Projeto_Id)
                .Index(t => t.Tarefa_Id);
            
            CreateTable(
                "dbo.Tarefa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeDaTarefa = c.String(),
                        DescrisaoDaTarefa = c.String(),
                        StatusDaTarefa = c.String(),
                        TempoParaRealizacaoDaTarefa = c.String(),
                        DataDeInicioDaTarefa = c.DateTime(nullable: false),
                        DataDeTerminoDaTarefa = c.DateTime(nullable: false),
                        ProjetoAssociado_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.ProjetoAssociado_Id)
                .Index(t => t.ProjetoAssociado_Id);
            
            CreateTable(
                "dbo.MileStone",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusDaTarefa = c.String(),
                        DescrisaoDeMetas = c.String(),
                        Tarefa_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tarefa", t => t.Tarefa_Id)
                .Index(t => t.Tarefa_Id);
            
            CreateTable(
                "dbo.Relatorio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeDoRelatorio = c.String(),
                        DataDoRelatorio = c.DateTime(nullable: false),
                        DescrisaoDoRelatorio = c.String(),
                        Projeto_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projeto", t => t.Projeto_Id)
                .Index(t => t.Projeto_Id);
            
            DropTable("dbo.Course");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Student");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(),
                        FirstMidName = c.String(),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        EnrollmentId = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.EnrollmentId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseID = c.Int(nullable: false),
                        Title = c.String(),
                        Credits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseID);
            
            DropForeignKey("dbo.Relatorio", "Projeto_Id", "dbo.Projeto");
            DropForeignKey("dbo.Comentario", "UsuarioAssociadosAoComentario_Id", "dbo.Usuario");
            DropForeignKey("dbo.Comentario", "TarefaAssociadaAoComentario_Id", "dbo.Tarefa");
            DropForeignKey("dbo.Usuario", "Tarefa_Id", "dbo.Tarefa");
            DropForeignKey("dbo.Tarefa", "ProjetoAssociado_Id", "dbo.Projeto");
            DropForeignKey("dbo.MileStone", "Tarefa_Id", "dbo.Tarefa");
            DropForeignKey("dbo.Comentario", "ProjetoAssociadoAoComentario_Id", "dbo.Projeto");
            DropForeignKey("dbo.Usuario", "Projeto_Id", "dbo.Projeto");
            DropIndex("dbo.Relatorio", new[] { "Projeto_Id" });
            DropIndex("dbo.MileStone", new[] { "Tarefa_Id" });
            DropIndex("dbo.Tarefa", new[] { "ProjetoAssociado_Id" });
            DropIndex("dbo.Usuario", new[] { "Tarefa_Id" });
            DropIndex("dbo.Usuario", new[] { "Projeto_Id" });
            DropIndex("dbo.Comentario", new[] { "UsuarioAssociadosAoComentario_Id" });
            DropIndex("dbo.Comentario", new[] { "TarefaAssociadaAoComentario_Id" });
            DropIndex("dbo.Comentario", new[] { "ProjetoAssociadoAoComentario_Id" });
            DropTable("dbo.Relatorio");
            DropTable("dbo.MileStone");
            DropTable("dbo.Tarefa");
            DropTable("dbo.Usuario");
            DropTable("dbo.Projeto");
            DropTable("dbo.Comentario");
            CreateIndex("dbo.Enrollment", "StudentID");
            CreateIndex("dbo.Enrollment", "CourseID");
            AddForeignKey("dbo.Enrollment", "StudentID", "dbo.Student", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Enrollment", "CourseID", "dbo.Course", "CourseID", cascadeDelete: true);
        }
    }
}
