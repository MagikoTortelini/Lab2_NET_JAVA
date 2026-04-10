using Lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class Currency_exchange
    {
        public int Id {  get; set; }
        public int From_Currency_id {  get; set; }
        public currency FromCurrency { get; set; }
        public int To_Currency_id { get; set; }
        public currency ToCurrency { get; set; }
        public decimal Rate { get; set; }
        public string Date {  get; set; }

        public override string ToString()
        {
            return $"Kurs dla {FromCurrency.Code}->{ToCurrency.Code} wynosi:{Rate} dnia:{Date}";
        }
    }
}
