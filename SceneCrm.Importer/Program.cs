using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SceneCrm.Entities;
using System.Data.SqlClient;
using System.Data.Objects;
using SceneCrm.LegacyData;

namespace SceneCrm.Importer {
    class Program {
        static CourseType oneOnOne;
        static CourseType pm1;
        static CourseType rp;
        static CourseType s1;
        static CourseType pb;
 
        static void Main(string[] args) {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

            using (var context = new SceneCRM()) {
                WipeDatabase(context);

                oneOnOne = context.CourseTypes.FindOrMake("OO", "One on One");
                pm1 = context.CourseTypes.FindOrMake("PM1", "Playmaking One");
                rp = context.CourseTypes.FindOrMake("RP", "Replay");
                s1 = context.CourseTypes.FindOrMake("S1", "Stage One");
                pb = context.CourseTypes.FindOrMake("PB", "Playback");

                PopulateJobTypes(context);

                var ss = new ChildrenProductionsSpreadsheet(@"C:\Users\dylan.beattie\Documents\Scene & Heard\Children and Productions.xls");

                foreach (var row in ss.Rows) {
                    var student = context.Students.FindOrMake(row.MembershipNumber, row.Forename, row.Surname);
                    student.QuestionnaireResponse = row.QuestionnaireResponses;
                    ImportPlaymakingOne(context, student, row);
                    ImportPlayback(context, student, row);
                    ImportReplay(context, student, row);
                    ImportStageOne(context, student, row);
                    ImportOneOnOne(context, student, row);
                    Console.WriteLine("Added " + student.Forename + " " + student.Surname);
                    context.SaveChanges();
                }
                ImportVolunteerDataFromAccessDatabase(context, @"C:\Users\dylan.beattie\Documents\Scene & Heard\Volunteers.mdb", "giraffe");
            }
            Console.ReadKey(false);
        }

        static void ImportReplay(SceneCRM context, Student student, ChildrenProductionsSpreadsheet.Row row) {
            if (row.AttendedReplay) {
                ImportPlay(context, student, rp, row.ReplayTerm, row.ReplayYear, row.ReplayProduction, row.ReplayPlay, row.ReplayDramaturg, row.ReplayDirector, row.ReplayActor1,
                    row.ReplayActor2, row.ReplayActor3);
            }
        }
        static void ImportStageOne(SceneCRM context, Student student, ChildrenProductionsSpreadsheet.Row row) {
            if (row.AttendedStageOne) {
                ImportPlay(context, student, s1, row.StageOneTerm, row.StageOneYear, String.Empty, String.Empty, String.Empty, String.Empty, row.StageOneActor);
            }
        }
        static void ImportOneOnOne(SceneCRM context, Student student, ChildrenProductionsSpreadsheet.Row row) {
            if (row.AttendedOneOnOne) {
                ImportPlay(context, student, oneOnOne, row.OneonOneTerm, row.OneonOneYear, row.OneonOneProduction, row.OneonOnePlay, row.OneonOneWriter, row.OneonOneDirector, row.OneonOneActor);
            }
        }
        static void ImportPlayback(SceneCRM context, Student student, ChildrenProductionsSpreadsheet.Row row) {
            if (row.AttendedPlayback) {
                ImportPlay(context, student, pb, "Playback", row.PlaybackYear, row.PlaybackProduction, row.PlaybackPlay, row.PlaybackDramaturg, row.PlaybackDirector,
                        row.PlaybackActor
                        );
            }
        }
        static object ImportPlay(SceneCRM context, Student student, CourseType courseType, string termName, string termYear, string productionName, string playName,
            string dramaturg, string director, params string[] actors) {
            object returnValue = null;
            var term = context.Terms.FindOrMake(termName);
            var course = context.Courses.FindOrMake(courseType, term, termYear);
            if (course != null) {
                var attendance = new CourseAttendance() {
                    Student = student,
                    Course = course,
                    Completed = true
                };
                Production production = context.Productions.FindOrMake(productionName);

                if (!String.IsNullOrWhiteSpace(playName)) {
                    var play = new Play() {
                        Student = student,
                        Title = playName
                    };
                    if (production != null) play.Production = production;
                    attendance.Play = play;
                    student.Plays.Add(play);
                    AddPlayVolunteer(context, play, dramaturg, Jobs.Dramaturg);
                    AddPlayVolunteer(context, play, director, Jobs.Director);
                    foreach (var actor in actors) {
                        AddPlayVolunteer(context, play, actor, Jobs.Actor);
                    }
                    returnValue = play;
                } else {
                    AddCourseVolunteer(context, course, dramaturg, Jobs.Dramaturg);
                    AddCourseVolunteer(context, course, director, Jobs.Director);
                    foreach (var actor in actors) {
                        AddCourseVolunteer(context, course, actor, Jobs.Actor);
                    }
                    returnValue = course;
                }
                student.CourseAttendances.Add(attendance);
            }
            return(returnValue);
        }

