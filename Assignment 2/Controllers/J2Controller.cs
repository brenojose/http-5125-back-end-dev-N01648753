using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Web.Http;
using System.Web.Http.Results;

namespace Assignment02.Controllers
{
    public class J2Controller : ApiController
    {
        //<summary>
        // Fergunsonball Ratings (J2 Problem - http://tinyurl.com/44jabx6e)
        // - Each Fergusonball player's star rating is calculated based on their points scored and fouls committed,
        // with 5 stars awarded per point and 3 stars deducted per foul.
        // - The task involves determining the count of players whose star rating exceeds 40 and assessing whether the entire team qualifies as a "gold team,"
        // where all players have a star rating greater than 40.
        // - Input consists of a positive integer N representing the total players, followed by pairs of lines indicating points and fouls for each player,
        // with both values being non-negative integers.
        //</summary>
        //<param name="N">Total number of players.</param>
        //<param name="SP1">Points scored by Player 1.</param>
        //<param name="FP1">Fouls committed by Player 1.</param>
        //<returns>
        //  Output the number of players that have a star rating greater than 40, immediately followed
        //  by a plus sign if the team is considered a gold team.
        //</returns>
        //<example>
        //  GET api/J2/1/12/4 ->
        //  ["Player Number: 1 | Points: 12| Fouls: 4 | Stars: 48 |",   <- Note that the number of stars is greater than 40, so the golden players will have a plus symbol.
        //   " ",   
        //   "| Golden players: 1+ |"]
        //</example>
        [HttpGet]
        [Route("api/J2/{N}/{SP1}/{FP1}")]
        public IHttpActionResult GetN1(int N, int SP1, int FP1)
        {
            int scoreValue = 5;
            int foulValue = -3;
            int goldTeam = 40;
            int scoreCount = scoreValue * SP1;
            int foulCount = foulValue * FP1;
            int starsP1 = scoreCount + foulCount;
            int goldenCount = 0;

            // Array to store player information
            string[] players = { "| Player Number: " + (N) + " | Points: " + SP1 + "| Fouls: " + FP1 + " | Stars: " + starsP1 + " |",
                                 " ",
                                 "| Golden players: " + goldenCount + " |"};

            // Check if Player 1's star rating is greater than 40
            if (starsP1 > goldTeam)
            {
                goldenCount += 1;
                // Check if the number of golden players equals N, giving the output asked in J2 Problem
                if (goldenCount == N)
                {
                    players[2] = "| Golden players: " + goldenCount.ToString() + "+" + " |";
                }
            }

            // Check if the number of players is not 1, to match the API route, as the question asks for
            // a non-negative number, it fills the objetive, and checks if at least one attribute is negative.
            if (N != 1 || SP1 < 0 || FP1 < 0)
            {
                return BadRequest("The number of players informed doesn't match with the length of the API route or at least one attribute is negative.");
            }

            return Ok(players);
        }

        //<summary>
        //  Endpoint to calculate the number of players with star rating greater than 40 and determine if it's a gold team.
        //</summary>
        //<param name="N">Total number of players.</param>
        //<param name="SP1">Points scored by Player 1.</param>
        //<param name="FP1">Fouls committed by Player 1.</param>
        //<param name="SP2">Points scored by Player 2.</param>
        //<param name="FP2">Fouls committed by Player 2.</param>
        //<returns>
        //  Output the number of players that have a star rating greater than 40, immediately followed
        //  by a plus sign if the team is considered a gold team.
        //</returns>
        //<example>
        //  GET api/J2/2/12/4/10/3 ->
        //  ["Player Number: 1 | Points: 12| Fouls: 4 | Stars: 48 |",
        //   "Player Number: 2 | Points: 10| Fouls: 3 | Stars: 41 |",
        //   " ",   
        //   "| Golden players: 2+ |"]
        //</example>
        //<example>
        //  GET api/J2/2/12/4/4/3 ->
        //  ["Player Number: 1 | Points: 12| Fouls: 4 | Stars: 48 |",
        //   "Player Number: 2 | Points: 4| Fouls: 3 | Stars: 11 |",
        //   " ",   
        //   "| Golden players: 1 |"]
        //</example>
        [HttpGet]
        [Route("api/J2/{N}/{SP1}/{FP1}/{SP2}/{FP2}")]
        public IHttpActionResult GetN2(int N, int SP1, int FP1, int SP2, int FP2)
        {
            int scoreValue = 5;
            int foulValue = -3;
            int goldTeam = 40;
            int scoreCount1 = scoreValue * SP1;
            int foulCount1 = foulValue * FP1;
            int scoreCount2 = scoreValue * SP2;
            int foulCount2 = foulValue * FP2;
            int starsP1 = scoreCount1 + foulCount1;
            int starsP2 = scoreCount2 + foulCount2;
            int[] playerScores = { starsP1, starsP2 };
            int goldenCount = 0;

            // Loop through player scores and count golden players
            for (int i = 0; i < playerScores.Length; i++)
            {
                if (playerScores[i] > goldTeam)
                {
                    goldenCount++;
                }
            }

            // Array to store player information
            string[] players = { "| Player Number: " + (N-1) + " | Points: " + SP1 + "| Fouls: " + FP1 + " | Stars: " + starsP1 + " |",
                                 "| Player Number: " + (N) + " | Points: " + SP2 + "| Fouls: " + FP2 + " | Stars: " + starsP2 + " |",
                                 " ",
                                 "| Golden players: " + goldenCount + " |"};


            // Check if the number of golden players equals N and changes the string determined inside the if statement
            if (goldenCount == N)
            {
                players[3] = "| Golden players: " + goldenCount.ToString() + "+" + " |";
            }

            // Check if the number of players(N) is not 2 or if exists non-negative attributes, returning a badrequest
            if (N != 2 || SP1 < 0 || FP1 < 0 || SP2 < 0 || FP2 < 0)
            {
                return BadRequest("The number of players informed doesn't match with the length of the API route or at least one attribute is negative.");
            }

            return Ok(players);
        }

