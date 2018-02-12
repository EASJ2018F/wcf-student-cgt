using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudentService
{
    [ServiceContract]
    public interface IStudentService
    {
        [OperationContract]
        Student AddStudent(Student student);

        [OperationContract]
        List<Student> GetAllStudents();

        [OperationContract]
        Student GetStudent(int id);

        [OperationContract]
        void EditStudent(Student student);

        [OperationContract]
        void DeleteStudent(int id);
    }
}
