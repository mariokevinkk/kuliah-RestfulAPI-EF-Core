using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using SimpleRESTApi.Models;


namespace SimpleRESTApi.Data
{
    public class CourseEF: ICourse
    {
        private readonly ApplicationDbContext _context;
        public CourseEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public Course AddCourse(Course Course)
        {
            try
            {
                _context.Courses.Add(Course);
                _context.SaveChanges();
                return Course;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding course: " + ex.Message);
            }
        }

        public void DeleteCourse(int CourseID)
        {
            var Course = _context.Courses.FirstOrDefault(c => c.CourseId == CourseID);
            if (Course == null)
            {
                throw new Exception("Course not found");
            }
            try
            {
                _context.Courses.Remove(Course);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting course: " + ex.Message);
            }
        }

        public ViewCourseWithCategory GetCourseById(int CourseID)
        {
            var course = (from c in _context.Courses
                          join category in _context.Categories on c.CategoryId equals category.CategoryId
                          where c.CourseId == CourseID
                          select new ViewCourseWithCategory
                          {
                              CourseId = c.CourseId,
                              CourseName = c.CourseName,
                              CourseDescription = c.CourseDescription,
                              Duration = c.Duration,
                              CategoryId = c.CategoryId,
                              CategoryName = category.CategoryName
                          }).FirstOrDefault();
            if (course == null)
            {
                throw new Exception("Course not found");
            }
            return course;
        }

        public IEnumerable<ViewCourseWithCategory> GetCourses()
        {
            var Course = (from c in _context.Courses
                           join cat in _context.Categories on c.CategoryId equals cat.CategoryId
                           select new ViewCourseWithCategory
                           {
                               CourseId = c.CourseId,
                               CourseName = c.CourseName,
                               CourseDescription = c.CourseDescription,
                               Duration = c.Duration,
                               CategoryId = c.CategoryId,
                               CategoryName = cat.CategoryName
                           }).ToList();
            return Course;
        }

        public Course UpdateCourse(Course Course)
        {
            var existingCourse = _context.Courses.FirstOrDefault(c => c.CourseId == Course.CourseId);
            if (existingCourse == null)
            {
                throw new Exception("Course not found");
            }
            try
            {
                existingCourse.CourseName = Course.CourseName;
                existingCourse.CourseDescription = Course.CourseDescription;
                existingCourse.Duration = Course.Duration;
                existingCourse.CategoryId = Course.CategoryId;
                _context.SaveChanges();
                return existingCourse;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating course: " + ex.Message);
            }
        }
    }

}