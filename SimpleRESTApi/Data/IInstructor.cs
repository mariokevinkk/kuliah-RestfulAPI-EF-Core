using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleRESTApi.Models;

namespace SimpleRESTApi.Data
{
    public interface IInstructor
    {
        //crud
         IEnumerable<Instructor> GetInstructors();
        Instructor GetInstructorById(int instructorId);
        Instructor AddInstructor(Instructor instructor);
        Instructor UpdateInstructor(Instructor instructor);
        void DeleteInstructor(int instructorId);
    }
}