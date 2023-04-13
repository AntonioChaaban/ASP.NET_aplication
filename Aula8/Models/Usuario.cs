using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aula8.Models
{
    public class Usuario
    {
        public int Id { get; set; }


        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cargo { get; set; }

        public virtual List<Projeto> Projetos { get; set; }
        public virtual List<Tarefa> Tarefas { get; set; }
    }
}