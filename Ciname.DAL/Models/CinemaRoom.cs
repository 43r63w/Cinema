using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Models
{
    [Table("cinema_room")]
    public class CinemaRoom
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("rows")]
        public int Rows { get; set; }

        [Column("amount_of_seats")]
        public int AmountOfSeats { get; set; }
    }
}
