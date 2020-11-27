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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        public void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                    State.Movies.Clear();
                    State.Movies.AddRange(API.GetMovieByName(textbox.Text));
                    var next_searchWindow = new SearchWindow();
                    next_searchWindow.Show();
                    this.Close();
            }
        }
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var x = Grid.GetColumn(sender as UIElement);
            var y = Grid.GetRow(sender as UIElement);

            int i = y * MovieGrid.ColumnDefinitions.Count + x;
            State.Pick = State.Movies[i];

            if (MessageBox.Show("Want to rent this movie?", "Rent Movie?", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                if (API.RegisterSale(State.User, State.Pick))
                MessageBox.Show("Suceed to download the movie.", "Renting Succeeded!", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("An error happened while buying the movie, please try again at a later time.", "Sale Failed!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private bool handle = true;
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }

        private void Handle()
        {
            switch (cmbSelect.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "Sort by name a-z":
                    State.Movies = API.GetMovieSliceName(0, 30);
                    for (int y = 0; y < MovieGrid.RowDefinitions.Count; y++)
                    {
                        for (int x = 0; x < MovieGrid.ColumnDefinitions.Count; x++)
                        {
                            int i = y * MovieGrid.ColumnDefinitions.Count + x;
                            if (i < State.Movies.Count)
                            {
                                var movie = State.Movies[i];
                                try
                                {
                                    var text = new Label() { }; // variabel för texten
                                    text.Content = movie.Title; // Vad texten ska innehålla, i detta fall Movie.Title i databasen
                                    text.HorizontalAlignment = HorizontalAlignment.Center; // vart texten ska ligga horisontelt
                                    text.VerticalAlignment = VerticalAlignment.Top; // vart texten ska ligga vertikalt
                                    text.FontSize = 16;
                                    text.FontWeight = FontWeights.UltraBold;
                                    text.FontFamily = new FontFamily("Sans-Serif");
                                    text.Foreground = Brushes.White;

                                    var image = new Image() { };
                                    image.Cursor = Cursors.Hand; // om man håller över en bild blir det ett sånt pekfinger
                                    image.MouseUp += Image_MouseUp; // Om man klickar på en bilden skickas man ner till Image_MouseUp Metoden.
                                    image.HorizontalAlignment = HorizontalAlignment.Center;
                                    image.VerticalAlignment = VerticalAlignment.Center;
                                    image.Source = new BitmapImage(new Uri(movie.ImageURL)); // hämtar url från ImageURL i databasen till bilderna
                                    image.Height = 120;
                                    image.Margin = new Thickness(4, 4, 4, 4);

                                    var rating = new Label() { };
                                    rating.Content = movie.Rating + "/10 ★";
                                    rating.HorizontalAlignment = HorizontalAlignment.Center;
                                    rating.VerticalAlignment = VerticalAlignment.Bottom;
                                    rating.Foreground = Brushes.White;
                                    rating.Margin = new Thickness(-5);

                                    var genre = new Label() { };
                                    genre.Content = movie.Genre;
                                    genre.HorizontalAlignment = HorizontalAlignment.Center;
                                    genre.VerticalAlignment = VerticalAlignment.Bottom;
                                    genre.Foreground = Brushes.White;
                                    genre.Margin = new Thickness(15);


                                    MovieGrid.Children.Add(text); // säger till att texten ska tillhöra den gridden
                                    Grid.SetRow(text, y); // vilken grid i y
                                    Grid.SetColumn(text, x); // vilken grid i x
                                    MovieGrid.Children.Add(image);
                                    Grid.SetRow(image, y);
                                    Grid.SetColumn(image, x);
                                    MovieGrid.Children.Add(rating);
                                    Grid.SetRow(rating, y);
                                    Grid.SetColumn(rating, x);
                                    MovieGrid.Children.Add(genre);
                                    Grid.SetRow(genre, y);
                                    Grid.SetColumn(genre, x);
                                }
                                catch (Exception exeption) when
                                    (exeption is ArgumentNullException ||
                                     exeption is System.IO.FileNotFoundException ||
                                     exeption is UriFormatException)
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    break;
                case "Sort by name z-a":
                    var next_windowZA = new MainWindowZA();
                    next_windowZA.Show();
                    this.Close();
                    break;
                case "Sort by highest rating":
                    var next_window = new MainWindowByRating();
                    next_window.Show();
                    this.Close();
                    break;
                case "Sort by lowest rating":
                    var next_window_lowestRating = new MainWindowByLowestRating();
                    next_window_lowestRating.Show();
                    this.Close();
                    break;
                default:
                    break;
            }
        }

        private void MyAccountClick(object sender, RoutedEventArgs e)
        {
            var next_window_myaccount = new MyAccount();
            next_window_myaccount.Show();
            this.Close();
        }

        private void SearchTextBoxClick(object sender, MouseButtonEventArgs e)
        {
            textbox.Clear();
        }
    }

}
