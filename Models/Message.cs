using System.ComponentModel.DataAnnotations;

namespace RPGoostBook.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime DatePosted { get; set; }
    }
}
