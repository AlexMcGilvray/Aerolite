using Aerolite.Subsystems;
using Aerolite.Subsystems.Graphics;
using Aerolite.Subsystems.Input;
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
        public AeTextureManager TextureManager { get; private set; }
        public AeGame GameReference;
        public AeStateManager StateManager;
        public AeGraphics Graphics;
        public AeInput Input;

        private void InitSubsystems()
        {
            var graphicsDeviceManager = new GraphicsDeviceManager(GameReference);
            Graphics = new AeGraphics(graphicsDeviceManager);
        }

        /// <summary>
        /// Order is super important. I try to make the dependencies as obvious as possible 
        /// via constructor injection.
        /// 
        /// NOTE : Consider using Requires in the constructors so we can crash in an obvious 
        /// place if the dependencies are not satisfied correctly.
        /// </summary>
        public void Load()
        {
            Graphics.Load();

            Input = new AeInput(Graphics);

            AeGraphicsSettings settings = new AeGraphicsSettings(Graphics);
            AeRenderer renderer = new AeRenderer(Graphics);

            TextureManager = new AeTextureManager(GameReference);

            StateManager = new AeStateManager();
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
