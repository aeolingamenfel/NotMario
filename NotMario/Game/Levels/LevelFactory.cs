using System;

using NotMario.Utility;

namespace NotMario.Game.Levels
{
	public class LevelFactory
	{
		public static string LEVEL_FOLDER = "Content/Levels/";

		private LevelFactory () {}

		/// <summary>
		/// 	Loads the level specified. Do not
		/// 	start level name with a "/" 
		/// 	character.
		/// </summary>
		/// <returns>The level.</returns>
		/// <param name="levelName">Level name.</param>
		public static Level loadLevel(string levelName){
			XMLLiteNode nameNode;
			XMLLiteNode blocksNode;

			Level output = new Level ();
			DataReader reader = new DataReader (LEVEL_FOLDER + levelName + ".level");

			// Load Name
			if (reader.data.TryGetValue ("name", out nameNode)) {
				output.name = nameNode.GetValue ();
			} else {
				Console.WriteLine ("WARNING: Level file \"" + levelName + "\" does not have a 'name' attribute.");
			}

			// Load Blocks
			if (reader.data.TryGetValue ("blocks", out blocksNode)) {

			}

			return output;
		}
	}
}

