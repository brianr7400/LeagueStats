using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
namespace LeagueStats
{
    public partial class LeagueStats : Form
    {
        #region Global Variables

        static string SummonerName;
        //creates instances of the user classes
        static User_basic currentUser = new User_basic();
        static User_ranked rankUser = new User_ranked();

        #endregion
        public LeagueStats()
        {
            InitializeComponent();
            //Colors everything
            this.BackColor = Color.FromArgb(13, 98, 162);
            searchBox.BackColor = Color.FromArgb(13, 98, 162);
            regionBox.BackColor = Color.FromArgb(13, 98, 162);
        }
        
        #region methods
        //Gets basic info like name & level
        public static void CallAPI_basic()
        {
            
            //Creates WebClient
            var Client = new WebClient();
            //Gets Summoner information | name, id, profileIconId, summonerLevel, revisionDate
                string url = ("https://na.api.pvp.net/api/lol/na/v1.4/summoner/by-name/" + SummonerName.Replace(" ", "%20") + "?api_key=c19aabb4-0d8e-44c1-ae83-4b03249382e9");
                var SummonerData = Client.DownloadString(url);
                //Puts summoner information into json
                JObject jsonSummonerData = JObject.Parse(SummonerData);
                //Removes spaces from variable SummonerName 
                SummonerName = SummonerName.Replace(" ", "");

                //Sets currentusers properties to the correct json ones;
                currentUser.name = (string)(jsonSummonerData[SummonerName]["name"]);
                currentUser.id = (string)(jsonSummonerData[SummonerName]["id"]);
                currentUser.profileIconId = (string)(jsonSummonerData[SummonerName]["profileIconId"]);
                currentUser.summonerLevel = (string)(jsonSummonerData[SummonerName]["summonerLevel"]);
                currentUser.revisionDate = (string)(jsonSummonerData[SummonerName]["revisionDate"]);
            
        }
        
        //Gets ranked info
        public static void CallAPI_ranked()
        {
            try
            {
                //creates web client
                var Client = new WebClient();
                //makes the url and downloads the json string
                string url = ("https://na.api.pvp.net/api/lol/na/v2.5/league/by-summoner/" + currentUser.id + "?api_key=c19aabb4-0d8e-44c1-ae83-4b03249382e9");
                var rankedData = Client.DownloadString(url);
                //creates an object to hold the json string and parses it
                //If you ever need to fix this just rewrite it (logic too complicated to comment)
                JObject tempjsonLeagueData = JObject.Parse(rankedData);
                JArray jsonLeagueData = JArray.Parse(tempjsonLeagueData[currentUser.id].ToString());
                JArray jsonRankData = JArray.Parse(jsonLeagueData[0]["entries"].ToString());

                rankUser.league = (string)jsonLeagueData[0]["name"];
                rankUser.tier = (string)jsonLeagueData[0]["tier"];
                rankUser.division = (string)jsonRankData[0]["division"];
                rankUser.wins = (string)jsonRankData[0]["wins"];
                rankUser.losses = (string)jsonRankData[0]["losses"];
                rankUser.leaguePoints = (string)jsonRankData[0]["leaguePoints"];
            }
            catch
            {
                
            }

        }
        public static void Display(PictureBox iconBox, Label nameLabel, Label levelLabel, Label winlossLabel,Label rankLabel, Label lpLabel)
        {
            //Changes name label & makes it visible
            nameLabel.Visible = true;
            nameLabel.Text = currentUser.name;

            //changes iconBox image location
            iconBox.ImageLocation = "http://ddragon.leagueoflegends.com/cdn/5.2.1/img/profileicon/" + currentUser.profileIconId + ".png";

            //Change levelLabel
            levelLabel.Visible = true;
            levelLabel.Text = String.Format("Level {0}", currentUser.summonerLevel);

            //Change winlossLabel
            winlossLabel.Visible = true;
            double wins = Convert.ToInt32(rankUser.wins);
            double losses = Convert.ToInt32(rankUser.losses);
            double winrate = (wins / (wins+losses) * 100);
            string winrate_string = String.Format("{0:0.00}", winrate);
            winlossLabel.Text = String.Format(
                "Games won in {3}: {0}\r\n" +
                "Games lost in {3}: {1}\r\n"+ 
                "Winrate: {2}%", 
                wins,losses,winrate_string,rankUser.tier);

            //change rankLabel
            rankLabel.Visible = true;
            string rank = string.Format("{0} {1}", rankUser.tier, rankUser.division);
            rankLabel.Text = rank;

            //change lpLabel
            lpLabel.Visible = true;
            string lp = string.Format("{0} LP",rankUser.leaguePoints);
        }
        #endregion

        #region Form Controlls
        private void searchButton_Click(object sender, EventArgs e)
        {
            //Gets summoner name
            SummonerName = searchBox.Text.ToLower();
            try
            {
                //Runs the CallAPI_basic method
                CallAPI_basic();
                //Runs the CallAPI_ranked method if currentUser level = 30
                if (Convert.ToInt32(currentUser.summonerLevel) == 30) { CallAPI_ranked(); }
                //Displays the data using the Display method
                Display(iconBox, nameLabel, levelLabel, winlossLabel, rankLabel, lpLabel);
            }
            catch
            {
                MessageBox.Show("There was an error. Perhaps try another username");
            }

        }
        //Accepts enter key for searching
        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                searchButton_Click(null, null);
            }
        }
        private void searchBox_Click(object sender, EventArgs e)
        {
            //Clears searchBox when clicked
            searchBox.Clear();
        }

        #region MenuStrip
        private void meToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AboutMessage = String.Format("LeagueStats\r\nCreatd By Brian Richardson");
            MessageBox.Show(AboutMessage);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void updatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/brianr7400/LeagueStats");
        }
        #endregion

        #endregion

        
    }
    public class User_basic
    {
        public string id;
        public string name;
        public string profileIconId;
        public string revisionDate;
        public string summonerLevel;
    }
    public class User_ranked
    {
        public string tier;
        public string division;
        public string wins;
        public string losses;
        public string league;
        public string leaguePoints;
    }
}
