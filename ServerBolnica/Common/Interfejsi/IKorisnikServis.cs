using Common.Dodatno;
using Common.ObjektiDTO;
using System.ServiceModel;

namespace Common.Interfejsi
{
    [ServiceContract]
    public interface IKorisnikServis
    {
        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        KorisnikDTO VratiInfoKorisnika(Sesija sesija);       

        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        void IzmijeniInfoKorisnika(Sesija sesija, KorisnikDTO korisnik);

        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        void DodajKorisnika(Sesija sesija, Korisnik korisnik);

        [OperationContract]
        [FaultContract(typeof(Izuzetak))]
        void PromijeniLozinku(Sesija sesija, string lozinka);
    }
}
