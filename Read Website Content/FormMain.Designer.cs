namespace NamespaceIMDB
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
            this.ButtonGet = new System.Windows.Forms.Button();
            this.TextboxAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TextboxContent = new System.Windows.Forms.TextBox();
            this.TextboxResult = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ButtonCopy = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.LabelTimeElapsed = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGet
            // 
            this.ButtonGet.Location = new System.Drawing.Point(97, 127);
            this.ButtonGet.Name = "btnGet";
            this.ButtonGet.Size = new System.Drawing.Size(75, 44);
            this.ButtonGet.TabIndex = 0;
            this.ButtonGet.Text = "Get Website";
            this.ButtonGet.UseVisualStyleBackColor = true;
            this.ButtonGet.Click += new System.EventHandler(this.ButtonGet_Click);
            // 
            // txtAddress
            // 
            this.TextboxAddress.Location = new System.Drawing.Point(12, 23);
            this.TextboxAddress.Multiline = true;
            this.TextboxAddress.Name = "txtAddress";
            this.TextboxAddress.Size = new System.Drawing.Size(260, 98);
            this.TextboxAddress.TabIndex = 1;
            this.TextboxAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextboxAddress_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(91, 190);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Web Site Content";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(107, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "IMDB IDs";
            // 
            // txtContent
            // 
            this.TextboxContent.Location = new System.Drawing.Point(9, 206);
            this.TextboxContent.Multiline = true;
            this.TextboxContent.Name = "txtContent";
            this.TextboxContent.Size = new System.Drawing.Size(260, 121);
            this.TextboxContent.TabIndex = 4;
            // 
            // txtResult
            // 
            this.TextboxResult.Location = new System.Drawing.Point(9, 411);
            this.TextboxResult.Multiline = true;
            this.TextboxResult.Name = "txtResult";
            this.TextboxResult.Size = new System.Drawing.Size(260, 121);
            this.TextboxResult.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(113, 395);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Result";
            // 
            // btnCopy
            // 
            this.ButtonCopy.Location = new System.Drawing.Point(97, 538);
            this.ButtonCopy.Name = "btnCopy";
            this.ButtonCopy.Size = new System.Drawing.Size(75, 44);
            this.ButtonCopy.TabIndex = 7;
            this.ButtonCopy.Text = "Copy Result To Clipboard";
            this.ButtonCopy.UseVisualStyleBackColor = true;
            this.ButtonCopy.Click += new System.EventHandler(this.ButtonCopy_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(72, 339);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Time Elapsed In Seconds";
            // 
            // lblTimeElapsed
            // 
            this.LabelTimeElapsed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LabelTimeElapsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelTimeElapsed.Location = new System.Drawing.Point(74, 360);
            this.LabelTimeElapsed.Name = "lblTimeElapsed";
            this.LabelTimeElapsed.Size = new System.Drawing.Size(123, 27);
            this.LabelTimeElapsed.TabIndex = 9;
            this.LabelTimeElapsed.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // AssemblyIMDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 588);
            this.Controls.Add(this.LabelTimeElapsed);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ButtonCopy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TextboxResult);
            this.Controls.Add(this.TextboxContent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TextboxAddress);
            this.Controls.Add(this.ButtonGet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssemblyIMDB";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Gather IMDB Info";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonGet;
        private System.Windows.Forms.TextBox TextboxAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TextboxContent;
        private System.Windows.Forms.TextBox TextboxResult;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button ButtonCopy;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LabelTimeElapsed;
    }
}

