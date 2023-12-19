using System;

namespace OthelloG
{
	// The states of the game
	public class State
	{
		public int[,] Board { get; set; }
		public int CurrentPlayer { get; set; }
		public string Player1Name { get; set; }
		public string Player2Name { get; set; }
		public DateTime dateTime { get; set; }
	}
}
