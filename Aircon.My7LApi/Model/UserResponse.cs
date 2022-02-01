using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.My7LApi.Model
{

    public class UserRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("officeId")]
        public string OfficeId { get; set; }
        [JsonProperty("officePrefix")]
        public string OfficePrefix { get; set; }
    }
    public class UserResponse
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public Result[] results { get; set; }
        public string message { get; set; }
    }

    public class Result
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string WorkPhone { get; set; }
        public string WorkEmail { get; set; }
        public Office Office { get; set; }
        public Accesslevel[] AccessLevel { get; set; }
    }

    public class Office
    {
        public string Name { get; set; }
        public string Prefix { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string Logo { get; set; }
    }

    public class Accesslevel
    {
        public string LevelName { get; set; }
        public Access Access { get; set; }
    }

    public class Access
    {
        public bool Add { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
    }


}
