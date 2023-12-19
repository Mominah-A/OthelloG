namespace OthelloG
{
	using Newtonsoft.Json;
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.IO;
	using System.Windows.Forms;

	public partial class GUI : Form
	{
		// This is the game logic
		private GameBoard board;
		// Current player
		private int currentPlayer;
		// GUI
		private GameboardImageArray gameboardImageArray;
	// This check if the game is in progress
		private bool gameInProgrss = false;
		// Board size
		private const int BoardSize = 8;
		// Maximum number of game states
		private const int MaxGameStates = 5;
		// Saving file
		 private const string SaveFileName = "game_data.json";

		public GUI()
		{
			// Initialise the GUI
			InitializeComponent();
		gameboardImageArray = new GameboardImageArray (pnlBoardContainer, new int[BoardSize, BoardSize], new Point(0, 0), new Point(0, 0), 2, ".\\..\\..\\");
			gameboardImageArray.TileClicked += Button_Click;
			 NewGame();

		}

		
		private void Button_Click(object sender, EventArgs e)
		{
			if (!gameInProgrss)
			{
				// Start the game
			gameInProgrss = true;
			}

		int x = gameboardImageArray.GetCurrentRowIndex(sender);
			int y = gameboardImageArray.GetCurrentColumnIndex(sender);

			// check if the player plays a valid move
			if (board.IsValidMove(x, y, currentPlayer))
			{
				// If the move is valid, then update player move and shift control to the other player
				
				player1NameTextBox.Enabled  = false;
				player2NameTextBox.Enabled = false;
				player1NameTextBox.Visible = false;
				 player2NameTextBox.Visible = false;
				 blackPlayerLabel.Visible = true;
				whitePlayerLabel.Visible = true;
				board.Move(x, y, currentPlayer);
				UpdatePlayerTurnDisplay();

			// Show the players a message if the game is over and start a new game
				if (!board.IsGameContinuable())
				{
					ShowGameOverMessage();
					  NewGame();
				}
			}
		}

		// Update the turn of a player to indicate which player is currently playing
		private void UpdatePlayerTurnDisplay()
		{
			// change the current player to the other player
			currentPlayer = currentPlayer == GameBoard.BLACK ? GameBoard.WHITE : GameBoard.BLACK ;
			board.MakePossibleMoves(currentPlayer);

			if (currentPlayer == GameBoard.WHITE)
			{
				blackPlayerLabel.Text = $"Turn -> {player2NameTextBox.Text}: {board.Count(GameBoard.BLACK)}";
				 whitePlayerLabel.Text = $"{player1NameTextBox.Text}: {board.Count(GameBoard.WHITE)}";
			}
		else
			{
				blackPlayerLabel.Text = $"{player2NameTextBox.Text}: {board.Count(GameBoard.BLACK)}";
			whitePlayerLabel.Text = $"Turn -> {player1NameTextBox.Text}: {board.Count(GameBoard.WHITE)}";
			}

			gameboardImageArray.UpdateBoardGui(GameBoard.gameStateArray);
		}

		// Reinitialise the game board and set the new game
		private void NewGame()
		{
			gameInProgrss = false;
			board = new GameBoard(BoardSize);
			GameBoard.Initialize();
			currentPlayer = GameBoard.BLACK;
			board.MakePossibleMoves(currentPlayer);
			gameboardImageArray.UpdateBoardGui(GameBoard.gameStateArray);
			 InitialPlayerDetails();
			player1NameTextBox.Enabled = true;
			player2NameTextBox.Enabled = true;
			player1NameTextBox.Visible = true;
			 player2NameTextBox.Visible = true;
			blackPlayerLabel.Visible = false;
			whitePlayerLabel.Visible = false;
			player1NameTextBox.Text = "Player 1";
			 player2NameTextBox.Text = "Player 2";
			UpdatePlayerTurnDisplay();
		}

		private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gameInProgrss)
			{
				if (DialogResult.Yes == MessageBox.Show("The game is currently in progress, do you want to save the game state?", "Save Game", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
				{
				// Save the game to a json file
					SaveGame();
				}
			}
			// Create a new game
		NewGame();
		}

		    // Show the game message to display the winner and when it is the end of the game
		private void ShowGameOverMessage()
		{
			    int blackScore = 0, whiteScore = 0;

			// Calculate player scores
			for (int i = 0; i < GameBoard.BoardSize; i++)
			{
				for (int j = 0; j < GameBoard.BoardSize; j++)
				{
			if (GameBoard.gameStateArray[i, j] == GameBoard.BLACK)
					{
						blackScore++;
					}
				else if (GameBoard.gameStateArray[i, j] == GameBoard.WHITE)
					{
						whiteScore++;
					}
				}
			}

			// Show the winner
			  string winner;
			if (blackScore > whiteScore)
			{
				winner = $"{player2NameTextBox.Text} (BLACK) wins!";
			}
			else if (whiteScore > blackScore)
			{
		winner = $"{player1NameTextBox.Text} (WHITE) wins!";
			}
			else
			{
				winner = "It's a tie!";
			}

			// Display the message to the players
			MessageBox.Show($"Game Over!\nBLACK: {blackScore}\nWHITE: {whiteScore}\n{winner}", "Game Over", MessageBoxButtons.OK);
		}

	
		private void InitialPlayerDetails()
		{
			// Update the statistics in the bottom information panel
			int blackCount = board.Count(GameBoard.BLACK);
		int whiteCount = board.Count(GameBoard.WHITE);
			blackPlayerLabel.Text = $"Player 1 (BLACK): {blackCount}";
			whitePlayerLabel.Text = $"Player 2 (WHITE): {whiteCount}";
		}

		private void informationPanelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Toggle display the information panel
			bool isVisible = informationPanelToolStripMenuItem.Checked;
			informationPanelToolStripMenuItem.Checked = !isVisible;
			tableLayoutPanel1.RowStyles[1].Height = isVisible ? 0 : 109;
		}

		// Save the current state of the game
		private void SaveGame()
		{
	bool isNewSave = false;

			// Get the game state
			State gameState = new State
			{
				Board = new int[GameBoard.BoardSize, GameBoard.BoardSize],
				CurrentPlayer = currentPlayer,
				 Player1Name = player1NameTextBox.Text,
				Player2Name = player2NameTextBox.Text,
			dateTime = DateTime.Now
			};

			// Get the values in the game state board
			for (int i = 0; i < GameBoard.BoardSize; i++)
			{
				for (int j = 0; j < GameBoard.BoardSize; j++)
				{
					gameState.Board[i, j] = GameBoard.gameStateArray[i, j];
				}
			}

			// Load existing game states
			List<State> gameStates = LoadGameStates();

			// Check if the maximum limit is reached
			if (gameStates.Count >= MaxGameStates)
			{
				// Ask the user which game state to overwrite
				int gameStateIndexToOverwrite = GetUserInputForOverwrite(gameStates);
				if (gameStateIndexToOverwrite == -1)
				{
					// User cancelled the operation
					return;
				}

				// Remove the selected game state to overwrite
				gameStates.RemoveAt(gameStateIndexToOverwrite);
			}
			else
			{
				isNewSave = true;
			}

			// Add the new game state to the list
			gameStates.Add(gameState);

			// Write the JSON format output to the given file
			string json = JsonConvert.SerializeObject(gameStates);
			File.WriteAllText(SaveFileName, json);

			if (isNewSave)
			{
				// Show this message to the user
				MessageBox.Show("Game state saved successfully! \n\nRemaing free slots: " + (MaxGameStates - gameStates.Count), "Save Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		// Load existing game states from the file
		private List<State> LoadGameStates()
		{
			if (File.Exists(SaveFileName))
			{
			string json = File.ReadAllText(SaveFileName);
				return JsonConvert.DeserializeObject<List<State>>(json) ?? new List<State>();
			}
			return new List<State>();
		}

		// Ask the user which game state to overwrite
		private int GetUserInputForOverwrite(List<State> gameStates)
		{
			SaveGame saveGame = new SaveGame(gameStates);
	if (saveGame.ShowDialog() == DialogResult.OK)
			{
				return saveGame.SavedGameIndex;
			}
			return -1;
		}


		private void saveGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Save game to a json file
			SaveGame();
		}

		private void exitGameToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (gameInProgrss)
			{
				if (DialogResult.Yes == MessageBox.Show("The game is currently in progress, do you want to save the game state?", "Save Game", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
				{
					// Save game to a json file
					SaveGame();
				}
			}
			Application.Exit();
		}

		private void aboutToolStripMenuItem_Click(Object sender, EventArgs e)
		{
			// Display the about screen
			About about = new About();
			about.ShowDialog();
		}
	}
}