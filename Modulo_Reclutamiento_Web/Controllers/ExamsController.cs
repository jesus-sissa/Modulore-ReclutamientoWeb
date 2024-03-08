using Microsoft.AspNetCore.Mvc;
using Modulo_Reclutamiento_Web.Service;
using Modulo_Reclutamiento_Web.Models.Exams;
using System.Text.Json;
using Modulo_Reclutamiento_Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace Modulo_Reclutamiento_Web.Controllers
{
    public class ExamsController : Controller
    {

        [Authorize(Roles = "User")]
        public IActionResult Index(int pto, int? MSG )
        {
            ViewBag.pto = pto;
            ViewBag.MSG = MSG;
            User_Persistent_Data.IdProspExam = pto;
            var exam = ExamsService.Instancia.ExamBarsit(pto);
            List<Exam> questions = ExamsService.Instancia.getQuestionsExamBarsit(); 
            
            return View(questions);
        }

        [Authorize(Roles = "User")]
        public IActionResult Example(int pto)
        {
            ViewBag.pto = pto;
           
            List<Exam> questions = ExamsService.Instancia.getExampleQuestionsExamBarsit();

            return View(questions);
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        public bool SaveQuestions([FromBody]List<QuestionResponse> questions)
        {
            bool resp = false;
            
            var _save = ExamsService.Instancia.SaveQuestions(questions);

            if (_save)
            {
                resp = true;
                
            }

            return resp;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public bool RestarExam(string password,int pto)
        {

            bool resp = false;
            if (Tools.EncriptacionSHA1(password).ToUpper() == User_Persistent_Data.Password)
            {
                var exam = ExamsService.Instancia.ExamBarsit(pto);
                resp = ExamsService.Instancia.ResetExam(User_Persistent_Data.IdExam);
            }

            return resp;
        }


    }
}
