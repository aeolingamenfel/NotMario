﻿#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

using MonoMac.AppKit;
using MonoMac.Foundation;

#endregion

namespace NotMario
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main (string[] args)
		{
			NSApplication.Init ();

			using (var game = new NotMarioGame1 ()) {
				game.Run ();
			}
		}
	}
}


