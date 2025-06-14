namespace Examination_System.ViewModels
{
    public class SubmitExamVM
    {
        public int StudentId { get; set; }
        public int ExamId { get; set; }
        public List<StudentAnswerVM> Answers { get; set; }
    }

    public class StudentAnswerVM
    {
        public int QuestionId { get; set; }
        public int ChoiceId { get; set; }
    }

}
