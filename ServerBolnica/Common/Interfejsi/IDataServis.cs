using Common.Dodatno;
using Common.Model;
using Common.ObjektiDTO;
using System.Collections.Generic;
using System.ServiceModel;

namespace Common.Interfejsi
{
    [ServiceContract]
    public interface IDataServis
    {
        [OperationContract]
        List<Bolnica> VratiBolnice(Sesija sesija);

        [OperationContract]
        void ObrisiBolnicu(Sesija sesija, int idBolnice);

        [OperationContract]
        Bolnica KreirajBolnicu(Sesija sesija, BolnicaKreirajDTO bolnicaDTO);

        [OperationContract]
        bool IzmijeniBolnicu(Sesija sesija, BolnicaIzmijeniDTO bolnicaIzmijeniDTO);

        [OperationContract]
        Bolnica DuplirajBolnicu(Sesija sesija, int idBolnice);

        [OperationContract]
        int DodajLjekara(Sesija sesija, Ljekar ljekar);

        [OperationContract]
        int DodajPacijenta(Sesija sesija, Pacijent pacijent);

        [OperationContract]
        Ljekar VratiLjekara(Sesija sesija, int idLjekara);

        [OperationContract]
        Pacijent VratiPacijenta(Sesija sesija, int idPacijenta);

        [OperationContract]
        List<Ljekar> VratiLjekare(Sesija sesija);

        [OperationContract]
        List<Pacijent> VratiPacijente(Sesija sesija);

        [OperationContract]
        void ObrisiLjekara(Sesija sesija, int idLjekara);

        [OperationContract]
        void ObrisiPacijenta(Sesija sesija, int idPacijenta);


        
    }
}
