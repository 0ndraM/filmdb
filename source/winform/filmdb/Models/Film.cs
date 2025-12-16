
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

    public class LoginResponse
    {
        public bool success { get; set; }
        public string username { get; set; }
        public string role { get; set; } // NOVÉ: Pole pro uložení role
        public string token { get; set; }
        public string message { get; set; }
    }

    // Mùže být použita pro všechny jednoduché odpovìdi
    public class SimpleApiResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string new_username { get; set; } // Pro zmìnu jména
    }
}
