using System;

using Microsoft.Xna.Framework;

namespace NotMario
{
	public abstract class PhysicsObject
	{
		public Vector2 speed;
		public Vector2 position;

		public float rotation;
		public float rotationSpeed;

		protected Rectangle drawRect;

		public PhysicsObject (int width, int height)
		{
			this.speed = new Vector2 (0.0f, 0.0f);
			this.position = new Vector2 (0.0f, 0.0f);
			this.rotation = 0f;
			this.rotationSpeed = 0f;

			this.drawRect = new Rectangle ((int)this.position.X, (int)this.position.Y,
				width, height);
		}

		public void Update(GameTime gameTime){
			float fraction = (float)gameTime.ElapsedGameTime.TotalSeconds;

			this.position.X += this.speed.X * fraction;
			this.position.Y += this.speed.Y * fraction;

			this.rotation += this.rotationSpeed * fraction;

			this.drawRect.X = (int)this.position.X;
			this.drawRect.Y = (int)this.position.Y;
		}
	}
}

