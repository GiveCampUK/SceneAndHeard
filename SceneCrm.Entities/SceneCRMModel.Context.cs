﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SceneCrm.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SceneCRM : DbContext
    {
    
    	public SceneCRM() : base(GetConnectionString())
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseAttendance> CourseAttendances { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<CourseVolunteer> CourseVolunteers { get; set; }
        public DbSet<CrbCheck> CrbChecks { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<PlayVolunteer> PlayVolunteers { get; set; }
        public DbSet<Production> Productions { get; set; }
        public DbSet<ProductionVolunteer> ProductionVolunteers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<StudentHistory> StudentHistories { get; set; }
        public DbSet<VolunteerCrbCheck> VolunteerCrbChecks { get; set; }
        public DbSet<VolunteerHistory> VolunteerHistories { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
    }
}
