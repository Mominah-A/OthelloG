using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OthelloG
{
	public partial class SaveGame : Form
	{
	public int SavedGameIndex;

		public SaveGame(List<State> gameStates )
		{
			InitializeComponent();
		List<string> gameNames = new List<string>() ;
			foreach (State state in gameStates)
			{
				gameNames.Add(state.dateTime.ToString());
			}
		cbSavedGames.DataSource = gameNames;
			cbSavedGames.SelectedIndex = 0;
		}

		private void btnOverwrite_Click(object sender, EventArgs e)
		{
	if (cbSavedGames.SelectedIndex != -1)
			{
				SavedGameIndex = cbSavedGames.SelectedIndex;
				this.DialogResult = DialogResult.OK;
			MessageBox.Show("Game state overwritten successfully!", "Save Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
			this.Close();
			}
		}
	}
}
