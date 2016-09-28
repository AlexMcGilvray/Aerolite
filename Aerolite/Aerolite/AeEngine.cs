using Aerolite.Subsystems;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite
{
    public class AeEngine
    {
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        public AeEngine()
        {

        }

        private void InitSubsystems()
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void Render()
        {

        }

        #region Singleton 
        private static AeEngine instance;
        public AeGame GameReference;
        private AeEngine(AeGame game)
        {
            GameReference = game;
        }
        public static void Initalize(AeGame game)
        {
            instance = new AeEngine();
            instance.GraphicsDeviceManager = new GraphicsDeviceManager(game);
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
