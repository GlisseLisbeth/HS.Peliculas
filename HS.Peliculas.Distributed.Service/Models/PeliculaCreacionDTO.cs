using System.ComponentModel.DataAnnotations;

namespace HS.Peliculas.Distributed.Service.Models
{
    public class PeliculaCreacionDTO
    {
        [Required]
        public string Url { get; set; }

        public string Title { get; set; }

        public string TitleEnglish { get; set; }

        public string TitleLong { get; set; }

        public string Director { get; set; }

        public string Slug { get; set; }

        public int Year { get; set; }

        public float Rating { get; set; }

        public int RunTime { get; set; }

        public string Genres { get; set; }

        public string DescriptionIntro { get; set; }

        public string DescriptionFull { get; set; }

        public string Language { get; set; }

        public string UrlImage { get; set; }
    }
}
