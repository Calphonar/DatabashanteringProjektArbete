using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace DatabaseConnection
{
    public class API
    {
        public static List<Movie> GetMovieSliceName(int a, int b)
        {
            using var ctx = new Context();
            return ctx.Movies.OrderBy(m => m.Title).Skip(a).Take(b).ToList();
        }
        public static List<Movie> GetMovieSliceNameZA(int a, int b)
        {
            using var ctx = new Context();
            return ctx.Movies.OrderByDescending(m => m.Title).Skip(a).Take(b).ToList();
        }
        public static List<Movie> GetMovieSliceByRating(int a, int b)
        {
            using var ctx = new Context();
            return ctx.Movies.OrderByDescending(m => m.Rating).Skip(a).Take(b).ToList();
        }
        public static List<Movie> GetMovieSliceByRatingLowest(int a, int b)
        {
            using var ctx = new Context();
            return ctx.Movies.OrderBy(m => m.Rating).Skip(a).Take(b).ToList();
        }
        public static List<Movie> GetMovieByName(string title)
        {
            using var ctx = new Context();
            return ctx.Movies.AsEnumerable().Where(m => m.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public static Customer GetCustomerByName(string name)
        {
            using var ctx = new Context();
            return ctx.Customers.FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        }
        public static Customer GetCustomerByPassword(string password)
        {
            using var ctx = new Context();
            return ctx.Customers.FirstOrDefault(c => c.Name.ToLower() == password.ToLower());
        }
        public static bool RegisterSale(Customer customer, Movie movie)
        {
            using var ctx = new Context();
            try
            {
                // Här säger jag åt contextet att inte oroa sig över innehållet i dessa records.
                // Om jag inte gör detta så kommer den att försöka updatera databasens Id och cracha.
                ctx.Entry(customer).State = EntityState.Unchanged;
                ctx.Entry(movie).State = EntityState.Unchanged;

                ctx.Add(new Rental() { Date = DateTime.Now, Customer = customer, Movie = movie });
                return ctx.SaveChanges() == 1;
            }
            catch(DbUpdateException e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.InnerException.Message);
                return false;
            }
        }
    }
}
