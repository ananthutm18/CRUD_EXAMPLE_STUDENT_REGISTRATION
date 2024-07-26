using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using CRUD_EXAMPLE.Models;
using System.Data;
namespace CRUD_EXAMPLE.DAL
{
    public class StudentDAL
    {

        string conString = ConfigurationManager.ConnectionStrings["mycrudconnectionstring"].ToString();

        //Get all Student

        public List<Student> GetAllStudent()
        {
            List<Student> StudentList = new List<Student>();

            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "sp_GetAllStudent";

                SqlDataAdapter sqlDA = new SqlDataAdapter(command);
                DataTable dtProducts = new DataTable();
                connection.Open();
                sqlDA.Fill(dtProducts);
                connection.Close();
                foreach (DataRow dr in dtProducts.Rows)
                {

                    StudentList.Add(new Student
                    {
                      

                        StudentID= Convert.ToInt32(dr["StudentID"]),
                        FirstName= dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString(),
                        Phone=Convert.ToInt64(dr["Phone"]),
                        Address= dr["Address"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        Dob= (DateTime)dr["Dob"],
                        City = dr["City"].ToString(),
                        State= dr["State"].ToString(),
                        Pin= Convert.ToInt64(dr["Pin"]),
                        Email= dr["Email"].ToString(),
                        ImageData = dr["ImageData"] != DBNull.Value ? (byte[])dr["ImageData"] : null,
                        PdfData = dr["PdfData"] != DBNull.Value ? (byte[])dr["PdfData"] : null



                        //  ImageData = dr["ImageData"] != DBNull.Value ? Convert.ToBase64String((byte[])dr["ImageData"]) : null,


                        // PdfData = dr["PdfData"] != DBNull.Value ? (byte[])dr["PdfData"] : null


                    });

                }
            }


            return StudentList;
        }


        //Create student

        public bool InsertStudent(Student student)
        {

            int id = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_InsertStudentsData", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);

                command.Parameters.AddWithValue("@Phone", student.Phone);
                command.Parameters.AddWithValue("@Address", student.Address);
                command.Parameters.AddWithValue("@Gender", student.Gender);
                command.Parameters.AddWithValue("@Dob", student.Dob);
                command.Parameters.AddWithValue("@City", student.City);
                command.Parameters.AddWithValue("@State", student.State);
                command.Parameters.AddWithValue("@pin", student.Pin);


                if (student.ImageData != null)
                {
                    command.Parameters.Add("@ImageData", SqlDbType.VarBinary, -1).Value = student.ImageData;
                }
                else
                {
                    command.Parameters.Add("@ImageData", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }


                if (student.PdfData != null)
                {
                    command.Parameters.Add("@pdfData", SqlDbType.VarBinary, -1).Value = student.PdfData;
                }
                else
                {
                    command.Parameters.Add("@pdfData", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }

                command.Parameters.AddWithValue("@Email", student.Email);


                connection.Open();
                id=command.ExecuteNonQuery();
                connection.Close();

            }
            if (id > 0)
            {
                return true;
            }
            else
            {
                return false;   
            }
        }






        public byte[] GetPdfData(int id)
        {
            byte[] pdfData = null;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetStudentPdfData", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentID", id); // Corrected parameter name

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0)) // Check if the data is not null
                            {
                                pdfData = (byte[])reader[0];
                            }
                        }
                    }
                }
            }

            return pdfData;
        }










        //Update student


        public bool UpdateStudent(Student student)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("sp_UpdateStudentData", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StudentID", student.StudentID); // Added ID for updating
                command.Parameters.AddWithValue("@FirstName", student.FirstName);
                command.Parameters.AddWithValue("@LastName", student.LastName);
                command.Parameters.AddWithValue("@Phone", student.Phone);
                command.Parameters.AddWithValue("@Address", student.Address);
                command.Parameters.AddWithValue("@Gender", student.Gender);
                command.Parameters.AddWithValue("@Dob", student.Dob);
                command.Parameters.AddWithValue("@City", student.City);
                command.Parameters.AddWithValue("@State", student.State);
                command.Parameters.AddWithValue("@Pin", student.Pin);

                if (student.ImageData != null)
                {
                    command.Parameters.Add("@ImageData", SqlDbType.VarBinary, -1).Value = student.ImageData;
                }
                else
                {
                    command.Parameters.Add("@ImageData", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }

                if (student.PdfData != null)
                {
                    command.Parameters.Add("@pdfData", SqlDbType.VarBinary, -1).Value = student.PdfData;
                }
                else
                {
                    command.Parameters.Add("@pdfData", SqlDbType.VarBinary, -1).Value = DBNull.Value;
                }

                command.Parameters.AddWithValue("@Email", student.Email);


                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
                connection.Close();
            }
            return rowsAffected > 0;
        }












        //get student by Id


        public Student GetStudentById(int id)
        {
            Student student = null;

            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("sp_GetStudentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentID", id);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            student = new Student
                            {
                                StudentID = Convert.ToInt32(reader["StudentID"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Phone = Convert.ToInt64(reader["Phone"]),
                                Address = reader["Address"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Dob = reader["Dob"] != DBNull.Value ? (DateTime)reader["Dob"] : default(DateTime),
                                City = reader["City"].ToString(),
                                State = reader["State"].ToString(),
                                Pin = Convert.ToInt64(reader["Pin"]),
                               

                                // Handle binary data
                                ImageData = reader["ImageData"] != DBNull.Value ? (byte[])reader["ImageData"] : null,
                                PdfData= reader["PdfData"] != DBNull.Value ? (byte[])reader["pdfData"] : null,
                                Email = reader["Email"].ToString(),
                            };
                        }
                    }
                }
            }

            return student;
        }




        //Delete Student


        public bool DeleteStudent(int studentID)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            {
                using (SqlCommand command = new SqlCommand("sp_DeleteStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentID", studentID);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    connection.Close();

                    return result > 0;
                }
            }
        }




    }
}