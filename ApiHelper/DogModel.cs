using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace ApiHelper
{
    public class DogModel
    {
        public string race { get; set; }

        [JsonProperty("message")]
        public string src { get; set; }
       

    }
}
