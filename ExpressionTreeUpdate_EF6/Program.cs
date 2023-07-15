using ExpressionTreeUpdate_EF6.Models;

namespace ExpressionTreeUpdate_EF6
{
    internal class Program
    {
        static async Task Main()
        {
            //TestGetData();
            await TestUpdateData();
            Console.ReadLine();
        }

        static void TestGetData() 
        {
            var dbContext = new EfSandboxDbContext();
            var repository = new GenericRepository<Student>(dbContext);

            var data = repository.GetData();

            Console.WriteLine("Retrieved Records {0}", data.Count());
        }

        static async Task TestUpdateData() 
        {
            var dbContext = new EfSandboxDbContext();
            var repository = new GenericRepository<Student>(dbContext);

            var singleRecord = repository.GetData().FirstOrDefault();

            if(singleRecord == null) return;

            singleRecord.LastName = "Doe";
           
            await repository.Update(singleRecord, s => new { s.LastName, s.FirstName });
        }
    }
}