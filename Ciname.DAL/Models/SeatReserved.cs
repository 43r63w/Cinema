using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Models
{
    [Table("seat_reserved")]
    public class SeatReserved
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("number_of_seat")]
        public int NumberOfSeat { get; set; }

        [ForeignKey("Reservation")]
        [Column("reservation_id")]
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        [ForeignKey("Session")]
        [Column("session_id")]
        public int SessionId { get; set; }
        public Session Session { get; set; }
    }
}
