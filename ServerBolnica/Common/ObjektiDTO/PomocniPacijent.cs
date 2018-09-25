using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.ObjektiDTO
{
    [DataContract]
    public class PomocniPacijent
    {
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public string Prezime { get; set; }
        [DataMember]
        public string Jmbg { get; set; }

         public PomocniPacijent() { }
    }
}
