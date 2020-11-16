using SQLite;
using System;
using System.Collections.ObjectModel;

namespace LAP1WGUApp
{
    [Table("courses")]
    public class Course
    {
        [PrimaryKey, Column("courseID")]
        public int CourseID { get; set; }

        [Column("termID")]
        public int TermID { get; set; }

        [MaxLength(64), Column("courseName")]
        public string CourseName { get; set; }

        [Column("startDate")]
        public DateTime StartDate { get; set; }

        [Column("endDate")]
        public DateTime EndDate { get; set; }

        [Column("courseStatus")]
        public string CourseStatus { get; set; }

        [Column("DetailedCourseView")]
        public string DetailedCourseView { get; set; }

        [Column("CourseNotes")]
        public string CourseNotes { get; set; }

        public string CourseDate { get { return StartDate.ToString("MMM yyyy") + " - " + EndDate.ToString("MMM yyyy"); } }

        public CourseInstructor Instructor = new CourseInstructor();

        public ObservableCollection<Assessment> Assessments = new ObservableCollection<Assessment>();

        public Course(int courseID, int termID, string courseName, DateTime startDate, DateTime endDate, string courseStatus, string detailedCourseView, string courseNotes, CourseInstructor instructor, ObservableCollection<Assessment> assessments)
        {
            CourseID = courseID;
            TermID = termID;
            CourseName = courseName;
            StartDate = startDate;
            EndDate = endDate;
            CourseStatus = courseStatus;
            DetailedCourseView = detailedCourseView;
            CourseNotes = courseNotes;
            Instructor = instructor;
            Assessments = assessments;
        }

        public Course()
        {

        }
    }
}
