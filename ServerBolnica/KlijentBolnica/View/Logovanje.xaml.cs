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
    /// Interaction logic for Logovanje.xaml
    /// </summary>
    public partial class Logovanje : Window
    {
        LogovanjeVM viewModel;
        public Logovanje()
        {
            object justToInit = LogVM.Instance;

            InitializeComponent();
            IProzorManager windowManager = new ProzorManager(this);

            viewModel = new LogovanjeVM(windowManager);
            base.DataContext = viewModel;
            //this.Close();

            //InitializeComponent();
        }

        private void ExitDugme_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
