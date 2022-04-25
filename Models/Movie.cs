using System.ComponentModel.DataAnnotations;

namespace MovieWebApp.Models
{
    public class Movie
    {
        //some Data annotations
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime? ReleaseDatte { get; set; }
        public string Genre { get; set; }
        public decimal? Price { get; set; }
    }
}