        //<summary>
        //  Endpoint to calculate the number of players with star rating greater than 40 and determine if it's a gold team.
        //</summary>
        //<param name="N">Total number of players.</param>
        //<param name="SP1">Points scored by Player 1.</param>
        //<param name="FP1">Fouls committed by Player 1.</param>
        //<param name="SP2">Points scored by Player 2.</param>
        //<param name="FP2">Fouls committed by Player 2.</param>
        //<param name="SP3">Points scored by Player 3.</param>
        //<param name="FP3">Fouls committed by Player 3.</param>
        //<returns>
        //  Output the number of players that have a star rating greater than 40, immediately followed
        //  by a plus sign if the team is considered a gold team.
        //</returns>
        //<example>
        //  GET api/J2/3/12/4/10/5/2/4 ->
        //  ["| Player Number: 1 | Points: 12| Fouls: 4 | Stars: 48 |",
        //   "| Player Number: 2 | Points: 10| Fouls: 5 | Stars: 35 |",
        //   "| Player Number: 3 | Points: 5 | Fouls: 2 | Stars: -2 |",
        //   " ",
        //   "| Golden players: 1 |"]
        //</example>
        [HttpGet]
        [Route("api/J2/{N}/{SP1}/{FP1}/{SP2}/{FP2}/{SP3}/{FP3}")]
        
        public IHttpActionResult GetN2(int N, int SP1, int FP1, int SP2, int FP2, int SP3, int FP3)
        {
            // Initialize variables
            int scoreValue = 5;
            int foulValue = -3;
            int goldTeam = 40;

            // Calculate the score and foul counts for each player
            int scoreCount1 = scoreValue * SP1;
            int foulCount1 = foulValue * FP1;
            int scoreCount2 = scoreValue * SP2;
            int foulCount2 = foulValue * FP2;
            int scoreCount3 = scoreValue * SP3;
            int foulCount3 = foulValue * FP3;

            // Calculate the star ratings for each player
            int starsP1 = scoreCount1 + foulCount1;
            int starsP2 = scoreCount2 + foulCount2;
            int starsP3 = scoreCount3 + foulCount3;

            // Store the star ratings in an array
            int[] playerScores = { starsP1, starsP2, starsP3 };
            int goldenCount = 0;

            // Count the number of players with star ratings greater than 40
            for (int i = 0; i < playerScores.Length; i++)
            {
                if (playerScores[i] > goldTeam)
                {
                    goldenCount++;
                }
            }

            // Create an array to store player information
            string[] players = { "| Player Number: " + (N - 2) + " | Points: " + SP1 + "| Fouls: " + FP1 + " | Stars: " + (scoreCount1 + foulCount1) + " |",
                                 "| Player Number: " + (N - 1) + " | Points: " + SP2 + "| Fouls: " + FP2 + " | Stars: " + (scoreCount2 + foulCount2) + " |",
                                 "| Player Number: " + (N) + " | Points: " + SP3 + "| Fouls: " + FP3 + " | Stars: " + (scoreCount3 + foulCount3) + " |",
                                 " ",
                                 "| Golden players: " + goldenCount + " |"};

            // Check if all players are golden players
            if (goldenCount == N)
            {
                players[4] = "| Golden players: " + goldenCount.ToString() + "+" + " |";
            }

            // Check if the number of players is not 3, to match the API route, as the question asks for
            // a non-negative number, it fills the objetive, and checks if at least one attribute is negative.
            if (N != 3 || SP1 < 0 || FP1 < 0 || SP2 < 0 || FP2 < 0 || SP3 < 0 || FP3 < 0)
            {
                return BadRequest("The number of players informed doesn't match with the length of the API route or at least one attribute is negative.");
            }

            return Ok(players);
        }
    }
}


