using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_1.Models
{
    public class DataBase
    {
        public static string connectionString = @"Data Source = 192.168.1.68; Initial Catalog = VidaBox; User ID = sa; Password=vidabox";

        public static List<NameValueCollection> select(string query, Dictionary<string, string> paramChamps)
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

            List<NameValueCollection> results = new List<NameValueCollection>();
            for (int i = 0; i < dataTableResults.Rows.Count; i++)
            {
                NameValueCollection nameValueCollection = new NameValueCollection();
                for (int j = 0; j < dataTableResults.Columns.Count; j++)
                    nameValueCollection[dataTableResults.Columns[j].ToString()] = dataTableResults.Rows[i][j].ToString();

                results.Add(nameValueCollection);
            }

            return results;
        }


        static public int AddFilms(string Titre, string synopsis, string dateSortie, string duree, string Url_Affiche, string url_BandeAnnoce, string Id_genre, int id_moviedb)
        {
            Int32 newMovieID = 0;
            string sql =
                "INSERT INTO Films VALUES(@Titre, @synopsis, @dateSortie, @duree, @Url_Affiche, @url_BandeAnnoce, @Id_genre, @id_moviedb) "
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
                cmd.Parameters.Add("@id_moviedb", SqlDbType.Int);
                cmd.Parameters["@id_moviedb"].Value = id_moviedb;

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


        public static int AddSerie(string Titre, string Synopsis, string Date_sortie, string Url_affiche, string Url_bande_annonce, string id_genre, int id_moviedb)
        {
            Int32 newMovieID = 0;
            string sql =
                "INSERT INTO Serie VALUES(@Titre, @Synopsis, @Date_sortie, @Url_affiche, @Url_bande_annonce, @Id_genre, @id_moviedb) "
                + "SELECT CAST(scope_identity() AS int)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@Titre", SqlDbType.VarChar);
                cmd.Parameters["@Titre"].Value = Titre;
                cmd.Parameters.Add("@Synopsis", SqlDbType.Text);
                cmd.Parameters["@Synopsis"].Value = Synopsis;
                cmd.Parameters.Add("@Date_sortie", SqlDbType.DateTime);
                cmd.Parameters["@Date_sortie"].Value = Date_sortie;
                cmd.Parameters.Add("@Url_affiche", SqlDbType.VarChar);
                cmd.Parameters["@Url_affiche"].Value = Url_affiche;
                cmd.Parameters.Add("@Url_bande_annonce", SqlDbType.VarChar);
                cmd.Parameters["@Url_bande_annonce"].Value = Url_bande_annonce;
                cmd.Parameters.Add("@Id_genre", SqlDbType.Int);
                cmd.Parameters["@Id_genre"].Value = id_genre;
                cmd.Parameters.Add("@id_moviedb", SqlDbType.Int);
                cmd.Parameters["@id_moviedb"].Value = id_moviedb;

                try
                {
                    conn.Open();
                    newMovieID = (Int32)cmd.ExecuteScalar();
                }
                catch (Exception e)
                {
                    
                }
            }
            return (int)newMovieID;
        }

    }
}