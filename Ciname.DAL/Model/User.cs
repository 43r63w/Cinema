using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Model
{
    [Table(name:"users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
