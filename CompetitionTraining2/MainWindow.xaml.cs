using CompetitionTraining2.DB;
using CompetitionTraining2.Model;
using System.IO;
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



            InitializeComponent();
        }
    }
}