using Aerolite.Subsystems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private SpriteBatch _spriteBatch;
        public AeGame GameReference;
        public AeStateManager StateManager;

        private void InitSubsystems()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(GameReference);
            TextureManager = new AeTextureManager(GameReference);

            

            StateManager = new AeStateManager(_spriteBatch);
        }

        public void Load()
        {
            _spriteBatch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            
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
