using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace CodingChallengeKneat.Models
{
    [JsonObject]
    public class StarShip 
    {
       
        public string name { get; set; }
              
        public string MGLT { get; set; }
              
        public string consumables { get; set; }
            
        public string stop { get; set; }

      


    }
}