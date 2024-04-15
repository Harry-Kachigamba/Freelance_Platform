using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freelance_Platform_Final.Models
{
    public class ClientProfile : Profile
    {
        public string PreviousFreelancer { get; set; }
        public string Company { get; set; }
        public string Interests { get; set; }
    }
}
