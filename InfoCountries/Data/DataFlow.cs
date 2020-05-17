namespace InfoCountries.Data
{
    using Models;
    using Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// This classe is used to verify if data comes from API or DataBase. And to store data to Database
    /// </summary>
    public class DataFlow
    {
        public List<Country> Countries;
        public List<Rate> Rates;
        public DataBaseServices dataBaseServices;

        private bool SetConnectionStatus()
        {
            NetworkService networkService = new NetworkService();

            var checkconnection = networkService.CheckConnection();

            if (checkconnection.IsSuccess)
            {
                return true;
            }
            else
            {
                MessageService.ShowMessage("Erro", checkconnection.Message);
                return false;
            }
        }
        
        /// <summary>
        /// This method searches for an internet connection. If connection is valid gets data from API, if not gets data from Databse
        /// </summary>
        /// <returns>List of Country</returns>
        public async Task<List<Country>> ReturnData()
        {
            dataBaseServices = new DataBaseServices();
            SetConnectionStatus();

            if (SetConnectionStatus())
            {
                Countries = await GetApiData.LoadCountriesFromAPIAsync();
            }
            else
            {
                Countries = dataBaseServices.GetDataFromDataBase();
            }

            if (!SetConnectionStatus())
            {
                if (Countries.Count == 0)
                {
                    MessageService.ShowMessage("Error", "Primeira utilização desta aplicação requer uma ligação válida à internet");
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
                await Task.Run(() => dataBaseServices.SaveDataBase(Countries));
                await Task.Run(() => dataBaseServices.SaveCurrencyData(Rates));
            }
        }

        /// <summary>
        /// Is connection status true, get rates data from GetApiData Class
        /// </summary>
        /// <returns>List of Rate</returns>
        public async Task<List<Rate>> GetRatesAPIDataAsync()
        {
            if (SetConnectionStatus())
            {
                Rates = await GetApiData.LoadRatesFromAPIAsync();
            }
            return Rates;
        }

    }
}
