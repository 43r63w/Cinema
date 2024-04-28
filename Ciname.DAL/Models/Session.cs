using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Models
{
    [Table("sessions")]
    public class Session
    {
        [Key]
        [Column("session_id")]
        public int SessionID { get; set; }

        [Column("film_id")]
        public int FilmID { get; set; }

        [Column("date_time")]
        public DateTime DateTime { get; set; }

        [Column("ticket_price")]
        public decimal TicketPrice { get; set; }

        [Column("hall")]
        public string Hall { get; set; } 

        [Column("language")]
       
        public bool Is3D { get; set; } 

        [Column("is_big_hall")]
        public bool IsBigHall { get; set; } 

    }
}
