using System.ComponentModel.DataAnnotations;

namespace HS.Peliculas.Distributed.Service.Models
{
    public class PeliculaDTO
    {
        public int Id { get; set; }
        [Required]

        public string Title { get; set; }

        public string Director { get; set; }

        public int Year { get; set; }

        public string Genres { get; set; }

        public string DescriptionIntro { get; set; }

    }
}
