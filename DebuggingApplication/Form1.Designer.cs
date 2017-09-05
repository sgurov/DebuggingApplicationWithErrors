namespace DebuggingApplication
{
    partial class Form1
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
            this.reminderControl1 = new DebuggingApplication.ReminderControl();
            this.SuspendLayout();
            // 
            // reminderControl1
            // 
            this.reminderControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.reminderControl1.Location = new System.Drawing.Point(79, 29);
            this.reminderControl1.MinimumSize = new System.Drawing.Size(212, 0);
            this.reminderControl1.Name = "reminderControl1";
            this.reminderControl1.Size = new System.Drawing.Size(312, 422);
            this.reminderControl1.TabIndex = 0;
            this.reminderControl1.Text = "reminderControl1";
            this.reminderControl1.View = DebuggingApplication.ViewKind.View;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(476, 509);
            this.Controls.Add(this.reminderControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ReminderControl reminderControl1;
    }
}

