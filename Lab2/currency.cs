using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class currency
    {
        public int Id { get; set; }
        public required string Code { get; set; }
        public decimal USD_rate {  get; set; }
        public ICollection<Currency_exchange> RatesFrom { get; set; }
        public ICollection<Currency_exchange> RatesTo { get; set; }

        public override string ToString()
        {
            return $"ID: {Id},\tCode:{Code},\tUSD_rate:{USD_rate}";
        }
    }
}
