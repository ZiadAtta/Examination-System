namespace Examination_System.DTOS
{
    public class QuestionDTO
    {
        public string? Text { get; set; }
        public int Difficulty { get; set; }
        public int InstructorId { get; set; }
        public List<ChoiceDTO>? Choices;
    }
}
