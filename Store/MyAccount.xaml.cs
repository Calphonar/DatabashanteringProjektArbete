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

namespace Store
{
    /// <summary>
    /// Interaction logic for MyAccount.xaml
    /// </summary>
    public partial class MyAccount : Window
    {
        public MyAccount()
        {
            InitializeComponent();

            AccountLabel.Content = "Welcome, " + State.User.Name + "!";

            /*State.Rental = 12;
             * userId = State.User.Id
            RentalList.Content = State.Rental.Movie.Title;*/

        }

        private void Logoutbutton(object sender, RoutedEventArgs e)
        {
            var logout = new LoginWindow();
            logout.Show();
            this.Close();
        }

        private void Backbutton(object sender, RoutedEventArgs e)
        {
            var mainwindow = new MainWindow();
            mainwindow.Show();
            this.Close();
        }

        private void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            var changePasswordWindow = new ChangePasswordWindow();
            changePasswordWindow.Show();
            this.Close();
        }
    }
}
