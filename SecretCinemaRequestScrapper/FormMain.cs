using System;
using System.Windows.Forms;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace SecretCinemaRequestScrapper
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            web.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(Web_DocumentCompleted);
        }

        #region Properties
        HtmlDocument doc;
        WebBrowser web = new WebBrowser();
        byte[] bytes;
        string url;
        Encoding encoding = System.Text.Encoding.UTF8;
        List<string> torrents = new List<string>();
        #endregion

        #region Events
        private void GetResultsButton_Click(object sender, EventArgs e)
        {
            GetResultsButton.Enabled = false;

            string postData = "username=" + "egemensavas" + "&password=" + "1qAZ2wSX";
            bytes = encoding.GetBytes(postData);
            url = "https://secret-cinema.pw/login.php";
            web.Navigate(url, string.Empty, bytes, "Content-Type: application/x-www-form-urlencoded");
            while (web.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();

            url = "https://secret-cinema.pw/requests.php";
            web.Navigate(url, string.Empty, bytes, "Content-Type: application/x-www-form-urlencoded");
            while (web.ReadyState != WebBrowserReadyState.Complete)
                Application.DoEvents();

            List<string> torrentResult = Program.ParsingHelper(Program.RequestSearchCriteria, doc.Body.InnerHtml);
            foreach (var item in torrentResult)
                torrents.Add(item);

            List<string> pageResult = Program.ParsingHelper(Program.PageSearchCriteria, doc.Body.InnerHtml);
            int maxPage = pageResult.Max(t => Convert.ToInt32(t));

            for (int i = 1; i <= maxPage; i++)
            {
                url = "https://secret-cinema.pw/requests.php?page=" + (i + 1).ToString();
                web.Navigate(url, string.Empty, bytes, "Content-Type: application/x-www-form-urlencoded");
                while (web.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();

                torrentResult = Program.ParsingHelper(Program.RequestSearchCriteria, doc.Body.InnerHtml);
                foreach (var item in torrentResult)
                    torrents.Add(item);
            }

            DataTable resultDT = new DataTable();
            resultDT.Clear();
            resultDT.Columns.Add("Movie");
            resultDT.Columns.Add("Year");
            resultDT.Columns.Add("Director");
            resultDT.Columns.Add("IMDb ID");
            resultDT.Columns.Add("Acceptable Media");
            resultDT.Columns.Add("Create Date");
            resultDT.Columns.Add("Created By");
            resultDT.Columns.Add("Bounty");
            resultDT.Columns.Add("Link");
            DataRow resultDR;

            foreach (var item in torrents)
            {
                resultDR = resultDT.NewRow();
                url = "https://secret-cinema.pw/requests.php?action=view&id=" + item;
                web.Navigate(url, string.Empty, bytes, "Content-Type: application/x-www-form-urlencoded");
                while (web.ReadyState != WebBrowserReadyState.Complete)
                    Application.DoEvents();

                resultDR["Movie"] = Program.ParsingHelperString(Program.RequestMovieSearchCriteria, doc.Body.InnerHtml);
                Program.RequestYearSearchCriteria.SearchStart_ = "<span dir=\"ltr\">" + resultDR["Movie"] + "</span> - ";
                resultDR["Year"] = Program.ParsingHelperString(Program.RequestYearSearchCriteria, doc.Body.InnerHtml);
                resultDR["Director"] = Program.ParsingHelperString(Program.RequestDirectorSearchCriteria, doc.Body.InnerHtml);
                resultDR["IMDb ID"] = Program.ParsingHelperString(Program.RequestIMDbIDSearchCriteria, doc.Body.InnerHtml); ;
                resultDR["Acceptable Media"] = Program.ParsingHelperString(Program.RequestAcceptableMediaSearchCriteria, doc.Body.InnerHtml);
                resultDR["Create Date"] = Program.ParsingHelperString(Program.RequestCreateDateSearchCriteria, doc.Body.InnerHtml);
                resultDR["Created By"] = Program.ParsingHelperString(Program.RequestCreatedBySearchCriteria, doc.Body.InnerHtml);
                resultDR["Bounty"] = Program.ParsingHelperString(Program.RequestBountySearchCriteria, doc.Body.InnerHtml);
                resultDR["Link"] = url;
                resultDT.Rows.Add(resultDR);

                ResultDataGridView.DataSource = resultDT;
            }

            ResultDataGridView.DataSource = resultDT;
            ResultDataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGridView.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGridView.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGridView.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGridView.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ResultDataGridView.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            GetResultsButton.Enabled = true;
        }

        private void Web_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            doc = ((WebBrowser)sender).Document;
        }

        void ResultDataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow r in ResultDataGridView.Rows)
            {
                if (System.Uri.IsWellFormedUriString(r.Cells["Link"].Value.ToString(), UriKind.Absolute))
                {
                    r.Cells["Link"] = new DataGridViewLinkCell { Value = r.Cells["Link"].Value };
                    DataGridViewLinkCell c = r.Cells["Link"] as DataGridViewLinkCell;
                }
            }
        }

        private void DataGridViewResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
                if (ResultDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell)
                {
                    System.Diagnostics.Process.Start(ResultDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as string);
                }
        }
        #endregion
    }
}
