using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cinema.DAL.Models
{
    [Table("movies")]
    public class Movie
    {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("director")]
        public string? Director { get; set; }

        [Column("cast")]
        public string? Cast { get; set; }

        [ForeignKey("Genre")]
        [Column("genre_id")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("duration_min")]
        public int DurationMin { get; set; }

        [Column("release_date")]
        public DateTime ReleaseDate { get; set; }

        [Column("price")]
        public int Price { get; set; }


        /*
        public int GenreId { get; set; }
        [ForeignKey(nameof(CategoryId))]    
        public Category Category { get; set; }
        */

    }
}
