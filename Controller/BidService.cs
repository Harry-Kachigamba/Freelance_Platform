using Freelance_Platform_Final.Models.Bid_System;
using MySqlConnector;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freelance_Platform_Final.Controller
{
    public class BidService
    {
        private readonly string connectionString = "xxxxxxxxxxxxxxxxxxxxxxx";

        public async Task<List<BidProject>> GetProjectBidsAsync()
        {
            var projectbid = new List<BidProject>();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using var command = new MySqlCommand("SELECT * FROM BidProjects", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        projectbid.Add(new BidProject
                        {
                            Project = reader.GetString("Project"),
                            Price = reader.GetString("Price"),
                            Duration = reader.GetString("Duration"),
                        });
                    }
                }
            }
            return projectbid;
        }
    }
}
