using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using NotMario.Physics;

namespace NotMario.Game.Elements
{
	public abstract class GameObject : PhysicsObject
	{
		public GameObject () : base(32, 32)
		{
			
		}

		public GameObject(int width, int height) : base(width, height){

		}

		public abstract void Draw(SpriteBatch batch);
		public abstract void Dispose();
	}
}

