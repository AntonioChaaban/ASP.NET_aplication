using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aula8.Models
{
    public class Relatorio
    {
        public int Id { get; set; }


        public string NomeDoRelatorio { get; set; }
        public DateTime DataDoRelatorio { get; set; }
        public string DescrisaoDoRelatorio { get; set; }
        public virtual Projeto Projeto { get; set; }
    }
}