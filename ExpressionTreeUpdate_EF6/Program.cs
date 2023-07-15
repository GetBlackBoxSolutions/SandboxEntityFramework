using ExpressionTreeUpdate_EF6.Models;

namespace ExpressionTreeUpdate_EF6
{
    internal class Program
    {
        static void Main()
        {
            TestGetData();
            Console.ReadLine();
        }

        static void TestGetData() 
        {
            var dbContext = new EfSandboxDbContext();
            var repository = new GenericRepository<Student>(dbContext);

            var data = repository.GetData();

            Console.WriteLine("Retrieved Records {0}", data.Count());
        }
    }
}