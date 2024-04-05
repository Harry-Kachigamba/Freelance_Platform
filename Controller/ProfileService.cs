using Freelance_Platform_Final.Models;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Platform_Final.Controller
{
    public class ProfileService
    {
        private string connectionString = "server=localhost; port=3307; user id=root; password=12345; database=Freelance";

        public async Task<List<ClientProfile>> GetClientProfilesAsync()
        {
            var profiles = new List<ClientProfile>();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM ClientProfile", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            profiles.Add(new ClientProfile
                            {
                                Firstname = reader.GetString("Firstname"),
                                Lastname = reader.GetString("Lastname"),
                                Email = reader.GetString("Email"),
                                Phone = reader.GetString("Phone"),
                                Username = reader.GetString("Username"),
                                Country = reader.GetString("Country"),
                                District = reader.GetString("District"),
                                PreviousFreelancer = reader.GetString("PreviousFreelancer"),
                                Company = reader.GetString("Company"),
                                Interests = reader.GetString("Interests")
                            });
                        }
                    }
                }
            }
            return profiles;
        }
        
        public async Task<List<FreelancerProfile>> GetFreelancerProfilesAsync()
        {
            var profiles = new List<FreelancerProfile>();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("SELECT * FROM FreelancerProfile", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            profiles.Add(new FreelancerProfile
                            {
                                Firstname = reader.GetString("Firstname"),
                                Lastname = reader.GetString("Lastname"),
                                Email = reader.GetString("Email"),
                                Phone = reader.GetString("Phone"),
                                Username = reader.GetString("Username"),
                                Country = reader.GetString("Country"),
                                District = reader.GetString("District"),
                                Skills = reader.GetString("Skills"),
                                Expertise = reader.GetString("Expertise"),
                                PastWork = reader.GetString("PastWork")
                            });
                        }
                    }
                }
            }
            return profiles;
        }
    }


}
