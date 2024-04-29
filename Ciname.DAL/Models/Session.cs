using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Models
{
    [Table("sessions")]
    public class Session
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Movie")]
        [Column("movie_id")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("CinemaRoom")]
        [Column("room_id")]
        public int RoomId { get; set; }
        public CinemaRoom CinemaRoom { get; set; }

        [Column("session_time")]
        public DateTime SessionTime { get; set; }
    }
}