        static void ImportPlaymakingOne(SceneCRM context, Student student, ChildrenProductionsSpreadsheet.Row row) {
            if (row.AttendedPm1) {
                ImportPlay(context, student, pm1, row.PlaymakingOneTerm, row.PlaymakingOneYear, row.PlaymakingOneProduction, row.PlaymakingOnePlay, row.PlaymakingOneDramaturg, row.PlaymakingOneDirector,
                    row.PlaymakingOneActor1, row.PlaymakingOneActor2, row.PlaymakingOneActor3);

                //var term = context.Terms.FindOrMake(row.PlaymakingOneTerm);
                //var course = context.Courses.FindOrMake(pm1, term, row.PlaymakingOneYear);
                //if (course != null) {
                //    var attendance = new CourseAttendance() {
                //        Student = student,
                //        Course = course,
                //        Completed = true
                //    };
                //    Production production = context.Productions.FindOrMake(row.PlaymakingOneProduction);

                //    if (!String.IsNullOrWhiteSpace(row.PlaymakingOnePlay)) {
                //        var play = new Play() {
                //            Student = student,
                //            Title = row.PlaymakingOnePlay
                //        };
                //        if (production != null) play.Production = production;
                //        attendance.Play = play;
                //        student.Plays.Add(play);
                //        AddPlayVolunteer(context, play, row.PlaymakingOneDramaturg, Jobs.Dramaturg);
                //        AddPlayVolunteer(context, play, row.PlaymakingOneDirector, Jobs.Director);
                //        AddPlayVolunteer(context, play, row.PlaymakingOneActor1, Jobs.Actor);
                //        AddPlayVolunteer(context, play, row.PlaymakingOneActor2, Jobs.Actor);
                //        AddPlayVolunteer(context, play, row.PlaymakingOneActor3, Jobs.Actor);
                //    } else {
                //        AddCourseVolunteer(context, course, row.PlaymakingOneDramaturg, Jobs.Dramaturg);
                //        AddCourseVolunteer(context, course, row.PlaymakingOneActor1, Jobs.Actor);
                //        AddCourseVolunteer(context, course, row.PlaymakingOneActor2, Jobs.Actor);
                //        AddCourseVolunteer(context, course, row.PlaymakingOneActor3, Jobs.Actor);
                //        AddCourseVolunteer(context, course, row.PlaymakingOneDirector, Jobs.Director);
                //    }
                //    student.CourseAttendances.Add(attendance);
            }
        }
 
        static void AddPlayVolunteer(SceneCRM context, Play play, string volunteerName, string jobTitle) {
            var vol = context.Volunteers.FindOrMake(volunteerName);
            if (vol == null) return;
            var job = context.Jobs.FindOrMake(jobTitle);
            if (!vol.PlayVolunteers.Any(cv => cv.Job == job && cv.Play == play)) {
                context.PlayVolunteers.AddObject(new PlayVolunteer() {
                    Play = play,
                    Volunteer = vol,
                    Job = job
                });
            }
            context.SaveChanges();
        }
        static void AddCourseVolunteer(SceneCRM context, Course course, string volunteerName, string jobTitle) {
            var vol = context.Volunteers.FindOrMake(volunteerName);
            if (vol == null) return;
            var job = context.Jobs.FindOrMake(jobTitle);
            if (!vol.CourseVolunteers.Any(cv => cv.Job == job && cv.Course == course)) {
                context.CourseVolunteers.AddObject(new CourseVolunteer() {
                    Course = course,
                    Volunteer = vol,
                    Job = job
                });
            }
            context.SaveChanges();
        }

