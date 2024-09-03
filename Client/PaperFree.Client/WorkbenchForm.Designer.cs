
namespace PaperFree.Client
{
    partial class WorkbenchForm
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_TestSignalR = new System.Windows.Forms.Button();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(69, 89);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(108, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "欢迎进入无纸化系统";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(332, 170);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_TestSignalR
            // 
            this.btn_TestSignalR.Location = new System.Drawing.Point(332, 231);
            this.btn_TestSignalR.Name = "btn_TestSignalR";
            this.btn_TestSignalR.Size = new System.Drawing.Size(121, 23);
            this.btn_TestSignalR.TabIndex = 1;
            this.btn_TestSignalR.Text = "SignalR一对一";
            this.btn_TestSignalR.UseVisualStyleBackColor = true;
            this.btn_TestSignalR.Click += new System.EventHandler(this.btn_TestSignalR_Click);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(103, 231);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(202, 20);
            this.textEdit1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(332, 278);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(140, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "SignalR广播模式";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // WorkbenchForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 329);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_TestSignalR);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.labelControl1);
            this.Name = "WorkbenchForm";
            this.Text = "工作台首页";
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_TestSignalR;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private System.Windows.Forms.Button button2;
    }
}