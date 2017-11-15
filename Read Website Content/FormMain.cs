using System;
using System.Net;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;

namespace NamespaceIMDB
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.ActiveControl = TextboxAddress;
        }

        #region Properties
        public DateTime StartDatetime { get; set; }
        public DateTime EndDatetime { get; set; }
        public SearchCriteria TitleSearchCriteria_ { get; set; }
        public SearchCriteria RuntimeSearchCriteria_ { get; set; }
        public SearchCriteria YearSearchCriteria_ { get; set; }
        public SearchCriteria DirectorSearchCriteria_ { get; set; }
        public SearchCriteria GenreSearchCriteria_ { get; set; }
        public SearchCriteria IMDBRatingSearchCriteria_ { get; set; }
        public SearchCriteria IMDBRatingCountSearchCriteria_ { get; set; }
        public SearchCriteria WriterSearchCriteria_ { get; set; }
        public SearchCriteria OriginalTitleSearchCriteria_ { get; set; }
        public SearchCriteria StarSearchCriteria_ { get; set; }
        public SearchCriteria StoryLineSearchCriteria_ { get; set; }
        public SearchCriteria AlternateStoryLineSearchCriteria_ { get; set; }
        public SearchCriteria CertificateSearchCriteria_ { get; set; }
        public SearchCriteria CountrySearchCriteria_ { get; set; }
        public SearchCriteria LanguageSearchCriteria_ { get; set; }

        public class Movie
        {
            public string ID { get; set; }
            public string Link { get; set; }
            public string Title { get; set; }
            public string Year { get; set; }
            public string Runtime { get; set; }
            public string Director { get; set; }
            public string Genre { get; set; }
            public string Keyword { get; set; }
            public string Rating { get; set; }
            public string RateCount { get; set; }
            public string Writer { get; set; }
            public string OriginalTitle { get; set; }
            public string Star { get; set; }
            public string StoryLine { get; set; }
            public string Certificate { get; set; }
            public string Country { get; set; }
            public string Language { get; set; }
        }
        public class SearchCriteria
        {
            public string DivisionStart_ { get; set; }
            public string DivisionEnd_ { get; set; }
            public string SearchStart_ { get; set; }
            public string SearchEnd_ { get; set; }
            public int MaxIndex_ { get; set; }
        }
        #endregion

        #region Events
        private void ButtonGet_Click(object sender, EventArgs e)
        {
            StartDatetime = System.DateTime.Now;
            char[] delimiterChars = { '\r', '\n' };
            string[] array_ = new string[1000];
            array_ = TextboxAddress.Text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            using (WebClient client_ = new WebClient())
            {
                Movie movie_ = new Movie();
                string siteContent_ = "";
                string siteAddress_ = "";
                DefineSearchCriteria();
                ShowResult();
                foreach (string ID_ in array_)
                {
                    siteAddress_ = "http://www.imdb.com/title/" + ID_ + "/";
                    siteContent_ = client_.DownloadString(siteAddress_);
                    TextboxContent.Text = siteContent_;
                    movie_.ID = ID_;
                    movie_.Link = siteAddress_;
                    movie_.Title = ParsingHelper(TitleSearchCriteria_, siteContent_);
                    movie_.Runtime = ParsingHelper(RuntimeSearchCriteria_, siteContent_);
                    movie_.Year = ParsingHelper(YearSearchCriteria_, siteContent_);
                    movie_.Director = ParsingHelper(DirectorSearchCriteria_, siteContent_);
                    movie_.Genre = ParsingHelper(GenreSearchCriteria_, siteContent_);
                    movie_.Keyword = GetKeywords(siteAddress_);
                    movie_.Rating = ParsingHelper(IMDBRatingSearchCriteria_, siteContent_).Replace(".", ",");
                    movie_.RateCount = ParsingHelper(IMDBRatingCountSearchCriteria_, siteContent_).Replace(",", "");
                    movie_.Writer = ParsingHelper(WriterSearchCriteria_, siteContent_);
                    movie_.OriginalTitle = ParsingHelper(OriginalTitleSearchCriteria_, siteContent_);
                    movie_.Star = ParsingHelper(StarSearchCriteria_, siteContent_);
                    movie_.StoryLine = ParsingHelper(StoryLineSearchCriteria_, siteContent_);
                    if (movie_.StoryLine == null || movie_.StoryLine.Length == 0)
                        movie_.StoryLine = ParsingHelper(AlternateStoryLineSearchCriteria_, siteContent_);
                    if (movie_.StoryLine != null && movie_.StoryLine.Length > 0)
                        movie_.StoryLine = movie_.StoryLine.Substring(1);
                    movie_.Certificate = ParsingHelper(CertificateSearchCriteria_, siteContent_) == string.Empty ? "Not Rated" : ParsingHelper(CertificateSearchCriteria_, siteContent_);
                    movie_.Country = ParsingHelper(CountrySearchCriteria_, siteContent_);
                    movie_.Language = ParsingHelper(LanguageSearchCriteria_, siteContent_);
                    ShowResult(movie_);
                }
                TextboxAddress.Text = "";
                TextboxAddress.Focus();
            }
            EndDatetime = System.DateTime.Now;
            LabelTimeElapsed.Text = (EndDatetime - StartDatetime).Seconds.ToString();
        }
        private void TextboxAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Int32)Keys.Enter)
            {
                TextboxResult.Text = "";
                ButtonGet.Focus();
            }
        }
        private void ButtonCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TextboxResult.Text);
        }
        #endregion

        #region Methods
        private string GetKeywords(string SiteAddress_)
        {
            string result_ = "";
            using (WebClient client_ = new WebClient())
            {
                string siteContent_ = "";
                string temp_ = "";
                SiteAddress_ += "keywords";
                siteContent_ = client_.DownloadString(SiteAddress_);
                siteContent_ = siteContent_.Substring(siteContent_.IndexOf("<h1 class=\"header\">Plot Keywords</h1>") - 1);
                siteContent_ = siteContent_.Substring(0, siteContent_.IndexOf("<div class=\"article\" id=\"see_also\">"));
                for (int i = 0; i < 100; i++)
                {
                    if (siteContent_.IndexOf("<a href=\"/keyword/") > 0)
                    {
                        temp_ = siteContent_.Substring(siteContent_.IndexOf("<a href=\"/keyword/"));
                        temp_ = temp_.Substring(temp_.IndexOf(">") + 1);
                        temp_ = temp_.Substring(0, temp_.IndexOf("<"));
                        if (result_.IndexOf(temp_ + ", ") == -1)
                            result_ += temp_ + ", ";
                        siteContent_ = siteContent_.Substring(siteContent_.IndexOf(temp_) + 1);
                    }
                    else
                        break;
                }
                if (result_.Length > 2)
                    result_ = result_.Substring(0, result_.Length - 2);
            }
            return result_;
        }
        private void DefineSearchCriteria()
        {
            int defaultMaxIndex_ = 20;
            TitleSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<meta property='og:title' content=\"",
                SearchEnd_ = " (",
                MaxIndex_ = 1
            };
            RuntimeSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<time itemprop=\"duration\" datetime=\"PT",
                SearchEnd_ = "M\"",
                MaxIndex_ = 1
            };
            YearSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<span id=\"titleYear\">(<a href=\"/year/",
                SearchEnd_ = "/?ref_=tt_ov_inf",
                MaxIndex_ = 1
            };
            DirectorSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "<h4 class=\"inline\">Director",
                DivisionEnd_ = "</div>",
                SearchStart_ = "<span class=\"itemprop\" itemprop=\"name\">",
                SearchEnd_ = "</span>",
                MaxIndex_ = defaultMaxIndex_
            };
            GenreSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<span class=\"itemprop\" itemprop=\"genre\">",
                SearchEnd_ = "</span>",
                MaxIndex_ = defaultMaxIndex_
            };
            IMDBRatingSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<span class=\"rating\">",
                SearchEnd_ = "<span",
                MaxIndex_ = 1
            };
            IMDBRatingCountSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<span class=\"small\" itemprop=\"ratingCount\">",
                SearchEnd_ = "</span>",
                MaxIndex_ = 1
            };
            WriterSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "<h4 class=\"inline\">Writer",
                DivisionEnd_ = "</div>",
                SearchStart_ = "<span class=\"itemprop\" itemprop=\"name\">",
                SearchEnd_ = "</span>",
                MaxIndex_ = defaultMaxIndex_
            };
            OriginalTitleSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<h1 itemprop=\"name\" class=\"\">",
                SearchEnd_ = "&nbsp;",
                MaxIndex_ = 1
            };
            StarSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "<h2>Cast</h2>",
                DivisionEnd_ = ">See full cast</a>",
                SearchStart_ = "<span class=\"itemprop\" itemprop=\"name\">",
                SearchEnd_ = "</span>",
                MaxIndex_ = defaultMaxIndex_
            };
            StoryLineSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "<h2>Storyline</h2>",
                DivisionEnd_ = "</div>",
                SearchStart_ = "            <p>",
                SearchEnd_ = "                <em class=\"nobr\">",
                MaxIndex_ = 1
            };
            AlternateStoryLineSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "<h2>Storyline</h2>",
                DivisionEnd_ = "</div>",
                SearchStart_ = "            <p>",
                SearchEnd_ = "            </p>",
                MaxIndex_ = 1
            };
            CertificateSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<span itemprop=\"contentRating\">",
                SearchEnd_ = "</span>",
                MaxIndex_ = 1
            };
            CountrySearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "<h4 class=\"inline\">Country:</h4>",
                DivisionEnd_ = "</div>",
                SearchStart_ = "itemprop='url'>",
                SearchEnd_ = "</a>",
                MaxIndex_ = defaultMaxIndex_
            };
            LanguageSearchCriteria_ = new SearchCriteria()
            {
                DivisionStart_ = "<h4 class=\"inline\">Language:</h4>",
                DivisionEnd_ = "</div>",
                SearchStart_ = "itemprop='url'>",
                SearchEnd_ = "</a>",
                MaxIndex_ = defaultMaxIndex_
            };
        }
        private void ShowResult()
        {
            TextboxResult.Text += "ID";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Link";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Title";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Runtime";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Year";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Director";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Genre";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Keyword";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Rating";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Rate Count";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Writers";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Original Title";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Stars";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Story Line";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Certificate";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Country";
            TextboxResult.Text += "\t";
            TextboxResult.Text += "Language";
            TextboxResult.Text += "\r\n";
        }
        private void ShowResult(Movie movie_)
        {
            string writeText_ = "";
            writeText_ += movie_.ID + "\t";
            writeText_ += movie_.Link + "\t";
            writeText_ += movie_.Title + "\t";
            writeText_ += movie_.Runtime + "\t";
            writeText_ += movie_.Year + "\t";
            writeText_ += movie_.Director + "\t";
            writeText_ += movie_.Genre + "\t";
            writeText_ += movie_.Keyword + "\t";
            writeText_ += movie_.Rating + "\t";
            writeText_ += movie_.RateCount + "\t";
            writeText_ += movie_.Writer + "\t";
            writeText_ += movie_.OriginalTitle + "\t";
            writeText_ += movie_.Star + "\t";
            writeText_ += movie_.StoryLine + "\t";
            writeText_ += movie_.Certificate + "\t";
            writeText_ += movie_.Country + "\t";
            writeText_ += movie_.Language + "\r\n";
            TextboxResult.Text += writeText_;
            System.IO.File.WriteAllText(@"C:\Users\egeme\Desktop\WriteText.txt", TextboxResult.Text);
        }
        private string ParsingHelper(SearchCriteria sc_, string Input_)
        {
            if (!string.IsNullOrEmpty(sc_.DivisionStart_) && !string.IsNullOrEmpty(sc_.DivisionEnd_))
            {
                if (Input_.IndexOf(sc_.DivisionStart_) > 0 && Input_.IndexOf(sc_.DivisionEnd_) > 0)
                {
                    Input_ = Input_.Substring(Input_.IndexOf(sc_.DivisionStart_) - 1);
                    Input_ = Input_.Substring(0, Input_.IndexOf(sc_.DivisionEnd_));
                }
            }
            string result_ = "";
            string temp_ = "";
            for (int i = 0; i < sc_.MaxIndex_; i++)
            {
                if (Input_.IndexOf(sc_.SearchStart_) > 0 && Input_.IndexOf(sc_.SearchEnd_) > 0)
                {
                    temp_ = Input_.Substring(Input_.IndexOf(sc_.SearchStart_) + sc_.SearchStart_.Length - 1);
                    temp_ = temp_.Substring(1, temp_.IndexOf(sc_.SearchEnd_) - 1);
                    if (result_.IndexOf(temp_ + ", ") == -1)
                        result_ += temp_ + ", ";
                    Input_ = Input_.Substring(Input_.IndexOf(temp_ + sc_.SearchEnd_) + temp_.Length - 1);
                }
                else
                    break;
            }
            if (result_.Length > 2)
                result_ = result_.Substring(0, result_.Length - 2);
            result_ = ReplaceNonAnsiChars(result_);
            return result_;
        }
        private string ReplaceNonAnsiChars(string input_)
        {
            string result_ = input_;
            NameValueCollection settingCollection = (NameValueCollection)ConfigurationManager.GetSection("CustomAppSettings");
            string[] allKeys = settingCollection.AllKeys;
            foreach (string key in allKeys)
                result_ = result_.Replace(key, settingCollection[key]);
            return result_;
        }
        #endregion
    }
}
