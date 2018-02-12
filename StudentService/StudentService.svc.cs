using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudentService
{
    public class StudentService : IStudentService
    {
        private IStudentManager _students;

        public StudentService(IStudentManager studentManager = null)
        {
            _students = studentManager ?? StudentManager.Instance;
        }

        public List<Student> GetAllStudents()
        {
            return _students.GetAllStudents();
        }

        public Student GetStudent(int id)
        {
            return _students.GetStudentById(id);
        }

        public void DeleteStudent(int id)
        {
            _students.DeleteStudent(id);
        }

        public void EditStudent(Student student)
        {
            _students.EditStudent(student);
        }

        public Student AddStudent(Student student)
        {
            return _students.AddStudent(student);
        }
    }
}