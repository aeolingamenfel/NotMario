using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;

using NotMario.Physics.Basics;
using NotMario.Utility;

namespace NotMario.Factories
{
	public class BlockFactory
	{
		private BlockFactory (){ }

		public static Block BuildBlockFromNode(XMLLiteNode node, GraphicsDevice graphics){
			Block output = new Block (graphics);

			// TODO: extra building

			return output;
		}
	}
}

