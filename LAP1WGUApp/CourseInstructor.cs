using SQLite;

namespace LAP1WGUApp
{
    [Table("courseInstructor")]
    public class CourseInstructor
    {
        [PrimaryKey, AutoIncrement , Column("instructorID")]
        public int InstructorID { get; set; }
        [Column("courseID")]
        public int CourseID { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("phoneNumber")]
        public string PhoneNumber { get; set; }
        [Column("email")]
        public string Email { get; set; }

        public CourseInstructor(int instructorID, int courseID, string name, string phoneNumber, string email)
        {
            InstructorID = instructorID;
            CourseID = courseID;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public CourseInstructor()
        {

        }

        public CourseInstructor (int courseID, string name, string phoneNumber, string email )
        {           
            CourseID = courseID;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
        }
    }
}
