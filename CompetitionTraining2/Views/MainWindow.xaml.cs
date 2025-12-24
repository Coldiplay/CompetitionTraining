using CompetitionTraining2.Model.Classes;
using CompetitionTraining2.Model.Tools;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.ComponentModel;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        private PlotModel? sales;

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
        public static string? UserName => Helper.User?.Fio;
        public static string? UserRole => Helper.User?.Role?.Title;


        public PlotModel? Sales
        {
            get => sales;
            set
            {
                sales = value;
                Signal();
            }
        }
        /*
        public PlotModel? Test
        {
            get => test;
            set
            {
                test = value;
                Signal();
            }
        }
        */

        private List<VendingMachine>? VendingMachines { get; set; }
        private List<Sale>? SalesList { get; set; }


        public static string SalesText => $"Данные по продажам с {DateTime.Now.AddDays(-10).ToShortDateString()} по {DateTime.Now.AddDays(-1).ToShortDateString()}";


        public string RevenueToday => GetRevenue(DateTime.Now);
        public string RevenueYesterday => GetRevenue(DateTime.Now.AddDays(-1));

        public string GetRevenue(DateTime date) => $"{SalesList?.Where(s => s.Timestamp.Date == date.Date).Sum(s => s.TotalCost)} р.";


        //public string Test { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Load();
        }
        private async void Load()
        {
            
            await LoadVendingMachines();

            await LoadSales();
            await LoadModelSales(bySumm: false);
        }

        private async Task LoadVendingMachines()
        {
            VendingMachines = [.. (await GetFromJson<List<VendingMachine>>("VendingMachines"))?.Where(m => m.UserId == Helper.User.Id) ?? []];
        }
        //public string MoneyInTA 
        private async Task LoadSales()
        {
            SalesList = [.. await GetFromJson<List<Sale>>("Sales")];
            Signal(nameof(RevenueToday));
            Signal(nameof(RevenueYesterday));
        }
        public async void LoadMainteince()
        {
            Maintenances = [.. (await GetFromJson<List<Maintenance>>("Maintenances")).Where(m => VendingMachines.Any(vm => vm.Id.Equals(m.VendingMachineId)))];

        }
        public uint MaintenancesToday => GetMaintenancesCount(DateTime.Now);
        public uint MaintenancesYesterday => GetMaintenancesCount(DateTime.Now.AddDays(-1));
        public uint GetMaintenancesCount(DateTime date) => (uint)Maintenances?.Where(m => m.Date.Date == date.Date)?.Count();
        public List<Maintenance>? Maintenances { get; set; }
        


        private static readonly JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static async Task<T?> GetFromJson<T>(string requestUrl) where T : notnull
        {
            var httpResponse = await Helper.Client.GetAsync(requestUrl);
            try
            {
                var test = await httpResponse.Content.ReadAsStringAsync();
                return await httpResponse.Content.ReadFromJsonAsync<T>(options);
            }
            catch (Exception)
            {
                return default;
            }
        }


        private async Task LoadModelSales(ushort days = 10, bool bySumm = true)
        {
            List<Sale> list = [.. SalesList ?? []];

            //var notBefore = list?.MaxBy(s => s.Timestamp)?.Timestamp.AddDays(-days) ?? DateTime.UtcNow;
            var notBefore = DateTime.Now.AddDays(-(days));

            list = list?
                    .Where(s => s.Timestamp >= notBefore)
                    .OrderByDescending(s => s.Timestamp)
                    .ToList() ?? [];

            PlotModel model = new() 
            {
                Title = "Test"
            };

            var categoryAxis = new CategoryAxis
            {
                Key = "Y",
                IsZoomEnabled = false,
               
            };
            
            model.Axes.Add(new LinearAxis 
            { 
                Key = "X", 
                AbsoluteMinimum = 0,
                IsZoomEnabled = false
            });

            var barSeries = new BarSeries
            {
                XAxisKey = "X",
                YAxisKey = "Y",
            };

            for (var i = 0; i <= days; i++)
            {
                var daydate = notBefore.AddDays(i-1).Date;

                categoryAxis.ActualLabels.Add($"{daydate.DayOfWeek}\n{daydate.Day}.{daydate.Month}");

                barSeries.Items.Add(new BarItem(
                    bySumm ?
                    (double)list.Where(l => l.Timestamp.Date == daydate).Sum(s => s.TotalCost)
                    :
                    list.Where(l => l.Timestamp.Date == daydate).Count()
                    ));
                
            }

            model.Axes.Add(categoryAxis);
            model.Series.Add(barSeries);

            Sales = model;


            GC.Collect();
        }
        /*
        private async void TestModel()
        {
            //var options = new JsonSerializerOptions()
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //};
            //var list = await Helper.Client.GetFromJsonAsync<IEnumerable<Sale>>("Sales", options);


            var model = new PlotModel();
            model.Axes.Add(new CategoryAxis() { Key = "Y"});
            model.Axes.Add(new LinearAxis { Key = "X" });


            for (var i = 0; i < 4; i++)
            {
                var series = new BarSeries();
                series.ActualItems.Add(new BarItem(50));
                series.Title = $"Test series {i}";
                model.Series.Add(series);
                series.XAxisKey = "X";
                series.YAxisKey = "Y";
            }
            



            Test = model;
        }
        */



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