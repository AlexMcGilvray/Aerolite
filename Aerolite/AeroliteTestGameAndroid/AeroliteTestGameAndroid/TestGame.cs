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
           // Window.Position = new Point(10, 10);
            Engine.Graphics.GraphicsSettings.ScalingMode = AeScalingMode.STRETCH;
            Engine.Graphics.GraphicsSettings.SetGameResolution(320, 240);
            Engine.Graphics.GraphicsSettings.SetScreenResolution(1920, 1080);
            Engine.StateManager.Add(new TestState());

        }
    }
}
