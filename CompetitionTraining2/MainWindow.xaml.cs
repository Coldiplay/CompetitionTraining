
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace CompetitionTraining2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            /*
            var db = new CompetitionContext();
            foreach (string filepath in Directory.GetFiles("C:\\Users\\student\\Desktop\\Приложение 6. Ресурсы\\Ресурсы\\Import\\users"))
            {
                using (var reader = new FileStream(filepath, FileMode.Open))
                {
                    var user = JsonSerializer.Deserialize<UserImport>(reader);

                    if (db.Users.Any(u => u.Id == user.Id))
                        continue;

                    var userAdd = new User
                    {
                        Fio = user.Fio,
                        Email = user.Email,
                        IsEngineer = user.IsEngineer,
                        Id = user.Id,
                        Image = user.Image,
                        IsManager = user.IsManager,
                        IsOperator = user.IsOperator,
                        Phone = user.Phone,
                    };
                    var role = db.Roles.FirstOrDefault(r => r.Title.Equals(user.Role));
                    if (role is null)
                    {
                        db.Roles.Add(new Role { Title = user.Role });
                        db.SaveChanges();
                        role = db.Roles.FirstOrDefault(r => r.Title.Equals(user.Role));
                        
                    }
                    userAdd.RoleId = role!.Id;

                    db.Users.Add(userAdd);
                }
            }
            var users = db.Users.ToList();
            db.SaveChanges();
            */

           // var db = new CompetitionContext();

            /*
            using (var reader = new FileStream("C:\\Users\\student\\Desktop\\Приложение 6. Ресурсы\\Ресурсы\\Import\\products.json", FileMode.Open))
            {
                var list = JsonSerializer.Deserialize<List<Product>>(reader) ?? [];
                foreach (var product in list)
                {
                    db.Products.Add(product);
                }
                db.SaveChanges();
            }
            */
            /*
            //var list1 = db.Maintenances.ToList();
            //var list2 = db.Users.ToList();
            //var list3 = db.Roles.ToList();
            //var list4 = db.Sales.ToList();
            //var list5 = db.StatusVendingMachines.ToList();
            //var list6 = db.ServicePriorities.ToList();
            //var list7 = db.Companies.ToList();
            //var list8 = db.CriticalThresholdTemplates.ToList();
            //var list9 = db.NotificationTemplates.ToList();

            //var test2 = new List<object>();
            //var test3 = db.GetType().GetProperties();
            //var test = test3.Where(p => p.PropertyType.Name.Contains("DbSet")).ToList();
            //foreach (var dbset in test)
            //{
            //    test2.Add(dbset.GetType().GetMethod("ToList").Invoke(dbset, []));
            //}
            */

            ///*
            //var list = db.VendingMachines.ToList();
            //db.VendingMachines.Add(list.First());
            //db.SaveChanges();
            //*/

            //var password = "12345";
            //var sha = SHA256.Create();
            
            //var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            
            //var hashString = Convert.ToBase64String(hash);



            InitializeComponent();
        }
    }
}