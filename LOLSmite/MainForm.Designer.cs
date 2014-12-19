/*
 * Created by SharpDevelop.
 * User: duketwo
 * Date: 01.12.2014
 * Time: 18:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace LOLSmite
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox logbox;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.button1 = new System.Windows.Forms.Button();
			this.logbox = new System.Windows.Forms.ListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(267, 218);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(126, 26);
			this.button1.TabIndex = 0;
			this.button1.Text = "draw floating text test";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// logbox
			// 
			this.logbox.FormattingEnabled = true;
			this.logbox.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.logbox.Location = new System.Drawing.Point(0, 0);
			this.logbox.Name = "logbox";
			this.logbox.Size = new System.Drawing.Size(1402, 212);
			this.logbox.TabIndex = 2;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(3, 218);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(126, 26);
			this.button2.TabIndex = 3;
			this.button2.Text = "devtools->log objects";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(135, 218);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(126, 26);
			this.button3.TabIndex = 4;
			this.button3.Text = "devtools->loginfo";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Button3Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Location = new System.Drawing.Point(12, 250);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(117, 22);
			this.checkBox1.TabIndex = 5;
			this.checkBox1.Text = "Smite Buffs!";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1403, 278);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.logbox);
			this.Controls.Add(this.button1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "LOLSmite - by duketwo ( seviers@gmail.com )";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}
