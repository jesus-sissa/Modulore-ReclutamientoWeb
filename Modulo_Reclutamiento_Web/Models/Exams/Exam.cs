namespace Modulo_Reclutamiento_Web.Models.Exams
{
    public class Exam
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public List<QuestionOption> Options { get; set; }
        public string CorrectAnswer { get; set; }
        public string ChosenAnswer { get; set; }
        public bool isCorrect { get; set; }

    }

    public class QuestionResponse
    {
        public int Id { get; set; }
        public string ChosenAnswer { get; set; }
        public bool Correct { get; set; }
    }

    public class QuestionOption 
    {
        public int Index { get; set; }
        public string Answer { get; set; }
    }
}
