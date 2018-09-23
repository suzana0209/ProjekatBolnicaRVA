using System.Runtime.Serialization;

namespace Common.Dodatno
{
    [DataContract]
    public class KorisnikZaLogovanje
    {
        [DataMember]
        public string KorisnickoIme { get; set; }
        [DataMember]
        public string Lozinka { get; set; }

        public KorisnikZaLogovanje()
        {
        }

        public KorisnikZaLogovanje(string korisnickoIme, string lozinka)
        {
            KorisnickoIme = korisnickoIme;
            Lozinka = lozinka;
        }
    }
}
