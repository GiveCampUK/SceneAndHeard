using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SceneCrm.Entities;
using System.Data.SqlClient;

namespace SceneCrm.Importer {
    class Program {
        static void Main(string[] args) {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            using (var context = new SceneCRM()) {
                Console.Write("Wiping database...");
                WipeDatabase(context);
                Console.WriteLine("Wiped!");
            }


            var ss = new ChildrenProductionsSpreadsheet(@"C:\Users\dylan.beattie\Documents\Scene & Heard\Children and Productions.xls");
            foreach (var row in ss.Rows) {
                Console.WriteLine(row.MembershipNumber);
                Console.WriteLine(row.Forename);
                Console.WriteLine(row.Surname);
            }



            //    foreach (var ct in context.CourseTypes) {
            //        Console.WriteLine(ct.CourseTypeName);
            //        var demoCourse = new Course() {
            //            CourseType = ct,
            //            Year = 2011,
            //        };
            //        context.Courses.AddObject(demoCourse);
            //    }
            //    context.SaveChanges();
            //}
            Console.ReadKey(false);
        }

        static void WipeDatabase(SceneCRM context) {
            context.Courses.ToList().ForEach(context.Courses.DeleteObject);
            context.CourseAttendances.ToList().ForEach(context.CourseAttendances.DeleteObject);
            context.CourseTypes.ToList().ForEach(context.CourseTypes.DeleteObject);
            context.CourseVolunteers.ToList().ForEach(context.CourseVolunteers.DeleteObject);
            context.CrbChecks.ToList().ForEach(context.CrbChecks.DeleteObject);
            context.Jobs.ToList().ForEach(context.Jobs.DeleteObject);
            context.Performances.ToList().ForEach(context.Performances.DeleteObject);
            context.Plays.ToList().ForEach(context.Plays.DeleteObject);
            context.PlayVolunteers.ToList().ForEach(context.PlayVolunteers.DeleteObject);
            context.Productions.ToList().ForEach(context.Productions.DeleteObject);
            context.ProductionVolunteers.ToList().ForEach(context.ProductionVolunteers.DeleteObject);
            context.Students.ToList().ForEach(context.Students.DeleteObject);
            context.Terms.ToList().ForEach(context.Terms.DeleteObject);
            context.Volunteers.ToList().ForEach(context.Volunteers.DeleteObject);
            context.SaveChanges();
        }
    }
}
