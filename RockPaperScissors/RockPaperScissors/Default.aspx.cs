using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace RockPaperScissors
{
    public partial class _Default : Page
    {
        string[] tournamentResults;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Boolean fileOK = false;
                String path = Server.MapPath("~/UploadedImages/");
                String message = "";
                if (fileuploader.HasFile)
                {
                    String fileExtension = System.IO.Path.GetExtension(fileuploader.FileName).ToLower();
                    if (fileExtension == ".txt")
                    {
                        fileOK = true;
                    }
                }

                if (fileOK)
                {
                    try
                    {
                        string inputContent="";
                        //process uploaded file as a string
                        using (StreamReader inputStreamReader = new StreamReader(fileuploader.PostedFile.InputStream))
                        {
                            string line;
                            while ((line = inputStreamReader.ReadLine()) != null)
                            {
                                inputContent += line.Replace(@"\","");
                            }
                        }

                        // process string as a json
                        string[] players = processChampionship(inputContent).ToArray();

                        while (players.Length > 2)
                        {
                            string newContent = "";
                            List<string> tem = new List<string>();
                            for (int i = 0; i < players.Length - 1; i=i+2)
                            {
                                string item = "[" + players[i] + "," + players[i + 1] + "]";
                                tem.Add( processjson(item));
                            }
                            players = tem.ToArray();
                        }
                        string tempWinner = processjson("[" + players[0] + "," + players[1] + "]");
                        string winner = "The List " + tempWinner + "wins.";
                        string winnername = tempWinner.Split(',')[0].Substring(2, tempWinner.Split(',')[0].Length - 3);
                        new SQLConector().insertWinner(winnername, 3);
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + winner + "');", true);
                        
                    }
                    catch (Exception ex)
                    {
                        message = "Uploading file fail";
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                    }
                }
                else
                {
                    message = "File must be txt";
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + message + "');", true);
                }
            }
        }

        /// <summary>
        /// converts json to a string to be processed
        /// </summary>
        /// <param name="json">string containing the json</param>
        /// <returns></returns>
        private string processjson(string json)
        {
            string winnername = "";
            try
            {
                String[][] players = JsonConvert.DeserializeObject<String[][]>(json);
                String firstPlayerOption = players[0][1];
                bool firstPlayerWins = false;

                if (players.Length % 2 != 0)
                    throw new Exception("The number of players is odd ");

                switch (firstPlayerOption)
                {
                    case "R":
                        firstPlayerWins = firstplayeroptionRock(players[1][1]);
                        break;
                    case "P":
                        firstPlayerWins = firstplayeroptionPaper(players[1][1]);
                        break;
                    case "S":
                        firstPlayerWins = firstplayeroptionScissors(players[1][1]);
                        break;
                    default:
                        throw new Exception("There is a player using an invalid strategy");                        
                }

                if (firstPlayerWins)
                    winnername = "[\"" +players[0][0] + "\", \"" + players[0][1] + "\"] ";
                else
                    winnername = "[\"" + players[1][0] + "\", \"" + players[1][1] + "\"]";
                
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + ex.Message + "');", true);
            }

            return winnername;
        }

        /// <summary>
        /// Returns true when the first player wins
        /// </summary>
        /// <param name="secondplayeroption">second player option</param>
        /// <returns></returns>
        private bool firstplayeroptionRock(string secondplayeroption)
        {
            if (secondplayeroption == "R" || secondplayeroption == "S")
            {
                return true;
            }
            else if (secondplayeroption == "P")
            {
                return false;
            }
            else
            {
                throw new Exception("There is a player using an invalid strategy"); 
            }
        }

        /// <summary>
        /// Returns true when the first player wins
        /// </summary>
        /// <param name="secondplayeroption">second player option</param>
        /// <returns></returns>
        private bool firstplayeroptionPaper(string secondplayeroption)
        {
            if (secondplayeroption == "P" || secondplayeroption == "R")
            {
                return true;
            }
            else if (secondplayeroption == "S")
            {
                return false;
            }
            else
            {
                throw new Exception("There is a player using an invalid strategy"); 
            }
        }

        /// <summary>
        /// Returns true when the first player wins
        /// </summary>
        /// <param name="secondplayeroption">second player option</param>
        /// <returns></returns>
        private bool firstplayeroptionScissors(string secondplayeroption)
        {
            if (secondplayeroption == "S" || secondplayeroption == "P")
            {
                return true;
            }
            else if (secondplayeroption == "R")
            {
                return false;
            }
            else
            {
                throw new Exception("There is a player using an invalid strategy"); 
            }
        }

        /// <summary>
        /// Process and build the tournament
        /// </summary>
        /// <param name="championship">string containing the player and strategies</param>
        /// <returns></returns>
        private List<string> processChampionship(string championship)
        {
            String[] array = championship.Select(c => c.ToString()).ToArray();
            List<string> games = new List<string>();
            String game = "";
            bool flag =false;
            
            for (int i = 0; i < array.Length-1; i++)
            {
                if(array[i].Equals("[") && array[i+1].Equals("\"") && flag==false)
                {
                    flag = true;
                }

                if (flag == true && !(array[i].Equals("\"") && array[i+1].Equals("]")))
                {
                    game+=array[i];
                }
                else if (flag == true && (array[i].Equals("\"") && array[i + 1].Equals("]")))
                {
                    flag = false;
                    game+="\"]";
                    games.Add(game);
                    game = "";
                }


            }
            return games;
        }

    }
}