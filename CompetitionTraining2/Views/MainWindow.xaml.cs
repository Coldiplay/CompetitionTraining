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
        private static readonly JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.Preserve,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public static string? UserName => Helper.User?.Last_name_fn_p;
        public static string? UserRole => Helper.User?.Role?.Title;
        public static byte[] Image => Convert.FromBase64String(Helper.User.Image?.Split(',').MaxBy(s => s.Length) ?? string.Empty);
        private static DateTime Now => DateTime.Now;
        public static string SalesText => $"Данные по продажам с {Now.AddDays(-10).ToShortDateString()} по {Now.AddDays(-1).ToShortDateString()}";

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



        public event PropertyChangedEventHandler? PropertyChanged;
        protected void Signal([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        private Visibility reportsVis = Visibility.Collapsed;
        private Visibility accountingVis = Visibility.Collapsed;
        private Visibility administrationVis = Visibility.Collapsed;
        private PlotModel? sales;
        private Visibility effeciencyBlock;
        private Visibility networkStatusBlock;
        private Visibility summaryBlock;
        private Visibility salesModelBlock;
        private Visibility newsBlock;
        private Visibility menuVis;

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
        public Visibility EffeciencyBlock
        {
            get => effeciencyBlock;
            set
            {
                effeciencyBlock = value;
                Signal();
            }
        }
        public Visibility NetworkStatusBlock
        {
            get => networkStatusBlock;
            set
            {
                networkStatusBlock = value;
                Signal();
            }
        }
        public Visibility SummaryBlock
        {
            get => summaryBlock; set
            {
                summaryBlock = value;
                Signal();
            }
        }
        public Visibility SalesModelBlock
        {
            get => salesModelBlock;
            set
            {
                salesModelBlock = value;
                Signal();
            }
        }
        public Visibility NewsBlock
        {
            get => newsBlock;
            set
            {
                newsBlock = value;
                Signal();
            }
        }
        public Visibility MenuVis
        {
            get => menuVis;
            set
            {
                menuVis = value;
                Signal();
            }
        }

        public PlotModel? SalesModel
        {
            get => sales;
            set
            {
                sales = value;
                Signal();
            }
        }

        private List<VendingMachine> VendingMachines { get; set; }
        private List<VendingMachine> UsersMachinesFromFranchise { get; set; }
        private List<VendingMachine> AllMachinesFromFranchise { get; set; }
        private List<Sale> SalesList { get; set; }
        private List<Maintenance> Maintenances { get; set; }
        private List<Company> Companies { get; set; }
        private int? SelectedCompanyId { get; set; }
        public uint MaintenancesToday => GetMaintenancesCount(Now);
        public uint MaintenancesYesterday => GetMaintenancesCount(Now.AddDays(-1));

        public string RevenueToday => GetRevenue(Now);
        public string RevenueYesterday => GetRevenue(Now.AddDays(-1));
        public string CompanyName => Companies?.FirstOrDefault(c => c.Id.Equals(SelectedCompanyId))?.Name ?? string.Empty;


        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Load();
        }

        private async void Load()
        {
            await LoadVendingMachines();

            await LoadCompanies();
            LoadFranchiseMachines();
            await LoadSales();
            await LoadMainteince();

            LoadModelSales(bySumm: false);
        }

        private async Task LoadVendingMachines()
        {
            AllMachinesFromFranchise = (await GetFromJson<List<VendingMachine>>("VendingMachines")) ?? [];
            VendingMachines = [.. AllMachinesFromFranchise.Where(m => m.UserId == Helper.User.Id) ?? []];
        }
        private void LoadFranchiseMachines()
        {
            AllMachinesFromFranchise = [..AllMachinesFromFranchise.Where(m => m.CreatorCompanyId.Equals(SelectedCompanyId))];
            UsersMachinesFromFranchise = [.. VendingMachines.Where(m => m.CreatorCompanyId.Equals(SelectedCompanyId))];
        }
        private async Task LoadCompanies()
        {
            var listFranchise = VendingMachines.Select(s => s.CreatorCompanyId).ToList();
            Companies = [..(await GetFromJson<List<Company>>("Companies") ?? []).Where(c => listFranchise.Any(f => f.Equals(c.Id)))];
            SelectedCompanyId = Companies.FirstOrDefault()?.Id;
        }
        private async Task LoadSales()
        {
            SalesList = [.. ((await GetFromJson<List<Sale>>("Sales")) ?? []).Where(s => VendingMachines.Any(vm => vm.Id.Equals(s.VendingMachineId)))];
            Signal(nameof(RevenueToday));
            Signal(nameof(RevenueYesterday));
        }
        public async Task LoadMainteince()
        {
            Maintenances = [.. ((await GetFromJson<List<Maintenance>>("Maintenances")) ?? []).Where(m => VendingMachines.Any(vm => vm.Id.Equals(m.VendingMachineId)))];
        }
        private void LoadModelSales(ushort days = 10, bool bySumm = true)
        {
            List<Sale> list = [.. SalesList ?? []];

            //var notBefore = list?.MaxBy(s => s.Timestamp)?.Timestamp.AddDays(-days) ?? DateTime.UtcNow;
            var notBefore = Now.AddDays(-days);

            list = list?
                    .Where(s => s.Timestamp >= notBefore)
                    .OrderByDescending(s => s.Timestamp)
                    .ToList() ?? [];

            PlotModel model = new();

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

            for (var i = 0; i < days; i++)
            {
                var daydate = notBefore.AddDays(i).Date;

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

            SalesModel = model;


            GC.Collect();
        }
        public uint GetMaintenancesCount(DateTime date) => Maintenances is null ? 0 : (uint)Maintenances.Where(m => m.Date.Date == date.Date).Count();
        public string GetRevenue(DateTime date) => $"{SalesList?.Where(s => s.Timestamp.Date == date.Date).Sum(s => s.TotalCost)} р.";
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

        private void ChangeSalesModelToSumm(object sender, RoutedEventArgs e) =>
            LoadModelSales(bySumm: true);

        private void ChangeSalesModelToCount(object sender, RoutedEventArgs e) =>
            LoadModelSales(bySumm: false);

        private void HideEfficiency(object sender, RoutedEventArgs e) =>
            EffeciencyBlock = EffeciencyBlock == Visibility.Visible ? Visibility.Collapsed: Visibility.Visible;
        private void HideNetworkStatus(object sender, RoutedEventArgs e) =>
            NetworkStatusBlock = NetworkStatusBlock == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        private void HideSummary(object sender, RoutedEventArgs e) =>
            SummaryBlock = SummaryBlock == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        private void HideSalesModel(object sender, RoutedEventArgs e) =>
            SalesModelBlock = SalesModelBlock == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        private void HideNews(object sender, RoutedEventArgs e) =>
            NewsBlock = NewsBlock == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

        private void ChangeCompany(object sender, RoutedEventArgs e)
        {
            if (SelectedCompanyId is not null)
            {
                var list = Companies.Select(c => c.Id).ToList();
                list.Remove((int)SelectedCompanyId);
                SelectedCompanyId = list.FirstOrDefault();
                return;
            }

            SelectedCompanyId = Companies.FirstOrDefault()?.Id;
        }

        private void ChangeMenuVis(object sender, RoutedEventArgs e) =>
            MenuVis = MenuVis == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
    }
}