        static void ImportVolunteerDataFromAccessDatabase(SceneCRM context, string fullPathToMdbFile, string databasePassword) {
            AccessVolunteers av = new AccessVolunteers();
            var adapter = new LegacyData.AccessVolunteersTableAdapters.AddressTableAdapter();
            var data = adapter.GetData();
            foreach (LegacyData.AccessVolunteers.AddressRow row in data.Rows) {
                var volunteer = context.Volunteers.FindOrMake(row.First_Name, row.Last_Name);
                Console.WriteLine("Volunteer {0} {1} (#{2})", volunteer.FirstName, volunteer.Surname, volunteer.VolunteerId);
                volunteer.AccessPersonId = row.Person_ID;
                var address = row.IsAddressNull() ? String.Empty : row.Address;
                if (!(row.IsAddress_1Null() || String.IsNullOrWhiteSpace(row.Address_1))) address += Environment.NewLine + row.Address_1;
                if (!(row.IsAddress_2Null() || String.IsNullOrWhiteSpace(row.Address_2))) address += Environment.NewLine + row.Address_2;
                volunteer.PerformanceAttended = "(imported from Access DB)";
                volunteer.Address = address;
                volunteer.AgentName = row.IsAgent_NameNull() ? null : row.Agent_Name;
                volunteer.CvWebUrl = row.IsCVNull() ? null : row.CV;
                volunteer.Deadwood = row.IsDeadwoodNull() ? false : row.Deadwood;
                volunteer.EEDirectDebit = row.Is_E_E_Direct_DebitNull() ? false : row._E_E_Direct_Debit;
                volunteer.EmailAddress = row.IsEmail_AddressNull() ? null : row.Email_Address;
                volunteer.EmailAddress2 = row.IsEmail_Address_2Null() ? null : row.Email_Address_2;
                volunteer.EyesEars = row.Is_Eyes___EarsNull() ? false : row._Eyes___Ears;
                volunteer.MobilePhone = row.IsMobile_telephoneNull() ? null : row.Mobile_telephone;
                volunteer.NoMailout = row.IsNo_MailoutNull() ? false : row.No_Mailout;
                volunteer.Notes = row.IsNotesNull() ? null : row.Notes;
                volunteer.Organisation = row.IsOrganisationNull() ? null : row.Organisation;
                volunteer.OtherProfession = row.IsOther_ProfessionNull() ? null : row.Other_Profession;
                volunteer.PartnerFirstName = row.IsSecond_Person_1st_NameNull() ? null : row.Second_Person_1st_Name;
                volunteer.PartnerSurname = row.IsSecond_Person_Last_NameNull() ? null : row.Second_Person_Last_Name;
                volunteer.Postcode = row.IsPostcodeNull() ? null : row.Postcode;
                volunteer.SpotlightNumber = row.IsSpotlight_NumberNull() ? false : row.Spotlight_Number;
                volunteer.Trustee = row.Is_Trustee_Board_MemberNull() ? false : row._Trustee_Board_Member;
                volunteer.CrbChecks.Clear();
                if (!(row.IsCRB_NumberNull() && row.IsDate_applied_CRBNull() && row.IsCRB_appliedNull() && row.IsCRB_ApprovedNull())) {
                    volunteer.CrbChecks.Add(new CrbCheck() {
                        Volunteer = volunteer,
                        CrbNumber = row.IsCRB_NumberNull() ? null : row.CRB_Number,
                        ApplicationDate = row.IsDate_applied_CRBNull() ? (DateTime?)null : row.Date_applied_CRB,
                        ApplicationSent = row.IsCRB_appliedNull() ? false : row.CRB_applied,
                        Approved = row.IsCRB_ApprovedNull() ? false : row.CRB_Approved,
                        ApprovalDate = row.IsCRB_DateNull() ? (DateTime?)null : row.CRB_Date
                    });
                }
                volunteer.Jobs.Clear();
                AddJob(context, volunteer, ((!(row.IsActorNull())) && row.Actor), Jobs.Actor);
                AddJob(context, volunteer, ((!(row.IsDirectorNull())) && row.Director), Jobs.Director);
                AddJob(context, volunteer, ((!(row.IsTechnicianNull())) && row.Technician), Jobs.Technician);
                AddJob(context, volunteer, ((!(row.Is_Costume_Prop_MakerNull())) && row._Costume_Prop_Maker), Jobs.CostumePropMaker);
            }
        }

