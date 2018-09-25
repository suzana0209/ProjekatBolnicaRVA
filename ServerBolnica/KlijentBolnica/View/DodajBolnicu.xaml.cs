using KlijentBolnica.ViewModel;
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
    /// Interaction logic for DodajBolnicu.xaml
    /// </summary>
    public partial class DodajBolnicu : Window
    {
        public DodajBolnicu(DodajBolnicuVM viewModel)
        {
            InitializeComponent();
            
            viewModel.Roditelj = this;
            DataContext = viewModel;
        }
    }
}
