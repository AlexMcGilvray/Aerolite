using Aerolite.Subsystems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite
{
    public sealed class AeEngine
    {
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public AeTextureManager TextureManager { get; private set; }
        public AeGame GameReference;

        private void InitSubsystems()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(GameReference);
            TextureManager = new AeTextureManager(GameReference);
        }

        public void Update(GameTime gameTime)
        {
            string ex = "dfds";
        }

        public void Render(GameTime gameTime)
        {
            GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        #region Singleton 
        private static AeEngine instance;
       
        private AeEngine(AeGame game)
        {
            GameReference = game;
        }
        public static void Initalize(AeGame game)
        {
            instance = new AeEngine(game);
            instance.InitSubsystems();
            //instance.State = new RsStateManager();
            //instance.TextureManager = new RsTextureManager(game);
            //instance.Input = new RsInput();
            //instance.Sound = new RsSoundManager(game);
            //instance.GraphicsSettings = new RsGraphicsSettings();
            //instance.batch = new SpriteBatch(instance.GraphicsDeviceManager.GraphicsDevice);
            //instance.Init();
        }

        public static AeEngine Singleton()
        {
            if (instance == null)
            {
                throw new Exception("Engine not initialized before use.");
            }
            return instance;
        }
        #endregion
    }
}
