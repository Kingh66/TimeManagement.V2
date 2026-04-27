using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TimeManagement.V2.Models
{
    public class StudySessions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int Hours { get; set; }

        public int ModuleID { get; set; }

        [ForeignKey("ModuleID")]
        public Module Module { get; set; }
    }

    public class Module
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Credits { get; set; }

        public int ClassHoursPerWeek { get; set; }

        public int NumWeeks { get; set; }

        public DateTime Date { get; set; }

        // EF Core will create a real table for this
        public List<StudySessions> StudySessions { get; set; } = new List<StudySessions>();

        // --- THE FOLLOWING PROPERTIES ARE IGNORED BY THE DATABASE ---
        // [NotMapped] means SQL Server won't create columns for these.
        // They are calculated in C# when you load the page.

        [NotMapped]
        public double SelfStudyHoursPerWeek => CalculateSelfStudyHours();

        [NotMapped]
        public int TotalHoursSpent
        {
            get
            {
                return StudySessions.Sum(ss => ss.Hours);
            }
        }

        [NotMapped]
        public string LastStudyDate
        {
            get
            {
                var lastSession = StudySessions.OrderByDescending(ss => ss.Date).FirstOrDefault();
                return lastSession?.Date.ToShortDateString() ?? "N/A";
            }
        }

        [NotMapped]
        public int HoursStudiedThisWeek
        {
            get
            {
                var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                var endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);

                return StudySessions
                       .Where(ss => ss.Date >= startOfWeek && ss.Date <= endOfWeek)
                       .Sum(ss => ss.Hours);
            }
        }

        [NotMapped]
        public double SelfStudyHoursRemainingForWeek => SelfStudyHoursPerWeek - HoursStudiedThisWeek;

        // --- HELPER METHODS ---

        private double CalculateSelfStudyHours()
        {
            if (NumWeeks == 0)
            {
                return 0;
            }
            return (double)(Credits * 10) / NumWeeks - ClassHoursPerWeek;
        }

        public void AddStudySession(StudySessions session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session), "The study session cannot be null.");
            }

            StudySessions.Add(session);
        }
    }
}