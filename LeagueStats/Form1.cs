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
using System.Threading;
namespace LeagueStats
{
    public partial class LeagueStats : Form
    {
        //Startup
        public LeagueStats()
        {
            InitializeComponent();
            Color themeColor = new Color();
            themeColor = Color.FromArgb(13, 98, 162);
            //Colors everything
            this.BackColor = themeColor;
            searchBox.BackColor = themeColor;
            regionBox.BackColor = themeColor;
        }

        #region Global Variables

        static string _datadragonVersion = "5.13.1";
        static string _SummonerName;
        
        //Creates the threads for collecting data
        Thread _OverviewTab = new Thread(new ThreadStart(CallAPI_ranked));
        Thread _MatchHistoryTab = new Thread(new ThreadStart(CallAPI_rankhistory));

    //creates instances of the user classes
        //List to hold  match history
        static List<User_rankhistory> RankMatchHistory = new List<User_rankhistory>();
        static User_basic currentUser = new User_basic();
        static User_ranked rankUser = new User_ranked();

        #endregion

        #region Methods
        //Calls for data from servers

        #region CallAPI
        //Gets basic info like name & level
        public static void CallAPI_basic()
        {

            //Creates WebClient
            using (var Client = new WebClient())
            {
                Client.Proxy = null;
                //Gets Summoner information | name, id, profileIconId, summonerLevel, revisionDate
                string url = ("https://na.api.pvp.net/api/lol/na/v1.4/summoner/by-name/" + _SummonerName.Replace(" ", "%20") + "?api_key=" + _apikey);
                var SummonerData = Client.DownloadString(url);

                //Puts summoner information into json
                JObject jsonSummonerData = JObject.Parse(SummonerData);
                //Removes spaces from variable _SummonerName 
                _SummonerName = _SummonerName.Replace(" ", "");

                //Sets currentusers properties to the correct json ones;
                currentUser.name = (string)(jsonSummonerData[_SummonerName]["name"]);
                currentUser.id = (string)(jsonSummonerData[_SummonerName]["id"]);
                currentUser.profileIconId = (string)(jsonSummonerData[_SummonerName]["profileIconId"]);
                currentUser.summonerLevel = (string)(jsonSummonerData[_SummonerName]["summonerLevel"]);
                currentUser.revisionDate = (string)(jsonSummonerData[_SummonerName]["revisionDate"]);
            }

        }

        //Gets ranked info
        public static void CallAPI_ranked()
        {
            try
            {
                //creates web client
                var Client = new WebClient();
                Client.Proxy = null;
                //makes the url and downloads the json string
                string url = ("https://na.api.pvp.net/api/lol/na/v2.5/league/by-summoner/" + currentUser.id + "?api_key=" + _apikey);
                var rankedData = Client.DownloadString(url);
                //creates an object to hold the json string and parses it
                //If you ever need to fix this just rewrite it (logic too complicated to comment)
                JObject tempjsonLeagueData = JObject.Parse(rankedData);
                JArray jsonLeagueData = JArray.Parse(tempjsonLeagueData[currentUser.id].ToString());
                JArray jsonRankData = JArray.Parse(jsonLeagueData[0]["entries"].ToString());

                //Get array line for data
                int arrayLength = jsonRankData.Count();
                int correctArrayLine = 0;

                for (int line = 0; line < arrayLength; line++)
                {
                    if ((string)jsonRankData[line]["playerOrTeamId"] == currentUser.id)
                    {
                        correctArrayLine = line;
                        break;
                    }
                }

                rankUser.league = (string)jsonLeagueData[0]["name"];
                rankUser.tier = (string)jsonLeagueData[0]["tier"];
                rankUser.division = (string)jsonRankData[correctArrayLine]["division"];
                rankUser.wins = (string)jsonRankData[correctArrayLine]["wins"];
                rankUser.losses = (string)jsonRankData[correctArrayLine]["losses"];
                rankUser.leaguePoints = (string)jsonRankData[correctArrayLine]["leaguePoints"];

                //Closes Client session
                Client.Dispose();
            }
            catch
            {

            }

        }

