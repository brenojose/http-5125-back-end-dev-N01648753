using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MyProjectW2024.Controllers
{
    public class HostingCostController : ApiController
    {
        //<summary>
        //  Calculate the total hosting cost based on the number of days elapsed since the beginning of hosting.        
        //</summary>
        //<param name="id">Number of days elapsed.</param>
        //<returns>
        //  A 3 strings containing the total hosting cost breakdown:
        //  - Number of fortnights at $5.50/FN
        //  - HST (13%)
        //  - Total cost 
        //</returns>
        //<example>
        // GET xx/api/hosting/0 ->
        // “1 fortnights at $5.50/FN = $5.50 CAD”
        // “HST 13% = $0.72 CAD”
        // “Total = $6.22 CAD”
        //
        // GET xx/api/hosting/14 ->
        // “2 fortnights at $5.50/FN = $5.50 CAD”
        // “HST13% = $1.43 CAD”
        // “Total = $12.43 CAD”
        //
        // GET xx/api/hosting/15 ->
        // “2 fortnights at $5.50/FN = $5.50 CAD”
        // “HST13% = $1.43 CAD”
        // “Total = $12.43 CAD”
        //</example>

        public (string fortString1, string fortString2, string fortString3) Get(int id)
        {
            int fortN = (int)Math.Ceiling((double)(id + 1) / 14);
            double cadCost = 5.50;
            double moneyCalc = cadCost * fortN;
            double HST = 0.13;
            double HSTCalc = moneyCalc * HST;
            double totalCalc = moneyCalc + HSTCalc;

            string fortString1 = fortN + " fortnights at $" + cadCost.ToString("F2") + "/FN = $" + moneyCalc.ToString("F2") + " CAD";
            string fortString2 = "HST " + HST.ToString("P0") + " = $" + HSTCalc.ToString("F2") + " CAD";
            string fortString3 = "Total = $" + totalCalc.ToString("F2") + " CAD";

            return (fortString1, fortString2, fortString3);

        }
    }
}
