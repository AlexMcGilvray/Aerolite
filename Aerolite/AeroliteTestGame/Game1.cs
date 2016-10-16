using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Aerolite;
using Aerolite.Subsystems.Graphics;

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
            engine.GameReference.Window.Position = new Point(10, 10);

            engine.Graphics.GraphicsSettings.ScalingMode = AeScalingMode.CLOSEST_MULTIPLE_OF_2;
            engine.Graphics.GraphicsSettings.SetGameResolution(320, 240);
            engine.Graphics.GraphicsSettings.SetScreenResolution(1280, 960);
            engine.StateManager.Add(new TestState());

        }
    }
}
