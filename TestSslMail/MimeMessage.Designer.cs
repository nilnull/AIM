namespace TestSslMail
{
    partial class MimeMessage
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
            this.button5 = new System.Windows.Forms.Button();
            this.loginPlain = new System.Windows.Forms.RadioButton();
            this.loginBase64 = new System.Windows.Forms.RadioButton();
            this.loginNone = new System.Windows.Forms.RadioButton();
            this.ccBtn = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.host = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxlogin = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.checkHTML = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.body = new System.Windows.Forms.TextBox();
            this.subject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.to = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bcc = new System.Windows.Forms.TextBox();
            this.cc = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.senderText = new System.Windows.Forms.TextBox();
            this.fromBtn = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBoxlogin.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 70);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(54, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "BCC...";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // loginPlain
            // 
            this.loginPlain.Location = new System.Drawing.Point(133, 16);
            this.loginPlain.Name = "loginPlain";
            this.loginPlain.Size = new System.Drawing.Size(79, 24);
            this.loginPlain.TabIndex = 0;
            this.loginPlain.Text = "Plain Text";
            this.loginPlain.CheckedChanged += new System.EventHandler(this.loginPlain_CheckedChanged);
            // 
            // loginBase64
            // 
            this.loginBase64.Location = new System.Drawing.Point(218, 16);
            this.loginBase64.Name = "loginBase64";
            this.loginBase64.Size = new System.Drawing.Size(72, 24);
            this.loginBase64.TabIndex = 1;
            this.loginBase64.Text = "Base64";
            this.loginBase64.CheckedChanged += new System.EventHandler(this.loginBase64_CheckedChanged);
            this.loginBase64.Click += new System.EventHandler(this.loginBase64_CheckedChanged);
            // 
            // loginNone
            // 
            this.loginNone.Checked = true;
            this.loginNone.Location = new System.Drawing.Point(11, 16);
            this.loginNone.Name = "loginNone";
            this.loginNone.Size = new System.Drawing.Size(122, 24);
            this.loginNone.TabIndex = 0;
            this.loginNone.TabStop = true;
            this.loginNone.Text = "Defualt Cridentials";
            this.loginNone.CheckedChanged += new System.EventHandler(this.loginNone_CheckedChanged);
            // 
            // ccBtn
            // 
            this.ccBtn.Location = new System.Drawing.Point(6, 43);
            this.ccBtn.Name = "ccBtn";
            this.ccBtn.Size = new System.Drawing.Size(54, 23);
            this.ccBtn.TabIndex = 3;
            this.ccBtn.Text = "CC...";
            this.ccBtn.Click += new System.EventHandler(this.button4_Click);
            // 
            // password
            // 
            this.password.Enabled = false;
            this.password.Location = new System.Drawing.Point(317, 19);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(160, 20);
            this.password.TabIndex = 4;
            this.password.TextChanged += new System.EventHandler(this.password_TextChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(258, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.TabIndex = 56;
            this.label10.Text = "Password";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(11, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 55;
            this.label9.Text = "User";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.host);
            this.groupBox1.Controls.Add(this.port);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBoxlogin);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 149);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setup";
            // 
            // host
            // 
            this.host.Location = new System.Drawing.Point(63, 18);
            this.host.Name = "host";
            this.host.Size = new System.Drawing.Size(152, 20);
            this.host.TabIndex = 1;
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(233, 18);
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(28, 20);
            this.port.TabIndex = 2;
            this.port.Text = "465";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(221, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 17);
            this.label2.TabIndex = 34;
            this.label2.Text = ":";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 31;
            this.label1.Text = "Host";
            // 
            // groupBoxlogin
            // 
            this.groupBoxlogin.Controls.Add(this.groupBox4);
            this.groupBoxlogin.Controls.Add(this.label10);
            this.groupBoxlogin.Controls.Add(this.label9);
            this.groupBoxlogin.Controls.Add(this.userName);
            this.groupBoxlogin.Controls.Add(this.password);
            this.groupBoxlogin.Location = new System.Drawing.Point(6, 40);
            this.groupBoxlogin.Name = "groupBoxlogin";
            this.groupBoxlogin.Size = new System.Drawing.Size(483, 100);
            this.groupBoxlogin.TabIndex = 58;
            this.groupBoxlogin.TabStop = false;
            this.groupBoxlogin.Text = "Login";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.loginNone);
            this.groupBox4.Controls.Add(this.loginPlain);
            this.groupBox4.Controls.Add(this.loginBase64);
            this.groupBox4.Location = new System.Drawing.Point(11, 45);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(301, 46);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Login Type";
            // 
            // userName
            // 
            this.userName.Enabled = false;
            this.userName.Location = new System.Drawing.Point(57, 19);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(198, 20);
            this.userName.TabIndex = 3;
            // 
            // checkHTML
            // 
            this.checkHTML.Location = new System.Drawing.Point(375, 152);
            this.checkHTML.Name = "checkHTML";
            this.checkHTML.Size = new System.Drawing.Size(104, 24);
            this.checkHTML.TabIndex = 6;
            this.checkHTML.Text = "Send as HTML";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(350, 175);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(129, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Add Attachments";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(429, 460);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(78, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Send";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // body
            // 
            this.body.AcceptsReturn = true;
            this.body.AcceptsTab = true;
            this.body.Location = new System.Drawing.Point(79, 204);
            this.body.Multiline = true;
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(408, 72);
            this.body.TabIndex = 4;
            // 
            // subject
            // 
            this.subject.Location = new System.Drawing.Point(77, 153);
            this.subject.Name = "subject";
            this.subject.Size = new System.Drawing.Size(246, 20);
            this.subject.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(14, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 46;
            this.label5.Text = "Subject";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(54, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "To...";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // to
            // 
            this.to.Enabled = false;
            this.to.Location = new System.Drawing.Point(66, 20);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(402, 20);
            this.to.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bcc);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.cc);
            this.groupBox2.Controls.Add(this.ccBtn);
            this.groupBox2.Controls.Add(this.to);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(11, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(474, 100);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Recipients";
            // 
            // bcc
            // 
            this.bcc.Enabled = false;
            this.bcc.Location = new System.Drawing.Point(66, 72);
            this.bcc.Name = "bcc";
            this.bcc.Size = new System.Drawing.Size(402, 20);
            this.bcc.TabIndex = 4;
            // 
            // cc
            // 
            this.cc.Enabled = false;
            this.cc.Location = new System.Drawing.Point(66, 45);
            this.cc.Name = "cc";
            this.cc.Size = new System.Drawing.Size(402, 20);
            this.cc.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.senderText);
            this.groupBox3.Controls.Add(this.fromBtn);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.subject);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.body);
            this.groupBox3.Controls.Add(this.checkHTML);
            this.groupBox3.Location = new System.Drawing.Point(12, 167);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(495, 287);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mail Message";
            // 
            // senderText
            // 
            this.senderText.Enabled = false;
            this.senderText.Location = new System.Drawing.Point(77, 127);
            this.senderText.Name = "senderText";
            this.senderText.Size = new System.Drawing.Size(402, 20);
            this.senderText.TabIndex = 1;
            // 
            // fromBtn
            // 
            this.fromBtn.Location = new System.Drawing.Point(17, 125);
            this.fromBtn.Name = "fromBtn";
            this.fromBtn.Size = new System.Drawing.Size(54, 23);
            this.fromBtn.TabIndex = 2;
            this.fromBtn.Text = "From...";
            this.fromBtn.Click += new System.EventHandler(this.button6_Click);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(14, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 67;
            this.label7.Text = "Body";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(348, 460);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(75, 23);
            this.button11.TabIndex = 3;
            this.button11.Text = "Reset";
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(350, 18);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 60;
            this.label3.Text = "Ssl Mode";
            // 
            // MimeMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 492);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Name = "MimeMessage";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxlogin.ResumeLayout(false);
            this.groupBoxlogin.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.RadioButton loginPlain;
        private System.Windows.Forms.RadioButton loginBase64;
        private System.Windows.Forms.RadioButton loginNone;
        private System.Windows.Forms.Button ccBtn;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkHTML;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox body;
        private System.Windows.Forms.TextBox subject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox to;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox host;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox bcc;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button fromBtn;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox cc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox senderText;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.GroupBox groupBoxlogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}

