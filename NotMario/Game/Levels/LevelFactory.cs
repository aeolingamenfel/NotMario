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
			Level output = new Level ();
			DataReader reader = new DataReader (LEVEL_FOLDER + levelName + ".level");

			// Load Name
			if (reader.data.ContainsKey ("name")) {
				output.name = reader.data ["name"].GetValue ();
			} else {
				Console.WriteLine ("WARNING: Level file \"" + levelName + "\" does not have a 'name' attribute.");
			}

			return output;
		}
	}
}

