using System;

using Microsoft.Xna.Framework;

namespace NotMario
{
	public abstract class PhysicsObject
	{
		public Vector2 speed;
		public Vector2 position;

		public PhysicsObject ()
		{
			this.speed = new Vector2 (0.0f, 0.0f);
			this.position = new Vector2 (0.0f, 0.0f);
		}

		public void Update(GameTime gameTime){
			float fraction = (float)gameTime.ElapsedGameTime.TotalSeconds;

			this.position.X += this.speed.X * fraction;
			this.position.Y += this.speed.Y * fraction;
		}
	}
}

