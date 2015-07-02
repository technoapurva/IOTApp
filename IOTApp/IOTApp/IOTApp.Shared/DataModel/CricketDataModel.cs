using System;
using System.Collections.Generic;
using System.Text;

namespace IOTApp.DataModel
{
    public class CricketDataModel
    {
        public string de { get; set; }
        public int id { get; set; }
        public string si { get; set; }
        public string GameTitle { get; set; }
        public MatchModel Teams { get; set; }

    }


}
