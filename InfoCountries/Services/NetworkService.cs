﻿namespace InfoCountries.Services
{
    using Models;
    using System.Net;

    /// <summary>
    /// Checks if there is Internet Connection
    /// </summary>
    public class NetworkService
    {
        public Response CheckConnection()
        {
            var client = new WebClient();

            try
            {
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                //using (client.OpenRead("http://www.google.com"))
                
                {
                    return new Response
                    {
                        IsSuccess = true,
                    };
                }
            }
            catch
            {
                return new Response
                {
                    IsSuccess = false,
                };
            }
        }
    }
}
