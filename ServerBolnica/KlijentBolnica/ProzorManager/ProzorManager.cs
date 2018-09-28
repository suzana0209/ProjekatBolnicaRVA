using KlijentBolnica.View;
using System;
using System.Collections.Generic;
using System.Windows;

namespace KlijentBolnica.WindowManager
{
    public class ProzorManager : IProzorManager
    {
        Stack<Window> stek = new Stack<Window>();

        public ProzorManager(Window roditelj)
        {
            stek.Push(roditelj);
        }

        public void PrikaziStranu(StanjeProzora sledecaStrana)
        {
            switch (sledecaStrana)
            {
                case StanjeProzora.Pocetna:
                    PrikaziSledecuStranu(new Pocetna(this));
                    break;

                case StanjeProzora.DodajKorisnika:
                    PrikaziSledecuStranu(new DodajKorisnika(this));
                    break;

                case StanjeProzora.IzmijeniPodatkeKorisniku:
                    PrikaziSledecuStranu(new IzmijeniPodatkeKorisniku(this));
                    break;

                case StanjeProzora.Bolnice:
                    PrikaziSledecuStranu(new Bolnice(this));
                    break;

                case StanjeProzora.Ljekari:
                    PrikaziSledecuStranu(new Ljekari(this));
                    break;
                case StanjeProzora.Pacijenti:
                    PrikaziSledecuStranu(new Pacijenti(this));
                    break;
                
            }
        }

        private void PrikaziSledecuStranu(Window sledecaStrana)
        {
            stek.Peek().Visibility = Visibility.Hidden;
            sledecaStrana.Closed += TopWindowClosed;
            stek.Push(sledecaStrana);
            sledecaStrana.Show();
        }

        private void TopWindowClosed(object sender, EventArgs e)
        {
            stek.Pop().Closed -= TopWindowClosed;
            stek.Peek().Show();
        }

        public void PrethodnaStrana()
        {
            Window top = stek.Pop();
            top.Closed -= TopWindowClosed;
            top.Close();
            stek.Peek().Show();
        }
    }
}
