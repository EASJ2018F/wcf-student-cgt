using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentApp.StudentService;

namespace StudentApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new StudentServiceClient())
            {
                var students = client.GetAllStudents();
                foreach (var s in students)
                {
                    Console.WriteLine($"{s.Id}\t{s.Name}");
                }
            }
        }
    }
}
