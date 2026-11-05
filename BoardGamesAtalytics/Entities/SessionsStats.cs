using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesAtalytics.Entities
{
    internal class SessionsStats
    {
        public string Title { get; set; }
        public string FullName { get; set; }    
        public DateTime Date { get; set; }
        public int DurationMinutes { get; set; }

        public override string ToString()
        {
            return $"{Title} -- {FullName}\n{Date} -- {DurationMinutes}";
        }
    }
}
