using DatabaseConnection;
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
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Store
{
    /// <summary>
    /// Interaction logic for ChangePasswordWindow.xaml
    /// </summary>
    public partial class ChangePasswordWindow : Window
    {
        public ChangePasswordWindow()
        {
            InitializeComponent();
        }

        private void ExistingPasswordBoxClick(object sender, MouseButtonEventArgs e)
        {
            ExistingPasswordBox.Clear();
        }

        private void NewPasswordBoxClick1(object sender, MouseButtonEventArgs e)
        {
            NewPasswordBox1.Clear();
        }

        private void NewPasswordBoxClick2(object sender, MouseButtonEventArgs e)
        {
            NewPasswordBox2.Clear();
        }

        private void ChangePasswordButton(object sender, RoutedEventArgs e)
        {
            if (ExistingPasswordBox.Password == State.User.Password)
            {
                if(NewPasswordBox1.Password == NewPasswordBox2.Password)
                {
                        State.User.Password = NewPasswordBox1.Password;
                        State.ct.Customers.Update(State.User);
                        State.ct.SaveChanges();
                    MessageBox.Show("Successfully changed password", "Password Changed", MessageBoxButton.OK, MessageBoxImage.Information);
                    var BackMyAccount = new MyAccount();
                    BackMyAccount.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("New password not matching", "Password Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Wrong Existing Password", "Password Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void GoBackButton(object sender, RoutedEventArgs e)
        {
            var BackMyAccount = new MyAccount();
            BackMyAccount.Show();
            this.Close();
        }
    }
}
