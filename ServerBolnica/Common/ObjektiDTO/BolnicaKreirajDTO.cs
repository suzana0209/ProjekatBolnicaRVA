using Common.Enumeracije;
using System.Runtime.Serialization;

namespace Common.ObjektiDTO
{
    [DataContract]
    public class BolnicaKreirajDTO
    {
        [DataMember]
        public int BrojLjekara { get; set; }
        [DataMember]
        public int BrojOdjeljenja { get; set; }
        [DataMember]
        public string NazivBolnice { get; set; }
        [DataMember]
        public VrstaBolnice VrstaBol { get; set; }
    }
}
