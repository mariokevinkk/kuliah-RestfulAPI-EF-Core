using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class CourseADO : ICourse
    { 
        private readonly IConfiguration _configuration;
        private string connStr = string.Empty;
        public CourseADO(IConfiguration configuration)
        {
            _configuration = configuration;
            connStr = _configuration.GetConnectionString("DefaultConnection");
        }

        public Course AddCourse(Course course)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"INSERT INTO Courses (CourseName, Duration, CourseDescription, CategoryId)
                                VALUES (@CourseName, @Duration, @CourseDescription, @CategoryId);select SCOPE_IDENTITY()";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CourseName",course.CourseName);
                    cmd.Parameters.AddWithValue("@Duration",course.Duration);
                    cmd.Parameters.AddWithValue("@CourseDescription",course.CourseDescription);
                    cmd.Parameters.AddWithValue("@CategoryId",course.CategoryId);
                    conn.Open();
                    int rowAffected = cmd.ExecuteNonQuery();
                    int courseId = Convert.ToInt32(cmd.ExecuteScalar());
                    course.CourseId = courseId;
                    return course;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public void DeleteCourse(int courseId)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"DELETE FROM Courses WHERE CourseId = @CourseId";
                SqlCommand cmd = new SqlCommand(strSql,conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CourseId",courseId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if(result==0)
                    {
                        throw new Exception("Course not found");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }

        public ViewCourseWithCategory GetCourseById(int courseId)
        {
            using(SqlConnection comm = new SqlConnection(connStr))
            {   //select from View
                string strSql = @"SELECT CourseId, CourseName, Duration,CourseDescription,CategoryId, CategoryName
                                    FROM ViewCourseWithCategory
                                    WHERE CourseId = @CourseId";
                SqlCommand cmd = new SqlCommand(strSql, comm);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                ViewCourseWithCategory course = new ViewCourseWithCategory();
                try
                {
                    comm.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        course.CourseId = Convert.ToInt32(dr["CourseId"]);
                        course.CourseName = dr["CourseName"].ToString();
                        course.Duration = Convert.ToInt32(dr["Duration"]);
                        course.CourseDescription = dr["CourseDescription"].ToString();
                        course.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                        course.CategoryName = dr["CategoryName"].ToString();
                    }
                    return course;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);                
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    comm.Close();
                }
            }
        }

        public IEnumerable<ViewCourseWithCategory> GetCourses()
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"  SELECT dbo.Courses.CourseId, dbo.Courses.CourseName, dbo.Courses.Duration, dbo.Courses.CourseDescription, dbo.Categories.CategoryId, dbo.Categories.CategoryName
                                    FROM dbo.Categories INNER JOIN
                                    dbo.Courses ON dbo.Categories.CategoryId = dbo.Courses.CategoryId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                List<ViewCourseWithCategory> courses = new List<ViewCourseWithCategory>();
                try
                {
                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ViewCourseWithCategory course = new ViewCourseWithCategory();
                        course.CourseId = Convert.ToInt32(dr["CourseId"]);
                        course.CourseName = dr["CourseName"].ToString();
                        course.Duration = Convert.ToInt32(dr["Duration"]);
                        course.CourseDescription = dr["CourseDescription"].ToString();
                        course.CategoryId = Convert.ToInt32(dr["CategoryId"]);
                        course.CategoryName = dr["CategoryName"].ToString();
                        courses.Add(course);
                    }
                    return courses;
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception(sqlEx.Message);                
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
                
            }
        }

        public Course UpdateCourse(Course course)
        {
            using(SqlConnection conn = new SqlConnection(connStr))
            {
                string strSql = @"UPDATE Courses
                                    SET CourseName = @CourseName, Duration = @Duration, CourseDescription = @CourseDescription, CategoryId = @CategoryId
                                    WHERE CourseId = @CourseId";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                try
                {
                    cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                    cmd.Parameters.AddWithValue("@Duration", course.Duration);
                    cmd.Parameters.AddWithValue("@CourseDescription", course.CourseDescription);
                    cmd.Parameters.AddWithValue("@CategoryId", course.CategoryId);
                    cmd.Parameters.AddWithValue("@CourseId", course.CourseId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return course;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
               
            }
        }
    }
}