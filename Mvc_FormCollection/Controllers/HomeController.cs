using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mvc_FormCollection.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AlunoBLL _aluno = new AlunoBLL();
            List<Aluno> alunos = _aluno.GetAlunos().ToList();

            return View(alunos);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Aluno aluno)
        {
            if (ModelState.IsValid)
            {
                AlunoBLL alunoBll = new AlunoBLL();
                alunoBll.IncluirAluno(aluno);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            AlunoBLL alunoBLL = new AlunoBLL();
            Aluno aluno = alunoBLL.GetAlunos().Single(a => a.Id == id);
            return View(aluno);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            AlunoBLL alunoBLL = new AlunoBLL();
            Aluno aluno = alunoBLL.GetAlunos().Single(a => a.Id == id);
            UpdateModel(aluno, null, null, excludeProperties: new[] { "Nome" });

            if (ModelState.IsValid)
            {
                alunoBLL.AtualizarAluno(aluno);
                return RedirectToAction("Index");
            }

            return View(aluno);
        }
    }
}