namespace InfoCountries.Data
{
    using Models;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Management;

    /// <summary>
    /// This classe is used to verify if data comes from API or DataBase. And to store data to Database
    /// </summary>
    public class DataFlow
    {
        public List<Country> Countries;
        public List<Rate> Rates;
        public List<Comment> Comments;
        public DataBaseServices dataBaseServices;

        public bool SetConnectionStatus()
        {
            NetworkService networkService = new NetworkService();

            var checkconnection = networkService.CheckConnection();

            if (checkconnection.IsSuccess)
            {
                return true;
               
            }
            else
            {
                
                return false;
            }
        }
        
        /// <summary>
        /// This method searches for an internet connection. If connection is valid gets data from API, if not gets data from Databse
        /// </summary>
        /// <returns>List of Country</returns>
        public async Task<List<Country>> ReturnCountriesData()
        {
            dataBaseServices = new DataBaseServices();
            SetConnectionStatus();

            if (SetConnectionStatus())
            {
                Countries = await GetApiData.LoadCountriesFromAPIAsync();
            }
            else
            {
                MessageService.ShowMessage("Error", "Set up your Internet connection");
                Countries = dataBaseServices.GetCountriesData();
            }

            if (!SetConnectionStatus())
            {
                
                if (Countries.Count == 0)
                {
                    MessageService.ShowMessage("Error", "First use of this application requires a valid internet connection");
                }
            }
            return Countries;
        }

        /// <summary>
        /// After data has been loaded from API into UI this method saves data to database if data comes from API
        /// </summary>
        /// <param name="Countries"></param>
        /// <returns>Task</returns>
        public async Task SaveData(List<Country> Countries, List<Rate> Rates)
        {
            if (SetConnectionStatus())
            {
                dataBaseServices = new DataBaseServices();
                await Task.Run(() => dataBaseServices.SaveCountriesData(Countries));
                await Task.Run(() => dataBaseServices.SaveRatesData(Rates));
            }
        }

        /// <summary>
        /// Is connection status true, get rates data from GetApiData Class
        /// </summary>
        /// <returns>List of Rate</returns>
        public async Task<List<Rate>> GetRatesAPIDataAsync()
        {
            dataBaseServices = new DataBaseServices();
            
            if (SetConnectionStatus())
            {
                Rates = await GetApiData.LoadRatesFromAPIAsync();
            }
            else
            {
               
                Rates = dataBaseServices.GetRatesData();
            }

            return Rates;
        }

        public async Task PostCommentsData(Country country, Comment comment)
        {
            ApiService ApiService = new ApiService();
            if (SetConnectionStatus())
            {
                string Controller = "/api/Comments/";
                await ApiService.PostComments("http://www.CountriesComments.somee.com", Controller + country.Alpha2Code, comment);
            }
        }

        public async Task<List<Comment>> GetCommentsAPIAsync()
        {
            if (SetConnectionStatus())
            {
                Comments = await GetApiData.LoadApiCommentsAsync();
            }
            return Comments;
        }
    }
}
