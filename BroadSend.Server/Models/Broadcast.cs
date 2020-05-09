using System.ComponentModel.DataAnnotations;

namespace BroadSend.Server.Models
{
    public class Broadcast
    {
        public int Id { get; set; }

        [Required]
        [StringLength(64)]
        public string Title { get; set; }

        [Required]
        [StringLength(2048)]
        public string Anons { get; set; }

        [Required]
        public ulong DateAired { get; set; }

        [Required]
        public ulong DateAiredEnd { get; set; }

        [Required]
        [StringLength(512)]
        public string Vendor { get; set; }

        [Required]
        [StringLength(2048)]
        public string Author { get; set; }

        [Required]
        [StringLength(512)]
        public string Composer { get; set; }

        [Required]
        [StringLength(256)]
        public string Director { get; set; }

        [Required]
        [StringLength(2048)]
        public string Fragment { get; set; }

        [Required]
        [StringLength(2048)]
        public string Presenter { get; set; }

        [Required]
        [StringLength(2048)]
        public string Guest { get; set; }

        [Required]
        [StringLength(256)]
        public string Country { get; set; }

        [Required]
        [StringLength(64)]
        public string Language { get; set; }

        [Required]
        [StringLength(512)]
        public string FileName { get; set; }

        [Required]
        [StringLength(64)]
        public string FileSHA256 { get; set; }

        [Required]
        public bool Transmitted { get; set; }

        [Required]
        public bool Completed { get; set; }
        
    }
}
