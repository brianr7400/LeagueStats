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
using System.Windows.Forms.DataVisualization.Charting;

namespace LeagueStats
{
    public partial class LeagueStats : Form
    {
        //Startup
        #region Startup
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

        static string _datadragonVersion = "5.14.1";
        static string _SummonerName;

        //Controll Lists
            //summoner icon
        static List<PictureBox> _sumIcon0 = new List<PictureBox>();
        static List<PictureBox> _sumIcon1 = new List<PictureBox>();
            //items
        static List<PictureBox> _item0 = new List<PictureBox>();
        static List<PictureBox> _item1 = new List<PictureBox>();
        static List<PictureBox> _item2 = new List<PictureBox>();
        static List<PictureBox> _item3 = new List<PictureBox>();
        static List<PictureBox> _item4 = new List<PictureBox>();
        static List<PictureBox> _item5 = new List<PictureBox>();
        static List<PictureBox> _item6 = new List<PictureBox>();
            //champ icon
        static List<PictureBox> _champPic = new List<PictureBox>();
            //labels
        static List<Label> _winLabelList = new List<Label>();
        static List<Label> _jobLabelList = new List<Label>();
        static List<Label> _kdaLabelList = new List<Label>();
        static List<Label> _levelLabelList = new List<Label>();
        static List<Label> _goldLabelList = new List<Label>();
        static List<Label> _csLabelList = new List<Label>();
        static List<Label> _towerLabelList = new List<Label>();
        static List<Label> _wardLabelList = new List<Label>();
        static List<Label> _visionwardLabelList = new List<Label>();
        static List<Label> _timeLabelList = new List<Label>();

            //Bools for deciding when to do things
        static bool _BasicInfoCalled = false;
        static bool _MatchHistoryCreated = false;
        static bool _GraphComboBox = false;

        //Graphing Lists
        static List<Color> _colors = new List<Color>();
        static List<string> _dataType = new List<string>();

        //Reset graph variables

    //creates instances of the user classes
        //List to hold  match history
        static List<User_rankhistory> RankMatchHistory = new List<User_rankhistory>();
        static User_basic currentUser = new User_basic();
        static User_ranked rankUser = new User_ranked();

        #endregion

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
                _BasicInfoCalled = true;
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
                rankUser.series = "";
                if (Convert.ToInt32(rankUser.leaguePoints) == 100)
                {
                    rankUser.series = (string)jsonRankData[correctArrayLine]["miniSeries"]["progress"];
                }

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
            //Reset History incase
            RankMatchHistory.Clear();
            //creates client to get json info
            var Client = new WebClient();
            //creates url to get json info from
            string url = String.Format("https://na.api.pvp.net/api/lol/na/v2.2/matchhistory/" + currentUser.id + "?rankedQueues=RANKED_SOLO_5x5&api_key=" + _apikey);
            //downloads data
            var rankhistory = Client.DownloadString(url);
            JObject tempjsonRankHistory = JObject.Parse(rankhistory);
            JArray jsonRankHistory = JArray.Parse(tempjsonRankHistory["matches"].ToString());
            
            //Get list of champions
            string champ_url = String.Format("https://global.api.pvp.net/api/lol/static-data/na/v1.2/champion?api_key=" + _apikey);
            var champ_list = Client.DownloadString(champ_url);
            JObject jsonChamp_list = JObject.Parse(champ_list);
            List<string> ChampNames = new List<string>();
            foreach (JProperty name in jsonChamp_list["data"])
            {
                ChampNames.Add(name.Name);
            }

            //Get list of summonerSpells
            string spell_url = String.Format("https://global.api.pvp.net/api/lol/static-data/na/v1.2/summoner-spell?api_key=" + _apikey);
            var spell_list = Client.DownloadString(spell_url);
            JObject jsonspell_list = JObject.Parse(spell_list);
            List<string> SpellNames = new List<string>();
            foreach (JProperty name in jsonspell_list["data"])
            {
                SpellNames.Add(name.Name);
            }

