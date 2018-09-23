using System.Runtime.Serialization;

namespace Common.Dodatno
{
    [DataContract]
    public class Sesija
    {
        [DataMember]
        public string IdSesije { get; set; }
    }
}