        //Gets Ranked Match History
        public static void CallAPI_rankhistory()
        {
            
            //creates client to get json info
            var Client = new WebClient();
            //creates url to get json info from
            string url = String.Format("https://na.api.pvp.net/api/lol/na/v2.2/matchhistory/" + currentUser.id + "?rankedQueues=RANKED_SOLO_5x5&api_key=" + _apikey);
            //downloads data
            var rankhistory = Client.DownloadString(url);
            JObject tempjsonRankHistory = JObject.Parse(rankhistory);
            JArray jsonRankHistory = JArray.Parse(tempjsonRankHistory["matches"].ToString());
            
            //Puts all the data into the User_rankhistory list backwards so that 0 is the most recent match
            for (int i = jsonRankHistory.Count() - 1; i >= 0; i--)
            {
                //Preps data that needs prepping
                string champ_id = jsonRankHistory[i]["participants"][0]["championId"].ToString();
                string champ_url = String.Format("https://global.api.pvp.net/api/lol/static-data/na/v1.2/champion/" + champ_id + "?api_key=" + _apikey);
                var champ_info = Client.DownloadString(champ_url);
                JObject ChampData = JObject.Parse(champ_info);
                string champ_name = ChampData["name"].ToString();

                string winner = jsonRankHistory[i]["participants"][0]["stats"]["winner"].ToString();
                string role= jsonRankHistory[i]["participants"][0]["timeline"]["role"].ToString();
                string lane = jsonRankHistory[i]["participants"][0]["timeline"]["lane"].ToString();
                string level = jsonRankHistory[i]["participants"][0]["stats"]["champLevel"].ToString();
                string kills = jsonRankHistory[i]["participants"][0]["stats"]["kills"].ToString();
                string deaths = jsonRankHistory[i]["participants"][0]["stats"]["deaths"].ToString();
                string assists = jsonRankHistory[i]["participants"][0]["stats"]["assists"].ToString();
                string gold = jsonRankHistory[i]["participants"][0]["stats"]["goldEarned"].ToString();
                string cs = jsonRankHistory[i]["participants"][0]["stats"]["minionsKilled"].ToString();
                string wards = jsonRankHistory[i]["participants"][0]["stats"]["sightWardsBoughtInGame"].ToString();
                string visionwards = jsonRankHistory[i]["participants"][0]["stats"]["visionWardsBoughtInGame"].ToString();
                string towers = jsonRankHistory[i]["participants"][0]["stats"]["towerKills"].ToString();
              
                //Adds the data to the list
                RankMatchHistory.Add(new User_rankhistory(winner,role,lane,level,kills,deaths,assists,gold,cs,wards,visionwards,towers,champ_id,champ_name));
            }
            Client.Dispose();

        }
        

        #endregion

        //Called when the search button is pressed
        #region Search
        public static void Search(TextBox searchBox, PictureBox iconBox, Label nameLabel, Label levelLabel, Label winlossLabel, Label rankLabel, Label lpLabel)
        {
            //Gets summoner name
            _SummonerName = searchBox.Text.ToLower();
            try
            {
                //Runs the CallAPI_basic method
                CallAPI_basic();
                //Runs the CallAPI_ranked method if currentUser level == 30
                if (Convert.ToInt32(currentUser.summonerLevel) == 30) { CallAPI_ranked(); CallAPI_rankhistory(); }

                //Displays the data using the Display method
                Display_Overview(true, iconBox, nameLabel, levelLabel, winlossLabel, rankLabel, lpLabel);

            }
            catch
            {
                MessageBox.Show("There was an error. Perhaps try another username");
            }
            
        }
        #endregion

        //Displays Data
        #region Display
        
        public static void Display_Overview(bool visible, PictureBox iconBox, Label nameLabel, Label levelLabel, Label winlossLabel, Label rankLabel, Label lpLabel)
        {

    //STARTUP
            //Resets the displayed items
            iconBox.Visible = false;
            nameLabel.Visible = false;
            levelLabel.Visible = false;
            winlossLabel.Visible = false;
            rankLabel.Visible = false;
            lpLabel.Visible = false;

    //BASIC INFO

            //Changes name label & makes it visible
            nameLabel.Visible = true;
            nameLabel.Text = currentUser.name;

            //changes iconBox image location
            iconBox.Visible = true;
            iconBox.ImageLocation = "http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/profileicon/" + currentUser.profileIconId + ".png";

            //Change levelLabel
            levelLabel.Visible = true;
            levelLabel.Text = String.Format("Level {0}", currentUser.summonerLevel);

    //RANKED
            if (Convert.ToInt32(currentUser.summonerLevel) == 30)
            {
                //Change winlossLabel
                winlossLabel.Visible = visible;
                double wins = Convert.ToInt32(rankUser.wins);
                double losses = Convert.ToInt32(rankUser.losses);
                double winrate = (wins / (wins + losses) * 100);
                string winrate_string = String.Format("{0:0.00}", winrate);
                winlossLabel.Text = String.Format(
                    "Wins: {0}\r\n" +
                    "Losses: {1}\r\n" +
                    "Winrate: {2}%",
                    wins, losses, winrate_string);

                //change rankLabel
                rankLabel.Visible = visible;
                string rank = string.Format("{0} {1}", rankUser.tier, rankUser.division);
                rankLabel.Text = rank;

                //change lpLabel
                lpLabel.Visible = visible;
                string lp = string.Format("{0} LP", rankUser.leaguePoints);
                lpLabel.Text = lp;
            }
            else
            {
                //set rankLabel to say "UNRANKED"
                rankLabel.Visible = visible;
                rankLabel.Text = "Unranked";
            }
        }

