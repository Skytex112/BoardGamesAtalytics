using BoardGamesAtalytics.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BoardGamesAtalytics
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string connectionstring = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = BoardGamesDateBase;";

            using SqlConnection connection = new SqlConnection(connectionstring);
            connection.Open();
            Menu(connection);

        }

        private static void Menu(SqlConnection connection)
        {
            Console.WriteLine("Menu:\n1)1 Task\n2) 2 Task\n 3) 3 Task\n4) 4 Task; ");
            string userChoose = Console.ReadLine();
            if (userChoose == "1")
            {
                Read(connection);
            }
            else if (userChoose == "2") { Top3(connection); }
            else if (userChoose == "3") { MostPlayedMember(connection); }
            else if (userChoose == "4")
            {
                Console.WriteLine("Enter start date and end date, enter for skipping;");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                ShowGeneralStats(connection, startDate, endDate);
            }
            else { Console.WriteLine("Entered something else , try again"); Menu(connection); }
        }


        private static void ShowGeneralStats(SqlConnection connection, DateTime? startDate = null, DateTime? endDate = null)
        {
            string query;

            if (startDate.HasValue && endDate.HasValue)
            {
                query = @"
            SELECT COUNT(*) AS TotalSessions, SUM(DurationMinutes) AS TotalDurationMinutes
            FROM Sessions
            WHERE Date BETWEEN @StartDate AND @EndDate;";
            }
            else
            {
                query = @"
            SELECT COUNT(*) AS TotalSessions, SUM(DurationMinutes) AS TotalDurationMinutes
            FROM Sessions;";
            }

            var stats = connection.QueryFirstOrDefault<GeneralStatsForTask4>(query, new { StartDate = startDate, EndDate = endDate });

            Console.WriteLine($"Total sessions: {stats.TotalSessions}");
            Console.WriteLine($"Total duration: {stats.TotalDurationMinutes} minutes");
        }

        private static void MostPlayedMember(SqlConnection connection)
        {
            string MostPlayedHours = @"Select TOP 1 s.durationMinutes , m.FullName From Sessions s JOIN Members m on m.Id = s.MemberId ORDER BY s.durationMinutes DESC;";
                //@"Select TOP 1 s.durationMinutes From Sessions s JOIN m.FullName From Members m on m.Id = s.MemberId "
                    var Reading = connection.Query<MemberStats>(MostPlayedHours).ToList();
            foreach (var member in Reading) { Console.WriteLine(member.ToString()); }
        }

        private static void Top3(SqlConnection connection)
        {
            string Top3 = @"Select TOP 3 g.Title ,SUM(s.DurationMinutes) From Games g Join Sessions s on s.GameId = g.Id GROUP BY g.Title ORDER BY SUM(s.DurationMinutes)";
            var Reading = connection.Query<GamesStats>(Top3).ToList();

            foreach (var games in Reading) { Console.WriteLine(games.ToString()); }
        }

        private static void Read(SqlConnection connection)
        {
            string ReadAll = @"SELECT g.Title, m.FullName, s.Date, s.DurationMinutes
                                FROM Sessions s
                                JOIN Games g ON s.GameId = g.Id
                                JOIN Members m ON s.MemberId = m.Id";

            var Reading = connection.Query<SessionsStats>(ReadAll).ToList();
            foreach (var session in Reading) { Console.WriteLine(session.ToString()); }
        }
    }
}



