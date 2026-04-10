using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Filtered_return
    {
        public decimal rate;
        public string code;
        public override string ToString()
        {
            return $"Kurs:{rate}\tNa:{code}";
        }
    }
    public class DB_operations
    {
        public async Task<string> Add(string waluty,string waluty2)
        {
            APITest_with_db t = new APITest_with_db();
            await t.GetData(waluty, waluty2);
            using var db = new Exchanges();
            var from = db.Currencies.FirstOrDefault(c => c.Code == waluty);
            var to = db.Currencies.FirstOrDefault(c => c.Code == waluty2);
            var kurs=db.Exchanges_rates.FirstOrDefault(e => e.From_Currency_id == from.Id && e.To_Currency_id == to.Id);
            return kurs.ToString();
        }
        public List<currency> show_sorted_down()
        {
            using var db = new Exchanges();
            var sorted=db.Currencies.OrderBy(e=>(e.Code)).Reverse().ToList();
            
         return sorted;
        }
        public List<currency> show_sorted_up()
        {
            using var db = new Exchanges();
            var sorted = db.Currencies.OrderBy(e => (e.Code)).ToList();
            return sorted;
        }

        public List<Filtered_return> show_filtered(string waluta)
        {
            using var db = new Exchanges();
            var filtered = db.Exchanges_rates.Where(e => e.FromCurrency.Code == waluta).Select(e => new Filtered_return { rate=e.Rate,code= e.ToCurrency.Code}).ToList();
            return filtered;
        }
        public List<currency> show_currency()
        {
            using var db = new Exchanges();
            var currencies = db.Currencies.ToList();
            return currencies;
        }
        public List<Currency_exchange> show_rates()
        {
            using var db = new Exchanges();
            var exchange_rates = db.Exchanges_rates
            .Include(e => e.FromCurrency)
            .Include(e => e.ToCurrency)
            .ToList();
            return exchange_rates;
        }
        public string add_to_db(string code,decimal rate)
        {
            using var db = new Exchanges();
            var new_currency = new currency { Code = code, USD_rate = rate };
            db.Add(new_currency);
            db.SaveChanges();
            return "Dodano walute";
        }

    }
}
