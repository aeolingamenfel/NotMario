﻿using System;
using System.IO;
using System.Reflection;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using NotMario.Physics.Basics;
using NotMario.Game.Levels;
using NotMario.Utility;
using NotMario.Factories;

namespace NotMario
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class NotMarioGame1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Level currentLevel;

		string currentLevelName;

		public NotMarioGame1 ()
		{
			currentLevelName = "Level1";

			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";

			this.IsFixedTimeStep = false;
			this.graphics.SynchronizeWithVerticalRetrace = false;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// File reader test
			Level loadedLevel = LevelFactory.loadLevel(this.currentLevelName, this.GraphicsDevice);

			// Setup
			currentLevel = loadedLevel;
            
			base.Initialize ();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// This is only true when the game window
			// has focus.
			if (IsActive) {
				// For Mobile devices, this logic will close the Game when the Back button is pressed
				// Exit() is obsolete on iOS
				#if !__IOS__ &&  !__TVOS__
				if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed
				    || Keyboard.GetState ().IsKeyDown (Keys.Escape))
					Exit ();
				#endif
            
				// Move the rectangle right slowly
				//rect.Update(gameTime);

				base.Update (gameTime);
			}

		}

		protected override void UnloadContent(){
			currentLevel.Dispose ();

			Content.Unload ();
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);
            
			// Sprite Drawing
			this.spriteBatch.Begin();

			currentLevel.Draw (this.spriteBatch);

			this.spriteBatch.End ();
            
			base.Draw (gameTime);
		}
	}
}

