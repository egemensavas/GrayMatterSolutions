using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SecretCinemaRequestScrapper
{
    static class Program
    {
        public class SearchCriteria
        {
            public string DivisionStart_ { get; set; }
            public string DivisionEnd_ { get; set; }
            public string SearchStart_ { get; set; }
            public string SearchEnd_ { get; set; }
            public int MaxIndex { get; set; }
        }
        public static List<string> ParsingHelper(SearchCriteria sc, string Input)
        {
            string temp = "";
            List<string> result = new List<string>();
            if (!string.IsNullOrEmpty(sc.DivisionStart_) && !string.IsNullOrEmpty(sc.DivisionEnd_))
            {
                if (Input.IndexOf(sc.DivisionStart_) > 0 && Input.IndexOf(sc.DivisionEnd_) > 0)
                {
                    Input = Input.Substring(Input.IndexOf(sc.DivisionStart_) - 1);
                    Input = Input.Substring(0, Input.IndexOf(sc.DivisionEnd_));
                }
            }
            for (int i = 0; i < sc.MaxIndex; i++)
            {
                if (Input.IndexOf(sc.SearchStart_) > 0 && Input.IndexOf(sc.SearchEnd_) > 0)
                {
                    temp = Input.Substring(Input.IndexOf(sc.SearchStart_) + sc.SearchStart_.Length - 1);
                    temp = temp.Substring(1, temp.IndexOf(sc.SearchEnd_) - 1);
                    temp = ReplaceNonAnsiChars(temp);
                    result.Add(temp);
                    Input = Input.Substring(Input.IndexOf(temp + sc.SearchEnd_) + temp.Length - 1);
                }
                else
                    break;
            }
            return result;
        }
        public static string ParsingHelperString(SearchCriteria sc, string Input)
        {
            string temp = "";
            string result = "";
            if (!string.IsNullOrEmpty(sc.DivisionStart_) && !string.IsNullOrEmpty(sc.DivisionEnd_))
            {
                if (Input.IndexOf(sc.DivisionStart_) > 0 && Input.IndexOf(sc.DivisionEnd_) > 0)
                {
                    Input = Input.Substring(Input.IndexOf(sc.DivisionStart_) - 1);
                    Input = Input.Substring(0, Input.IndexOf(sc.DivisionEnd_));
                }
            }
            if (Input.IndexOf(sc.SearchStart_) > 0 && Input.IndexOf(sc.SearchEnd_) > 0)
            {
                temp = Input.Substring(Input.IndexOf(sc.SearchStart_) + sc.SearchStart_.Length - 1);
                temp = temp.Substring(1, temp.IndexOf(sc.SearchEnd_) - 1);
                temp = ReplaceNonAnsiChars(temp);
                result = temp;
            }
            return result;
        }
        public static string ReplaceNonAnsiChars(string input_)
        {
            string result_ = input_;
            NameValueCollection settingCollection = (NameValueCollection)ConfigurationManager.GetSection("CustomAppSettings");
            string[] allKeys = settingCollection.AllKeys;
            foreach (string key in allKeys)
                result_ = result_.Replace(key, settingCollection[key]);
            return result_;
        }
        public static SearchCriteria RequestSearchCriteria;
        public static SearchCriteria PageSearchCriteria;
        public static SearchCriteria RequestMovieSearchCriteria;
        public static SearchCriteria RequestYearSearchCriteria;
        public static SearchCriteria RequestDirectorSearchCriteria;
        public static SearchCriteria RequestIMDbIDSearchCriteria;
        public static SearchCriteria RequestAcceptableMediaSearchCriteria;
        public static SearchCriteria RequestCreateDateSearchCriteria;
        public static SearchCriteria RequestCreatedBySearchCriteria;
        public static SearchCriteria RequestBountySearchCriteria;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PageSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<strong>1-25</strong>",
                DivisionEnd_ = "<strong> Last ",
                SearchStart_ = "requests.php?page=",
                SearchEnd_ = "\">",
                MaxIndex = 50
            };
            RequestSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<tr class=\"rowa\">",
                DivisionEnd_ = "<div id=\"footer\">",
                SearchStart_ = "<a href=\"requests.php?action=view&amp;id=",
                SearchEnd_ = "\">",
                MaxIndex = 50
            };
            RequestMovieSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<h2><a href=\"requests.php\">Requests</a> &gt; Movies &gt; ",
                DivisionEnd_ = "</a></h2>",
                SearchStart_ = "<span dir=\"ltr\">",
                SearchEnd_ = "</span>"
            };
            RequestYearSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<h2><a href=\"requests.php\">Requests</a> &gt; Movies &gt; ",
                DivisionEnd_ = "</a></h2>",
                SearchStart_ = "",
                SearchEnd_ = " - <a dir"
            };
            RequestDirectorSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = " - <a dir=\"ltr\" href=\"artist.php?id=",
                DivisionEnd_ = "</h2>",
                SearchStart_ = "\">",
                SearchEnd_ = "</a>"
            };
            RequestIMDbIDSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<td class=\"label\">IMDB tt code</td>",
                DivisionEnd_ = "</tr>",
                SearchStart_ = "<td>",
                SearchEnd_ = "</td>"
            };
            RequestAcceptableMediaSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<td class=\"label\">Acceptable media</td>",
                DivisionEnd_ = "</tr>",
                SearchStart_ = "<td>",
                SearchEnd_ = "</td>"
            };
            RequestCreateDateSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<td class=\"label\">Created</td>",
                DivisionEnd_ = "<td class=\"label\">IMDB tt code</td>",
                SearchStart_ = "<span class=\"time tooltip\">",
                SearchEnd_ = "</span>"
            };
            RequestCreatedBySearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "span> by <strong><a href",
                DivisionEnd_ = "</td>",
                SearchStart_ = "\">",
                SearchEnd_ = "</a>"
            };
            RequestBountySearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<td class=\"label\">Bounty</td>",
                DivisionEnd_ = "<td class=\"label\" valign=\"top\">Fill request</td>",
                SearchStart_ = "<td id=\"formatted_bounty\">",
                SearchEnd_ = "</td>"
            };

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
