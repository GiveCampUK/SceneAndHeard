﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SceneCrm.Entities;
using System.Data.SqlClient;
using System.Data.Objects;

namespace SceneCrm.Importer {
    class Program {
        static void Main(string[] args) {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            using (var context = new SceneCRM()) {
                Console.Write("Wiping database...");
                WipeDatabase(context);
                Console.WriteLine("Wiped!");
            }

            using (var context = new SceneCRM()) {
                var oneOnOne = new CourseType() { CourseTypeCode = "OO", CourseTypeName = "One on One" };
                context.CourseTypes.AddObject(oneOnOne);
                var pm1 = new CourseType() { CourseTypeCode = "PM1", CourseTypeName = "Playmaking One" };
                context.CourseTypes.AddObject(pm1);
                var rp = new CourseType() { CourseTypeCode = "RP", CourseTypeName = "Replay" };
                context.CourseTypes.AddObject(rp);
                var s1 = new CourseType() { CourseTypeCode = "S1", CourseTypeName = "Stage One" };
                context.CourseTypes.AddObject(rp);
                var pb = new CourseType() { CourseTypeCode = "PB", CourseTypeName = "Playback" };
                context.CourseTypes.AddObject(rp);
                context.SaveChanges();
                var ss = new ChildrenProductionsSpreadsheet(@"C:\Users\dylan.beattie\Documents\Scene & Heard\Children and Productions.xls");
                foreach (var row in ss.Rows) {
                    var student = context.Students.FindOrMake(row.MembershipNumber, row.Forename, row.Surname);
                    if (row.AttendedPm1) {
                        var term = context.Terms.FindOrMake(row.PlaymakingOneTerm);
                        var course = context.Courses.FindOrMake(pm1, term, row.PlaymakingOneYear);
                        if (course != null) {
                            student.CourseAttendances.Add(new CourseAttendance() {
                                Student = student,
                                Course = course,
                                Completed = true
                            });
                        }
                    }
                    Console.WriteLine("Added " + student.Forename + " " + student.Surname);
                    context.SaveChanges();
                }
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

    public static class SceneCRMExtensions {
        public static Student FindOrMake(this ObjectSet<Student> students, string membershipNumber, string forename, string surname) {
            var student = students.FirstOrDefault(s => s.MembershipNumber == membershipNumber);
            if (student == null) {
                student = new Student() {
                    MembershipNumber = membershipNumber,
                    Forename = forename,
                    Surname = surname
                };
                students.AddObject(student);
                students.Context.SaveChanges();
            }
            return (student);
        }
        public static Course FindOrMake(this ObjectSet<Course> courses, CourseType type, Term term, string fourDigitYear) {
            int year;
            Course course = null;
            if (Int32.TryParse(fourDigitYear, out year) && year > 1900 && year < 2100) {
                course = courses.FirstOrDefault(c => c.CourseTypeCode == type.CourseTypeCode && c.TermId == term.TermId && c.Year == year);
                if (course == default(Course)) {
                    course = new Course() { Term = term, CourseType = type, Year = year };
                    courses.AddObject(course);
                    courses.Context.SaveChanges();
                }
            }
            return (course);
        }
        public static Term FindOrMake(this ObjectSet<Term> terms, string termName) {
            var term = terms.FirstOrDefault(t => t.TermName == termName);
            if (term == default(Term)) {
                term = new Term() { TermName = termName };
                terms.AddObject(term);
                terms.Context.SaveChanges();
            }
            return (term);
        }
    }
}