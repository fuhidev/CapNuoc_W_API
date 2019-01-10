using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Profile
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Role { get; set; }
        public List<String> Capabilities { get; set; }
    }
}