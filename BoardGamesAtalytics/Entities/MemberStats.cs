using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGamesAtalytics.Entities
{
    internal class MemberStats
    {
        public string FullName { get; set; }
        public DateTime JoinDate { get; set; }

        public int DurationMinutes { get; set; }
        public override string ToString()
        {
            return $"{FullName} -- {DurationMinutes}";
        }

    }
}
