using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Lab_2
{
	public class Form1 : Form
	{
		private IContainer components = null;

		private TextBox txtBox;

		private CheckBox chkBoxOne;

		private CheckBox chkBoxTwo;

		public string Company
		{
			get;
			set;
		} = "Victor Corp.";


		public string AppName
		{
			get;
			set;
		} = "Lab_2";


		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			LoadFromReg();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveReg();
		}

		private void SaveXml()
		{
			Save o = new Save(base.Location, base.Size, txtBox.Text, new bool[2]
			{
				chkBoxOne.Checked,
				chkBoxTwo.Checked
			});
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(Save));
			if (!File.Exists("Save.xml"))
			{
				File.Create("Save.xml");
			}
			using (Stream stream = File.Open("Save.xml", FileMode.Truncate, FileAccess.Write, FileShare.None))
			{
				xmlSerializer.Serialize(stream, o);
			}
		}

		private void LoadFromXml()
		{
			using (Stream stream = File.Open("Save.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(Save));
				Save save = (Save)xmlSerializer.Deserialize(stream);
				base.Location = save.Position;
				base.Size = save.Size;
				txtBox.Text = save.TextBox;
				chkBoxOne.Checked = save.CheckBox[0];
				chkBoxTwo.Checked = save.CheckBox[1];
			}
		}

		private void SaveReg()
		{
			RegistryKey registryKey = null;
			try
			{
				registryKey = Registry.CurrentUser.OpenSubKey($"Software\\{Company}\\{AppName}\\", writable: true);
				if (registryKey == null)
				{
					throw new NullReferenceException();
				}
			}
			catch (NullReferenceException)
			{
				registryKey = Registry.CurrentUser.CreateSubKey($"Software\\{Company}\\{AppName}\\");
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message, ex2.Source, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			try
			{
				registryKey.SetValue("Pos_x", base.Location.X, RegistryValueKind.DWord);
				registryKey.SetValue("Pos_y", base.Location.Y, RegistryValueKind.DWord);
				registryKey.SetValue("Width", base.Width, RegistryValueKind.DWord);
				registryKey.SetValue("Height", base.Height, RegistryValueKind.DWord);
				registryKey.SetValue("Text", txtBox.Text, RegistryValueKind.String);
				registryKey.SetValue("Check 1", chkBoxOne.Checked.ToString(), RegistryValueKind.String);
				registryKey.SetValue("Check 2", chkBoxTwo.Checked.ToString(), RegistryValueKind.String);
			}
			catch (Exception ex3)
			{
				MessageBox.Show(ex3.Message, ex3.Source, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		private void LoadFromReg()
		{
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.OpenSubKey($"Software\\{Company}\\{AppName}\\");
				base.Location = new Point((int)registryKey.GetValue("Pos_x"), (int)registryKey.GetValue("Pos_y"));
				base.Size = new Size((int)registryKey.GetValue("Width"), (int)registryKey.GetValue("Height"));
				txtBox.Text = (string)registryKey.GetValue("Text");
				chkBoxOne.Checked = bool.Parse((string)registryKey.GetValue("Check 1"));
				chkBoxTwo.Checked = bool.Parse((string)registryKey.GetValue("Check 2"));
			}
			catch (NullReferenceException ex)
			{
				MessageBox.Show("There is no saved data", ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			catch (Exception ex2)
			{
				MessageBox.Show(ex2.Message, ex2.Source, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			txtBox = new System.Windows.Forms.TextBox();
			chkBoxOne = new System.Windows.Forms.CheckBox();
			chkBoxTwo = new System.Windows.Forms.CheckBox();
			SuspendLayout();
			txtBox.Location = new System.Drawing.Point(12, 12);
			txtBox.Name = "txtBox";
			txtBox.Size = new System.Drawing.Size(202, 22);
			txtBox.TabIndex = 0;
			chkBoxOne.AutoSize = true;
			chkBoxOne.Location = new System.Drawing.Point(12, 40);
			chkBoxOne.Name = "chkBoxOne";
			chkBoxOne.Size = new System.Drawing.Size(98, 21);
			chkBoxOne.TabIndex = 1;
			chkBoxOne.Text = "checkBox1";
			chkBoxOne.UseVisualStyleBackColor = true;
			chkBoxTwo.AutoSize = true;
			chkBoxTwo.Location = new System.Drawing.Point(116, 40);
			chkBoxTwo.Name = "chkBoxTwo";
			chkBoxTwo.Size = new System.Drawing.Size(98, 21);
			chkBoxTwo.TabIndex = 2;
			chkBoxTwo.Text = "checkBox2";
			chkBoxTwo.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(8f, 16f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(246, 88);
			base.Controls.Add(chkBoxTwo);
			base.Controls.Add(chkBoxOne);
			base.Controls.Add(txtBox);
			base.Name = "Form1";
			Text = "Form1";
			base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Form1_FormClosing);
			base.Load += new System.EventHandler(Form1_Load);
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
