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
using System.Linq;
using DatabaseConnection;

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
            int y = 0;

            for (int i = 0; i < State.User.Sales.Count; i++)
            {
                Rental rental = State.User.Sales[i];
                
                var rental1 = new Label() { };
                rental1.Content = rental.Movie.Title;
                rental1.HorizontalAlignment = HorizontalAlignment.Left;
                rental1.VerticalAlignment = VerticalAlignment.Top;
                rental1.Foreground = Brushes.White;
                rental1.Margin = new Thickness(0,y,0,0);
                RentalList.Children.Add(rental1);
                y += 25;
            }
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
