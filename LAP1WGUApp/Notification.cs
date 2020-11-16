using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace LAP1WGUApp
{
    [Table("Notifications")]
   public  class Notification
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("body")]
        public string Body { get; set; }

        [Column("dt")]
        public DateTime Dt { get; set; }

        [Column("ts")]
        public TimeSpan Ts { get; set; }


        public Notification(string title, string body, int id, TimeSpan ts, DateTime dt)
        {
            Title = title;
            Body = body;
            ID = id;
            Dt = dt;
            Ts = ts;
        }
        public Notification()
        {

        }
    }
}
