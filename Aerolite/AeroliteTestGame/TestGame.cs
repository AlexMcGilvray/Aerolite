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
    public class TestGame : AeGame
    {
        
        public TestGame():base()
        {
            Content.RootDirectory = "Content";
        }
 
        protected override void Load()
        {
            base.Load();
            Window.Position = new Point(10, 10);
            Engine.Graphics.GraphicsSettings.ScalingMode = AeScalingMode.CLOSEST_MULTIPLE_OF_2;
            Engine.Graphics.GraphicsSettings.SetGameResolution(320, 240);
            Engine.Graphics.GraphicsSettings.SetScreenResolution(1280, 960);
            Engine.StateManager.Add(new TestState());

        }
    }
}
