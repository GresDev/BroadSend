using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Date { get; set; }
        public Director Interval01 { get; set; }
        public Director Interval02 { get; set; }
        public Director Interval03 { get; set; }
        public Director Interval04 { get; set; }

    }
}

