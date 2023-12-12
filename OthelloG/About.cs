using System;
using System.Windows.Forms;

namespace OthelloG
{
	public partial class About : Form
	{
		public About()
		{
			InitializeComponent();
			richTextBox1.Text = "";
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
