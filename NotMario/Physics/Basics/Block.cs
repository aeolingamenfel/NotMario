using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NotMario.Physics.Basics;
using NotMario.Game.Elements;

namespace NotMario.Physics.Basics
{
	public class Block : GameObject
	{
		protected Texture2D texture;

		public Block (GraphicsDevice graphics) 
			: base(10, 10){
			this.position = new Vector2 (10.0F, 10.0F);
			this.texture = new Texture2D (graphics, 10, 10);

			this.buildColor (Color.Red);
		}

		public Block (Color color, GraphicsDevice graphics) 
			: base(10, 10){
			this.position = new Vector2 (10.0F, 10.0F);
			this.texture = new Texture2D (graphics, 10, 10);

			this.buildColor (color);
		}

		public Block (float x, float y, int width, int height, GraphicsDevice graphics) 
			: base(width, height){
			this.position.X = x;
			this.position.Y = y;
			this.texture = new Texture2D (graphics, width, height);

			this.buildColor (Color.Red);
		}

		public Block (float x, float y, int width, int height, Color color, GraphicsDevice graphics) 
			: base(width, height){
			this.position.X = x;
			this.position.Y = y;
			this.texture = new Texture2D (graphics, width, height);

			this.buildColor (color);
		}

		protected void buildColor(Color color){
			int x = this.texture.Bounds.Height;
			int y = this.texture.Bounds.Width;
			int dimension = x * y;

			Color[] colorMap = new Color[dimension];

			for (int i = 0; i < dimension; i++) {
				colorMap [i] = color;
			}

			this.texture.SetData<Color> (colorMap);
		}

		public override void Draw(SpriteBatch batch){
			batch.Draw (this.texture, position: this.position, rotation: this.rotation);
		}

		public override void Dispose(){
			this.texture.Dispose ();
		}
	}
}

