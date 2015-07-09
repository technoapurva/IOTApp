using IOTApp.DataModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace IOTApp.Utility
{
    class WeatherRepository
    {
        public static async Task<List<WeatherDataModel>> GetWeatherData()
        {

            Geolocator geolocator = new Geolocator();
            Geoposition geoposition = null;
            geoposition = await geolocator.GetGeopositionAsync();
            var points = geoposition.Coordinate.Point;

            var lati = geoposition.Coordinate.Point.Position.Latitude;
            var la = lati.GetType();
            var longi = geoposition.Coordinate.Point.Position.Longitude;
            var lo = longi.GetType();
            string url = "http://api.openweathermap.org/data/2.5/weather?lat="+lati+"&lon="+longi;
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
