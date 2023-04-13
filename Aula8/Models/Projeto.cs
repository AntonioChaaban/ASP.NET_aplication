using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aula8.Models
{
    public class Projeto
    {
        public int Id { get; set; }


        public string NomeDoProjeto { get; set; }
        public string DescrisaoDoProjeto { get; set; }
        public DateTime DataDeInicioDoProjeto { get; set; }
        public DateTime DataDeTerminoDoProjeto { get; set; }
        public string EstadoDoProjeto { get; set; }
        public virtual List<Usuario> Usuarios { get; set; }
    }
}