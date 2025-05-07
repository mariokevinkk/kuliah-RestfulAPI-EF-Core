using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public class InstructorEF : IInstructor
    {
        private readonly ApplicationDbContext _context;
        public InstructorEF(ApplicationDbContext context)
        {
            _context = context;
        }
        public Instructor AddInstructor(Instructor instructor)
        {
            try
            {
                _context.Instructors.Add(instructor);
                _context.SaveChanges();
                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding instructor", ex);
            }
        }

        public void DeleteInstructor(int instructorId)
        {
            var instructor = GetInstructorById(instructorId);
            if (instructor == null)
            {
                throw new Exception("Instructor not found");
            }
            try
            {
                _context.Instructors.Remove(instructor);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting instructor", ex);
            }
        }

        public IEnumerable<Instructor> GetInstructors()
        {
           var instructors = _context.Instructors.OrderByDescending(i => i.InstructorName).ToList();
            return instructors;
        }

        public Instructor GetInstructorById(int instructorId)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.InstructorId == instructorId);
            if (instructor == null)
            {
                throw new Exception("Instructor not found");
            }
            return instructor;
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            var existingInstructor = GetInstructorById(instructor.InstructorId);
            if (existingInstructor == null)
            {
                throw new Exception("Instructor not found");
            }
            try
            {
                existingInstructor.InstructorName = instructor.InstructorName;
                existingInstructor.InstructorEmail = instructor.InstructorEmail;
                existingInstructor.InstructorPhone = instructor.InstructorPhone;
                existingInstructor.InstructorAddress = instructor.InstructorAddress;
                existingInstructor.InstructorCity = instructor.InstructorCity;
                _context.SaveChanges();
                return existingInstructor;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating instructor", ex);
            }
        }
    }
}