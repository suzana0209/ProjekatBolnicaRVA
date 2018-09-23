using System.Runtime.Serialization;

namespace Common.Dodatno
{
    [DataContract]
    public class Izuzetak
    {
        [DataMember]
        public string Poruka { get; set; }
        public Izuzetak() { }
       
        public Izuzetak(string poruka)
        {
            Poruka = poruka;
        }
    }
}
