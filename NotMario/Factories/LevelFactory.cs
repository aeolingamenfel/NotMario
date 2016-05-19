using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using NotMario.Utility;
using NotMario.Game.Elements;
using NotMario.Game.Levels;
using NotMario.Physics.Basics;

namespace NotMario.Factories
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
		public static Level loadLevel(string levelName, GraphicsDevice graphics){
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
			if (reader.data.TryGetValue ("objects", out blocksNode)) {
				List<XMLLiteNode> objectNodes = blocksNode.lists ["object"];

				IEnumerator<XMLLiteNode> enumerator = objectNodes.GetEnumerator ();

				while (enumerator.MoveNext ()) {

					XMLLiteNode currentObjectNode = enumerator.Current;
					GameObject currentObject = null;
					string type;

					if (currentObjectNode.attributes.TryGetValue ("type", out type)) {
						switch (type) {
						case "block":
							currentObject = BlockFactory.BuildBlockFromNode (currentObjectNode, graphics);
							break;
						default:
							Console.WriteLine("WARNING: unrecognized object type \"" + type + "\"");
							break;
						}
					} else {
						Console.WriteLine ("Ignoring object; no type supplied.");
					}

					if (currentObject != null)
						output.addObject (currentObject);
				}
			}

			return output;
		}
	}
}

