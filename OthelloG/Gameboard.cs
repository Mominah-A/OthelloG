using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OthelloG
{
	// The game board on which the player makes their moves
	public class GameBoard
	{
	// The size of the board
		public static int BoardSize;
		// the 2d array of moves according to the board blocks
		public static int[,] gameStateArray;

		// Move constants
		public readonly static int EMPTY = 0;
		public readonly static int POSSIBLE_MOVES = 1;
		 public readonly static int BLACK = 2;
		public readonly static int WHITE = 3;

		// Initialise the game board
		public GameBoard(int boardSize)
		{
			BoardSize = boardSize;
			gameStateArray = new int[BoardSize, BoardSize];
			Initialize();
		}

		
		public static void Initialize()
		{
			// Place the initial moves in the center of the board
			int i = BoardSize / 2 ;
			gameStateArray[i - 1, i - 1] = WHITE;
			gameStateArray[i - 1, i] = BLACK;
			gameStateArray[i, i - 1] = BLACK;
			 gameStateArray[i, i] = WHITE ;
		}

		// Get all the possible moves a player can play
		public void MakePossibleMoves(int move)
		{
			for (int col = 0; col < BoardSize; col++ )
			{
				for (int row = 0; row < BoardSize; row++)
				{
					if (gameStateArray[col, row] == POSSIBLE_MOVES)
					{
					gameStateArray[col, row] = EMPTY;
					}

					// if the move is valid then add it to the valid moves
					if (gameStateArray[col, row] == EMPTY && IsValidMove(col, row, move))
					{
						gameStateArray[col, row] = POSSIBLE_MOVES;
					}
				}
			}
		}

		// Check whether the move is valid or not
		public bool IsValidMove(int x, int y, int move)
		{
			int opponentMove = move == BLACK ? WHITE : BLACK;

			// if the move has already been made then the move is invalid
			if (gameStateArray[x, y] != POSSIBLE_MOVES && gameStateArray[x, y] != EMPTY)
			{
				return false;
			}

			// Initialise the x, y directions
		int[] directionsX = { -1, -1, -1, 0, 1, 1, 1, 0 };
			int[] directionsY = { -1, 0, 1, 1, 1, 0, -1, -1 };

			// This condition placed within the for lopop checks the move against all values
			for (int i = 0; i < 8; i++)
			{
				int dx = directionsX[i];
			int dy = directionsY[i];
				int step = 1;
				bool isConvert = false;

		while (true)
				{
					int newX = x + dx * step;
					int newY = y + dy * step;

					// discontinue after boundary
					if (newX < 0 || newX >= BoardSize || newY < 0 || newY >= BoardSize || gameStateArray[newX, newY] == EMPTY)
					{
					break;
					}

					// if the value matches the move of the opponent then move to next iteration
					if (gameStateArray[newX, newY] == opponentMove)
					{
						step++;
						continue;
					}

					// This checks if the player move matches and has some moves to convert
					if (gameStateArray[newX, newY] == move && step > 1)
					{
						isConvert = true;
					}

					break;
				}

				// if the player move is valid then return true
				if (isConvert)
				{
			return true;
				}
			}

			// if the move fails to match any values then it is invalid and will return false
			return false;
		}

		 // Check if the game is able to continue or not
		public bool IsGameContinuable()
		{
		    for (int col = 0; col < BoardSize; col++)
			{
				for (int row = 0; row < BoardSize; row++)
				{
					// if the move is valid return true
					if (gameStateArray[col, row] == EMPTY || gameStateArray[col, row] == POSSIBLE_MOVES)
					{
					       return true;
					}
				}
			}
			return false;
		}

		  // Count the number of player moves
		public int Count(int move)
		{
			int count = 0;
			for (int col = 0; col < BoardSize; col++)
			{
				for (int row = 0; row < BoardSize; row++)
				{
	// if the value matches the move then increment the count
					if (gameStateArray[col, row] == move)
					{
						count++;
					}
				}
			}
			return count;
		}

 // Make a player move
		public void Move(int x, int y, int move)
		{
			if (gameStateArray[x, y] != POSSIBLE_MOVES)
			{
				throw new InvalidOperationException("The selected square is not a valid move.");
			}

			 int opponentMove = move == BLACK ? WHITE : BLACK;
			gameStateArray[x, y] = move;

			int[] directionsX = { -1, -1, -1, 0, 1, 1, 1, 0 } ;
			 int[] directionsY = { -1, 0, 1, 1, 1, 0, -1, -1 };

			for (int i = 0; i < 8; i++)
			{
				int dx = directionsX[i];
				int dy = directionsY[i];
				int step = 1;
				 bool isConvert = false;

				while (true)
				{
					int newX = x + dx * step;
					int newY = y + dy * step;

					// discontinue after boundary
					if (newX < 0 || newX >= BoardSize || newY < 0 || newY >= BoardSize || gameStateArray[newX, newY] == EMPTY)
					{
						break;
					}

					// if the value matches the move of the opponent then move to the next iteration
					if (gameStateArray[newX, newY] == opponentMove)
					{
						step++;
						continue;
					}

					
					if (gameStateArray[newX, newY] == move && step > 1)
					{
						isConvert = true;
					}

					break;
				}

				// if the player move is valid then return true
				if (isConvert)
				{
					for (int j = 1; j < step; j++)
					{
						int newX = x + dx * j;
						int newY = y + dy * j;

						if (gameStateArray[newX, newY] == opponentMove)
						{
						gameStateArray[newX, newY] = move;
						}
						else
						{
				    	break;
						}
					}
				}
			}
		}
	}
}