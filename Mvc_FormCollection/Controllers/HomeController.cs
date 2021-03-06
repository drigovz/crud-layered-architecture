﻿using BLL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Mvc_FormCollection.Controllers
{
    public class HomeController : Controller
    {
        AlunoBLL alunoBLL = new AlunoBLL();
        Aluno aluno = new Aluno();
        List<Aluno> alunos = new List<Aluno>();

        public ActionResult Index()
        {
            alunos = alunoBLL.GetAlunos().ToList();
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
                alunoBLL.IncluirAluno(aluno);
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            aluno = alunoBLL.GetAlunos().Single(a => a.Id == id);
            return View(aluno);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditPost(int id)
        {
            aluno = alunoBLL.GetAlunos().Single(a => a.Id == id);
            UpdateModel(aluno, null, null, excludeProperties: new[] { "Nome" });

            if (ModelState.IsValid)
            {
                alunoBLL.AtualizarAluno(aluno);
                return RedirectToAction("Index");
            }

            return View(aluno);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            alunoBLL.DeletarAluno(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            aluno = alunoBLL.GetAlunos().Single(a => a.Id == id);
            return View(aluno);
        }

        public ActionResult Procurar(string procurarPor, string criterio)
        {
            if (procurarPor == "Email")
            {
                aluno = alunoBLL.GetAlunos().SingleOrDefault(a => a.Email == criterio || criterio == null);
                return View(aluno);
            }
            else
            {
                aluno = alunoBLL.GetAlunos().SingleOrDefault(a => a.Nome == criterio || criterio == null);
                return View(aluno);
            }
        }
    }
}