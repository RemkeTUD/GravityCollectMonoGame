﻿using System;

namespace Game1
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1()) {
                game.IsMouseVisible = true;
                game.IsFixedTimeStep = true;
                game.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 60.0f);
                game.Run();
            }
        }
    }
#endif
}
