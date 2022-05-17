using System;
using System.IO;
using System.Text;

namespace ARCommander.Tests
{
	public class ConsoleCapture : TextWriter
	{
		private StringBuilder Captured = new StringBuilder();
		public string Output
		{
			get => Captured.ToString();
		}

		public override void Write(char value)
		{
			Captured.Append(value);
		}

		public override Encoding Encoding
		{
			get => Encoding.Default;
		}
	}
}