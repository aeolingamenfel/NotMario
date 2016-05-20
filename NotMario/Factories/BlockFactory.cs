using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

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
			/*node.children["X"]. ("X" , out output.position.X)

			if (node.TryGetValue  ){
				output.postion.Y = val
			}
			if (node.TryGetValue){
				output.buidColor = ValueType

			}
			if (node.TryGetValue){
				output.texture.Bounds.Width = val
			}
			if (node.TryGetValue){
				oupput.texture.Bounds.Height = val
			}*/
			string childVal;
			if (node.HasChild("color")){
				XMLLiteNode colorNode = node.children["color"];
				
				Color outputColor = new Color(
					int.Parse(colorNode.GetAttribute("r")),
					int.Parse(colorNode.GetAttribute("g")),
					int.Parse(colorNode.GetAttribute("b"))
				);

				output.SetColor (outputColor);
			}
			
			childVal = node.GetChildValue ("X");
			if (!childVal.Equals(string.Empty)) {
				output.position.X = float.Parse(childVal);
			}
			childVal = node.GetChildValue ("Y");
			if (!childVal.Equals(string.Empty)) {
				output.position.Y = float.Parse(childVal);
			}

			childVal = node.GetChildValue ("width");
			if (!childVal.Equals(string.Empty)) {
				output.SetWidth (int.Parse(childVal));
			}
			childVal = node.GetChildValue ("height");
			if (!childVal.Equals(string.Empty)) {
				output.SetHeight (int.Parse(childVal));
			}
			return output;
		}
	}
}

