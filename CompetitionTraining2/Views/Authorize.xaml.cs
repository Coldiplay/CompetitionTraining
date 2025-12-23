using API.DB;
using CompetitionTraining2.Model.Tools;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows;

namespace CompetitionTraining2
{
    /// <summary>
    /// Логика взаимодействия для Authorize.xaml
    /// </summary>
    public partial class Authorize : Window
    {
        public string Login {  get; set; }
        private readonly HttpClient client = Helper.Client;

        public Authorize()
        {
            InitializeComponent();
            DataContext = this;
        }

        private async void LogIn(object sender, RoutedEventArgs e)
        {
            //var hash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(passwordBox.Password)));
            var result = await client.GetAsync($"Auth/Authentificate?username={Login}&password={passwordBox.Password}");//{SHA256.HashData(Encoding.UTF8.GetBytes(passwordBox.Password))}");

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string token = await result.Content.ReadAsStringAsync();
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                Helper.User = await client.GetFromJsonAsync<User>($"Auth/GetUser?username={Login}&password={passwordBox.Password}");

                new MainWindow().Show();
                Close();
            }
            else
                MessageBox.Show("Неверные данные");
        }
    }
}
