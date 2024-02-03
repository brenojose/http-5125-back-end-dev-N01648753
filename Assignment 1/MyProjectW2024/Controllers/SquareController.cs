using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyProjectW2024.Controllers
{
    public class SquareController : ApiController
    {
        //<summary>
        //  Receive a request to localhost: xx/api/Square/{id}
        //  Use of int id as input  
        //</summary>
        //<param name="id"> Integer value </param>
        //<returns>
        //  Returns the square of the input number
        //</returns>
        //
        //<example>
        //  GET api/addten/10 -> "100"
        //  GET api/addten/25 -> "625"
        //  GET api/addten/50 -> "2500"
        //</example>

        public int Get (int id)
        {
            int valueSquare = id * id;
            return valueSquare;
        }
    }
}
