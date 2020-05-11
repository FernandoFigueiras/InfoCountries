namespace InfoCountries.Services
{
    using InfoCountries.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.IO;
    public class DataBaseServices
    {
        private SQLiteConnection connection;

        private SQLiteCommand command;

        /// <summary>
        /// Contructor to set de database connection and contruction of tables
        /// </summary>
        public DataBaseServices()
        {
            string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string dataBasePath = Path.Combine(rootPath, @"Data\");

            if (!Directory.Exists(dataBasePath))
            {
                Directory.CreateDirectory(dataBasePath);
            }

            string dataPath = dataBasePath + "Countries.sqlite";

            try
            {
                connection = new SQLiteConnection("Data Source=" + dataPath);
                connection.Open();

                string sqlCommand = "create table if not exists countries (Name nvarchar(250)," +
                    "Capital nvarchar(30), " +
                    "Region nvarchar(30)," +
                    "Subregion nvarchar(30), " +
                    "Population int," +
                    "Gini decimal)";

                command = new SQLiteCommand(sqlCommand, connection);

                command.ExecuteNonQuery();


            }
            catch (Exception e)
            {

                MessageService.ShowMessage("Erro", e.Message);
            }
        }


        /// <summary>
        /// Method used to save data to database from loaded API
        /// </summary>
        /// <param name="Countries"></param>
        public void SaveDataBase(List<Country> Countries)
        {
            try
            {
                string sqlCommand = "delete from countries";
                command = new SQLiteCommand(sqlCommand, connection);
                command.ExecuteNonQuery();

                foreach (var Country in Countries)
                {

                    if (Country.Gini != null)
                    {
                        string sql = string.Format($"insert into countries values ('{Country.Name.Replace("'", " ")}', '{Country.Capital.Replace("'", " ")}', '{Country.Region}', '{Country.Subregion}', {Country.Population}, {Country.Gini});");
                        command = new SQLiteCommand(sql, connection);
                    }
                    else
                    {
                        string sql = string.Format($"insert into countries values ('{Country.Name.Replace("'", " ")}','{Country.Capital.Replace("'", " ")}', '{Country.Region}', '{Country.Subregion}', {Country.Population}, 0);");
                        command = new SQLiteCommand(sql, connection);
                    }



                    command.ExecuteNonQuery();

                }
                connection.Close();
            }
            catch (Exception e)
            {

                MessageService.ShowMessage("Erro", e.Message);
            }

        }

        /// <summary>
        /// If no internet connection this method returns data from database
        /// </summary>
        /// <returns></returns>
        public List<Country> GetDataFromDataBase()
        {
            List<Country> Countries = new List<Country>();

            try
            {
                string sql = "Select Name, Capital, Region, Subregion, Population, Gini from countries;";

                command = new SQLiteCommand(sql, connection);

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Countries.Add(new Country
                    {
                        Name = reader["Name"].ToString(),
                        Capital = reader["Capital"].ToString(),
                        Region = reader["Region"].ToString(),
                        Subregion = reader["Subregion"].ToString(),
                        Population = Convert.ToInt32(reader["Population"]),
                        Gini = Convert.ToDouble(reader["Gini"])

                    }); ; ;
                }
                connection.Close();
                return Countries;
            }
            catch (Exception e)
            {

                MessageService.ShowMessage("Erro", e.Message);
            }
            return Countries;
        }
    }
}
