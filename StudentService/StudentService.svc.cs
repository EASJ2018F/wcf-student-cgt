using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace StudentService
{
	public class StudentService : IStudentService
	{
		private readonly string _connstr;

		public StudentService()
		{
			// Use Azure DB if available. Otherwise use local testing DB.
			var azure = ConfigurationManager.ConnectionStrings["azuredb"]?.ConnectionString;
			var desktop = ConfigurationManager.ConnectionStrings["desktop"]?.ConnectionString;
			_connstr = azure ?? desktop ?? throw new ApplicationException("no connection string");
		}

		public int AddStudent(Student student)
		{
			if (student == null)
			{
				throw new ArgumentNullException(nameof(student));
			}
			if (student.Id != 0)
			{
				throw new ArgumentException("Id of new Student must be 0, an id will be assigned by AddStudent", nameof(student));
			}
			if (string.IsNullOrWhiteSpace(student.Name))
			{
				throw new ArgumentException("empty student name");
			}
			if (string.IsNullOrWhiteSpace(student.Email))
			{
				throw new ArgumentException("empty student email");
			}

			using (var conn = new SqlConnection(_connstr))
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						var cmd = conn.CreateCommand();
						cmd.Transaction = trans;
						cmd.CommandText = "INSERT INTO Students ([Name], Email) VALUES (@name, @email); SELECT SCOPE_IDENTITY();";
						cmd.Parameters.AddWithValue("@name", student.Name);
						cmd.Parameters.AddWithValue("@email", student.Email);
						int id = (int)(decimal)cmd.ExecuteScalar(); // WTF? https://stackoverflow.com/a/14287953
						trans.Commit();
						return id;
					}
					catch
					{
						trans.Rollback();
						throw;
					}
				}
			}
		}

		public void DeleteStudent(int id)
		{
			using (var conn = new SqlConnection(_connstr))
			using (var cmd = new SqlCommand("DELETE FROM Students where Id = @id", conn))
			{
				cmd.Parameters.AddWithValue("@id", id);
				conn.Open();
				int deleted = cmd.ExecuteNonQuery();
				if (deleted == 0)
				{
					throw new InvalidOperationException($"no student with id {id}");
				}
			}
		}

		public void EditStudent(Student student)
		{
			if (student == null)
			{
				throw new ArgumentNullException(nameof(student));
			}
			if (string.IsNullOrWhiteSpace(student.Name))
			{
				throw new ArgumentException("empty student name");
			}
			if (string.IsNullOrWhiteSpace(student.Email))
			{
				throw new ArgumentException("empty student email");
			}

			using (var conn = new SqlConnection(_connstr))
			{
				conn.Open();
				using (var trans = conn.BeginTransaction())
				{
					try
					{
						using (var cmd = new SqlCommand("UPDATE Students SET [Name] = @name, Email = @email WHERE Id = @id", conn, trans))
						{
							cmd.Parameters.AddWithValue("@id", student.Id);
							cmd.Parameters.AddWithValue("@name", student.Name);
							cmd.Parameters.AddWithValue("@email", student.Email);
							int changed = cmd.ExecuteNonQuery();
							if (changed == 0)
							{
								throw new InvalidOperationException($"no student with id {student.Id}");
							}
						}
						trans.Commit();
					}
					catch
					{
						trans.Rollback();
						throw;
					}
				}
			}
		}

		public List<Student> GetAllStudents()
		{
			using (var conn = new SqlConnection(_connstr))
			using (var cmd = new SqlCommand("SELECT Id, [Name], Email FROM Students", conn))
			{
				conn.Open();
				var r = cmd.ExecuteReader();
				var names = new List<Student>();
				while (r.Read())
				{
					names.Add(new Student
					{
						Id = (int)r["Id"],
						Name = (string)r["Name"],
						Email = (string)r["Email"],
					});
				}
				r.Close();
				return names;
			}
		}

		public Student GetStudent(int id)
		{
			using (var conn = new SqlConnection(_connstr))
			using (var cmd = new SqlCommand("SELECT Id, [Name], Email from Students where Id = @id", conn))
			{
				cmd.Parameters.AddWithValue("@id", id);
				conn.Open();

				var r = cmd.ExecuteReader();
				try
				{
					if (!r.Read())
					{
						throw new InvalidOperationException($"no student with id {id}");
					}
					return new Student
					{
						Id = (int)r["Id"],
						Name = (string)r["Name"],
						Email = (string)r["Email"],
					};
				}
				finally
				{
					r.Close();
				}
			}
		}
	}
}