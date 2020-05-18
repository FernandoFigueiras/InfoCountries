namespace InfoCountries.Services
{
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class ApiService
    {

        /// <summary>
        /// Gets Countries data from API.
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="controller"></param>
        /// <returns>Class Response/object if Success</returns>
        public async Task<Response> GetCountriesAsync(string baseUrl, string controller)
        {
            try
            {
                var client = new HttpClient();

                client.BaseAddress = new Uri(baseUrl);

                var status = await client.GetAsync(controller);

                var result = await status.Content.ReadAsStringAsync();

                if (!status.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                var countries = JsonConvert.DeserializeObject<List<Country>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = countries,
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
        }

        /// <summary>
        /// This methos is used to get rates from API
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="controller"></param>
        /// <returns>list of rates</returns>
        public async Task<Response> GetRatesAsync(string baseUrl, string controller)
        {
            try
            {
                var client = new HttpClient();

                client.BaseAddress = new Uri(baseUrl);

                var status = await client.GetAsync(controller);

                var result = await status.Content.ReadAsStringAsync();

                if (!status.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                var rates = JsonConvert.DeserializeObject<List<Rate>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = rates,
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }
        }


        /// <summary>
        /// Method used to post comment for a specific country
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="controller"></param>
        /// <param name="comment"></param>
        /// <returns> Task </returns>
        public async Task PostComments(string baseUrl, string controller, Comment comment)
        {

            var comments = JsonConvert.SerializeObject(comment);

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(
                    baseUrl + controller + comment.Alphacode,
                     new StringContent(comments, Encoding.UTF8, "application/json"));
            }
        }

        /// <summary>
        /// Gets comments from API
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="controller"></param>
        /// <returns> Task of object Response</returns>
        public async Task<Response> GetCommentsAsync(string baseUrl, string controller)
        {
            try
            {
                var client = new HttpClient();

                client.BaseAddress = new Uri(baseUrl);

                var status = await client.GetAsync(controller);

                var result = await status.Content.ReadAsStringAsync();

                if (!status.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }
                var comments = JsonConvert.DeserializeObject<List<Comment>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Result = comments,
                };
            }
            catch (Exception e)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = e.Message,
                };
            }

        }
    }
}
