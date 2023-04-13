namespace Aula8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atualizasao2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjetoTarefas",
                c => new
                {
                    TarefaId = c.Int(nullable: false),
                    ProjetoId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.TarefaId, t.ProjetoId })
                .ForeignKey("dbo.Tarefa", t => t.TarefaId, cascadeDelete: true)
                .ForeignKey("dbo.Projeto", t => t.ProjetoId, cascadeDelete: true)
                .Index(t => t.TarefaId)
                .Index(t => t.ProjetoId);
        }
        
        public override void Down()
        {
        }
    }
}
