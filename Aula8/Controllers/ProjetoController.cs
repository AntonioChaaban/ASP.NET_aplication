using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Aula8.DAL;
using Aula8.Models;

namespace Aula8.Controllers
{
    public class ProjetoController : Controller
    {
        private GerenciamentoDeProjetosContext db = new GerenciamentoDeProjetosContext();
        public int? idProjeto;
        public Projeto projetoAuxiliar;

        // GET: Projeto
        public ActionResult Index()
        {
            return View(db.Projeto.ToList());
        }

        // GET: Projeto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projeto.Find(id);
            List<int> usuariosId = db.ObterUsuariosDoProjeto(projeto.Id);
            foreach (int idusuario in usuariosId)
            {
                projeto.Usuarios.Add(db.Usuario.Find(idusuario));
            }
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }
        public ActionResult AdicionarParticipanteAoProjeto(int? id,int? idProjeto)
        {
            db.InserirEmProjetoUsuariosPorId(idProjeto, id);
            db.SaveChanges();
            return View();
        }

        public ActionResult AdicionarParticipantesDoProjeto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projeto.Find(id);

            idProjeto = projeto.Id;
            projetoAuxiliar = new Projeto();
            projetoAuxiliar.Id = projeto.Id;

            List<Usuario> TodosOsUsuarios = db.Usuario.ToList();
            List<Usuario> usuarios = new List<Usuario>();

            List<int> usuariosId = db.ObterUsuariosDoProjeto(projeto.Id);

            foreach (int idusuario in usuariosId)
            {
                usuarios.Add(db.Usuario.Find(idusuario));
                
            }
            
            var listaDeUsuarios = TodosOsUsuarios.Except(projeto.Usuarios).ToList();
            foreach (Usuario usu in listaDeUsuarios) 
            {
                usu.Projetos.Add(projeto);
            }
            
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(listaDeUsuarios);
        }
        public ActionResult AlterarParticipantesDoProjeto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Usuario> usuarios = new List<Usuario>();

            Projeto projeto = db.Projeto.Find(id);
            List<int> usuariosId = db.ObterUsuariosDoProjeto(projeto.Id);
            foreach (int idusuario in usuariosId)
            {
                usuarios.Add(db.Usuario.Find(idusuario));
            }
            projeto.Usuarios = usuarios.Distinct().ToList();
            foreach (Usuario usu in projeto.Usuarios)
            {
                usu.Projetos.Add(projeto);
            }
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto.Usuarios);
        }


        public ActionResult ParticipantesDoProjeto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Usuario> usuarios = new List<Usuario>();

            Projeto projeto = db.Projeto.Find(id);
            List<int> usuariosId = db.ObterUsuariosDoProjeto(projeto.Id);
            foreach (int idusuario in usuariosId)
            {
                usuarios.Add(db.Usuario.Find(idusuario));
            }
            projeto.Usuarios = usuarios.Distinct().ToList();
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto.Usuarios);
        }
        public ActionResult ExcluirParticipanteDoProjeto(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        public ActionResult ExcluirParticipanteComSucesso(int? id, int? idProjeto)
        {
            db.DeletarEmProjetoUsuariosPorId(idProjeto,id);
            db.SaveChanges();
            return View();
        }

        // GET: Projeto/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projeto/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomeDoProjeto,DescrisaoDoProjeto,DataDeInicioDoProjeto,DataDeTerminoDoProjeto,EstadoDoProjeto")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                db.Projeto.Add(projeto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projeto);
        }

        // GET: Projeto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projeto.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // POST: Projeto/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomeDoProjeto,DescrisaoDoProjeto,DataDeInicioDoProjeto,DataDeTerminoDoProjeto,EstadoDoProjeto")] Projeto projeto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projeto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projeto);
        }

        // GET: Projeto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeto projeto = db.Projeto.Find(id);
            if (projeto == null)
            {
                return HttpNotFound();
            }
            return View(projeto);
        }

        // POST: Projeto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projeto projeto = db.Projeto.Find(id);
            db.Projeto.Remove(projeto);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
