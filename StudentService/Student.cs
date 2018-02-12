using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentService
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Courses { get; set; }
    }
}