using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Aerolite;

namespace AeroliteTestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : AeGame
    {
        
        public Game1():base()
        {
            Content.RootDirectory = "Content";
        }
 
        protected override void Load()
        {
            base.Load();
            var engine = AeEngine.Singleton();
            engine.Graphics.GraphicsSettings.SetGameResolution(640, 360);
            engine.Graphics.GraphicsSettings.SetScreenResolution(1280, 720);
            engine.StateManager.Add(new TestState());

        }
    }
}
