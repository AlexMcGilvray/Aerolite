using Aerolite;
using Aerolite.Subsystems.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AeroliteTestWindows
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class TestGame : AeGame
    {
        public TestGame() : base()
        {
            Content.RootDirectory = "Content";
        }

        protected override void Load()
        {
            base.Load();
            //this.IsMouseVisible = true;
#if WINDOWS || LINUX
            Window.Position = new Point(10, 10);
            Engine.Graphics.GraphicsSettings.ScalingMode = AeScalingMode.UNIFORM_STRETCH;
            Engine.Graphics.GraphicsSettings.SetGameResolution(540, 960);
            Engine.Graphics.GraphicsSettings.SetScreenResolution(540, 960);
#else
            Engine.Graphics.GraphicsSettings.ScalingMode = AeScalingMode.STRETCH;
            Engine.Graphics.GraphicsSettings.SetGameResolution(540, 960);
            int screenWidth = Engine.Graphics.GraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferWidth;
            int screenHeight = Engine.Graphics.GraphicsDeviceManager.GraphicsDevice.PresentationParameters.BackBufferHeight;
            Engine.Graphics.GraphicsSettings.SetScreenResolution(screenWidth, screenHeight); //is this necessary?
            Engine.Graphics.GraphicsDeviceManager.IsFullScreen = true;
            Engine.Graphics.GraphicsDeviceManager.SupportedOrientations = DisplayOrientation.Portrait | DisplayOrientation.PortraitDown;
#endif
            //Engine.StateManager.PushState(new ShitGameState(Resources));
            Engine.StateManager.ChangeStateWithCurtains(new EntryState());

            IsMouseVisible = true;
        }
    }

}
