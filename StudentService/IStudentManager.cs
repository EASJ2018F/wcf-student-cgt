using System.Collections.Generic;

namespace StudentService
{
    public interface IStudentManager
    {
        Student AddStudent(Student student);
        void DeleteStudent(int id);
        void EditStudent(Student student);
        List<Student> GetAllStudents();
        Student GetStudentById(int id);
        List<Student> GetStudentsByCourse(string courseName);
    }
}