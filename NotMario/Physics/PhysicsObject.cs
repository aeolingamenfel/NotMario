using System;

using NotMario.Game.Levels;

using Microsoft.Xna.Framework;

namespace NotMario
{
	public abstract class PhysicsObject
	{
		public Vector2 speed;
		protected Vector2 position;
		protected Vector2 dimensions;

		public float rotation;
		public float rotationSpeed;

		public PhysicsObject (int width, int height)
		{
			this.speed = new Vector2 (0.0f, 0.0f);
			this.position = new Vector2 (0.0f, 0.0f);
			this.dimensions = new Vector2 (width * Level.GRID_SIZE, height * Level.GRID_SIZE);

			this.rotation = 0f;
			this.rotationSpeed = 0f;
		}

		public virtual void SetPosition(float x, float y){
			this.position.X = x;
			this.position.Y = y;
		}

		public virtual void SnapToGrid(float x, float y){
			this.position.X = (x * Level.GRID_SIZE);
			this.position.Y = (y * Level.GRID_SIZE);
		}

		public virtual void SnapXToGrid(float x){
			this.position.X = (x * Level.GRID_SIZE);
		}

		public virtual void SnapYToGrid(float y){
			this.position.Y = (y * Level.GRID_SIZE);
		}

		public virtual void SetWidth(int newValue){
			this.dimensions.X = (newValue * Level.GRID_SIZE);
		}

		public virtual void SetHeight(int newValue){
			this.dimensions.Y = (newValue * Level.GRID_SIZE);
		}

		public void Update(GameTime gameTime){
			float fraction = (float)gameTime.ElapsedGameTime.TotalSeconds;

			this.position.X += this.speed.X * fraction;
			this.position.Y += this.speed.Y * fraction;

			this.rotation += this.rotationSpeed * fraction;
		}
	}
}

