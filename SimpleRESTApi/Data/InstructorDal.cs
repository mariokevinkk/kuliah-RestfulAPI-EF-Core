// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using SimpleRESTApi.Models;

// namespace SimpleRESTApi.Data
// {
//     public class InstructorDal : IInstructor
//     {
// private List<Instructor> _instructors = new List<Instructor>();
// public InstructorDal()
// {
//     _instructors = new List<Instructor>
//     {
// new Instructor { InstructorId = 1, InstructorName = "John 1",InstructorEmail="john1@g.c", InstructorPhone="0888888881", InstructorPhone="Jalan 1", InstructorCity="Yogyakarta" },
// new Instructor { InstructorId = 2, InstructorName = "John 2",InstructorEmail="john2@g.c", InstructorPhone="0888888882", InstructorAddress="Jalan 2", InstructorCity="Yogyakarta" },
// new Instructor { InstructorId = 3, InstructorName = "John 3",InstructorEmail="john3@g.c", InstructorPhone="0888888883", InstructorAddress="Jalan 3", InstructorCity="Yogyakarta" },
// new Instructor { InstructorId = 4, InstructorName = "John 4",InstructorEmail="john4@g.c", InstructorPhone="0888888884", InstructorAddress="Jalan 4", InstructorCity="Yogyakarta" },
// new Instructor { InstructorId = 5, InstructorName = "John 5",InstructorEmail="john5@g.c", InstructorPhone="0888888885", InstructorAddress="Jalan 5", InstructorCity="Yogyakarta" }
          

//     };
// }
       

       

//         public Instructor GetInstructorById(int instructorId)
//         {
//              var instructor = _instructors.FirstOrDefault(i => i.InstructorId == instructorId);
//             if(instructor == null)
//             {
//                 throw new Exception("Could not found");
//             }
//             return instructor;
//         }

//         public IEnumerable<Instructor> GetInstructors()
//         {
//             return _instructors;
//         }
//          public Instructor AddInstructor(Instructor instructor)
//         {
//              _instructors.Add(instructor);
//             return instructor;
//         }
//          public void DeleteInstructor(int instructorId)
//         {
//              var instructor = GetInstructorById(instructorId);
//             if(instructor != null){
//                 _instructors.Remove(instructor);
//             }
//         }

//         public Instructor UpdateInstructor(Instructor instructor)
//         {
//            var existingInstructor = GetInstructorById(instructor.InstructorId);
//             if(existingInstructor != null){
//                 existingInstructor.InstructorName = instructor.InstructorName;
//                 existingInstructor.InstructorEmail = instructor.InstructorEmail;
//                 existingInstructor.InstructorPhone = instructor.InstructorPhone;
//                 existingInstructor.InstructorAddress = instructor.InstructorAddress;
//                 existingInstructor.InstructorCity = instructor.InstructorCity;
//             }
//             return existingInstructor;
//         }
//     }
// }