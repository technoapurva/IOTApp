using IOTApp.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IOTApp.Utility
{
    public static class CricketRepository
    {
        public static async Task<List<CricketDataModel>> GetCricketData(MatchModel teams)
        {
            var httpClient = new HttpClient(new HttpClientHandler());
            string url = "http://cricscore-api.appspot.com/csa?id=" + teams.id;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            List<CricketDataModel> dataModelList=null;
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                dataModelList = JsonConvert.DeserializeObject<List<CricketDataModel>>(responseString);
                foreach (var data in dataModelList)
                {
                    data.GameTitle = teams.t1 + " vs " + teams.t2;
                    data.Teams = teams;
                }
            }
                return dataModelList;
            
            
        }

        public static async Task<List<CricketDataModel>> GetCricketData()
        {
            var httpClient = new HttpClient(new HttpClientHandler());
            string url = "http://cricscore-api.appspot.com/csa?id=804229";
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<CricketDataModel> dataModelList = JsonConvert.DeserializeObject<List<CricketDataModel>>(responseString);
            //foreach (var data in dataModelList)
            //{
            //    data.GameTitle = teams.t1 + " vs " + teams.t2;
            //    data.Teams = teams;
            //}
            return dataModelList;
        }
        public static async Task<List<CricketDataModel>> GetMatchData()
        {
            string url = "http://cricscore-api.appspot.com/csa";
            List<CricketDataModel> cricketDataModel = new List<CricketDataModel>();
            var httpClient = new HttpClient(new HttpClientHandler());
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();

                List<MatchModel> dataModelList = JsonConvert.DeserializeObject<List<MatchModel>>(responseString);
                foreach (var data in dataModelList)
                {
                    List<CricketDataModel> cricketList = await GetCricketData(data);
                    //cricketList.Wait();
                    cricketDataModel.AddRange(cricketList);
                }
            }
            return cricketDataModel;
        }
    }
}
