using Common.Enumeracije;
using System.Runtime.Serialization;

namespace Common.ObjektiDTO
{
    [DataContract]
    public class KorisnikDTO
    {
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public string Prezime { get; set; }
        [DataMember]
        public string KorisnickoIme { get; set; }
        [DataMember]
        public TipKorisnika Tip { get; set; } 
    }
}