        public static void Display_MatchHistory(PictureBox champPic, Label winLabel, Label jobLabel, Label kdaLabel, Label levelLabel, Label goldLabel, Label csLabel, Label towerLabel, Label wardLabel, Label visionwardLabel)
        {
    //ChampPic
            string image_url = String.Format("http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/champion/" + RankMatchHistory[0].champ + ".png");
            champPic.ImageLocation = image_url;
    //WinLabel
            if (RankMatchHistory[0].winner == "true") { winLabel.Text = "Win"; } 
            else { winLabel.Text = "Loss"; }
    //Joblabel
            #region JobLabel
            //BOTTOM
            if (RankMatchHistory[0].lane == "BOTTOM")
            {
                switch (RankMatchHistory[0].role)
                {
                    case ("DUO_CARRY"):
                        jobLabel.Text = "ADC";
                        break;
                    case ("DUO_SUPPORT"):
                        jobLabel.Text = "Support";
                        break;
                }
            }
        //MID
            if (RankMatchHistory[0].lane == "MIDDLE")
            {
                jobLabel.Text = "Mid";
            }
        //TOP
            if (RankMatchHistory[0].lane == "TOP")
            {
                jobLabel.Text = "Top";
            }
        //JUNGLE
            if (RankMatchHistory[0].lane == "NONE")
            {
                jobLabel.Text = "Jungle";
            }
            #endregion
    //KDALabel
            string kills = RankMatchHistory[0].kills;
            string deaths = RankMatchHistory[0].deaths;
            string assists = RankMatchHistory[0].assists;
            string kda = String.Format("{0}/{1}/{2}",kills,deaths,assists);
            kdaLabel.Text = kda;
    //LevelLabel
            string level = RankMatchHistory[0].level;
            levelLabel.Text = String.Format("Level: {0}", level);
    //goldLabel
            string gold = RankMatchHistory[0].gold;
            goldLabel.Text = String.Format("Gold Earned: {0}", gold);
    //csLabel
            string cs = RankMatchHistory[0].cs;
            csLabel.Text = String.Format("Minions Killed: {0}", cs);
    //towerLabel
            string towers = RankMatchHistory[0].towers;
            towerLabel.Text = String.Format("Towers Destroyed: {0}",towers);
    //wardLabel
            string wards = RankMatchHistory[0].towers;
            wardLabel.Text = String.Format("Wards Placed: {0}", wards);
    //visionwardLabel
            string visionwards = RankMatchHistory[0].visionwards;
            visionwardLabel.Text = String.Format("Pink Wards Placed: {0}", visionwards);

        }

        #endregion

        #endregion

        #region Form Controlls
        #region Searching
        private void searchButton_Click(object sender, EventArgs e)
        {
            Search(searchBox, iconBox, nameLabel, levelLabel, winlossLabel, rankLabel, lpLabel);
            tabControl.SelectTab("overviewTab");
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
        #endregion

        #region Tabs
        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            //When Clicked gets match history data
            //CallAPI_rankhistory();
            Display_MatchHistory(champPic1, winLabel1, jobLabel1, kdaLabel1, levelLabel1, goldLabel1, csLabel1, towerLabel1, wardLabel1, visionwardLabel1);
        }

        #endregion

        #region MenuStrip
        private void meToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AboutMessage = String.Format("LeagueStats\r\nCreatd By Brian Richardson AKA FoodIsBeast");
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

        static string _apikey = "c19aabb4-0d8e-44c1-ae83-4b03249382e9";
    }
    //Bases for Users
    #region Users
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
    public class User_rankhistory
    {
        public string winner;        
        public string role;
        public string lane;
        public string level;
        public string kills;
        public string deaths;
        public string assists;
        public string gold;
        public string cs;
        public string wards;
        public string visionwards;
        public string towers;
        public string champId;
        public string champ;
        
        //Constructors
        public User_rankhistory(string winner, string role, string lane, string level, string kills, string deaths, string assists, string gold, string cs, string wards, string visionwards, string towers, string champId, string champ)
        {
            this.winner = winner;
            this.role = role;
            this.lane = lane;
            this.level = level;
            this.kills = kills;
            this.deaths = deaths;
            this.assists = assists;
            this.gold = gold;
            this.cs = cs;
            this.wards = wards;
            this.visionwards = visionwards;
            this.towers = towers;
            this.champId = champId;
            this.champ = champ;
        }

        //public string Winner { get { return winner; } set { winner = value; } }
        
    }
#endregion
}
