using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lab2
{
    public class APITest_with_db
    {
        public HttpClient client;
        private Exchanges exchanges;
        private string API_KEY = "";
        public async Task GetData(string waluty, string waluty2)
        {
            exchanges = new Exchanges();
            client = new HttpClient();
            var currencies = exchanges.Currencies.ToDictionary(c => c.Code);
            if (!currencies.ContainsKey(waluty) || !currencies.ContainsKey(waluty2))
            {
                string call = $"https://openexchangerates.org/api/latest.json?app_id={API_KEY}&symbols={waluty},{waluty2}";
                string response = await client.GetStringAsync(call);
                var kursy = JsonSerializer.Deserialize<rates_from_api>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (!currencies.ContainsKey(waluty))
                {
                    exchanges.Currencies.Add(new currency() { Code = waluty, USD_rate = kursy.Rates[waluty] });
                    exchanges.SaveChanges();
                }
                if (!currencies.ContainsKey(waluty2))
                {
                    exchanges.Currencies.Add(new currency() { Code = waluty2, USD_rate = kursy.Rates[waluty2] });
                    exchanges.SaveChanges();
                }

                var from = exchanges.Currencies.FirstOrDefault(c => c.Code == waluty);
                var to = exchanges.Currencies.FirstOrDefault(c => c.Code == waluty2);
                var new_exchange = new Currency_exchange
                {
                    Date = DateTimeOffset.FromUnixTimeSeconds(kursy.Timestamp).ToString("dd-MM-yyyy"),
                    Rate = Math.Round(to.USD_rate / from.USD_rate, 2),
                    FromCurrency = from,
                    ToCurrency = to,
                    To_Currency_id = to.Id,
                    From_Currency_id = from.Id,
                };
                exchanges.Exchanges_rates.Add(new_exchange);
                exchanges.SaveChanges();
                var found = exchanges.Exchanges_rates.FirstOrDefault(e => e.From_Currency_id == from.Id && e.To_Currency_id == to.Id);
                Console.WriteLine(found.ToString() + " Dodano z API");
            }
            else
            {
                var from = exchanges.Currencies.FirstOrDefault(c => c.Code == waluty);
                var to = exchanges.Currencies.FirstOrDefault(c => c.Code == waluty2);
                if (exchanges.Exchanges_rates.Any(e => e.From_Currency_id == from.Id && e.To_Currency_id == to.Id))
                {

                    var found = exchanges.Exchanges_rates.FirstOrDefault(e => e.From_Currency_id == from.Id && e.To_Currency_id == to.Id);
                    if (found.Date != DateTime.UtcNow.ToString("dd-MM-yyyy"))
                    {
                        string call = $"https://openexchangerates.org/api/latest.json?app_id={API_KEY}&symbols={waluty},{waluty2}";
                        string response = await client.GetStringAsync(call);
                        var kursy = JsonSerializer.Deserialize<rates_from_api>(response, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        });
                        found.Date = DateTime.UtcNow.ToString("dd-MM-yyyy");
                        from.USD_rate = kursy.Rates[waluty];
                        to.USD_rate = kursy.Rates[waluty2];
                        found.Rate = Math.Round(to.USD_rate / from.USD_rate, 2);
                        exchanges.SaveChanges();
                        Console.WriteLine("Zaktualizowano dane");
                    }
                    Console.WriteLine(found.ToString() + " Wyswietlono warości z bazy");
                }
                else
                {
                    var new_exchange = new Currency_exchange
                    {
                        Date = DateTime.UtcNow.ToString("dd-MM-yyyy"),
                        Rate = Math.Round(to.USD_rate / from.USD_rate, 2),
                        FromCurrency = from,
                        ToCurrency = to,
                        To_Currency_id = to.Id,
                        From_Currency_id = from.Id,
                    };
                    exchanges.Exchanges_rates.Add(new_exchange);
                    exchanges.SaveChanges();
                    var found = exchanges.Exchanges_rates.FirstOrDefault(e => e.From_Currency_id == from.Id && e.To_Currency_id == to.Id);
                    Console.WriteLine(found.ToString() + " Dodano z wartości z bazy");

                }
            }

            exchanges.SaveChanges();


        }
    }
}
