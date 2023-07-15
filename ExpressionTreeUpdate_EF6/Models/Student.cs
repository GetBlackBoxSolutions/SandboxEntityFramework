namespace ExpressionTreeUpdate_EF6.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;
      
        public DateTime? Birthday { get; set; }
    }
}
