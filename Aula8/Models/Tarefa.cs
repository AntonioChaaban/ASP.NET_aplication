using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aula8.Models
{
    public class Tarefa
    {
        public int Id { get; set; }


        public string NomeDaTarefa { get; set; }
        public string DescrisaoDaTarefa { get; set; }
        public string StatusDaTarefa { get; set; }
        public string TempoParaRealizacaoDaTarefa { get; set; }
        public DateTime DataDeInicioDaTarefa { get; set; }
        public DateTime DataDeTerminoDaTarefa { get; set; }
        public virtual Projeto ProjetoAssociado { get; set; }
        public virtual List<Usuario> UsuariosAssociadosATarefa { get; set; }
        public virtual List<MileStone> MilestonesAssociadosATarefa { get; set; }
    }
}