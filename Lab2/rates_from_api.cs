using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class rates_from_api
    {
            public string Disclaimer { get; set; }
            public string License { get; set; }
            public long Timestamp { get; set; }
            public string Base { get; set; }
            public Dictionary<string, decimal> Rates { get; set; }
            public string Currency_name_from { get; set; }
            public string Currency_name_to { get; set; }


            public override string ToString()
            {
                var temp = $"{DateTimeOffset.FromUnixTimeSeconds(Timestamp).ToString("dd-MM-yyyy")}\t";
                temp += $"{Currency_name_from}->{Currency_name_to}: {Math.Round(Rates[Currency_name_from] / Rates[Currency_name_to], 2)}\t";

                return temp;
            }
        
    }
}
