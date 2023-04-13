namespace Aula8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizasao1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Usuario", "Projeto_Id", "dbo.Projeto");
            DropForeignKey("dbo.Usuario", "Tarefa_Id", "dbo.Tarefa");
            DropIndex("dbo.Usuario", new[] { "Projeto_Id" });
            DropIndex("dbo.Usuario", new[] { "Tarefa_Id" });
            CreateTable(
                "dbo.TarefaUsuario",
                c => new
                    {
                        Tarefa_Id = c.Int(nullable: false),
                        Usuario_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tarefa_Id, t.Usuario_Id })
                .ForeignKey("dbo.Tarefa", t => t.Tarefa_Id, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.Usuario_Id, cascadeDelete: true)
                .Index(t => t.Tarefa_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.ProjetoUsuarios",
                c => new
                    {
                        ProjetoId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjetoId, t.UsuarioId })
                .ForeignKey("dbo.Projeto", t => t.ProjetoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.ProjetoId)
                .Index(t => t.UsuarioId);
            
            DropColumn("dbo.Usuario", "Projeto_Id");
            DropColumn("dbo.Usuario", "Tarefa_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Usuario", "Tarefa_Id", c => c.Int());
            AddColumn("dbo.Usuario", "Projeto_Id", c => c.Int());
            DropForeignKey("dbo.ProjetoUsuarios", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.ProjetoUsuarios", "ProjetoId", "dbo.Projeto");
            DropForeignKey("dbo.TarefaUsuario", "Usuario_Id", "dbo.Usuario");
            DropForeignKey("dbo.TarefaUsuario", "Tarefa_Id", "dbo.Tarefa");
            DropIndex("dbo.ProjetoUsuarios", new[] { "UsuarioId" });
            DropIndex("dbo.ProjetoUsuarios", new[] { "ProjetoId" });
            DropIndex("dbo.TarefaUsuario", new[] { "Usuario_Id" });
            DropIndex("dbo.TarefaUsuario", new[] { "Tarefa_Id" });
            DropTable("dbo.ProjetoUsuarios");
            DropTable("dbo.TarefaUsuario");
            CreateIndex("dbo.Usuario", "Tarefa_Id");
            CreateIndex("dbo.Usuario", "Projeto_Id");
            AddForeignKey("dbo.Usuario", "Tarefa_Id", "dbo.Tarefa", "Id");
            AddForeignKey("dbo.Usuario", "Projeto_Id", "dbo.Projeto", "Id");
        }
    }
}
