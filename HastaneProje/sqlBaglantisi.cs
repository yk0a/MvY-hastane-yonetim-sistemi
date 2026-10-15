using System;
using System.Configuration;
using System.Data.SqlClient;

namespace HastaneProje
{
    internal class sqlBaglantisi
    {
        public SqlConnection baglanti()
        {
            const string connectionName =
                "HastaneProje.Properties.Settings.HastaneProjeConnectionString";

            ConnectionStringSettings setting =
                ConfigurationManager.ConnectionStrings[connectionName];

            if (setting == null || string.IsNullOrWhiteSpace(setting.ConnectionString))
            {
                throw new InvalidOperationException(
                    "Veritabanı bağlantı ayarı App.config dosyasında bulunamadı. " +
                    "Connection string adını ve değerini kontrol edin.");
            }

            var builder = new SqlConnectionStringBuilder(setting.ConnectionString);
            if (string.IsNullOrWhiteSpace(builder.DataSource) ||
                string.IsNullOrWhiteSpace(builder.InitialCatalog))
            {
                throw new InvalidOperationException(
                    "App.config içindeki bağlantı cümlesinde Data Source ve " +
                    "Initial Catalog alanları doldurulmalıdır.");
            }

            SqlConnection conn = new SqlConnection(builder.ConnectionString);
            try
            {
                conn.Open();
                return conn;
            }
            catch
            {
                conn.Dispose();
                throw;
            }
        }
    }
}
