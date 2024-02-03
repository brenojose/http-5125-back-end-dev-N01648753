using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyProjectW2024.Controllers
{
    public class NumberMachineController : ApiController
    {
        //<summary>
        //  Receives a GET request to localhost: xx/api/numbermachine/{id}
        //  Uses four mathematical operations on it using integers.
        //</summary>
        //<param name="id"> Integer value. </param>
        //<returns>
        //  Returns a string containing the result of four mathematical operations:
        //  - Square of the number
        //  - Doubled value of the number
        //  - Half of the number
        //  - Cube of the number
        //</returns>
        //
        //<example>
        //  GET xx/api/numbermachine/10 -> The number machine gives you: Square number = 100, The doubled value = 20, Half of the number = 5 and the cube of the number = 1000
        //  GET xx/api/numbermachine/25 -> The number machine gives you: Square number = 625, The doubled value = 50, Half of the number = 12 and the cube of the number = 15625
        //  GET xx/api/numbermachine/50 -> The number machine gives you: Square number = 2500, The doubled value = 100, Half of the number = 25 and the cube of the number = 125000
        //</example>

        public string Get(int id)
        {
            int valueSquare = id * id;
            int valueDouble = id + id;
            int valueHalf = id / 2;
            int valueCube = id * id * id;
            string nMach = "The number machine gives you:" +
                " Square number = " + valueSquare +
                ", The doubled value = " + valueDouble +
                ", Half of the number = " + valueHalf +
                " and the cube of the number = " + valueCube;
            return nMach;
        }
    }
}
