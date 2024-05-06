using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("movie")]
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

   
    [Column("genre_id")]
    public int? GenreId { get; set; } 
    [ForeignKey(nameof(GenreId))]  
    public Genre? Genre { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("duration_time")]
    public int DurationTime { get; set; }

    [Column("release_date")]
    public DateTime ReleaseDate { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

 
}
