using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DatabaseConnection;

namespace Store
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            State.User = API.GetCustomerByName(NameField.Text.Trim());
            if (State.User != null)
            {
                if (State.User.Password == PasswordField.Text)
                {
                    var next_window = new MainWindow();
                    next_window.Show();
                    this.Close();
                }
                else
                {
                    PasswordField.Text = "...";
                    MessageBox.Show("Wrong Password Try again.", "Wrong password", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                NameField.Text = "...";
                MessageBox.Show("Wrong Username Try again.", "Wrong username", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CreateAccountClick(object sender, RoutedEventArgs e)
        {
            var next_window = new CreateAccountWindow();
            next_window.Show();
            this.Close();
        }
    }
}
