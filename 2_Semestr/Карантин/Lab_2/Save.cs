using System;
using System.Drawing;
using System.Xml.Serialization;

namespace Lab_2
{
	[Serializable]
	public class Save
	{
		public bool[] CheckBox;

		[XmlElement]
		public Point Position
		{
			get;
			set;
		}

		[XmlElement]
		public Size Size
		{
			get;
			set;
		}

		public string TextBox
		{
			get;
			set;
		}

		public Save()
		{
		}

		public Save(Point p, Size s, string textbox, bool[] check)
		{
			Position = p;
			Size = s;
			TextBox = textbox;
			CheckBox = check;
		}
	}
}