        static void AddJob(SceneCRM context, Volunteer volunteer, bool condition, string jobTitle) {
            if (!condition) return;
            var job = context.Jobs.FindOrMake(jobTitle);
            if (!volunteer.Jobs.Contains(job)) {
                Console.WriteLine("Marking {0} as a {1}", volunteer.Name, job.Description);
                volunteer.Jobs.Add(job);
            }
            context.SaveChanges();
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
        static void PopulateJobTypes(SceneCRM context) {
            context.Jobs.FindOrMake(Jobs.Actor);
            context.Jobs.FindOrMake(Jobs.Class);
            context.Jobs.FindOrMake(Jobs.Costume);
            context.Jobs.FindOrMake(Jobs.CostumePropMaker);
            context.Jobs.FindOrMake(Jobs.CourseLeader);
            context.Jobs.FindOrMake(Jobs.Designer);
            context.Jobs.FindOrMake(Jobs.Director);
            context.Jobs.FindOrMake(Jobs.Dramaturg);
            context.Jobs.FindOrMake(Jobs.Mailout);
            context.Jobs.FindOrMake(Jobs.ProdAsst);
            context.Jobs.FindOrMake(Jobs.SoundDesigner);
            context.Jobs.FindOrMake(Jobs.StageMan);
            context.Jobs.FindOrMake(Jobs.TechAsst);
            context.Jobs.FindOrMake(Jobs.Technician);
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

        public static CourseType FindOrMake(this ObjectSet<CourseType> types, string code, string name) {
            var ct = types.FirstOrDefault(t => t.CourseTypeCode == code);
            if (ct == default(CourseType)) {
                ct = new CourseType() {
                    CourseTypeName = name,
                    CourseTypeCode = code
                };
                types.AddObject(ct);
                types.Context.SaveChanges();
            }
            return (ct);
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

        public static Job FindOrMake(this ObjectSet<Job> jobs, string jobTitle) {
            var job = jobs.FirstOrDefault(j => j.Description == jobTitle);
            if (job == default(Job)) {
                job = new Job() { Description = jobTitle };
                jobs.AddObject(job);
                jobs.Context.SaveChanges();
            }
            return (job);
        }

        public static Production FindOrMake(this ObjectSet<Production> productions, string productionTitle) {
            if (String.IsNullOrWhiteSpace(productionTitle)) return (null);
            var production = productions.FirstOrDefault(p => p.Title == productionTitle);
            if (production == null) {
                production = new Production() { Title = productionTitle };
                productions.AddObject(production);
                productions.Context.SaveChanges();
            }
            return (production);
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
        public static Volunteer FindOrMake(this ObjectSet<Volunteer> volunteers, string volunteerName) {
            if (string.IsNullOrWhiteSpace(volunteerName)) return (null);
            string forename, surname;
            var tokens = volunteerName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (tokens.Length == 2) {
                forename = tokens[0];
                surname = tokens[1];
            } else if (tokens.Length > 2) {
                surname = tokens[tokens.Length - 1];
                forename = String.Join(" ", tokens.Take(tokens.Length - 1).ToArray());
            } else {
                surname = volunteerName;
                forename = String.Empty;
            }
            return (volunteers.FindOrMake(forename, surname));
        }

        public static Volunteer FindOrMake(this ObjectSet<Volunteer> volunteers, string forename, string surname) {
            var volunteer = volunteers.FirstOrDefault(v => v.FirstName == forename && v.Surname == surname);
            if (volunteer == default(Volunteer)) {
                volunteer = new Volunteer() {
                    FirstName = forename,
                    Surname = surname,
                };
                volunteers.AddObject(volunteer);
                volunteers.Context.SaveChanges();
            }
            return (volunteer);
        }

    }

    public static class Jobs {
        public const string Dramaturg = "Dramaturg";
        public const string Actor = "Actor";
        public const string Director = "Director";
        public const string Designer = "Designer";
        public const string Technician = "Technician";
        public const string Class = "Class Assistant";
        public const string Mailout = "Mailout";
        public const string StageMan = "Stage Management";
        public const string TechAsst = "Technical Assistant";
        public const string ProdAsst = "Production Assistant";
        public const string SoundDesigner = "Sound Designer";
        public const string Props = "Props Handler";
        public const string CostumePropMaker = "Costume & Prop Maker";
        public const string Costume = "Costume Assistant";
        public const string CourseLeader = "Course Leader";
    }
}
