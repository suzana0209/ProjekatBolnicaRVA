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
    /// Interaction logic for Pacijenti.xaml
    /// </summary>
    public partial class Pacijenti : Window
    {
        public Pacijenti(IProzorManager prozorManager)
        {
            InitializeComponent();
            PacijentiVM viewModel = new PacijentiVM(prozorManager);
            DataContext = viewModel;

        }
    }
}
