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
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            List<CricketDataModel> dataModelList = JsonConvert.DeserializeObject<List<CricketDataModel>>(responseString);
            foreach(var data in dataModelList)
            {
                data.GameTitle = teams.t1 + " vs " + teams.t2;
                data.Teams = teams;
            }
            return dataModelList ;
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
            List<CricketDataModel> cricketDataModel = new List<CricketDataModel>();
            var httpClient = new HttpClient(new HttpClientHandler());
            HttpResponseMessage response = await httpClient.GetAsync("http://cricscore-api.appspot.com/csa");
            var responseString = await response.Content.ReadAsStringAsync();
            List<MatchModel> dataModelList = JsonConvert.DeserializeObject<List<MatchModel>>(responseString);
            foreach (var data in dataModelList)
            {
                Task<List<CricketDataModel>> cricketList = GetCricketData(data);
                cricketList.Wait();
                cricketDataModel.AddRange(cricketList.Result);
            }
            return cricketDataModel;
        }
    }
}
