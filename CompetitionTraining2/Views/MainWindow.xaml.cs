using CompetitionTraining2.Model.Tools;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace CompetitionTraining2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void Signal([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private Visibility reportsVis = Visibility.Collapsed;
        private Visibility accountingVis = Visibility.Collapsed;
        private Visibility administrationVis = Visibility.Collapsed;

        public Visibility ReportsVis
        {
            get => reportsVis;
            set
            {
                reportsVis = value;
                Signal();
            }
        }
        public Visibility AccountingVis
        {
            get => accountingVis;
            set
            {
                accountingVis = value;
                Signal();
            }
        }
        public Visibility AdministrationVis { get => administrationVis; set {
                administrationVis = value;
                Signal();
            } }

        public static string CompanyName => Config.CompanyName;
        


        public string Test { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }



        private void ReportsVisChange(object sender, RoutedEventArgs e)
        {
            ReportsVis = ReportsVis == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void AccountingVisChange(object sender, RoutedEventArgs e)
        {
            AccountingVis = AccountingVis == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void AdministrationVisChange(object sender, RoutedEventArgs e)
        {
            AdministrationVis = AdministrationVis == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void MainPageShow(object sender, RoutedEventArgs e)
        {

        }
        private void MonitorVendMachinesShow(object sender, RoutedEventArgs e)
        {

        }
    }
}