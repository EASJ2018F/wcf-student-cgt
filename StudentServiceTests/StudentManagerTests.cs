using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.Tests
{
    [TestClass()]
    public class StudentManagerTests
    {
        [TestMethod(), ExpectedException(typeof(ArgumentException))]
        public void AddStudentIdNot0Test()
        {
            var students = new StudentManager();
            var student = new Student
            {
                Id = 5,
            };
            students.AddStudent(student);
        }

        [TestMethod(), ExpectedException(typeof(ArgumentNullException))]
        public void AddNullStudentTest()
        {
            var students = new StudentManager();
            students.AddStudent(null);
        }
    }
}