            //Puts all the data into the User_rankhistory list backwards so that 0 is the most recent match
            for (int i = jsonRankHistory.Count() - 1; i >= 0; i--)
            {
                //Preps data that needs prepping
                string champ_id = jsonRankHistory[i]["participants"][0]["championId"].ToString();
                string champ_name = "";
                foreach (string name in ChampNames)
                {
                    string id = jsonChamp_list["data"][name]["id"].ToString();
                    if ( id == champ_id)
                    {
                        champ_name = name;
                        break;
                    }
                }

                string sum0 = jsonRankHistory[i]["participants"][0]["spell1Id"].ToString();
                string sum1 = jsonRankHistory[i]["participants"][0]["spell2Id"].ToString();
                string sum0_name = "";
                string sum1_name = "";
                foreach (string name in SpellNames)
                {
                    string id = jsonspell_list["data"][name]["id"].ToString();
                    if (id == sum0)
                    {
                        sum0_name = name;
                        break;
                    }
                }
                foreach (string name in SpellNames)
                {
                    string id = jsonspell_list["data"][name]["id"].ToString();
                    if (id == sum1)
                    {
                        sum1_name = name;
                        break;
                    }
                }
                
                string winner = jsonRankHistory[i]["participants"][0]["stats"]["winner"].ToString();
                string role= jsonRankHistory[i]["participants"][0]["timeline"]["role"].ToString();
                string lane = jsonRankHistory[i]["participants"][0]["timeline"]["lane"].ToString();
                string level = jsonRankHistory[i]["participants"][0]["stats"]["champLevel"].ToString();
                string kills = jsonRankHistory[i]["participants"][0]["stats"]["kills"].ToString();
                string deaths = jsonRankHistory[i]["participants"][0]["stats"]["deaths"].ToString();
                string assists = jsonRankHistory[i]["participants"][0]["stats"]["assists"].ToString();
                string gold = jsonRankHistory[i]["participants"][0]["stats"]["goldEarned"].ToString();
                //Adds both jung minions and lane minions
                int laneminions = (int)jsonRankHistory[i]["participants"][0]["stats"]["minionsKilled"];
                int jungminions = (int)jsonRankHistory[i]["participants"][0]["stats"]["neutralMinionsKilled"];
                string cs = (laneminions + jungminions).ToString();
                string wards = jsonRankHistory[i]["participants"][0]["stats"]["sightWardsBoughtInGame"].ToString();
                string visionwards = jsonRankHistory[i]["participants"][0]["stats"]["visionWardsBoughtInGame"].ToString();
                string towers = jsonRankHistory[i]["participants"][0]["stats"]["towerKills"].ToString();
                string item0 = jsonRankHistory[i]["participants"][0]["stats"]["item0"].ToString();
                string item1 = jsonRankHistory[i]["participants"][0]["stats"]["item1"].ToString();
                string item2 = jsonRankHistory[i]["participants"][0]["stats"]["item2"].ToString();
                string item3 = jsonRankHistory[i]["participants"][0]["stats"]["item3"].ToString();
                string item4 = jsonRankHistory[i]["participants"][0]["stats"]["item4"].ToString();
                string item5 = jsonRankHistory[i]["participants"][0]["stats"]["item5"].ToString();
                string item6 = jsonRankHistory[i]["participants"][0]["stats"]["item6"].ToString();
                string time = jsonRankHistory[i]["matchDuration"].ToString();
                //Adds the data to the list
                RankMatchHistory.Add(new User_rankhistory(winner,role,lane,level,kills,deaths,assists,gold,cs,wards,visionwards,towers,champ_id,champ_name,item0,item1,item2,item3,item4,item5,item6,sum0,sum0_name,sum1,sum1_name, time));
            }
            Client.Dispose();

        }
        
        #endregion

