using System;
using System.Net;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.ComponentModel;

namespace CompleteMovieArchiveKit
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            this.ActiveControl = TextboxAddress;
            ComboBoxNumberOfItems.SelectedItem = "20";
            DataGridViewResult.BackgroundColor = SystemColors.Control;
            DataGridViewDetail.BackgroundColor = SystemColors.Control;
            DataGridViewFile.BackgroundColor = SystemColors.Control;
            DataGridViewEmptyFolder.BackgroundColor = SystemColors.Control;
            PictureBoxPoster.SizeMode = PictureBoxSizeMode.Zoom;
            BackgroundWorkerFile.DoWork += BackgroundWorkerFile_DoWork;
            BackgroundWorkerFile.RunWorkerCompleted += BackgroundWorkerFile_RunWorkerCompleted;
            BackgroundWorkerSite.DoWork += BackgroundWorkerSite_DoWork;
            BackgroundWorkerSite.RunWorkerCompleted += BackgroundWorkerSite_RunWorkerCompleted;
            BackgroundWorkerDetail.DoWork += BackgroundWorkerDetail_DoWork;
            BackgroundWorkerDetail.RunWorkerCompleted += BackgroundWorkerDetail_RunWorkerCompleted;
        }

        #region Properties
        DataTable DataTableFile;
        DataTable DataTableSite;
        DataTable DataTableDetail;
        string Plot = "";
        string ClipboardText = "";
        string FileFullPath = "";
        string FolderFullPath = "";
        string LastSearch = "";
        string SelectedID = "";
        string SelectedPath = "";
        DateTime StartDatetime { get; set; }
        DateTime EndDatetime { get; set; }
        SearchCriteria PosterBigSearchCriteria { get; set; }
        SearchCriteria TitleSearchCriteria { get; set; }
        SearchCriteria TitleSearchCriteria2 { get; set; }
        SearchCriteria IDSearchCriteria { get; set; }
        SearchCriteria YearSearchCriteria { get; set; }
        SearchCriteria PosterSmallSearchCriteria { get; set; }
        SearchCriteria RuntimeSearchCriteria { get; set; }
        SearchCriteria DirectorSearchCriteria { get; set; }
        SearchCriteria GenreSearchCriteria { get; set; }
        SearchCriteria IMDBRatingSearchCriteria { get; set; }
        SearchCriteria IMDBRatingCountSearchCriteria { get; set; }
        SearchCriteria WriterSearchCriteria { get; set; }
        SearchCriteria OriginalTitleSearchCriteria { get; set; }
        SearchCriteria StarSearchCriteria { get; set; }
        SearchCriteria StoryLineSearchCriteria { get; set; }
        SearchCriteria AlternateStoryLineSearchCriteria { get; set; }
        SearchCriteria CertificateSearchCriteria { get; set; }
        SearchCriteria CountrySearchCriteria { get; set; }
        SearchCriteria LanguageSearchCriteria { get; set; }
        Movie movie = new Movie();
        List<StringValue> EmptyFolderList = new List<StringValue>();
        string[] fileExtensions = { "*.avi", "*.mkv", "*.mp4", "*.ogm", "*.dat", "*.mpg", "*.AVI", "*.MKV", "*.MP4", "*.OGM", "*.DAT", "*.MPG" };
        BackgroundWorker BackgroundWorkerFile = new BackgroundWorker();
        BackgroundWorker BackgroundWorkerSite = new BackgroundWorker();
        BackgroundWorker BackgroundWorkerDetail = new BackgroundWorker();
        int ComboBoxNumber = 0;
        int FolderCount = 0;

        public class Movie
        {
            public string ID { get; set; }
            public string StoryLine { get; set; }
            public bool TVEpisode { get; set; }
            public string OriginalTitle { get; set; }
            public string Year { get; set; }
            public string PosterSmall { get; set; }
            public string PosterBig { get; set; }
            public string Link { get; set; }
            public string Runtime { get; set; }
            public string Director { get; set; }
            public string Genre { get; set; }
            public string Rating { get; set; }
            public string RateCount { get; set; }
            public string Writer { get; set; }
            public string Title { get; set; }
            public string Star { get; set; }
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
            public string SearchStartPass1 { get; set; }
            public string SearchStartPass2 { get; set; }
            public string SearchStartPass3 { get; set; }
            public int MaxIndex { get; set; }
        }
        public class StringValue
        {
            public StringValue(string s)
            {
                _value = s;
            }
            public string Value { get { return _value; } set { _value = value; } }
            string _value;
        }
        #endregion

        #region Events
        void TextboxAddress_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Int32)Keys.Enter && !string.IsNullOrEmpty(TextboxAddress.Text))
            {
                this.Enabled = false;
                ProgressBar.Visible = true;
                BackgroundWorkerSite.RunWorkerAsync();
            }
        }
        void TextboxPlot_Enter(object sender, EventArgs e)
        {
            ActiveControl = TextboxAddress;
        }
        void TextboxPlot_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Arrow;
        }

        void DataGridViewResult_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow r in DataGridViewResult.Rows)
            {
                if (System.Uri.IsWellFormedUriString(r.Cells["IDLink"].Value.ToString(), UriKind.Absolute))
                {
                    r.Cells["IDLink"] = new DataGridViewLinkCell { Value = r.Cells["IDLink"].Value };
                    DataGridViewLinkCell c = r.Cells["IDLink"] as DataGridViewLinkCell;
                }
            }
        }
        void DataGridViewResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
                if (DataGridViewResult.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
                    System.Diagnostics.Process.Start(DataGridViewResult.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string);
        }
        void DataGridViewEmptyFolder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView senderGrid = (DataGridView)sender;
            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                if (DataGridViewEmptyFolder.Rows[e.RowIndex].Cells[0].Value.ToString() == "Remove")
                    Directory.Delete(DataGridViewEmptyFolder.Rows[e.RowIndex].Cells[1].Value.ToString());
                else
                    Directory.Delete(DataGridViewEmptyFolder.Rows[e.RowIndex].Cells[0].Value.ToString());
                ShowEmptyFolders();
            }
        }
        void DataGridViewResult_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && DataGridViewResult.Rows.Count > 0)
                if (SelectedID != DataGridViewResult.Rows[e.RowIndex].Cells[0].Value.ToString() && movie.PosterBig == null)
                    if (e.ColumnIndex == 0 || e.ColumnIndex == 1 || e.ColumnIndex == 2 || e.ColumnIndex == 3)
                    {
                        DoSearchDetail(e.RowIndex);
                        DataGridViewDetail.DataSource = DataTableDetail;
                        if (DataGridViewDetail.Rows.Count > 0)
                        {
                            Clipboard.SetText(ClipboardText);
                            TextboxPlot.Text = Plot;
                            DataGridViewDetail.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                            DataGridViewDetail.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            ButtonMoveFolder.Enabled = true;
                            ButtonRefresh.Enabled = true;
                            ButtonPlayMovie.Enabled = true;
                            ButtonOpenFolder.Enabled = true;
                        }
                    }
        }
        void DataGridViewFile_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                this.Enabled = false;
                ProgressBar.Visible = true;
                TextboxAddress.Text = DataGridViewFile.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                FileFullPath = DataGridViewFile.Rows[e.RowIndex].Cells[2].Value.ToString();
                FolderFullPath = DataGridViewFile.Rows[e.RowIndex].Cells[3].Value.ToString();
                ComboBoxNumber = Convert.ToInt32(ComboBoxNumberOfItems.SelectedItem);
                DoClearData();
                BackgroundWorkerSite.RunWorkerAsync();
            }
        }

        void ButtonLastSearch_Click(object sender, EventArgs e)
        {
            TextboxAddress.Text = LastSearch;
            TextboxAddress.Focus();
        }
        void ButtonGetFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = false,
                Description = "Choose movie folder",
                RootFolder = Environment.SpecialFolder.MyComputer
            };
            folderBrowserDialog.ShowDialog();
            SelectedPath = folderBrowserDialog.SelectedPath;
            ShowEmptyFolders();
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                this.Enabled = false;
                ProgressBar.Visible = true;
                BackgroundWorkerFile.RunWorkerAsync();
            }
        }
        void ButtonPlayMovie_Click(object sender, EventArgs e)
        {
            if (File.Exists(FileFullPath))
                System.Diagnostics.Process.Start(FileFullPath);
            else
                MessageBox.Show("File was moved!");
        }
        void ButtonMoveFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Directory.Move(FolderFullPath, "Q:\\IMDb\\" + SelectedID);
                MessageBox.Show("Done.");
                DoUpdateFolder();
                ButtonRefresh.Text = "REFRESH (" + (FolderCount - 1).ToString() + ")";
                DoClearData();
                ButtonMoveFolder.Enabled = false;
                ButtonPlayMovie.Enabled = false;
                ButtonOpenFolder.Enabled = false;
            }
            catch
            {
                MessageBox.Show("Something ain't right!!!");
            }
        }
        void ButtonRefresh_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                ShowEmptyFolders();
                DoClearData();
                this.Enabled = false;
                ProgressBar.Visible = true;
                BackgroundWorkerFile.RunWorkerAsync();
            }
        }
        void ButtonOpenFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(FolderFullPath))
                System.Diagnostics.Process.Start(FolderFullPath);
            else
                MessageBox.Show("Folder was moved!");
        }

        void BackgroundWorkerFile_DoWork(object sender, DoWorkEventArgs e)
        {
            DoFillFolder();
        }
        void BackgroundWorkerFile_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            ButtonGetFolder.Enabled = true;
            ProgressBar.Visible = false;
            ButtonMoveFolder.Enabled = false;
            ButtonPlayMovie.Enabled = false;
            ButtonOpenFolder.Enabled = false;
            DataGridViewFile.DataSource = DataTableFile;
            DataGridViewFile.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridViewFile.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewFile.Columns[2].Visible = false;
            DataGridViewFile.Columns[3].Visible = false;
            FolderCount = DataTableFile.AsEnumerable().Select(r => r.Field<string>("Folder Name")).Distinct().Count();
            ButtonRefresh.Text = "REFRESH (" + FolderCount + ")";
            ButtonRefresh.Enabled = true;
        }
        void BackgroundWorkerSite_DoWork(object sender, DoWorkEventArgs e)
        {
            DoFillResult();
        }
        void BackgroundWorkerSite_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ShowResult(DataTableSite);
            TextboxAddress.Text = "";
            TextboxAddress.Focus();
            LabelTimeElapsed.Text = (EndDatetime - StartDatetime).Seconds.ToString();
            if (DataTableSite.Rows.Count > 0)
                BackgroundWorkerDetail.RunWorkerAsync();
            else
            {
                this.Enabled = true;
                ProgressBar.Visible = false;
            }
            ButtonRefresh.Enabled = true;
            ButtonOpenFolder.Enabled = true;
            ButtonPlayMovie.Enabled = true;
        }
        void BackgroundWorkerDetail_DoWork(object sender, DoWorkEventArgs e)
        {
            DoFillDetail();
        }
        void BackgroundWorkerDetail_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            ProgressBar.Visible = false;
            DataGridViewDetail.DataSource = DataTableDetail;
            if (DataGridViewDetail.Rows.Count > 0)
            {
                Clipboard.SetText(ClipboardText);
                TextboxPlot.Text = Plot;
                DataGridViewDetail.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                DataGridViewDetail.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                ButtonMoveFolder.Enabled = true;
                ButtonRefresh.Enabled = true;
                ButtonPlayMovie.Enabled = true;
                ButtonOpenFolder.Enabled = true;
            }
        }
        #endregion

        #region Methods
        void DoFillResult()
        {
            DoSearchTitle();
        }
        void DoFillDetail()
        {
            DoSearchDetail(0);
        }
        void DoFillFolder()
        {
            string[] filesTemp;
            string[] files = null;
            int i = 0;
            if (!string.IsNullOrEmpty(SelectedPath))
            {
                foreach (var fileExtension in fileExtensions)
                {
                    if (i == 0)
                        files = Directory.GetFiles(SelectedPath, fileExtension, SearchOption.AllDirectories);
                    else
                    {
                        filesTemp = Directory.GetFiles(SelectedPath, fileExtension, SearchOption.AllDirectories);
                        files = files.Union(filesTemp).ToArray();
                    }
                    i++;
                }
                DataTable dataTable = new DataTable();
                dataTable.Clear();
                dataTable.Columns.Add("File Name");
                dataTable.Columns.Add("Folder Name");
                dataTable.Columns.Add("Full Path");
                dataTable.Columns.Add("Folder Path");
                FileInfo fileInfo;
                DirectoryInfo directoryInfo;
                string folderName, fileName;
                foreach (var file in files)
                {
                    DataRow dataRow = dataTable.NewRow();
                    fileInfo = new FileInfo(file);
                    directoryInfo = Directory.GetParent(Directory.GetParent(file).ToString());
                    folderName = file.Substring(directoryInfo.ToString().Length + 1);
                    folderName = folderName.Substring(0, folderName.Length - fileInfo.Name.Length - 1);
                    fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
                    dataRow["File Name"] = fileName;
                    dataRow["Folder Name"] = folderName;
                    dataRow["Full Path"] = fileInfo.FullName;
                    dataRow["Folder Path"] = fileInfo.DirectoryName;
                    dataTable.Rows.Add(dataRow);
                }
                DataView dv = dataTable.DefaultView;
                dv.Sort = "[Folder Name]";
                DataTableFile = dv.ToTable();
            }
        }
        void DoClearData()
        {
            DataGridViewResult.DataSource = null;
            DataGridViewDetail.DataSource = null;
            TextboxPlot.Text = "";
            PictureBoxPoster.Image = null;
        }
        void DoUpdateFolder()
        {
            for (int i = DataTableFile.Rows.Count - 1; i >= 0; i--)
            {
                DataRow dr = DataTableFile.Rows[i];
                if (dr["Folder Path"].ToString() == FolderFullPath)
                    dr.Delete();
            }
        }
        void DoSearchTitle()
        {
            LastSearch = TextboxAddress.Text;
            string siteContent = "";
            string siteAddress = "";
            StartDatetime = DateTime.Now;
            char[] delimiterChars = { ' ' };
            string[] array = new string[1000];
            array = TextboxAddress.Text.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
            foreach (string temp in array)
                siteAddress += temp + "+";
            siteAddress = siteAddress.Substring(0, siteAddress.Length - 1);
            siteAddress = "http://www.imdb.com/find?ref=nv_sr_fn&q=" + siteAddress + "&s=tt";
            using (WebClient client_ = new WebClient())
                siteContent = client_.DownloadString(siteAddress);
            DefineSearchCriteria();
            movie.ID = ParsingHelper(IDSearchCriteria, siteContent, ",");
            movie.Title = ParsingHelper(TitleSearchCriteria, siteContent, "~");
            movie.Year = ParsingHelperNew(YearSearchCriteria, siteContent, ",");
            movie.PosterSmall = ParsingHelper(PosterSmallSearchCriteria, siteContent, "Ü");

            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Title");
            dataTable.Columns.Add("Year");
            dataTable.Columns.Add("Poster", typeof(Bitmap));
            dataTable.Columns.Add("IDLink");

            char[] delimiterChars2 = { ',' };
            char[] delimiterChars3 = { 'Ü' };
            char[] delimiterChars4 = { '~' };
            string[] arrayID = new string[1000];
            arrayID = movie.ID.Split(delimiterChars2, StringSplitOptions.RemoveEmptyEntries);
            string[] arrayTitle = new string[1000];
            arrayTitle = movie.Title.Split(delimiterChars4, StringSplitOptions.RemoveEmptyEntries);
            string[] arrayYear = new string[1000];
            arrayYear = movie.Year.Split(delimiterChars2, StringSplitOptions.RemoveEmptyEntries);
            string[] arrayPoster = new string[1000];
            arrayPoster = movie.PosterSmall.Split(delimiterChars3, StringSplitOptions.RemoveEmptyEntries);

            bool TVEpisode;
            string toAdd;
            string toCheck;
            for (int i = 0; i < arrayID.Length; i++)
            {
                TVEpisode = false;
                DataRow dataRow = dataTable.NewRow();
                if (arrayID[i].StartsWith(" "))
                    arrayID[i] = arrayID[i].Substring(1, arrayID[i].Length - 1);
                if (arrayTitle[i].StartsWith(" "))
                    arrayTitle[i] = arrayTitle[i].Substring(1, arrayTitle[i].Length - 1);
                if (arrayYear[i].StartsWith(" "))
                    arrayYear[i] = arrayYear[i].Substring(1, arrayYear[i].Length - 1);
                if (arrayPoster[i].StartsWith(" "))
                    arrayPoster[i] = arrayPoster[i].Substring(1, arrayPoster[i].Length - 1);
                WebRequest request = WebRequest.Create(arrayPoster[i]);
                WebResponse response = request.GetResponse();
                System.IO.Stream responseStream = response.GetResponseStream();
                Bitmap bitmap = new Bitmap(responseStream);

                dataRow["ID"] = arrayID[i];
                dataRow["Title"] = arrayTitle[i];
                dataRow["Year"] = arrayYear[i].Length > 4 ? arrayYear[i].Contains("evelopment") ? arrayYear[i] : "" : arrayYear[i];
                dataRow["Poster"] = bitmap;
                dataRow["IDLink"] = "http://www.imdb.com/title/" + arrayID[i] + "/";

                siteContent = siteContent.Replace("(I) ", "");
                siteContent = siteContent.Replace("(II) ", "");
                siteContent = siteContent.Replace("(III) ", "");

                toCheck = "<a href=\"/title/" + arrayID[i] + "/?ref_=fn_tt_tt_" + (i + 1).ToString() + "\" >" + arrayTitle[i] + "</a> (" + arrayYear[i] + ") (TV Episode)";
                if (siteContent.Contains(toCheck))
                    TVEpisode = true;
                toCheck = "<a href=\"/title/" + arrayID[i] + "/?ref_=fn_tt_tt_" + (i + 1).ToString() + "\" >" + arrayTitle[i] + "</a> (TV Episode)";
                if (siteContent.Contains(toCheck))
                    TVEpisode = true;

                toAdd = TVEpisode ? " (TV Episode)" : "";
                dataRow["Title"] = arrayTitle[i] + toAdd;
                dataTable.Rows.Add(dataRow);
            }
            DataTableSite = dataTable;
            EndDatetime = DateTime.Now;
        }
        void DoSearchDetail(int RowIndex)
        {
            SelectedID = DataGridViewResult.Rows[RowIndex].Cells[0].Value.ToString();
            ClipboardText = DataGridViewResult.Rows[RowIndex].Cells[0].Value.ToString();
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Add("Property");
            dataTable.Columns.Add("Value");
            using (WebClient client_ = new WebClient())
            {
                Movie movie_ = new Movie();
                string siteContent_ = "";
                string siteAddress_ = "";
                DefineSearchCriteria();
                string ID = DataGridViewResult.Rows[RowIndex].Cells[0].Value.ToString();
                siteAddress_ = "http://www.imdb.com/title/" + ID + "/";
                siteContent_ = client_.DownloadString(siteAddress_);
                movie_.ID = ID;
                movie_.Link = siteAddress_;
                movie_.Title = ParsingHelper(TitleSearchCriteria2, siteContent_);
                movie_.Runtime = ParsingHelper(RuntimeSearchCriteria, siteContent_);
                movie_.Year = ParsingHelper(YearSearchCriteria, siteContent_);
                movie_.Director = ParsingHelper(DirectorSearchCriteria, siteContent_);
                movie_.Genre = ParsingHelper(GenreSearchCriteria, siteContent_);
                movie_.Rating = ParsingHelper(IMDBRatingSearchCriteria, siteContent_).Replace(".", ",");
                movie_.RateCount = ParsingHelper(IMDBRatingCountSearchCriteria, siteContent_).Replace(",", "");
                movie_.Writer = ParsingHelper(WriterSearchCriteria, siteContent_);
                movie_.OriginalTitle = ParsingHelper(OriginalTitleSearchCriteria, siteContent_);
                movie_.Star = ParsingHelper(StarSearchCriteria, siteContent_);
                movie_.Certificate = ParsingHelper(CertificateSearchCriteria, siteContent_) == string.Empty ? "Not Rated" : ParsingHelper(CertificateSearchCriteria, siteContent_);
                movie_.Country = ParsingHelper(CountrySearchCriteria, siteContent_);
                movie_.Country = movie_.Country.Contains("desktop-sprite follow-facebook") ? "" : movie_.Country;
                movie_.Language = ParsingHelper(LanguageSearchCriteria, siteContent_);
                movie_.Language = movie_.Language.Contains("desktop-sprite follow-facebook") ? "" : movie_.Language;
                movie_.PosterBig = ParsingHelper(PosterBigSearchCriteria, siteContent_);
                movie_.StoryLine = ParsingHelper(StoryLineSearchCriteria, siteContent_);
                if (!string.IsNullOrEmpty(movie_.Runtime))
                {
                    if (movie_.StoryLine == null || movie_.StoryLine.Length == 0)
                        movie_.StoryLine = ParsingHelper(AlternateStoryLineSearchCriteria, siteContent_);
                    if (movie_.StoryLine != null && movie_.StoryLine.Length > 0)
                        movie_.StoryLine = movie_.StoryLine.Substring(1);
                }
                if (!string.IsNullOrEmpty(movie_.StoryLine))
                    Plot = "Plot: " + movie_.StoryLine;
                int i = 0;
                foreach (PropertyInfo propertyInfo in movie_.GetType().GetProperties())
                {
                    i++;
                    if (i < 8)
                        continue;
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Property"] = propertyInfo.Name;
                    dataRow["Value"] = propertyInfo.GetValue(movie_);
                    dataTable.Rows.Add(dataRow);
                }
                if (!string.IsNullOrEmpty(movie_.PosterBig) && movie_.PosterBig.Length < 300)
                {
                    var request = WebRequest.Create(movie_.PosterBig);
                    using (var response = request.GetResponse())
                    using (var stream = response.GetResponseStream())
                        PictureBoxPoster.Image = Bitmap.FromStream(stream);
                }
            }
            DataTableDetail = dataTable;
        }
        void DefineSearchCriteria()
        {
            IDSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<table class=\"findList\">",
                DivisionEnd_ = "</table>",
                SearchStart_ = "<td class=\"result_text\"> <a href=\"/title/",
                SearchEnd_ = "/?ref_=fn_tt_tt_"
            };
            YearSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<table class=\"findList\">",
                DivisionEnd_ = "</table>",
                SearchStart_ = "</a> (",
                SearchEnd_ = ") "
            };
            TitleSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<table class=\"findList\">",
                DivisionEnd_ = "</table>",
                SearchStart_ = "\" >",
                SearchEnd_ = "</a>",
                SearchStartPass1 = "<",
                SearchStartPass2 = "a",
            };
            TitleSearchCriteria2 = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<meta property='og:title' content=\"",
                SearchEnd_ = " (",
                MaxIndex = 1
            };
            PosterSmallSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<table class=\"findList\">",
                DivisionEnd_ = "</table>",
                SearchStart_ = "<img src=\"",
                SearchEnd_ = "\" /></a> </td>"
            };
            PosterBigSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = " Poster\"",
                DivisionEnd_ = "=\"image\" />",
                SearchStart_ = "src=\"",
                SearchEnd_ = "itemprop",
                MaxIndex = 1
            };
            RuntimeSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<time itemprop=\"duration\" datetime=\"PT",
                SearchEnd_ = "M\"",
                MaxIndex = 1
            };
            DirectorSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<h4 class=\"inline\">Director",
                DivisionEnd_ = "</div>",
                SearchStart_ = "<span class=\"itemprop\" itemprop=\"name\">",
                SearchEnd_ = "</span>",
                MaxIndex = 50
            };
            GenreSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<span class=\"itemprop\" itemprop=\"genre\">",
                SearchEnd_ = "</span>",
                MaxIndex = 50
            };
            IMDBRatingSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<span class=\"rating\">",
                SearchEnd_ = "<span",
                MaxIndex = 1
            };
            IMDBRatingCountSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<span class=\"small\" itemprop=\"ratingCount\">",
                SearchEnd_ = "</span>",
                MaxIndex = 1
            };
            WriterSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<h4 class=\"inline\">Writer",
                DivisionEnd_ = "</div>",
                SearchStart_ = "<span class=\"itemprop\" itemprop=\"name\">",
                SearchEnd_ = "</span>",
                MaxIndex = 50
            };
            OriginalTitleSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<h1 itemprop=\"name\" class=\"\">",
                SearchEnd_ = "&nbsp;",
                MaxIndex = 1
            };
            StarSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<h2>Cast</h2>",
                DivisionEnd_ = ">See full cast</a>",
                SearchStart_ = "<span class=\"itemprop\" itemprop=\"name\">",
                SearchEnd_ = "</span>",
                MaxIndex = 50
            };
            StoryLineSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<h2>Storyline</h2>",
                DivisionEnd_ = "</div>",
                SearchStart_ = "            <p>",
                SearchEnd_ = "                <em class=\"nobr\">",
                MaxIndex = 1
            };
            AlternateStoryLineSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<h2>Storyline</h2>",
                DivisionEnd_ = "</div>",
                SearchStart_ = "            <p>",
                SearchEnd_ = "            </p>",
                MaxIndex = 1
            };
            CertificateSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "",
                DivisionEnd_ = "",
                SearchStart_ = "<span itemprop=\"contentRating\">",
                SearchEnd_ = "</span>",
                MaxIndex = 1
            };
            CountrySearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<h4 class=\"inline\">Country:</h4>",
                DivisionEnd_ = "</div>",
                SearchStart_ = "itemprop='url'>",
                SearchEnd_ = "</a>",
                MaxIndex = 50
            };
            LanguageSearchCriteria = new SearchCriteria()
            {
                DivisionStart_ = "<h4 class=\"inline\">Language:</h4>",
                DivisionEnd_ = "</div>",
                SearchStart_ = "itemprop='url'>",
                SearchEnd_ = "</a>",
                MaxIndex = 50
            };
        }
        void ShowResult(DataTable dt)
        {
            DataGridViewResult.DataSource = dt;
            DataGridViewResult.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridViewResult.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            DataGridViewResult.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridViewResult.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            DataGridViewResult.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
        }
        string ParsingHelper(SearchCriteria sc_, string Input_, string Seperator)
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
            if (!string.IsNullOrEmpty(sc_.SearchStartPass1))
            {
                Input_ = Input_.Replace(sc_.SearchStart_ + sc_.SearchStartPass1, "####");
                Input_ = Input_.Replace(sc_.SearchStartPass1 + sc_.SearchStart_, "####");
            }
            if (!string.IsNullOrEmpty(sc_.SearchStartPass2))
            {
                Input_ = Input_.Replace(sc_.SearchStart_ + sc_.SearchStartPass2, "####");
                Input_ = Input_.Replace(sc_.SearchStartPass2 + sc_.SearchStart_, "####");
            }
            if (!string.IsNullOrEmpty(sc_.SearchStartPass3))
            {
                Input_ = Input_.Replace(sc_.SearchStart_ + sc_.SearchStartPass3, "####");
                Input_ = Input_.Replace(sc_.SearchStartPass3 + sc_.SearchStart_, "####");
            }
            for (int i = 0; i < ComboBoxNumber; i++)
            {
                if (Input_.IndexOf(sc_.SearchStart_) > 0 && Input_.IndexOf(sc_.SearchEnd_) > 0)
                {
                    temp_ = Input_.Substring(Input_.IndexOf(sc_.SearchStart_) + sc_.SearchStart_.Length - 1);
                    temp_ = temp_.Substring(1, temp_.IndexOf(sc_.SearchEnd_) - 1);
                    result_ += temp_ + Seperator + " ";
                    Input_ = Input_.Substring(Input_.IndexOf(sc_.SearchStart_ + temp_ + sc_.SearchEnd_) + temp_.Length - 1);
                    if (sc_ == YearSearchCriteria && Input_.IndexOf("<td class=\"result_text\">") > -1)
                        Input_ = Input_.Substring(Input_.IndexOf("<td class=\"result_text\">"));
                }
                else
                    break;
            }
            if (result_.Length > 2)
                result_ = result_.Substring(0, result_.Length - 2);
            result_ = ReplaceNonAnsiChars(result_);
            return result_;
        }
        string ParsingHelperNew(SearchCriteria sc_, string Input_, string Seperator)
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

            int rowCount = Regex.Matches(Input_, "<small>").Count;
            for (int i = 0; i < rowCount - 1; i++)
            {
                if (Input_.IndexOf("<small>") < 0)
                    break;
                temp_ = Input_.Substring(Input_.IndexOf("<small>"));
                temp_ = temp_.Substring(0, temp_.IndexOf("</small>") + 8);
                Input_ = Input_.Replace(temp_, "####");
            }

            Input_ = Input_.Replace("(I) ", "");
            Input_ = Input_.Replace("(II) ", "");
            Input_ = Input_.Replace("(III) ", "");

            rowCount = Regex.Matches(Input_, "<td class=\"result_text\">").Count > Convert.ToInt32(ComboBoxNumber) ? Convert.ToInt32(ComboBoxNumber) : Regex.Matches(Input_, "<td class=\"result_text\">").Count;

            Input_ = Input_.Replace("</a> </td> </tr><tr class=\"findResult", "</a> () </td> </tr><tr class=\"findResult");

            for (int i = 0; i < rowCount; i++)
            {
                if (Input_.IndexOf(sc_.SearchStart_) > -1 && Input_.IndexOf(sc_.SearchEnd_) > -1)
                {
                    temp_ = Input_.Substring(Input_.IndexOf(sc_.SearchStart_) + sc_.SearchStart_.Length - 1);
                    temp_ = temp_.Substring(1, temp_.IndexOf(sc_.SearchEnd_) - 1);
                    result_ += temp_ + Seperator + " ";
                    Input_ = Input_.Substring(Input_.IndexOf(sc_.SearchStart_ + temp_ + sc_.SearchEnd_) + temp_.Length - 1);
                    if (Input_.IndexOf("<td class=\"result_text\">") < 0)
                        break;
                    Input_ = Input_.Substring(Input_.IndexOf("<td class=\"result_text\">"));
                }
                else
                    break;
            }

            if (result_.Length > 2)
                result_ = result_.Substring(0, result_.Length - 2);
            result_ = ReplaceNonAnsiChars(result_);
            return result_;
        }
        string ParsingHelper(SearchCriteria sc_, string Input_)
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
            for (int i = 0; i < sc_.MaxIndex; i++)
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
        string ReplaceNonAnsiChars(string input_)
        {
            string result_ = input_;
            NameValueCollection settingCollection = (NameValueCollection)ConfigurationManager.GetSection("CustomAppSettings");
            string[] allKeys = settingCollection.AllKeys;
            foreach (string key in allKeys)
                result_ = result_.Replace(key, settingCollection[key]);
            return result_;
        }
        void ProcessEmptyDirectory(string startLocation)
        {
            foreach (var directory in Directory.GetDirectories(startLocation))
            {
                ProcessEmptyDirectory(directory);
                StringValue directoryString = new StringValue(directory);
                if (Directory.GetFiles(directory).Length == 0 && Directory.GetDirectories(directory).Length == 0)
                    EmptyFolderList.Add(directoryString);
            }
        }
        void ShowEmptyFolders()
        {
            EmptyFolderList.Clear();
            ProcessEmptyDirectory(SelectedPath);
            EmptyFolderList.ConvertAll(x => new { Value = x });
            BindingSource source = new BindingSource
            {
                DataSource = EmptyFolderList
            };
            DataGridViewEmptyFolder.DataSource = source;
            if (DataGridViewEmptyFolder.Columns.Count < 2)
            {
                DataGridViewButtonColumn ButtonColumn = new DataGridViewButtonColumn
                {
                    Name = "Remove_column",
                    Text = "Remove",
                    HeaderText = "Remove",
                    UseColumnTextForButtonValue = true
                };
                DataGridViewEmptyFolder.Columns.Insert(1, ButtonColumn);
                DataGridViewEmptyFolder.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DataGridViewEmptyFolder.Columns[1].Width = 70;
            }
        }
        #endregion
    }
}
