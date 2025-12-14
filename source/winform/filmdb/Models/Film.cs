
namespace filmdb.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Nazev { get; set; }
        public int Rok { get; set; }
        public string Zanr { get; set; }
        public string Reziser { get; set; }
        public decimal? Hodnoceni { get; set; }
        public string Popis { get; set; }
        public bool Schvaleno { get; set; }
        public string Autor { get; set; }
        public string Poster { get; set; }
    }
}
