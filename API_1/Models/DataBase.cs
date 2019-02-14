using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_1.Models
{
    public class DataBase
    {
        public static string connectionString = @"Data Source = 192.168.1.68; Initial Catalog = VidaBox; User ID = sa; Password=vidabox";

        public static DataTable select(string query, Dictionary<string, string> paramChamps)
        {
            DataTable dataTableResults = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.CommandType = CommandType.Text;

                    foreach (var item in paramChamps)
                    {
                        string key = item.Key;
                        string value = item.Value;

                        command.Parameters.AddWithValue(key, value);
                    }
                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                    {
                        dataAdapter.Fill(dataTableResults);
                    }
                }
            }
            return dataTableResults;
        }


        static public int AddMovie(string Titre, string synopsis, string dateSortie, string duree, string Url_Affiche, string url_BandeAnnoce, string Id_genre)
        {
            Int32 newMovieID = 0;
            string sql =
                "INSERT INTO Films VALUES(@Titre, @synopsis, @dateSortie, @duree, @Url_Affiche, @url_BandeAnnoce, @Id_genre) "
                + "SELECT CAST(scope_identity() AS int)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Titre", SqlDbType.VarChar);
                cmd.Parameters["@Titre"].Value = Titre;
                cmd.Parameters.Add("@synopsis", SqlDbType.Text);
                cmd.Parameters["@synopsis"].Value = synopsis;
                cmd.Parameters.Add("@dateSortie", SqlDbType.DateTime);
                cmd.Parameters["@dateSortie"].Value = dateSortie;
                cmd.Parameters.Add("@duree", SqlDbType.Int);
                cmd.Parameters["@duree"].Value = duree;
                cmd.Parameters.Add("@Url_Affiche", SqlDbType.VarChar);
                cmd.Parameters["@Url_Affiche"].Value = Url_Affiche;
                cmd.Parameters.Add("@url_BandeAnnoce", SqlDbType.VarChar);
                cmd.Parameters["@url_BandeAnnoce"].Value = url_BandeAnnoce;
                cmd.Parameters.Add("@Id_genre", SqlDbType.Int);
                cmd.Parameters["@Id_genre"].Value = Id_genre;

                try
                {
                    conn.Open();
                    newMovieID = (Int32)cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return (int)newMovieID;
        }

    }
}