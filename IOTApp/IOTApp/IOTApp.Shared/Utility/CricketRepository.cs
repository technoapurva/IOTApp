using IOTApp.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media;




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

        private void CreateSaveBitmap(Canvas canvas, string filename)
        {
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
             (int)canvas.Width, (int)canvas.Height,
             96d, 96d, PixelFormats.Pbgra32);
            // needed otherwise the image output is black
            canvas.Measure(new Size((int)canvas.Width, (int)canvas.Height));
            canvas.Arrange(new Rect(new Size((int)canvas.Width, (int)canvas.Height)));

            renderBitmap.Render(canvas);

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (FileStream file = File.Create(filename))
            {
                encoder.Save(file);
            }
        }
    }
}
