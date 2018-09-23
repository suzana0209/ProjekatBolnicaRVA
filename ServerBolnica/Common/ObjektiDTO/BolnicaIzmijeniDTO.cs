using Common.Enumeracije;
using Common.Model;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.ObjektiDTO
{
    [DataContract]
    public class BolnicaIzmijeniDTO
    {
        [DataMember]
        public int IdBolnice { get; set; }
        [DataMember]
        public int NoviBrojLjekara { get; set; }
        [DataMember]
        public int NoviBrojOdjeljenja { get; set; }
        [DataMember]
        public string NoviNazivBolnice { get; set; }
        [DataMember]
        public VrstaBolnice NovaVrstaBol { get; set; }
        [DataMember]
        public int Verzija { get; set; }
        [DataMember]
        public List<Ljekar> NovaListaLjekara { get; set; }
        [DataMember]
        public List<Pacijent> NovaListaPacijenata { get; set; }
        [DataMember]
        public bool Azurirano { get; set; }
    }
}