        //Called when the search button is pressed
        #region Search
        public static void Search(TextBox searchBox, PictureBox iconBox, Label nameLabel, Label levelLabel, Label winlossLabel, Label rankLabel, Label seriesLabel)
        {
            //Gets summoner name
            _SummonerName = searchBox.Text.ToLower();
            //Runs the CallAPI_basic method
            //Resets global bools
            _BasicInfoCalled = false;
            try { CallAPI_basic(); }
            catch (WebException e)
            {
                if (e.Response != null) { MessageBox.Show(e.Message, "API_REQUEST_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                else { MessageBox.Show("There was an error getting basic information", "API_BASIC ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); } 
            }
                //Runs the CallAPI_ranked method if currentUser level == 30
                if (Convert.ToInt32(currentUser.summonerLevel) == 30)
                {
                    try { CallAPI_ranked(); }
                    catch (WebException e)
                    {
                        if (e != null) { MessageBox.Show(e.Message, "API_REQUEST_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        else { MessageBox.Show("There was an error getting ranked information", "API_RANKED ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }
                    try { CallAPI_rankhistory(); }
                    catch(WebException e) 
                    {
                        if (e != null) { MessageBox.Show(e.Message, "API_REQUEST_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                        else { MessageBox.Show("There was an error displaying rank history", "DISPLAY_ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); } 
                    }
                    //catch { MessageBox.Show("There was an error getting Ranked History","API_RANKHISTORY ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

                //Displays the data using the Display method
                Display_Overview(true, iconBox, nameLabel, levelLabel, winlossLabel, rankLabel, seriesLabel);

            
            
        }
        #endregion

        //Displays Data
        #region Display

        #region Create Controlls
        public static Label CreateLabel(TabPage page, string labelName, Point cord, List<Label> labels, Font font)
        {
            Label newlabel = new Label();
            newlabel.Name = labelName;
            newlabel.Location = cord;
            newlabel.AutoSize = true;
            newlabel.Font = font;
            newlabel.ForeColor = Color.White;
            page.Controls.Add(newlabel);
            return newlabel;
        }
        public static PictureBox CreatePictureBox(TabPage page, string picName, Point cord, Size boxSize)
        {
            PictureBox picBox = new PictureBox();
            picBox.Name = picName;
            picBox.Location = cord;
            picBox.Size = boxSize;
            picBox.SizeMode = PictureBoxSizeMode.StretchImage;
            picBox.ImageLocation = "http://images.kaneva.com/filestore10/5483612/7216070/blac.jpg";
            picBox.BorderStyle = BorderStyle.FixedSingle;
            picBox.ErrorImage = null;
            page.Controls.Add(picBox);
            return picBox;
        }
 
        #endregion

        public static void Display_Overview(bool visible, PictureBox iconBox, Label nameLabel, Label levelLabel, Label winlossLabel, Label rankLabel, Label seriesLabel)
        {

    //STARTUP
            //Resets the displayed items
            iconBox.Visible = false;
            nameLabel.Visible = false;
            levelLabel.Visible = false;
            winlossLabel.Visible = false;
            rankLabel.Visible = false;
            seriesLabel.Visible = false;

    //BASIC INFO

            //Changes name label & makes it visible
            nameLabel.Visible = true;
            nameLabel.Text = currentUser.name;

            //changes iconBox image location
            iconBox.Visible = true;
            iconBox.ImageLocation = "http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/profileicon/" + currentUser.profileIconId + ".png";
            iconBox.BorderStyle = BorderStyle.FixedSingle;

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

                //format lp
                string lp = string.Format("{0} LP", rankUser.leaguePoints);

                //change rankLabel
                rankLabel.Visible = visible;
                string rank = string.Format("{0} {1}  {2}", rankUser.tier, rankUser.division, lp);
                rankLabel.Text = rank;


                //Series Label
                if (Convert.ToInt32(rankUser.leaguePoints) == 100)
                {
                    seriesLabel.Visible = visible;
                    string series = "Series:  ";
                    foreach (char result in rankUser.series)
                    {
                        switch (result)
                        {
                            case 'W':
                                series = series + "✔ ";
                                break;
                            case 'L':
                                series = series + "X ";
                                break;
                            case 'N':
                                series = series + "__ ";
                                break;
                        }
                    }
                    seriesLabel.Text = series;
                }
                
            }
            else
            {
                //set rankLabel to say "UNRANKED"
                rankLabel.Visible = visible;
                rankLabel.Text = "Unranked";
                seriesLabel.Visible = false;
            }
                

                
        }

        public static void Display_Create_MatchHistory(TabPage matchhistoryTab, PictureBox champPic, List<Label> label_list, List<PictureBox> itemPic, List<PictureBox> sumPic)
        {
        //INITIALIZE
            _MatchHistoryCreated = true;
            //Location to start
            //int pictureboxDrop = -210;
            int pictureboxDrop = -96;
            int labelDrop = -114;
            int itemDrop = -114;
            int sumDrop = -114;
            //tab page
            TabPage page = matchhistoryTab;

            //Starts loop to replicate
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                //Drops each item by set amount
                pictureboxDrop = pictureboxDrop + 114;
                labelDrop = labelDrop + 114;
                itemDrop = itemDrop + 114;
                sumDrop = sumDrop + 114;
            //Starts replicating items

                #region Champ Icon

                string name = "champPic" + match.ToString();
                Point location = new Point (champPic.Location.X, pictureboxDrop);
                Size size = champPic.Size;
                _champPic.Add(CreatePictureBox(page, name, location, size));
                #endregion

                #region label

                for (int current_label = 0; current_label < label_list.Count(); current_label++)
                {
                    string label_type = "";
                    switch (current_label)
                    {
                        case 0:
                            label_type = "winLabel";
                            //Creates the label with the specified properties
                            string label_name = label_type + match.ToString();
                            location = new Point(label_list[current_label].Location.X, label_list[current_label].Location.Y + labelDrop);
                            Font current_font = label_list[current_label].Font;
                            string text = match.ToString();
                            //adds it to the list
                            _winLabelList.Add(CreateLabel(page, label_name, location, label_list, current_font));
                            break;
                        case 1:
                            label_type = "jobLabel";
                            //Creates the label with the specified properties
                            label_name = label_type + match.ToString();
                            location = new Point(label_list[current_label].Location.X, label_list[current_label].Location.Y + labelDrop);
                            current_font = label_list[current_label].Font;
                            //adds it to the list
                            _jobLabelList.Add(CreateLabel(page, label_name, location, label_list, current_font));
                            break;
                        case 2:
                            label_type = "kdaLabel";
                            //Creates the label with the specified properties
                            label_name = label_type + match.ToString();
                            location = new Point(label_list[current_label].Location.X, label_list[current_label].Location.Y + labelDrop);
                            current_font = label_list[current_label].Font;
                            //adds it to the list
                            _kdaLabelList.Add(CreateLabel(page, label_name, location, label_list, current_font));
                            break;
                        case 3:
                            label_type = "levelLabel";
                            //Creates the label with the specified properties
                            label_name = label_type + match.ToString();
                            location = new Point(label_list[current_label].Location.X, label_list[current_label].Location.Y + labelDrop);
                            current_font = label_list[current_label].Font;
                            //adds it to the list
                            _levelLabelList.Add(CreateLabel(page, label_name, location, label_list, current_font));
                            break;
                        case 4:
                            label_type = "goldLabel";
                            //Creates the label with the specified properties
                            label_name = label_type + match.ToString();
                            location = new Point(label_list[current_label].Location.X, label_list[current_label].Location.Y + labelDrop);
                            current_font = label_list[current_label].Font;
                            //adds it to the list
                            _goldLabelList.Add(CreateLabel(page, label_name, location, label_list, current_font));
                            break;
                        case 5:
                            label_type = "csLabel";
                            //Creates the label with the specified properties
                            label_name = label_type + match.ToString();
                            location = new Point(label_list[current_label].Location.X, label_list[current_label].Location.Y + labelDrop);
                            current_font = label_list[current_label].Font;
                            //adds it to the list
                            _csLabelList.Add(CreateLabel(page, label_name, location, label_list, current_font));
                            break;
                        case 6:
                            label_type = "towerLabel";
                            //Creates the label with the specified properties
                            label_name = label_type + match.ToString();
                            location = new Point(label_list[current_label].Location.X, label_list[current_label].Location.Y + labelDrop);
                            current_font = label_list[current_label].Font;
                            //adds it to the list
                            _towerLabelList.Add(CreateLabel(page, label_name, location, label_list, current_font));
                            break;
                        case 7:
                            label_type = "wardLabel";
                            //Creates the label with the specified properties
                            label_name = label_type + match.ToString();
                            location = new Point(label_list[current_label].Location.X, label_list[current_label].Location.Y + labelDrop);
                            current_font = label_list[current_label].Font;
                            //adds it to the list
                            _wardLabelList.Add(CreateLabel(page, label_name, location, label_list, current_font));
                            break;
                        case 8:
                            label_type = "visionwardLabel";
                            //Creates the label with the specified properties
                            label_name = label_type + match.ToString();
                            location = new Point(label_list[current_label].Location.X, label_list[current_label].Location.Y + labelDrop);
                            current_font = label_list[current_label].Font;
                            //adds it to the list
                            _visionwardLabelList.Add(CreateLabel(page, label_name, location, label_list, current_font));
                            break;
                        case 9:
                            label_type = "timeLabel";
                            label_name = label_type + match.ToString();
                            location = new Point(label_list[current_label].Location.X, label_list[current_label].Location.Y + labelDrop);
                            current_font = label_list[current_label].Font;
                            //adds it to the list
                            _timeLabelList.Add(CreateLabel(page, label_name, location, label_list, current_font));
                            break;
                    }
                }

                #endregion

                #region itemPic

                for (int currentitem = 0; currentitem < itemPic.Count(); currentitem++)
                {
                    string pic_type = "itemPic";
                    string pic_name = "";
                    Point item_location = new Point(0, 0);
                    Size pic_size = new Size(1, 1);
                    pic_name = pic_type + currentitem.ToString() + match.ToString();
                    item_location = new Point(itemPic[currentitem].Location.X, itemPic[currentitem].Location.Y + itemDrop);
                    pic_size = itemPic[currentitem].Size;
                    switch (currentitem)
                    {
                        case 0:
                            _item0.Add(CreatePictureBox(page, pic_name, item_location, pic_size));
                            break;
                        case 1:
                            _item1.Add(CreatePictureBox(page, pic_name, item_location, pic_size));
                            break;
                        case 2:
                            _item2.Add(CreatePictureBox(page, pic_name, item_location, pic_size));
                            break;
                        case 3:
                            _item3.Add(CreatePictureBox(page, pic_name, item_location, pic_size));
                            break;
                        case 4:
                            _item4.Add(CreatePictureBox(page, pic_name, item_location, pic_size));
                            break;
                        case 5:
                            _item5.Add(CreatePictureBox(page, pic_name, item_location, pic_size));
                            break;
                        case 6:
                            _item6.Add(CreatePictureBox(page, pic_name, item_location, pic_size));
                            break;
                    }
                }
                #endregion

                #region sumPic

                for (int currentSum = 0; currentSum < sumPic.Count(); currentSum++)
                {
                    string pic_type = "sumPic";
                    string pic_name = pic_type + currentSum.ToString() + match.ToString();
                    Point sum_location = new Point(sumPic[currentSum].Location.X, sumPic[currentSum].Location.Y + sumDrop);
                    Size pic_size = sumPic[currentSum].Size;
                    switch (currentSum)
                    {
                        case 0:
                            _sumIcon0.Add(CreatePictureBox(page, pic_name, sum_location, pic_size));
                            break;
                        case 1: _sumIcon1.Add(CreatePictureBox(page, pic_name, sum_location, pic_size));
                            break;
                    }
                }
                #endregion
            }
        }

        public static void Display_MatchHistory(PictureBox champPic, List<Label> labels)
        {
            #region PictureBoxes

    //ChampPic
            #region ChampPic
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                string url = String.Format("http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/champion/" + RankMatchHistory[match].champ + ".png");
                _champPic[match].ImageLocation = url;
            }
            #endregion
    //Item Boxes
            #region itemBoxes
            string itemId;
            //itembox0
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                itemId = RankMatchHistory[match].item0;
                string url = "http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/item/" + itemId + ".png";
                _item0[match].ImageLocation = url;
            }

