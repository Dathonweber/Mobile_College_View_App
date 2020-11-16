using SQLite;
using System;

namespace LAP1WGUApp
{
    [Table("assessment")]
    public class Assessment
    {
        [PrimaryKey, Column("assessmentID")]
        public int AssessmentID { get; set; }

        [Column("courseID")]
        public int CourseID { get; set; }
        [Column("name")]
        public string AssessmentName { get; set; }
        [Column("date")]
        public DateTime AssessmentDate { get; set; }
        [Column("time")]
        public DateTime AssessmentTime { get; set; }
        [Column("info")]
        public string AssessmentInfo { get; set; }
        [Column("type")]
        public string AssessmentType { get; set; }

        public Assessment(int assessmentID, int courseID, string assessmentName, DateTime assessmentDate, DateTime assessmentTime, string assessmentInfo, string assessmentType)
        {
            AssessmentID = assessmentID;
            CourseID = courseID;
            AssessmentName = assessmentName;
            AssessmentDate = assessmentDate;
            AssessmentTime = assessmentTime;
            AssessmentInfo = assessmentInfo;
            AssessmentType = assessmentType;
        }

        public Assessment()
        {

        }
    }
}
