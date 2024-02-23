using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Assignment02.Controllers
{
    public class J1Controller : ApiController
    {
        //<summary>
        // Deliv-e-droid (J1 Problem - http://tinyurl.com/5bcv6zz4)
        // - In Deliv-e-droid, gain 50 points per package delivered and lose 10 points per obstacle collision.
        // - Earn a 500-point bonus if the delivered packages exceed obstacle collisions.
        // - Input includes two non-negative integers: P for delivered packages and C for obstacle collisions.        
        //</summary>
        //<param name="P">Number of packages delivered.</param>
        //<param name="C">Number of collisions with obstacles.</param>
        //<returns>
        //  The final score based on the point system described in the problem description. 
        //</returns>
        //<example>
        // GET xx/api/J1/5/2 ->
        // 730
        //
        // GET xx/api/J1/0/10 ->
        // -100
        //</example>
        [HttpGet]
        [Route("api/J1/{P}/{C}")]
        public IHttpActionResult Get(int P, int C)
        {
            // Calculate score for delivered packages
            int deliveryScore = 50 * P;

            // Calculate penalty for collisions with obstacles
            int packageLoss = C * -10;

            // Calculate initial final score
            int F = deliveryScore + packageLoss;

            // Check if inputs are negative
            if (P < 0 || C < 0)
            {
                return BadRequest("The values can't be negative.");
            }

            // Check if bonus points are earned
            if (P > C)
            {
                F = F + 500;
            }

            return Ok(F);
        }
    }
}
