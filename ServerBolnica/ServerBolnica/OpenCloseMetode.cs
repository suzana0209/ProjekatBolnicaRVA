using ServerBolnica.Servisi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ServerBolnica
{
    public class OpenCloseMetode
    {
        private ServiceHost logovanjeSvc;
        private ServiceHost dataServisSvc;
        private ServiceHost korisnikServisSvc;

        public OpenCloseMetode() { }

        public void Open()
        {
            logovanjeSvc = new ServiceHost(typeof(LogovanjeServis));
            dataServisSvc = new ServiceHost(typeof(DataServis));
            korisnikServisSvc = new ServiceHost(typeof(KorisnikServis));

            logovanjeSvc.Open();
            dataServisSvc.Open();
            korisnikServisSvc.Open();
        }

        public void Close()
        {
            logovanjeSvc.Close();
            dataServisSvc.Close();
            korisnikServisSvc.Close();
        }
    }
}
