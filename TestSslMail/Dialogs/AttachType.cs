/*
 * Copyright (C)2014 Araz Farhang Dareshuri
 * This file is a part of Aegis Implicit Mail (AIM)
 * Aegis Implicit Mail is free software: 
 * you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  
 * See the GNU General Public License for more details.
 * You should have received a copy of the GNU General Public License along with this program.  
 * If not, see <http://www.gnu.org/licenses/>.
 *
 * If you need any more details please contact <a.farhang.d@gmail.com>
 * 
 * Aegis Implicit Mail is an implict ssl package to use mine/smime messages on implict ssl servers
 */

using System.Windows.Forms;

namespace TestEmailer
{
	/// <summary>
	/// Summary description for AttachType.
	/// </summary>
	public class AttachType : Form
	{
		private GroupBox groupBox1;
	    public RadioButton attachAttachment;
		internal RadioButton attachInline;
	    public TextBox contentType;
		private Label label1;
		private Button button1;
		private Button button2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private readonly System.ComponentModel.Container _components = null;

		public AttachType()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(_components != null)
				{
					_components.Dispose();
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
			this.groupBox1 = new GroupBox();
			this.attachInline = new RadioButton();
			this.attachAttachment = new RadioButton();
			this.contentType = new TextBox();
			this.label1 = new Label();
			this.button1 = new Button();
			this.button2 = new Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.AddRange(new Control[] {
																																						this.attachInline,
																																						this.attachAttachment});
			this.groupBox1.Location = new System.Drawing.Point(8, 40);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(264, 96);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Attachment Location";
			// 
			// attachInline
			// 
			this.attachInline.Location = new System.Drawing.Point(8, 56);
			this.attachInline.Name = "attachInline";
			this.attachInline.Size = new System.Drawing.Size(152, 24);
			this.attachInline.TabIndex = 1;
			this.attachInline.Text = "As Inline HTML Image";
			this.attachInline.CheckedChanged += new System.EventHandler(this.attachInline_CheckedChanged);
			// 
			// attachAttachment
			// 
			this.attachAttachment.Checked = true;
			this.attachAttachment.Location = new System.Drawing.Point(8, 24);
			this.attachAttachment.Name = "attachAttachment";
			this.attachAttachment.TabIndex = 0;
			this.attachAttachment.TabStop = true;
			this.attachAttachment.Text = "As Attachment";
			// 
			// contentType
			// 
			this.contentType.Location = new System.Drawing.Point(120, 16);
			this.contentType.Name = "contentType";
			this.contentType.Size = new System.Drawing.Size(152, 20);
			this.contentType.TabIndex = 1;
			this.contentType.Text = "application/octet-stream";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 16);
			this.label1.Name = "label1";
			this.label1.TabIndex = 2;
			this.label1.Text = "Content Type";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(48, 152);
			this.button1.Name = "button1";
			this.button1.TabIndex = 3;
			this.button1.Text = "Ok";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(144, 152);
			this.button2.Name = "button2";
			this.button2.TabIndex = 4;
			this.button2.Text = "Cancel";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// AttachType
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(298, 205);
			this.Controls.AddRange(new Control[] {
																																	this.button2,
																																	this.button1,
																																	this.label1,
																																	this.contentType,
																																	this.groupBox1});
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "AttachType";
			this.StartPosition = FormStartPosition.CenterParent;
			this.Text = "AttachType";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Hide();
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Hide();
		}

		private void attachInline_CheckedChanged(object sender, System.EventArgs e)
		{
			if(!attachInline.Checked)
			{
				contentType.Text = "application/octet-stream";
			}
		}
	}
}
