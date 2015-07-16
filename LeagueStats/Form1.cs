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
namespace LeagueStats
{
    public partial class LeagueStats : Form
    {
        #region Global Variables

        static string SummonerName;
        static User currentUser = new User();

        #endregion
        public LeagueStats()
        {
            InitializeComponent();

        }
        #region MenuStrip
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AboutMessage = String.Format("LeagueStats\r\nCreatd By Brian Richardson");
            MessageBox.Show(AboutMessage);
        }
        #endregion
        #region methods
        public static void CallAPI()
        {
            
            //Creates WebClient
            var Client = new WebClient();
            //Gets Summoner information | name, id, profileIconId, summonerLevel, revisionDate
            try
            {
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
            catch
            {
                MessageBox.Show("There was an error. Perhaps try another username");
            }
            
        }
        public static void Display(PictureBox iconBox, Label nameLabel)
        {
            //Changes name label & makes it visible
            nameLabel.Visible = true;
            nameLabel.Text = currentUser.name;
            //changes iconBox image location
            iconBox.ImageLocation = "http://ddragon.leagueoflegends.com/cdn/5.2.1/img/profileicon/" + currentUser.profileIconId + ".png";
        }
        #endregion

        #region Form Controlls
        private void searchButton_Click(object sender, EventArgs e)
        {
            //Gets summoner name
            SummonerName = searchBox.Text.ToLower();
            //Runs the CallAPI method
            CallAPI();
            //Displays the data using the Display method
            Display(iconBox,nameLabel);

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
    }
    public class User
    {
        public string id;
        public string name;
        public string profileIconId;
        public string revisionDate;
        public string summonerLevel;
    }
}
