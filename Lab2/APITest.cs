using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab2
{
    public class APITest
    {
        public HttpClient client;
        private string API_KEY = "";

        public async Task GetData(string waluty, string waluty2)
        {
            client = new HttpClient();
            string call = $"https://openexchangerates.org/api/latest.json?app_id={API_KEY}&symbols={waluty},{waluty2}";
            string response = await client.GetStringAsync(call);
            var kursy = JsonSerializer.Deserialize<rates_from_api>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            kursy.Currency_name_from = waluty;
            kursy.Currency_name_to = waluty2;
            Console.WriteLine(kursy.ToString());
        }
    }
    }
