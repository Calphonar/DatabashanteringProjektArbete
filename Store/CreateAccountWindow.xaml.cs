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
                try
                {
                    if (Password.Password == Password2.Password)
                    {
                        State.User = API.GetCustomerByName(Name.Text);
                        if (State.User != null)
                        {
                            MessageBox.Show("Username already taken.", "Account creation Failed!", MessageBoxButton.OK, MessageBoxImage.Information);
                            Name.Text = "...";
                            Password.Password = "...";
                            Email.Text = "...";
                            Age.Text = "...";
                        }
                        else
                        {
                            ctx.Customers.Add(new Customer { Name = Name.Text, Password = Password.Password, Email = Email.Text, Age = Convert.ToInt32(Age.Text) });
                            ctx.SaveChanges();

                            MessageBox.Show("Account successful creation.", "Account creation Succeeded!", MessageBoxButton.OK, MessageBoxImage.Information);
                            var next_window = new LoginWindow();
                            next_window.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        Password.Background = Brushes.Red;
                        Password2.Background = Brushes.Red;
                        MessageBox.Show("Password doesn't match.", "Password error.", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (System.FormatException)
                {
                    Age.Text = "...";
                    Age.Background = Brushes.Red;
                    MessageBox.Show("Not correct input");
                }

            }
        }
        
        // Makes the three dots in the boxes disappear
        private void AgeTextBoxClick(object sender, MouseButtonEventArgs e)
        {
            Age.Clear();
        }
        private void NameTextBoxClick(object sender, MouseButtonEventArgs e)
        {
            Name.Clear();
        }
        private void PasswordTextBoxClick(object sender, MouseButtonEventArgs e)
        {
            Password.Clear();
        }
        private void Password2TextBoxClick(object sender, MouseButtonEventArgs e)
        {
            Password2.Clear();
        }
        private void EmailTextBoxClick(object sender, MouseButtonEventArgs e)
        {
            Email.Clear();
        }
    }
}
