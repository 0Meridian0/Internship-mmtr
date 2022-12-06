using Microsoft.EntityFrameworkCore;
using System;

namespace UrlCutter.Models
{
    public class DbUrl : DbContext
    {
        public DbSet<URL> Urls { get; set; }
        public string Dbpath { get; }

        public DbUrl()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            Dbpath = System.IO.Path.Join(path, "DbUrl.db");
        }

        public void ClearDb()
        {
            Urls.RemoveRange(Urls);
            SaveChanges();
        }
        public bool CheckDataInDb(URL url)
        {
            if (Urls.Where(s => s.Token == url.Token).FirstOrDefault() != null)
                return true;
            else
                return false;
        }

        public void SaveToDb(URL url)
        {
            Urls.Add(url);
            SaveChanges();
        }

        public void ShowDbContent()
        {
            Urls.ToList();

            foreach (var i in Urls.ToList())
            {
                Console.WriteLine($"ID: {i.id} \nToken: {i.Token}\nLongUrl: {i.LongUrl} \n\n");
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={Dbpath}");
    }

}
