using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyProjectW2024.Controllers
{
    public class AddTenController : ApiController
    {   //<summary>
        //  Receive a request to localhost: xx/api/AddTen/{id}
        //</summary>
        //<param name="id"> integer value </param>
        //<returns>
        //  Returns the result of adding 10 units to the input number.
        //</returns>
        //
        //<example>
        //  GET api/addten/10 -> "20"
        //  GET api/addten/25 -> "35"
        //  GET api/addten/50 -> "60"
        //</example>

        public int Get(int id)
        {
            int plusTen = id + 10;
            return plusTen;
        }
    }
}
