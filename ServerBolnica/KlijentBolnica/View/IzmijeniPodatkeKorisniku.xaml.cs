using KlijentBolnica.ViewModel;
using KlijentBolnica.WindowManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KlijentBolnica.View
{
    /// <summary>
    /// Interaction logic for IzmijeniPodatkeKorisniku.xaml
    /// </summary>
    public partial class IzmijeniPodatkeKorisniku : Window
    {
        public IProzorManager ProzorManager { get; set; }
        public IzmijeniPodatkeKorisniku(IProzorManager prozorManager)
        {
            ProzorManager = prozorManager;
            InitializeComponent();

            IzmijeniPodatkeKorisnikuVM viewModel = new IzmijeniPodatkeKorisnikuVM(ProzorManager);
            DataContext = viewModel;
        }
    }
}
