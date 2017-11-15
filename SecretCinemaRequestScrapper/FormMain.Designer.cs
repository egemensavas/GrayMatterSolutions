namespace SecretCinemaRequestScrapper
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
            this.GetResultsButton = new System.Windows.Forms.Button();
            this.ResultDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.ResultDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // GetResultsButton
            // 
            this.GetResultsButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.GetResultsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GetResultsButton.Location = new System.Drawing.Point(0, 496);
            this.GetResultsButton.Name = "GetResultsButton";
            this.GetResultsButton.Size = new System.Drawing.Size(1313, 43);
            this.GetResultsButton.TabIndex = 0;
            this.GetResultsButton.Text = "Get Result";
            this.GetResultsButton.UseVisualStyleBackColor = true;
            this.GetResultsButton.Click += new System.EventHandler(this.GetResultsButton_Click);
            // 
            // ResultDataGridView
            // 
            this.ResultDataGridView.AllowUserToAddRows = false;
            this.ResultDataGridView.AllowUserToDeleteRows = false;
            this.ResultDataGridView.AllowUserToResizeColumns = false;
            this.ResultDataGridView.AllowUserToResizeRows = false;
            this.ResultDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.ResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ResultDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResultDataGridView.Location = new System.Drawing.Point(0, 0);
            this.ResultDataGridView.Name = "ResultDataGridView";
            this.ResultDataGridView.RowHeadersVisible = false;
            this.ResultDataGridView.Size = new System.Drawing.Size(1313, 496);
            this.ResultDataGridView.TabIndex = 1;
            this.ResultDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridViewResult_CellContentClick);
            this.ResultDataGridView.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.ResultDataGridView_DataBindingComplete);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1313, 539);
            this.Controls.Add(this.ResultDataGridView);
            this.Controls.Add(this.GetResultsButton);
            this.Name = "FormMain";
            this.Text = "Secret Cinema Request Details";
            ((System.ComponentModel.ISupportInitialize)(this.ResultDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GetResultsButton;
        private System.Windows.Forms.DataGridView ResultDataGridView;
    }
}