            //itembox1
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                itemId = RankMatchHistory[match].item1;
                string url = "http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/item/" + itemId + ".png";
                _item1[match].ImageLocation = url;
            }

            //itembox2
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                itemId = RankMatchHistory[match].item2;
                string url = "http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/item/" + itemId + ".png";
                _item2[match].ImageLocation = url;
            }

            //itembox3
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                itemId = RankMatchHistory[match].item3;
                string url = "http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/item/" + itemId + ".png";
                _item3[match].ImageLocation = url;
            }

            //itembox4
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                itemId = RankMatchHistory[match].item4;
                string url = "http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/item/" + itemId + ".png";
                _item4[match].ImageLocation = url;
            }

            //itembox5
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                itemId = RankMatchHistory[match].item5;
                string url = "http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/item/" + itemId + ".png";
                _item5[match].ImageLocation = url;
            }

            //itembox6
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                itemId = RankMatchHistory[match].item6;
                string url = "http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/item/" + itemId + ".png";
                _item6[match].ImageLocation = url;
            }
            #endregion
    //sumIcon
            #region sumIcon
            //sumIcon0
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                string url = String.Format("http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/spell/" + RankMatchHistory[match].sum0_name + ".png");
                _sumIcon0[match].ImageLocation = url;
            }
            //sumIcon1
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                string url = String.Format("http://ddragon.leagueoflegends.com/cdn/" + _datadragonVersion + "/img/spell/" + RankMatchHistory[match].sum1_name + ".png");
                _sumIcon1[match].ImageLocation = url;
            }
            #endregion

            #endregion

            #region Labels
    //WinLabel
                //Cycles through all the labels of the same type
                for (int match = 0; match < RankMatchHistory.Count(); match++)
                {
                    if (RankMatchHistory[match].winner == "True") { _winLabelList[match].Text = "Win"; }
                    else { _winLabelList[match].Text = "Loss"; }
                }
    //Joblabel
            #region JobLabel
            
            //Cycles through all the labels of the same type
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                //BOTTOM
                if (RankMatchHistory[match].lane == "BOTTOM")
                {
                    switch (RankMatchHistory[match].role)
                    {
                        case ("DUO_CARRY"):
                            _jobLabelList[match].Text = "ADC";
                            break;
                        case ("DUO_SUPPORT"):
                            _jobLabelList[match].Text = "Support";
                            break;
                        default:
                            _jobLabelList[match].Text = "Bot";
                            break;
                    }
                }
                //MID
                if (RankMatchHistory[match].lane == "MIDDLE")
                {
                    _jobLabelList[match].Text = "Mid";
                }
                //TOP
                if (RankMatchHistory[match].lane == "TOP")
                {
                    _jobLabelList[match].Text = "Top";
                }
                //JUNGLE
                if (RankMatchHistory[match].lane == "JUNGLE")
                {
                    _jobLabelList[match].Text = "Jungle";
                }
                
            }
            #endregion
    //KDALabel
            string kills;
            string deaths;
            string assists;
            string kda;
            
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                
                kills = RankMatchHistory[match].kills;
                deaths = RankMatchHistory[match].deaths;
                assists = RankMatchHistory[match].assists;
                kda = String.Format("{0}/{1}/{2}", kills, deaths, assists);
                _kdaLabelList[match].Text = kda;
                
            }
    //LevelLabel
            string level;
            
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                level = RankMatchHistory[match].level;
                _levelLabelList[match].Text = string.Format("Level: {0}",level);
                
            } 
    //goldLabel
            string gold;
            
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                gold = RankMatchHistory[match].gold;
                _goldLabelList[match].Text = String.Format("Gold Earned: {0}", gold);
                
            }
    //csLabel
            string cs;
            
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                cs = RankMatchHistory[match].cs;
                _csLabelList[match].Text = String.Format("Creep Score: {0}", cs);
                
            }
    //towerLabel
            string towers;
            
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                towers = RankMatchHistory[match].towers;
                _towerLabelList[match].Text = String.Format("Towers Destroyed: {0}", towers);
                
            }
    //wardLabel
            string wards;
            
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                wards = RankMatchHistory[match].wards;
                _wardLabelList[match].Text = String.Format("Wards Placed: {0}", wards);
                
            }
    //visionwardLabel
            string visionwards;
            
            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                visionwards = RankMatchHistory[match].visionwards;
                _visionwardLabelList[match].Text = String.Format("Pink Wards Placed: {0}", visionwards);

            }
    //TimeLabel

            int time;

            for (int match = 0; match < RankMatchHistory.Count(); match++)
            {
                time = Convert.ToInt32(RankMatchHistory[match].time);
                var timespan = TimeSpan.FromSeconds(time);
                _timeLabelList[match].Text = timespan.ToString(@"mm\:ss");
            }

            #endregion
    
        }

        public static void CreateSeries(Chart mainChart, ChartArea mainChartArea, SeriesChartType chartType, Color colorChoice, string datatype)
        {
            //Reset
            mainChart.Titles.Clear();
            mainChart.Series.Clear();
            Series data = new Series();
            data.Name = datatype;
            data.LabelForeColor = Color.White;
            data.ChartType = chartType;
            data.Color = colorChoice;
            data.ChartType = SeriesChartType.Line;
            //mainChart.Titles.Add(datatype);
            mainChartArea.AxisX.LabelStyle.ForeColor = Color.White;
            mainChartArea.AxisY.LabelStyle.ForeColor = Color.White;
            mainChartArea.AxisX.Title = "Match (Oldest to Latest)";
            mainChartArea.AxisX.TitleFont = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
            mainChartArea.AxisY.TitleFont = new Font("Microsoft Sans Serif", 16, FontStyle.Bold);
            mainChartArea.AxisY.Title = datatype;
            mainChart.Series.Add(data);
            mainChart.Series[0].LabelForeColor = Color.White;
            double max = 10;
            switch (datatype)
            {
                case "KDA":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match > 0; match--)
                    {
                        double kills = Convert.ToDouble(RankMatchHistory[match].kills);
                        double deaths = Convert.ToDouble(RankMatchHistory[match].deaths);
                        double assists = Convert.ToDouble(RankMatchHistory[match].assists);
                        if (deaths == 0) { deaths = 1; }
                        double kda = (kills + assists) / deaths;
                        mainChart.Series[datatype].Points.AddXY(match, kda);
                        if (kda > max) { max = kda * 1.25; }
                    }
                    break;
                case "Kills":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match >= 0; match--)
                    {
                        double kills = Convert.ToDouble(RankMatchHistory[match].kills);
                        mainChart.Series[datatype].Points.AddXY(match, kills);
                        if (kills > max) { max = kills * 1.25; }
                    }
                    break;
                case "Deaths":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match >= 0; match--)
                    {
                        double deaths = Convert.ToDouble(RankMatchHistory[match].deaths);
                        mainChart.Series[datatype].Points.AddXY(match, deaths);
                        if (deaths > max) { max = deaths * 1.25; }
                    }
                    break;
                case "Assists":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match >= 0; match--)
                    {
                        double assists = Convert.ToDouble(RankMatchHistory[match].assists);
                        mainChart.Series[datatype].Points.AddXY(match, assists);
                        if (assists > max) { max = assists * 1.25; }
                    }
                    break;
                case "Level":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match >= 0; match--)
                    {
                        double level = Convert.ToDouble(RankMatchHistory[match].level);
                        mainChart.Series[datatype].Points.AddXY(match, level);
                        if (level > max) { max = level * 1.25; }
                    }
                    break;
                case "Gold":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match >= 0; match--)
                    {
                        double gold = Convert.ToDouble(RankMatchHistory[match].gold);
                        mainChart.Series[datatype].Points.AddXY(match, gold);
                        if (gold > max) { max = gold * 1.25; }
                    }
                    break;
                case "Creep Score":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match >= 0; match--)
                    {
                        double cs = Convert.ToDouble(RankMatchHistory[match].cs);
                        mainChart.Series[datatype].Points.AddXY(match, cs);
                        if (cs > max) { max = cs * 1.25; }
                    }
                    break;
                case "Towers Destroyed":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match >= 0; match--)
                    {
                        double towers = Convert.ToDouble(RankMatchHistory[match].towers);
                        mainChart.Series[datatype].Points.AddXY(match, towers);
                        if (towers > max) { max = towers * 1.25; }
                    }
                    break;
                case "Wards Placed":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match >= 0; match--)
                    {
                        double wards = Convert.ToDouble(RankMatchHistory[match].wards);
                        mainChart.Series[datatype].Points.AddXY(match, wards);
                        if (wards > max) { max = wards * 1.25; }
                    }
                    break;
                case "Pink Wards Placed":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match >= 0; match--)
                    {
                        double visionwards = Convert.ToDouble(RankMatchHistory[match].visionwards);
                        mainChart.Series[datatype].Points.AddXY(match, visionwards);
                        if (visionwards > max) { max = visionwards * 1.25; }
                    }
                    break;
                case "Match Length":
                    max = 0;
                    for (int match = RankMatchHistory.Count() - 1; match >= 0; match--)
                    {
                        double time = Convert.ToDouble(RankMatchHistory[match].time);
                        mainChart.Series[datatype].Points.AddXY(match, time / 60);
                        if (time / 60 > max) { max = (time / 60) * 1.25; }

                    }
                    break;
                    
            }
            mainChart.ChartAreas[0].AxisY.Maximum = max;
            //mainChart.Series[datatype].Points.AddXY();
            
        }

        public static void Display_Graph(Chart mainChart, ComboBox dataSelect, ComboBox typeSelect, ComboBox colorSelect)
        {
            if (_GraphComboBox == false)
            {
                #region Prep Data


                //Adds data to the type list
                //_dataType.Add("Role");
                _dataType.Add("KDA");
                _dataType.Add("Kills");
                _dataType.Add("Deaths");
                _dataType.Add("Assists");
                _dataType.Add("Level");
                _dataType.Add("Gold");
                _dataType.Add("Creep Score");
                _dataType.Add("Towers Destroyed");
                _dataType.Add("Wards Placed");
                _dataType.Add("Pink Wards Placed");
                _dataType.Add("Match Length");

                //Adds colors to the color list
                _colors.Add(Color.Red);
                _colors.Add(Color.Orange);
                _colors.Add(Color.Yellow);
                _colors.Add(Color.Green);
                _colors.Add(Color.Blue);
                _colors.Add(Color.Indigo);
                _colors.Add(Color.Violet);

                //Adds data to the combo boxes
                foreach (Color colorChoice in _colors)
                {
                    colorSelect.Items.Add(colorChoice);
                }
                foreach (String dataChoice in _dataType)
                {
                    dataSelect.Items.Add(dataChoice);
                }
                typeSelect.Items.Add("Line Graph");
                typeSelect.Items.Add("Bar Graph");

                //Initializies combo boxes only once
                colorSelect.SelectedIndex = 0;
                typeSelect.SelectedIndex = 0;
                dataSelect.SelectedIndex = 0;
                _GraphComboBox = true;
                #endregion
            }
                //Get choices from user
            SeriesChartType typeIndex = (SeriesChartType.Line);
                if ((string)typeSelect.SelectedItem == "Line Graph") { typeIndex = SeriesChartType.Line; }
                if ((string)typeSelect.SelectedItem == "Bar Graph") { typeIndex = SeriesChartType.Bar; }
                String dataIndex = (string)dataSelect.SelectedItem;
                int colorIndex = colorSelect.SelectedIndex;
            string colorText = colorSelect.SelectedText;
                Color colorname = Color.FromName(colorText);
                if (colorname == null) { colorname = Color.Red; }

                //Create Data points
                CreateSeries(mainChart, mainChart.ChartAreas["mainChartArea"], typeIndex, colorname, dataIndex);

            }

        #endregion
  
        #endregion

        #region Form Controlls
        #region Searching
        private void searchButton_Click(object sender, EventArgs e)
        {
            Search(searchBox, iconBox, nameLabel, levelLabel, winlossLabel, rankLabel, seriesLabel);
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
        //Occurs when the tabControl changes
        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            int default_size = 394;
            int matchhistory_size = 394;
            if (RankMatchHistory.Count * 120 > matchhistory_size) { matchhistory_size = RankMatchHistory.Count * 120; }
            var currentTab = tabControl.SelectedIndex;

            //Lists
            List<Label> MatchHistoryLabel = new List<Label>();
            List<PictureBox> itemPic = new List<PictureBox>();
            List<PictureBox> sumPic = new List<PictureBox>();

            switch (currentTab)
            {
                case 0:
                    tabControl.Size = new Size(tabControl.Size.Width, default_size);
                    break;
                case 1:
                    #region Ranked History
                    //Run events
                        tabControl.Size = new Size(tabControl.Size.Width, matchhistory_size);

                        //Label List
                        
                        MatchHistoryLabel.Add(winLabel0);
                        MatchHistoryLabel.Add(jobLabel0);
                        MatchHistoryLabel.Add(kdaLabel0);
                        MatchHistoryLabel.Add(levelLabel0);
                        MatchHistoryLabel.Add(goldLabel0);
                        MatchHistoryLabel.Add(csLabel0);
                        MatchHistoryLabel.Add(towerLabel0);
                        MatchHistoryLabel.Add(wardLabel0);
                        MatchHistoryLabel.Add(visionwardLabel0);
                        MatchHistoryLabel.Add(timeLabel0);

                        //Item List
                        
                        itemPic.Add(itemPic0);
                        itemPic.Add(itemPic1);
                        itemPic.Add(itemPic2);
                        itemPic.Add(itemPic3);
                        itemPic.Add(itemPic4);
                        itemPic.Add(itemPic5);
                        itemPic.Add(itemPic6);

                        //sumPic List
                        
                        sumPic.Add(sumPic0);
                        sumPic.Add(sumPic1);

                        //Disable Match History template
                    //champPic
                        champPic0.Visible = false;
                    //labels
                        winLabel0.Visible = false;
                        jobLabel0.Visible = false;
                        kdaLabel0.Visible = false;
                        levelLabel0.Visible = false;
                        goldLabel0.Visible = false;
                        csLabel0.Visible = false;
                        towerLabel0.Visible = false;
                        wardLabel0.Visible = false;
                        visionwardLabel0.Visible = false;
                        timeLabel0.Visible = false;
                    //itemPics
                        itemPic0.Visible = false;
                        itemPic1.Visible = false;
                        itemPic2.Visible = false;
                        itemPic3.Visible = false;
                        itemPic4.Visible = false;
                        itemPic5.Visible = false;
                        itemPic6.Visible = false;
                    //sumPics
                        sumPic0.Visible = false;
                        sumPic1.Visible = false;
                    //Only display mathc history if basic api has already been called
                    if(_BasicInfoCalled == true)
                    {
                        //Only create match history tab if it isnt created yet
                        if (_MatchHistoryCreated == false) { Display_Create_MatchHistory(matchhistoryTab, champPic0, MatchHistoryLabel, itemPic, sumPic); }
                        Display_MatchHistory(champPic0, MatchHistoryLabel);
                    }
#endregion
                    break;
                case 2:
                    tabControl.Size = new Size(tabControl.Size.Width, default_size);
                    Display_Graph(mainChart, dataSelect, typeSelect, colorSelect);
                    break;
            }
        }
        #endregion

        #region MenuStrip
        private void projectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RiotMessage = ("LeagueStats isn't endorsed by Riot Games and doesn't reflect the views or opinions of Riot Games or anyone officially involved in producing or managing League of Legends. League of Legends and Riot Games are trademarks or registered trademarks of Riot Games, Inc. League of Legends © Riot Games, Inc.");
           
            MessageBox.Show(String.Format("Created by Brian Richardson\r\n\n" + RiotMessage));
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

        #region ComboBoxes
        private void dataSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_GraphComboBox == true)
            {
                Display_Graph(mainChart, dataSelect, typeSelect, colorSelect);
            }
        }

        private void colorSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_GraphComboBox == true)
            {
                Display_Graph(mainChart, dataSelect, typeSelect, colorSelect);
            }
        }

        private void typeSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_GraphComboBox == true)
            {
                Display_Graph(mainChart, dataSelect, typeSelect, colorSelect);
            }     
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
        public string series;
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
        public string item0;
        public string item1;
        public string item2;
        public string item3;
        public string item4;
        public string item5;
        public string item6;
        public string sum0;
        public string sum0_name;
        public string sum1;
        public string sum1_name;
        public string time;
        //Constructors
        public User_rankhistory(string winner, string role, string lane, string level, string kills, string deaths, string assists, string gold, string cs, string wards, string visionwards, string towers, string champId, string champ, 
        string item0, string item1, string item2, string item3, string item4, string item5, string item6,
        string sum0, string sum0_name, string sum1, string sum1_name,
        string time)
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
            this.item0 = item0;
            this.item1 = item1;
            this.item2 = item2;
            this.item3 = item3;
            this.item4 = item4;
            this.item5 = item5;
            this.item6 = item6;
            this.sum0 = sum0;
            this.sum0_name = sum0_name;
            this.sum1 = sum1;
            this.sum1_name = sum1_name;
            this.time = time;
        }        
    }
#endregion
}
