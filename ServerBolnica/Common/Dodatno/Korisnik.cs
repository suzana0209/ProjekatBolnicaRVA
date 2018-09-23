using Common.Enumeracije;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Common.Dodatno
{
    [DataContract]
    public class Korisnik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int IdKorisnika { get; set; }
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public string Prezime { get; set; }
        [DataMember]
        public string KorisnickoIme { get; set; }
        [DataMember]
        public string Lozinka { get; set; }
        [DataMember]
        public TipKorisnika Tip { get; set; }

        public Korisnik()
        {
        }

        public Korisnik(string ime, string prezime, string korisnickoIme, string lozinka, TipKorisnika tip)
        {
            Ime = ime;
            Prezime = prezime;
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
            Tip = tip;
        }
    }
}
