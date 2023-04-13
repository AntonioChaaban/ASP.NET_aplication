using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aula8.Models
{
    public class Comentario
    {
        public int Id { get; set; }


        public string DescrisaoDoComentario { get; set; }
        public DateTime DataDoComentario { get; set; }
        public virtual Projeto ProjetoAssociadoAoComentario { get; set; }
        public virtual Tarefa TarefaAssociadaAoComentario { get; set; }
        public virtual Usuario UsuarioAssociadosAoComentario { get; set; }
    }
}