using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentService
{
    public class StudentManager : IStudentManager
    {
        public static StudentManager Instance = new StudentManager();

        public StudentManager()
        {
            _students = new List<Student>();

            AddStudent(new Student
            {
                Name = "Anders",
            });
            AddStudent(new Student
            {
                Name = "Bo",
                Courses = new List<string>(),
            });
            AddStudent(new Student
            {
                Name = "Christoffer",
                Courses = new List<string> { "Programmering", "Teknik", "Systemudvikling", "Valgfag: databaser" },
            });
            AddStudent(new Student
            {
                Name = "David",
                Courses = new List<string> { "Programmering" },
            });
        }

        private List<Student> _students;

        private int _nextId = 0;
        private int NextId { get => ++_nextId; }

        public Student AddStudent(Student student)
        {
            if (student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            else if (student.Id != 0)
            {
                throw new ArgumentException("Id of new Student must be 0, an id will be assigned by AddStudent", nameof(student));
            }

            student.Id = NextId;
            _students.Add(student);
            return student;
        }

        public List<Student> GetAllStudents() => _students;

        public Student GetStudentById(int id) => _students.Single(s => s.Id == id);

        public void EditStudent(Student student)
        {
            var old = _students.Single(s => s.Id == student.Id);
            old.Name = student.Name;
            old.Courses = student.Courses;
        }

        public void DeleteStudent(int id) => _students.RemoveAt(_students.FindIndex(s => s.Id == id));

        public List<Student> GetStudentsByCourse(string courseName) => _students.Where(s => s.Courses.Contains(courseName)).ToList();
    }
}