using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyProjectW2024.Controllers
{
    public class GreetingController : ApiController
    {
        //<summary>
        //  Receives a POST request to localhost: xx/api/Greeting  
        //</summary>
        //
        //<returns>
        //  Returns a predefined string
        //</returns>
        //
        //<example>
        // curl -d "" xx/api/Greeting -> "Hello World"
        //</example>

        [HttpPost]
        public string Post()
        {
            string greet = "Hello World!";
            return greet;
        }

        //<summary>
        //  Receive a request to localhost: xx/api/Greeting
        //  Use of GET method
        //  xx/api/greeting/{id}
        //</summary>
        //<param name="id"> integer value </param>
        //<returns>
        //  Returns a string with a greeting message based on the input number of people.
        //</returns>
        //
        //<example>
        //  GET api/greeting/10 -> "Greetings to 10 people!"
        //  GET api/greeting/25 -> "Greetings to 25 people!"
        //  GET api/greeting/50 -> "Greetings to 50 people!"
        //</example>

        [HttpGet()]
        public string Get(int id)
        {
            string gPeople = "Greetings to " + id + " people!";
            return gPeople;
        }
    }

}
