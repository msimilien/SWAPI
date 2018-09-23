using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;  
using System.Web.Mvc;
using Newtonsoft.Json;

using CodingChallengeKneat.Models; 


namespace CodingChallengeKneat.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //Hosted web API REST Service base url  
        string Baseurl = "https://swapi.co/";

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> StopList(string distance)

        {
            List<StarShip> ShipInfo = new List<StarShip>();
            using (var client = new HttpClient())
            {
                try
                {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlApi="api/starships/";
                string condition = null;
                do{
                
                HttpResponseMessage Res = await client.GetAsync(urlApi);

                
                if (Res.IsSuccessStatusCode)
                {
                     
                    var ShipResponse = Res.Content.ReadAsStringAsync().Result;
                    dynamic parsed = JsonConvert.DeserializeObject(ShipResponse);

                    
                        foreach (var item in parsed.results)
                        {
                            string name = item["name"];
                            string consumables = item["consumables"];
                            string MGLT = item["MGLT"];
                            if (!string.IsNullOrEmpty(MGLT) ||  !string.IsNullOrEmpty(consumables)) 
                            {
                                if (MGLT != "unknown" && consumables != "unknown")
                                {
                                    StarShip st = new StarShip();
                                    st.name = name;
                                    st.MGLT = MGLT;
                                    st.consumables = consumables;
                                    int totalStop = stops(st.MGLT, st.consumables, distance);
                                    st.stop = totalStop.ToString();

                                    ShipInfo.Add(st);
                                }
                              }
                           
                           
                        }
                        string nextPage = parsed["next"];
                        condition = nextPage;
                        if(!string.IsNullOrEmpty(condition)){
                         urlApi = "api/starships/";
                         urlApi =urlApi+"?"+ nextPage.Split('?')[1];
                         }
                     
                    }

                  } while (!string.IsNullOrEmpty(condition));
                return PartialView("StopList",ShipInfo);
                }

                catch (Exception ex) { return View("Error in processing data"); }
               
            }

           
        }

        public int stops(string mglt, string consumables,string distance)
        {
            double _consumables = ConvertToDay(consumables);
            double _mlgt = Convert.ToDouble(mglt);
            double _distance = Convert.ToDouble(distance);
            double _distanceHour = _distance / _mlgt;
            double _distanceDay = _distanceHour / 24;
            double _stops = Math.Round(_distanceDay / _consumables);
            int _stopReturn = Convert.ToInt32(_stops);
            return _stopReturn;

        }

        public double ConvertToDay(string value)
        {
            string time = null;
            double number = 0;
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    string[] cons = value.Split(' ');
                    if (cons.Length > 1)
                    {
                        time = cons[1];
                        number = Convert.ToDouble(cons[0]);
                    }

                }


                switch (time)
                {
                    case "years":
                        return number * 365;
                        break;
                    case "year":
                        return number * 365;
                        break;
                    case "month":
                        return number * 31;
                        break;
                    case "months":
                        return number * 31;
                        break;
                    case "week":
                        return number * 7;
                        break;
                    case "weeks":
                        return number * 7;
                        break;
                    case "day":
                        return number * 1;
                        break;
                    case "days":
                        return number * 1;
                        break;
                    default:
                        return 0;
                        break;

                }

            }
            catch (Exception ex)
            {
                return 0;
            }
                   
            
        }
    }
}