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
            this.button4 = new System.Windows.Forms.Button();
            this.password = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.host = new System.Windows.Forms.TextBox();
            this.userName = new System.Windows.Forms.TextBox();
            this.port = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.checkHTML = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.button2 = new System.Windows.Forms.Button();
            this.body = new System.Windows.Forms.TextBox();
            this.subject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.to = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.from = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bcc = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cc = new System.Windows.Forms.TextBox();
            this.formName = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(247, 75);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(25, 23);
            this.button5.TabIndex = 62;
            this.button5.Text = "+";
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // loginPlain
            // 
            this.loginPlain.Location = new System.Drawing.Point(210, 16);
            this.loginPlain.Name = "loginPlain";
            this.loginPlain.Size = new System.Drawing.Size(104, 24);
            this.loginPlain.TabIndex = 2;
            this.loginPlain.Text = "Plain Text";
            this.loginPlain.CheckedChanged += new System.EventHandler(this.loginPlain_CheckedChanged);
            // 
            // loginBase64
            // 
            this.loginBase64.Location = new System.Drawing.Point(385, 16);
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
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(247, 49);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(25, 23);
            this.button4.TabIndex = 61;
            this.button4.Text = "+";
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // password
            // 
            this.password.Enabled = false;
            this.password.Location = new System.Drawing.Point(360, 42);
            this.password.Name = "password";
            this.password.PasswordChar = '*';
            this.password.Size = new System.Drawing.Size(127, 20);
            this.password.TabIndex = 45;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(298, 46);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 16);
            this.label10.TabIndex = 56;
            this.label10.Text = "Password";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 55;
            this.label9.Text = "User";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.host);
            this.groupBox1.Controls.Add(this.userName);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.port);
            this.groupBox1.Controls.Add(this.password);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 129);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setup";
            // 
            // host
            // 
            this.host.Enabled = false;
            this.host.Location = new System.Drawing.Point(54, 17);
            this.host.Name = "host";
            this.host.PasswordChar = '*';
            this.host.Size = new System.Drawing.Size(129, 20);
            this.host.TabIndex = 57;
            // 
            // userName
            // 
            this.userName.Enabled = false;
            this.userName.Location = new System.Drawing.Point(54, 44);
            this.userName.Name = "userName";
            this.userName.PasswordChar = '*';
            this.userName.Size = new System.Drawing.Size(127, 20);
            this.userName.TabIndex = 56;
            // 
            // port
            // 
            this.port.Enabled = false;
            this.port.Location = new System.Drawing.Point(190, 18);
            this.port.Name = "port";
            this.port.PasswordChar = '*';
            this.port.Size = new System.Drawing.Size(28, 20);
            this.port.TabIndex = 55;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(180, 20);
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
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.loginNone);
            this.groupBox4.Controls.Add(this.loginPlain);
            this.groupBox4.Controls.Add(this.loginBase64);
            this.groupBox4.Location = new System.Drawing.Point(6, 74);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(483, 46);
            this.groupBox4.TabIndex = 58;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Login Type";
            // 
            // checkHTML
            // 
            this.checkHTML.Location = new System.Drawing.Point(63, 281);
            this.checkHTML.Name = "checkHTML";
            this.checkHTML.Size = new System.Drawing.Size(104, 24);
            this.checkHTML.TabIndex = 54;
            this.checkHTML.Text = "Send as HTML";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(405, 13);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 23);
            this.button3.TabIndex = 53;
            this.button3.Text = "Add Attachments";
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 504);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(524, 22);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 50;
            this.statusBar.Text = "Ready.";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(432, 474);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 47;
            this.button2.Text = "Send";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // body
            // 
            this.body.AcceptsReturn = true;
            this.body.AcceptsTab = true;
            this.body.Location = new System.Drawing.Point(63, 202);
            this.body.Multiline = true;
            this.body.Name = "body";
            this.body.Size = new System.Drawing.Size(400, 77);
            this.body.TabIndex = 37;
            // 
            // subject
            // 
            this.subject.Location = new System.Drawing.Point(67, 161);
            this.subject.Name = "subject";
            this.subject.Size = new System.Drawing.Size(193, 20);
            this.subject.TabIndex = 36;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 16);
            this.label5.TabIndex = 46;
            this.label5.Text = "Subject";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(247, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 23);
            this.button1.TabIndex = 35;
            this.button1.Text = "+";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // to
            // 
            this.to.Location = new System.Drawing.Point(48, 25);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(193, 20);
            this.to.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 41;
            this.label4.Text = "To";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(21, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 16);
            this.label3.TabIndex = 38;
            this.label3.Text = "From";
            // 
            // from
            // 
            this.from.Location = new System.Drawing.Point(155, 16);
            this.from.Name = "from";
            this.from.Size = new System.Drawing.Size(213, 20);
            this.from.TabIndex = 63;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(173, 285);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(70, 17);
            this.checkBox1.TabIndex = 64;
            this.checkBox1.Text = "Is Signed";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(249, 285);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(84, 17);
            this.checkBox2.TabIndex = 65;
            this.checkBox2.Text = "is Encrypted";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bcc);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.cc);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.to);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Location = new System.Drawing.Point(15, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(474, 114);
            this.groupBox2.TabIndex = 66;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Recipients";
            // 
            // bcc
            // 
            this.bcc.Location = new System.Drawing.Point(48, 77);
            this.bcc.Name = "bcc";
            this.bcc.Size = new System.Drawing.Size(193, 20);
            this.bcc.TabIndex = 44;
            // 
            // label13
            // 
            this.label13.Location = new System.Drawing.Point(8, 80);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 16);
            this.label13.TabIndex = 43;
            this.label13.Text = "BCC:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 16);
            this.label6.TabIndex = 42;
            this.label6.Text = "CC:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.formName);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.from);
            this.groupBox3.Controls.Add(this.subject);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.body);
            this.groupBox3.Controls.Add(this.checkHTML);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Location = new System.Drawing.Point(12, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(495, 321);
            this.groupBox3.TabIndex = 67;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mail Message";
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(374, 13);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(25, 23);
            this.button6.TabIndex = 68;
            this.button6.Text = "+";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(15, 202);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 16);
            this.label7.TabIndex = 67;
            this.label7.Text = "Body";
            // 
            // cc
            // 
            this.cc.Location = new System.Drawing.Point(48, 50);
            this.cc.Name = "cc";
            this.cc.Size = new System.Drawing.Size(193, 20);
            this.cc.TabIndex = 63;
            // 
            // formName
            // 
            this.formName.Location = new System.Drawing.Point(54, 16);
            this.formName.Name = "formName";
            this.formName.Size = new System.Drawing.Size(95, 20);
            this.formName.TabIndex = 69;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 526);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkHTML;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.StatusBar statusBar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox body;
        private System.Windows.Forms.TextBox subject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox to;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox host;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.TextBox from;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox bcc;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox cc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox formName;
        private System.Windows.Forms.Button button7;
    }
}

