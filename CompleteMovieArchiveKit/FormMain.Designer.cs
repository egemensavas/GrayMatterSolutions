namespace CompleteMovieArchiveKit
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.TextboxAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LabelTimeElapsed = new System.Windows.Forms.Label();
            this.ComboBoxNumberOfItems = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DataGridViewResult = new System.Windows.Forms.DataGridView();
            this.DataGridViewDetail = new System.Windows.Forms.DataGridView();
            this.ButtonLastSearch = new System.Windows.Forms.Button();
            this.PictureBoxPoster = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextboxPlot = new System.Windows.Forms.TextBox();
            this.ButtonGetFolder = new System.Windows.Forms.Button();
            this.DataGridViewFile = new System.Windows.Forms.DataGridView();
            this.ButtonMoveFolder = new System.Windows.Forms.Button();
            this.ButtonPlayMovie = new System.Windows.Forms.Button();
            this.ButtonRefresh = new System.Windows.Forms.Button();
            this.DataGridViewEmptyFolder = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.ButtonOpenFolder = new System.Windows.Forms.Button();
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPoster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewEmptyFolder)).BeginInit();
            this.SuspendLayout();
            // 
            // TextboxAddress
            // 
            this.TextboxAddress.Location = new System.Drawing.Point(77, 249);
            this.TextboxAddress.Name = "TextboxAddress";
            this.TextboxAddress.Size = new System.Drawing.Size(400, 20);
            this.TextboxAddress.TabIndex = 1;
            this.TextboxAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxAddress_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 254);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Movie Title";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(766, 254);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(134, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Time Elapsed In Seconds :";
            // 
            // LabelTimeElapsed
            // 
            this.LabelTimeElapsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.LabelTimeElapsed.Location = new System.Drawing.Point(904, 250);
            this.LabelTimeElapsed.Name = "LabelTimeElapsed";
            this.LabelTimeElapsed.Size = new System.Drawing.Size(83, 20);
            this.LabelTimeElapsed.TabIndex = 9;
            this.LabelTimeElapsed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComboBoxNumberOfItems
            // 
            this.ComboBoxNumberOfItems.FormattingEnabled = true;
            this.ComboBoxNumberOfItems.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.ComboBoxNumberOfItems.Location = new System.Drawing.Point(674, 249);
            this.ComboBoxNumberOfItems.Name = "ComboBoxNumberOfItems";
            this.ComboBoxNumberOfItems.Size = new System.Drawing.Size(45, 21);
            this.ComboBoxNumberOfItems.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(580, 254);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Items Per Search";
            // 
            // DataGridViewResult
            // 
            this.DataGridViewResult.AllowUserToAddRows = false;
            this.DataGridViewResult.AllowUserToDeleteRows = false;
            this.DataGridViewResult.AllowUserToResizeColumns = false;
            this.DataGridViewResult.AllowUserToResizeRows = false;
            this.DataGridViewResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DataGridViewResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewResult.Location = new System.Drawing.Point(12, 275);
            this.DataGridViewResult.Name = "DataGridViewResult";
            this.DataGridViewResult.ReadOnly = true;
            this.DataGridViewResult.RowHeadersVisible = false;
            this.DataGridViewResult.RowTemplate.Height = 60;
            this.DataGridViewResult.Size = new System.Drawing.Size(546, 391);
            this.DataGridViewResult.TabIndex = 12;
            this.DataGridViewResult.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewResult_CellClick);
            this.DataGridViewResult.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewResult_CellContentClick);
            this.DataGridViewResult.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DataGridViewResult_DataBindingComplete);
            // 
            // DataGridViewDetail
            // 
            this.DataGridViewDetail.AllowUserToAddRows = false;
            this.DataGridViewDetail.AllowUserToDeleteRows = false;
            this.DataGridViewDetail.AllowUserToResizeColumns = false;
            this.DataGridViewDetail.AllowUserToResizeRows = false;
            this.DataGridViewDetail.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.DataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewDetail.Location = new System.Drawing.Point(564, 275);
            this.DataGridViewDetail.MultiSelect = false;
            this.DataGridViewDetail.Name = "DataGridViewDetail";
            this.DataGridViewDetail.RowHeadersVisible = false;
            this.DataGridViewDetail.Size = new System.Drawing.Size(419, 287);
            this.DataGridViewDetail.TabIndex = 13;
            // 
            // ButtonLastSearch
            // 
            this.ButtonLastSearch.Location = new System.Drawing.Point(483, 249);
            this.ButtonLastSearch.Name = "ButtonLastSearch";
            this.ButtonLastSearch.Size = new System.Drawing.Size(75, 23);
            this.ButtonLastSearch.TabIndex = 14;
            this.ButtonLastSearch.Text = "Last Search";
            this.ButtonLastSearch.UseVisualStyleBackColor = true;
            this.ButtonLastSearch.Click += new System.EventHandler(this.ButtonLastSearch_Click);
            // 
            // PictureBoxPoster
            // 
            this.PictureBoxPoster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBoxPoster.Location = new System.Drawing.Point(989, 275);
            this.PictureBoxPoster.Name = "PictureBoxPoster";
            this.PictureBoxPoster.Size = new System.Drawing.Size(267, 391);
            this.PictureBoxPoster.TabIndex = 15;
            this.PictureBoxPoster.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1105, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Poster";
            // 
            // TextboxPlot
            // 
            this.TextboxPlot.BackColor = System.Drawing.SystemColors.Control;
            this.TextboxPlot.Location = new System.Drawing.Point(564, 568);
            this.TextboxPlot.Multiline = true;
            this.TextboxPlot.Name = "TextboxPlot";
            this.TextboxPlot.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextboxPlot.Size = new System.Drawing.Size(419, 98);
            this.TextboxPlot.TabIndex = 17;
            this.TextboxPlot.Text = "Plot: ";
            this.TextboxPlot.Enter += new System.EventHandler(this.TextboxPlot_Enter);
            this.TextboxPlot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TextboxPlot_MouseMove);
            // 
            // ButtonGetFolder
            // 
            this.ButtonGetFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonGetFolder.Location = new System.Drawing.Point(12, 12);
            this.ButtonGetFolder.Name = "ButtonGetFolder";
            this.ButtonGetFolder.Size = new System.Drawing.Size(25, 206);
            this.ButtonGetFolder.TabIndex = 18;
            this.ButtonGetFolder.Text = "G\r\nE\r\nT\r\n\r\nF\r\nO\r\nL\r\nD\r\nE\r\nR";
            this.ButtonGetFolder.UseVisualStyleBackColor = true;
            this.ButtonGetFolder.Click += new System.EventHandler(this.ButtonGetFolder_Click);
            // 
            // DataGridViewFile
            // 
            this.DataGridViewFile.AllowUserToAddRows = false;
            this.DataGridViewFile.AllowUserToDeleteRows = false;
            this.DataGridViewFile.AllowUserToResizeColumns = false;
            this.DataGridViewFile.AllowUserToResizeRows = false;
            this.DataGridViewFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewFile.Location = new System.Drawing.Point(43, 12);
            this.DataGridViewFile.MultiSelect = false;
            this.DataGridViewFile.Name = "DataGridViewFile";
            this.DataGridViewFile.RowHeadersVisible = false;
            this.DataGridViewFile.Size = new System.Drawing.Size(792, 178);
            this.DataGridViewFile.TabIndex = 19;
            this.DataGridViewFile.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewFile_CellClick);
            // 
            // ButtonMoveFolder
            // 
            this.ButtonMoveFolder.Enabled = false;
            this.ButtonMoveFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonMoveFolder.Location = new System.Drawing.Point(841, 12);
            this.ButtonMoveFolder.Name = "ButtonMoveFolder";
            this.ButtonMoveFolder.Size = new System.Drawing.Size(25, 206);
            this.ButtonMoveFolder.TabIndex = 20;
            this.ButtonMoveFolder.Text = "M\r\nO\r\nV\r\nE\r\n\r\nF\r\nO\r\nL\r\nD\r\nE\r\nR";
            this.ButtonMoveFolder.UseVisualStyleBackColor = true;
            this.ButtonMoveFolder.Click += new System.EventHandler(this.ButtonMoveFolder_Click);
            // 
            // ButtonPlayMovie
            // 
            this.ButtonPlayMovie.Enabled = false;
            this.ButtonPlayMovie.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonPlayMovie.Location = new System.Drawing.Point(43, 195);
            this.ButtonPlayMovie.Name = "ButtonPlayMovie";
            this.ButtonPlayMovie.Size = new System.Drawing.Size(260, 23);
            this.ButtonPlayMovie.TabIndex = 21;
            this.ButtonPlayMovie.Text = "PLAY MOVIE";
            this.ButtonPlayMovie.UseVisualStyleBackColor = true;
            this.ButtonPlayMovie.Click += new System.EventHandler(this.ButtonPlayMovie_Click);
            // 
            // ButtonRefresh
            // 
            this.ButtonRefresh.Enabled = false;
            this.ButtonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonRefresh.Location = new System.Drawing.Point(575, 195);
            this.ButtonRefresh.Name = "ButtonRefresh";
            this.ButtonRefresh.Size = new System.Drawing.Size(260, 23);
            this.ButtonRefresh.TabIndex = 22;
            this.ButtonRefresh.Text = "REFRESH";
            this.ButtonRefresh.UseVisualStyleBackColor = true;
            this.ButtonRefresh.Click += new System.EventHandler(this.ButtonRefresh_Click);
            // 
            // DataGridViewEmptyFolder
            // 
            this.DataGridViewEmptyFolder.AllowUserToAddRows = false;
            this.DataGridViewEmptyFolder.AllowUserToDeleteRows = false;
            this.DataGridViewEmptyFolder.AllowUserToResizeColumns = false;
            this.DataGridViewEmptyFolder.AllowUserToResizeRows = false;
            this.DataGridViewEmptyFolder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridViewEmptyFolder.ColumnHeadersVisible = false;
            this.DataGridViewEmptyFolder.Location = new System.Drawing.Point(895, 12);
            this.DataGridViewEmptyFolder.MultiSelect = false;
            this.DataGridViewEmptyFolder.Name = "DataGridViewEmptyFolder";
            this.DataGridViewEmptyFolder.RowHeadersVisible = false;
            this.DataGridViewEmptyFolder.Size = new System.Drawing.Size(361, 206);
            this.DataGridViewEmptyFolder.TabIndex = 23;
            this.DataGridViewEmptyFolder.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewEmptyFolder_CellContentClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(879, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 169);
            this.label3.TabIndex = 24;
            this.label3.Text = "E\r\nM\r\nP\r\nT\r\nY\r\n\r\nF\r\nO\r\nL\r\nD\r\nE\r\nR\r\nS";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ButtonOpenFolder
            // 
            this.ButtonOpenFolder.Enabled = false;
            this.ButtonOpenFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonOpenFolder.Location = new System.Drawing.Point(309, 195);
            this.ButtonOpenFolder.Name = "ButtonOpenFolder";
            this.ButtonOpenFolder.Size = new System.Drawing.Size(260, 23);
            this.ButtonOpenFolder.TabIndex = 25;
            this.ButtonOpenFolder.Text = "OPEN FOLDER";
            this.ButtonOpenFolder.UseVisualStyleBackColor = true;
            this.ButtonOpenFolder.Click += new System.EventHandler(this.ButtonOpenFolder_Click);
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(34, 222);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(1200, 23);
            this.ProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.ProgressBar.TabIndex = 26;
            this.ProgressBar.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1268, 672);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.ButtonOpenFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DataGridViewEmptyFolder);
            this.Controls.Add(this.ButtonRefresh);
            this.Controls.Add(this.ButtonPlayMovie);
            this.Controls.Add(this.ButtonMoveFolder);
            this.Controls.Add(this.DataGridViewFile);
            this.Controls.Add(this.ButtonGetFolder);
            this.Controls.Add(this.TextboxPlot);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PictureBoxPoster);
            this.Controls.Add(this.ButtonLastSearch);
            this.Controls.Add(this.DataGridViewDetail);
            this.Controls.Add(this.DataGridViewResult);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ComboBoxNumberOfItems);
            this.Controls.Add(this.LabelTimeElapsed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TextboxAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Complete Tool Set For Movie Archive";
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPoster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridViewEmptyFolder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TextboxAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LabelTimeElapsed;
        private System.Windows.Forms.ComboBox ComboBoxNumberOfItems;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView DataGridViewResult;
        private System.Windows.Forms.DataGridView DataGridViewDetail;
        private System.Windows.Forms.Button ButtonLastSearch;
        private System.Windows.Forms.PictureBox PictureBoxPoster;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextboxPlot;
        private System.Windows.Forms.Button ButtonGetFolder;
        private System.Windows.Forms.DataGridView DataGridViewFile;
        private System.Windows.Forms.Button ButtonMoveFolder;
        private System.Windows.Forms.Button ButtonPlayMovie;
        private System.Windows.Forms.Button ButtonRefresh;
        private System.Windows.Forms.DataGridView DataGridViewEmptyFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ButtonOpenFolder;
        private System.Windows.Forms.ProgressBar ProgressBar;
    }
}

