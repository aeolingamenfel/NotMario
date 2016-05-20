using System;

using Microsoft.Xna.Framework;

namespace NotMario
{
	public abstract class PhysicsObject
	{
		public Vector2 speed;
		public Vector2 position;
		protected Vector2 dimensions;

		public float rotation;
		public float rotationSpeed;

		public PhysicsObject (int width, int height)
		{
			this.speed = new Vector2 (0.0f, 0.0f);
			this.position = new Vector2 (0.0f, 0.0f);
			this.dimensions = new Vector2 (width, height);

			this.rotation = 0f;
			this.rotationSpeed = 0f;
		}

		public virtual void SetWidth(int newValue){
			this.dimensions.X = newValue;
		}

		public virtual void SetHeight(int newValue){
			this.dimensions.Y = newValue;
		}

		public void Update(GameTime gameTime){
			float fraction = (float)gameTime.ElapsedGameTime.TotalSeconds;

			this.position.X += this.speed.X * fraction;
			this.position.Y += this.speed.Y * fraction;

			this.rotation += this.rotationSpeed * fraction;
		}
	}
}

