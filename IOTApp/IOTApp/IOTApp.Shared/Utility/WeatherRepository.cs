using IOTApp.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IOTApp.Utility
{
    class WeatherRepository
    {
        public static async Task<List<WeatherDataModel>> GetWeatherData()
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=Hyderabad";
            //List<WeatherDataModel> cricketDataModel = new List<WeatherDataModel>();
            var httpClient = new HttpClient(new HttpClientHandler());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            List<WeatherDataModel> dataModel=null;
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                string obj = "[" + responseString + "]";
                dataModel = JsonConvert.DeserializeObject<List<WeatherDataModel>>(obj);
                
                
               
            }
            return dataModel;
        }
    }
}
