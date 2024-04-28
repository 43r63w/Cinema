using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Models
{
    [Table("bookings")]
    public class Booking
    {
        [Key]
        [Column("booking_id")]
        public int BookingID { get; set; }

        [Column("user_id")]
        public int UserID { get; set; }

        [Column("session_id")]
        public int SessionID { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }
    }
}
