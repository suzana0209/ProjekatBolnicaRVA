using Common.Dodatno;
using System.ServiceModel;

namespace Common.Interfejsi
{
    [ServiceContract]
    public interface ILogovanjeServis
    {
        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        Sesija PrijaviSe(KorisnikZaLogovanje korisnik);

        [OperationContract]
        //[FaultContract(typeof(Izuzetak))]
        bool PostojiUBaziKorisnik(KorisnikZaLogovanje korisnik);

        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        void OdjaviSe(Sesija sesija);
    }
}
