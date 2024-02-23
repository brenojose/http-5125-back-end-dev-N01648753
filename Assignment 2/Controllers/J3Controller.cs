using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment02.Controllers
{
    public class J3Controller : ApiController
    {
        //<summary>
        // Sumac Sequence (J3 Problem - http://tinyurl.com/bdz97b9z)
        //  Calculate the length of the sumac sequence given two starting numbers.
        //</summary>
        //<param name="t1">First number in the sumac sequence.</param>
        //<param name="t2">Second number in the sumac sequence.</param>
        //<returns>
        //  The length of the sumac sequence.
        //</returns>
        //<example>
        //  GET xx/api/J3/120/71 ->
        //  "The sumac sequence was: 120, 71, 49, 22, 27. It gives us a sequence of 5 numbers."
        //</example>
        [HttpGet]
        [Route("api/J3/{t1}/{t2}")]
        public IHttpActionResult Get(int t1, int t2)
        {
            int[] sumacSequence = { t1, t2 };
            int counter;
            int sumacQuantity = 2; // Initial value since we have two elements already

            List<int> sequenceList = new List<int>(sumacSequence);

            for (int i = 0; ; i++)
            {
                counter = sequenceList[i] - sequenceList[i + 1];

                if (counter <= 0)
                {
                    break;
                }

                sequenceList.Add(counter);
                sumacQuantity++;
            }

            int[] resultArray = sequenceList.ToArray();

            string SumacFinal = "The sumac sequence was: " + string.Join(", ", resultArray) + "." +
                                " It gives us a sequence of " + sumacQuantity + " numbers.";

            return Ok(SumacFinal);
        }
    }
}