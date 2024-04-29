using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Models
{
    [Table("reservations")]
    public class Reservation
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Session")]
        [Column("session_id")]
        public int SessionId { get; set; }
        public Session Session { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("total_sum")]
        public int TotalSum { get; set; }

        [Column("reserved")]
        public bool Reserved { get; set; }

        [Column("paid")]
        public bool Paid { get; set; }

        [Column("active")]
        public bool Active { get; set; }
    }
}
