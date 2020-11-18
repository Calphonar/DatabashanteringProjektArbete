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
using System.Windows.Navigation;
using System.Windows.Shapes;
using DatabaseConnection;

namespace Store
{
    /// <summary>
    /// Interaction logic for CreateAccountWindow.xaml
    /// </summary>
    public partial class CreateAccountWindow : Window
    {
        public CreateAccountWindow()
        {
            InitializeComponent();
        }

        private void CreateNewAccountClick(object sender, RoutedEventArgs e)
        {
            using (var ctx = new Context())
            {
                State.User = API.GetCustomerByName(Name.Text);
                if (State.User != null)
                {
                    MessageBox.Show("Username already taken.", "Account creation Failed!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Name.Text = "...";
                    Password.Text = "...";
                    Email.Text = "...";
                    Age.Text = "...";
                }
                else
                {
                ctx.Customers.Add(new Customer { Name = Name.Text, Password = Password.Text, Email = Email.Text, Age = Convert.ToInt32(Age.Text) });
                ctx.SaveChanges();

                MessageBox.Show("Account successful creation.", "Account creation Succeeded!", MessageBoxButton.OK, MessageBoxImage.Information);
                var next_window = new LoginWindow();
                next_window.Show();
                this.Close();
                }
            }
        }
    }
}
