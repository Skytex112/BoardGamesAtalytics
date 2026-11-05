using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesAtalytics.Entities
{
    internal class GamesStats
    {
        public string Title { get; set; }
        public int TotalPlaytime { get; set; }

        public override string ToString()
        {
            return $"{Title} -- {TotalPlaytime}";
        }
    }
}
