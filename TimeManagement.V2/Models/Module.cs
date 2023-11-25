using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace TimeManagement.V2.Models
{
    public class StudySessions
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int Hours { get; set; }
        public int ModuleID { get; set; }
        public Module Module { get; set; }
    }

    public class Module : INotifyPropertyChanged
    {
        private static int currentMaxId = 0;

        public int ID { get; private set; }

        public Module()
        {
            this.ID = ++currentMaxId;
        }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int ClassHoursPerWeek { get; set; }
        public int NumWeeks { get; set; }

        public List<StudySessions> StudySessions { get; set; } = new List<StudySessions>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public double SelfStudyHoursPerWeek => CalculateSelfStudyHours();

        private DateTime _date;

        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }
        private double CalculateSelfStudyHours()
        {
            if (NumWeeks == 0)
            {
                return 0;
            }
            return (double)(Credits * 10) / NumWeeks - ClassHoursPerWeek;
        }

        public int TotalHoursSpent
        {
            get
            {
                return StudySessions.Sum(ss => ss.Hours);
            }
        }

        public string LastStudyDate
        {
            get
            {
                var lastSession = StudySessions.OrderByDescending(ss => ss.Date).FirstOrDefault();
                return lastSession?.Date.ToShortDateString() ?? "N/A";
            }
        }

        public void AddStudySession(StudySessions session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session), "The study session cannot be null.");
            }

            StudySessions.Add(session);
            OnPropertyChanged(nameof(TotalHoursSpent));
            OnPropertyChanged(nameof(LastStudyDate));
            OnPropertyChanged(nameof(HoursStudiedThisWeek));
            OnPropertyChanged(nameof(SelfStudyHoursRemainingForWeek));
        }

        public int HoursStudiedThisWeek
        {
            get
            {
                var startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
                var endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);  // till end of Sunday

                return StudySessions
                       .Where(ss => ss.Date >= startOfWeek && ss.Date <= endOfWeek)
                       .Sum(ss => ss.Hours);
            }
        }

        public double SelfStudyHoursRemainingForWeek => SelfStudyHoursPerWeek - HoursStudiedThisWeek;
    }
}

