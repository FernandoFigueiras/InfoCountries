namespace InfoCountries.Services
{
    using InfoCountries.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.SQLite;
    using System.IO;
    using System.Windows;

    public class DataBaseServices
    {
        private SQLiteConnection connectionCountries;
        private SQLiteConnection connectionRates;

        private SQLiteCommand command;

        /// <summary>
        /// Contructor to set de database connection and contruction of tables
        /// </summary>
        public DataBaseServices()
        {
            string rootPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            string dataBasePath = Path.Combine(rootPath, @"Data\");

            if (!Directory.Exists(dataBasePath))
            {
                Directory.CreateDirectory(dataBasePath);
            }

            string dataPath = dataBasePath + "Countries.sqlite";


            try
            {
                connectionCountries = new SQLiteConnection("Data Source=" + dataPath);
                connectionCountries.Open();

                string sqlCommand = "create table if not exists Countries (Name nvarchar(250)," +
                    "Capital nvarchar(30), " +
                    "Region nvarchar(30)," +
                    "Subregion nvarchar(30), " +
                    "Population int," +
                    "Gini  nvarchar(30))";


                command = new SQLiteCommand(sqlCommand, connectionCountries);
                command.ExecuteNonQuery();


                string dataPath2 = dataBasePath + "Rates.sqlite";
                connectionRates = new SQLiteConnection("Data Source=" + dataPath2);
                connectionRates.Open();

                string sqlCommand2 = "create table if not exists Rates(RateId int primary key, Code varchar(3), TaxRate real, Name varchar(150))";
                command = new SQLiteCommand(sqlCommand2, connectionRates);
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
        public void SaveCountriesData(List<Country> Countries)
        {
            try
            {
                string sqlCommand = "delete from countries";
                command = new SQLiteCommand(sqlCommand, connectionCountries);
                command.ExecuteNonQuery();

                foreach (var Country in Countries)
                {
                    string sql = string.Format($"insert into countries values ('{Country.Name.Replace("'", " ")}', '{Country.Capital.Replace("'", " ")}', '{Country.Region.Replace("'", " ")}', '{Country.Subregion.Replace("'", " ")}', {Country.Population}, '{Country.Gini}');");
                        command = new SQLiteCommand(sql, connectionCountries);
                   
                    command.ExecuteNonQuery();

                }
                connectionCountries.Close();
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
        public List<Country> GetCountriesData()
        {
            List<Country> Countries = new List<Country>();

            try
            {
                string sql = "Select Name, Capital, Region, Subregion, Population, Gini from countries;";

                command = new SQLiteCommand(sql, connectionCountries);

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
                        Gini = Convert.ToString(reader["Gini"])

                    });
                }
                connectionCountries.Close();
                return Countries;
            }
            catch (Exception e)
            {

                MessageService.ShowMessage("Erro", e.Message);
            }
            return Countries;
        }


        /// <summary>
        /// Method used to save rates into dataBase
        /// </summary>
        /// <param name="rates"></param>
        public void SaveRatesData(List<Rate> rates)
        {
            try
            {
                string sqlCommand = "delete from Rates";
                command = new SQLiteCommand(sqlCommand, connectionRates);
                command.ExecuteNonQuery();

                foreach (var rate in rates)
                {
                    string sql = string.Format($"insert into Rates values ({rate.RateId}, '{rate.Code}', {rate.TaxRate}, '{rate.Name.Replace("'", " ")}')");
                    command = new SQLiteCommand(sql, connectionRates);
                    command.ExecuteNonQuery();

                }
                connectionRates.Close();
            }
            catch (Exception e)
            {
                MessageService.ShowMessage("Erro", e.Message);
            }

        }

        public List<Rate> GetRatesData()
        {
            List<Rate> Rates = new List<Rate>();

            try
            {
                string sql = "Select RateId, Code, TaxRate, Name from Rates;";

                command = new SQLiteCommand(sql, connectionRates);

                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Rates.Add(new Rate
                    {
                        RateId = Convert.ToInt32(reader["RateID"]),
                        Code = reader["Code"].ToString(),
                        TaxRate = Convert.ToDecimal(reader["TaxRate"]),
                        Name = reader["Name"].ToString(),
                    });
                }
                connectionCountries.Close();
                return Rates;
            }
            catch (Exception e)
            {

                MessageService.ShowMessage("Erro", e.Message);
            }
            return Rates;
        }
    }
}