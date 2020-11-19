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

            State.Movies = API.GetMovieSlice(0, 30);;
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
                            text.FontFamily = new FontFamily("Comic Sans MS");
                            text.Foreground = Brushes.White;

                            var image = new Image() { };
                            image.Cursor = Cursors.Hand; // om man håller över en bild blir det ett sånt pekfinger
                            image.MouseUp += Image_MouseUp; // Om man klickar på en bilden skickas man ner till Image_MouseUp Metoden.
                            image.HorizontalAlignment = HorizontalAlignment.Center;
                            image.VerticalAlignment = VerticalAlignment.Center;
                            image.Source = new BitmapImage(new Uri(movie.ImageURL)); // hämtar url från ImageURL i databasen till bilderna
                            image.Height = 120;
                            image.Margin = new Thickness(4, 4, 4, 4);

                            MovieGrid.Children.Add(text); // säger till att texten ska tillhöra den gridden
                            Grid.SetRow(text, y); // vilken grid i y
                            Grid.SetColumn(text, x); // vilken grid i x
                            MovieGrid.Children.Add(image);
                            Grid.SetRow(image, y);
                            Grid.SetColumn(image, x);
                        }
                        catch (Exception e) when 
                            (e is ArgumentNullException || 
                             e is System.IO.FileNotFoundException || 
                             e is UriFormatException)
                        {
                            continue;
                        }
                    }
                }
            }
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var x = Grid.GetColumn(sender as UIElement);
            var y = Grid.GetRow(sender as UIElement);

            int i = y * MovieGrid.ColumnDefinitions.Count + x;
            State.Pick = State.Movies[i];

            if(API.RegisterSale(State.User, State.Pick))
                MessageBox.Show("All is well and you can download your movie now.", "Sale Succeeded!", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show("An error happened while buying the movie, please try again at a later time.", "Sale Failed!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
