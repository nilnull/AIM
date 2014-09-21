/*
 * SmtpEmailer : A class/library for sending SMTP messages.
 * Provided by Steaven Woyan swoyan@hotmail.com
 * This package is provided without any warranty.
 * By using this package you agree that the provider makes no guarantee of use
 * and shall not be held liable or accountable for use of this software, even if it
 * fails to work properly.
 * 
 * Parts of this package include logic/code design from PJ Naughter's C++ SMTP package
 * located at http://www.naughter.com/smpt.html.  This link is provided for documentation
 * purposes and is not meant to imply his use/acknowledgement/approval of this package.
 * 
 * This package is provided with source and is free for use for any reason except that
 * it may not be sold or re-packaged by itself.
 * 
 */
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using smw.smtp;

namespace TestEmailer
{
	/// <summary>
	/// A test email form for SmtpEmailer.
	/// Steaven Woyan swoyan@hotmail.com
	/// </summary>
	public class TestForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox port;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox from;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox to;
		private System.Windows.Forms.ListBox toList;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox subject;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox body;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox host;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ListBox attachList;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.CheckBox checkHTML;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton loginNone;
    private System.Windows.Forms.RadioButton loginBase64;
    private System.Windows.Forms.RadioButton loginPlain;
    private System.Windows.Forms.Label label9;
    private System.Windows.Forms.TextBox user;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.TextBox password;
    private System.Windows.Forms.Label label11;
    private System.Windows.Forms.Label label12;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button5;
    private System.Windows.Forms.ListBox ccList;
    private System.Windows.Forms.ListBox bccList;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TestForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      this.label1 = new System.Windows.Forms.Label();
      this.host = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.port = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.from = new System.Windows.Forms.TextBox();
      this.label4 = new System.Windows.Forms.Label();
      this.to = new System.Windows.Forms.TextBox();
      this.toList = new System.Windows.Forms.ListBox();
      this.button1 = new System.Windows.Forms.Button();
      this.label5 = new System.Windows.Forms.Label();
      this.subject = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.body = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.button2 = new System.Windows.Forms.Button();
      this.statusBar = new System.Windows.Forms.StatusBar();
      this.label8 = new System.Windows.Forms.Label();
      this.attachList = new System.Windows.Forms.ListBox();
      this.button3 = new System.Windows.Forms.Button();
      this.checkHTML = new System.Windows.Forms.CheckBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.loginPlain = new System.Windows.Forms.RadioButton();
      this.loginBase64 = new System.Windows.Forms.RadioButton();
      this.loginNone = new System.Windows.Forms.RadioButton();
      this.label9 = new System.Windows.Forms.Label();
      this.user = new System.Windows.Forms.TextBox();
      this.label10 = new System.Windows.Forms.Label();
      this.password = new System.Windows.Forms.TextBox();
      this.label11 = new System.Windows.Forms.Label();
      this.ccList = new System.Windows.Forms.ListBox();
      this.label12 = new System.Windows.Forms.Label();
      this.bccList = new System.Windows.Forms.ListBox();
      this.button4 = new System.Windows.Forms.Button();
      this.button5 = new System.Windows.Forms.Button();
      this.groupBox1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(8, 408);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(40, 16);
      this.label1.TabIndex = 0;
      this.label1.Text = "Host";
      // 
      // host
      // 
      this.host.Location = new System.Drawing.Point(8, 432);
      this.host.Name = "host";
      this.host.Size = new System.Drawing.Size(128, 20);
      this.host.TabIndex = 5;
      this.host.Text = "";
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(144, 408);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(32, 16);
      this.label2.TabIndex = 2;
      this.label2.Text = "Port";
      // 
      // port
      // 
      this.port.Location = new System.Drawing.Point(144, 432);
      this.port.Name = "port";
      this.port.Size = new System.Drawing.Size(40, 20);
      this.port.TabIndex = 6;
      this.port.Text = "25";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(0, 8);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(40, 16);
      this.label3.TabIndex = 4;
      this.label3.Text = "From";
      // 
      // from
      // 
      this.from.Location = new System.Drawing.Point(48, 8);
      this.from.Name = "from";
      this.from.Size = new System.Drawing.Size(200, 20);
      this.from.TabIndex = 0;
      this.from.Text = "";
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(0, 40);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(40, 16);
      this.label4.TabIndex = 6;
      this.label4.Text = "To";
      // 
      // to
      // 
      this.to.Location = new System.Drawing.Point(48, 40);
      this.to.Name = "to";
      this.to.Size = new System.Drawing.Size(200, 20);
      this.to.TabIndex = 1;
      this.to.Text = "";
      // 
      // toList
      // 
      this.toList.Location = new System.Drawing.Point(8, 272);
      this.toList.Name = "toList";
      this.toList.Size = new System.Drawing.Size(104, 134);
      this.toList.Sorted = true;
      this.toList.TabIndex = 5;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(264, 40);
      this.button1.Name = "button1";
      this.button1.TabIndex = 2;
      this.button1.Text = "Add To";
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(0, 72);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(48, 16);
      this.label5.TabIndex = 10;
      this.label5.Text = "Subject";
      // 
      // subject
      // 
      this.subject.Location = new System.Drawing.Point(48, 72);
      this.subject.Name = "subject";
      this.subject.Size = new System.Drawing.Size(200, 20);
      this.subject.TabIndex = 3;
      this.subject.Text = "";
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(0, 104);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(48, 16);
      this.label6.TabIndex = 12;
      this.label6.Text = "Body";
      // 
      // body
      // 
      this.body.AcceptsReturn = true;
      this.body.AcceptsTab = true;
      this.body.Location = new System.Drawing.Point(0, 128);
      this.body.Multiline = true;
      this.body.Name = "body";
      this.body.Size = new System.Drawing.Size(488, 112);
      this.body.TabIndex = 4;
      this.body.Text = "";
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(8, 248);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(48, 16);
      this.label7.TabIndex = 14;
      this.label7.Text = "To List";
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(496, 480);
      this.button2.Name = "button2";
      this.button2.TabIndex = 10;
      this.button2.Text = "Send";
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // statusBar
      // 
      this.statusBar.Location = new System.Drawing.Point(0, 536);
      this.statusBar.Name = "statusBar";
      this.statusBar.Size = new System.Drawing.Size(608, 22);
      this.statusBar.SizingGrip = false;
      this.statusBar.TabIndex = 16;
      this.statusBar.Text = "Ready.";
      // 
      // label8
      // 
      this.label8.Location = new System.Drawing.Point(376, 248);
      this.label8.Name = "label8";
      this.label8.Size = new System.Drawing.Size(88, 16);
      this.label8.TabIndex = 17;
      this.label8.Text = "Attachment List";
      // 
      // attachList
      // 
      this.attachList.Location = new System.Drawing.Point(376, 272);
      this.attachList.Name = "attachList";
      this.attachList.Size = new System.Drawing.Size(216, 134);
      this.attachList.TabIndex = 18;
      // 
      // button3
      // 
      this.button3.Location = new System.Drawing.Point(472, 248);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(104, 23);
      this.button3.TabIndex = 19;
      this.button3.Text = "Add Attachment";
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // checkHTML
      // 
      this.checkHTML.Location = new System.Drawing.Point(56, 96);
      this.checkHTML.Name = "checkHTML";
      this.checkHTML.TabIndex = 20;
      this.checkHTML.Text = "Send as HTML";
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                            this.loginPlain,
                                                                            this.loginBase64,
                                                                            this.loginNone});
      this.groupBox1.Location = new System.Drawing.Point(224, 416);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(256, 104);
      this.groupBox1.TabIndex = 7;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Login Type";
      // 
      // loginPlain
      // 
      this.loginPlain.Location = new System.Drawing.Point(8, 56);
      this.loginPlain.Name = "loginPlain";
      this.loginPlain.TabIndex = 2;
      this.loginPlain.Text = "Plain Text";
      this.loginPlain.CheckedChanged += new System.EventHandler(this.loginPlain_CheckedChanged);
      // 
      // loginBase64
      // 
      this.loginBase64.Location = new System.Drawing.Point(80, 24);
      this.loginBase64.Name = "loginBase64";
      this.loginBase64.TabIndex = 1;
      this.loginBase64.Text = "Base64";
      this.loginBase64.CheckedChanged += new System.EventHandler(this.loginBase64_CheckedChanged);
      // 
      // loginNone
      // 
      this.loginNone.Checked = true;
      this.loginNone.Location = new System.Drawing.Point(8, 24);
      this.loginNone.Name = "loginNone";
      this.loginNone.Size = new System.Drawing.Size(56, 24);
      this.loginNone.TabIndex = 0;
      this.loginNone.TabStop = true;
      this.loginNone.Text = "None";
      this.loginNone.CheckedChanged += new System.EventHandler(this.loginNone_CheckedChanged);
      // 
      // label9
      // 
      this.label9.Location = new System.Drawing.Point(8, 456);
      this.label9.Name = "label9";
      this.label9.Size = new System.Drawing.Size(40, 16);
      this.label9.TabIndex = 22;
      this.label9.Text = "User";
      // 
      // user
      // 
      this.user.Enabled = false;
      this.user.Location = new System.Drawing.Point(8, 480);
      this.user.Name = "user";
      this.user.Size = new System.Drawing.Size(80, 20);
      this.user.TabIndex = 8;
      this.user.Text = "";
      // 
      // label10
      // 
      this.label10.Location = new System.Drawing.Point(104, 456);
      this.label10.Name = "label10";
      this.label10.Size = new System.Drawing.Size(56, 16);
      this.label10.TabIndex = 24;
      this.label10.Text = "Password";
      // 
      // password
      // 
      this.password.Enabled = false;
      this.password.Location = new System.Drawing.Point(104, 480);
      this.password.Name = "password";
      this.password.PasswordChar = '*';
      this.password.Size = new System.Drawing.Size(80, 20);
      this.password.TabIndex = 9;
      this.password.Text = "";
      // 
      // label11
      // 
      this.label11.Location = new System.Drawing.Point(128, 248);
      this.label11.Name = "label11";
      this.label11.Size = new System.Drawing.Size(48, 16);
      this.label11.TabIndex = 25;
      this.label11.Text = "CC List";
      // 
      // ccList
      // 
      this.ccList.Location = new System.Drawing.Point(128, 272);
      this.ccList.Name = "ccList";
      this.ccList.Size = new System.Drawing.Size(104, 134);
      this.ccList.Sorted = true;
      this.ccList.TabIndex = 26;
      // 
      // label12
      // 
      this.label12.Location = new System.Drawing.Point(248, 248);
      this.label12.Name = "label12";
      this.label12.Size = new System.Drawing.Size(56, 16);
      this.label12.TabIndex = 27;
      this.label12.Text = "BCC List";
      // 
      // bccList
      // 
      this.bccList.Location = new System.Drawing.Point(248, 272);
      this.bccList.Name = "bccList";
      this.bccList.Size = new System.Drawing.Size(104, 134);
      this.bccList.Sorted = true;
      this.bccList.TabIndex = 28;
      // 
      // button4
      // 
      this.button4.Location = new System.Drawing.Point(344, 40);
      this.button4.Name = "button4";
      this.button4.TabIndex = 29;
      this.button4.Text = "Add CC";
      this.button4.Click += new System.EventHandler(this.button4_Click);
      // 
      // button5
      // 
      this.button5.Location = new System.Drawing.Point(424, 40);
      this.button5.Name = "button5";
      this.button5.TabIndex = 30;
      this.button5.Text = "Add BCC";
      this.button5.Click += new System.EventHandler(this.button5_Click);
      // 
      // TestForm
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(608, 558);
      this.Controls.AddRange(new System.Windows.Forms.Control[] {
                                                                  this.button5,
                                                                  this.button4,
                                                                  this.bccList,
                                                                  this.label12,
                                                                  this.ccList,
                                                                  this.label11,
                                                                  this.password,
                                                                  this.label10,
                                                                  this.user,
                                                                  this.label9,
                                                                  this.groupBox1,
                                                                  this.checkHTML,
                                                                  this.button3,
                                                                  this.attachList,
                                                                  this.label8,
                                                                  this.statusBar,
                                                                  this.button2,
                                                                  this.label7,
                                                                  this.body,
                                                                  this.label6,
                                                                  this.subject,
                                                                  this.label5,
                                                                  this.button1,
                                                                  this.toList,
                                                                  this.to,
                                                                  this.label4,
                                                                  this.from,
                                                                  this.label3,
                                                                  this.port,
                                                                  this.label2,
                                                                  this.host,
                                                                  this.label1});
      this.Name = "TestForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "TestEmailer";
      this.groupBox1.ResumeLayout(false);
      this.ResumeLayout(false);

    }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new TestForm());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if(to.Text.Trim().Length > 0)
			{
				toList.Items.Add(to.Text);
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			SmtpEmailer emailer = new SmtpEmailer();
			emailer.Host = host.Text;
			emailer.Port = Int32.Parse(port.Text);
			emailer.From = from.Text;
			emailer.Subject = subject.Text;
			emailer.Body = body.Text;
			emailer.SendAsHtml = checkHTML.Checked;
			for(int x = 0; x < toList.Items.Count; ++x)
			{
				emailer.To.Add((string) toList.Items[x]);
			}
      for(int x = 0; x < ccList.Items.Count; ++x)
      {
        emailer.CC.Add((string) ccList.Items[x]);
      }
      for(int x = 0; x < bccList.Items.Count; ++x)
      {
        emailer.BCC.Add((string) bccList.Items[x]);
      }
			for(int x = 0; x < attachList.Items.Count; ++x)
			{
				emailer.Attachments.Add((SmtpAttachment) attachList.Items[x]);
			}
      if(!loginNone.Checked)
      {
        emailer.User = user.Text;
        emailer.Password = password.Text;
        if(loginBase64.Checked)
        {
          emailer.AuthenticationMode = AuthenticationType.Base64;
        }
        else
        {
          emailer.AuthenticationMode = AuthenticationType.Plain;
        }
      }      
			emailer.OnMailSent += new SmtpEmailer.MailSentDelegate(OnMailSent);
			emailer.SendMessageAsync();
			statusBar.Text = "Sending...";
		}

		private void OnMailSent()
		{
			statusBar.Text = "Mail sent.";
			MessageBox.Show(this, "The mail has been sent.", "Mail Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.CheckFileExists = true;
			dlg.CheckPathExists = true;
			if(dlg.ShowDialog() == DialogResult.OK)
			{
				SmtpAttachment a = new SmtpAttachment();
				a.FileName = dlg.FileName;
				AttachType at = new AttachType();
				at.ShowDialog(this);
				if(at.DialogResult == DialogResult.OK)
				{
					a.ContentType = at.contentType.Text;
					a.Location = at.attachAttachment.Checked ? AttachmentLocation.Attachment : AttachmentLocation.Inline;
					attachList.Items.Add(a);
				}
			}
		}

    private void loginBase64_CheckedChanged(object sender, System.EventArgs e)
    {
      user.Enabled = true;
      password.Enabled = true;
    }

    private void loginPlain_CheckedChanged(object sender, System.EventArgs e)
    {
      user.Enabled = true;
      password.Enabled = true;
    }

    private void loginNone_CheckedChanged(object sender, System.EventArgs e)
    {
      user.Enabled = false;
      password.Enabled = false;
    }

    private void button4_Click(object sender, System.EventArgs e)
    {
      if(to.Text.Trim().Length > 0)
      {
        ccList.Items.Add(to.Text);
      }    
    }

    private void button5_Click(object sender, System.EventArgs e)
    {
      if(to.Text.Trim().Length > 0)
      {
        bccList.Items.Add(to.Text);
      }    
    }
	}
}
