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
    public sealed class AeRenderer
    {
        private AeGraphics _graphics;
        private RenderTarget2D finalPassTarget;
        

        public AeRenderer(AeGraphics graphics)
        {
            _graphics = graphics;
            _graphics.Renderer = this;

            finalPassTarget = new RenderTarget2D(
                _graphics.GraphicsDeviceManager.GraphicsDevice, 
                _graphics.GraphicsSettings.GameResolutionWidth,
                _graphics.GraphicsSettings.GameResolutionHeight, 
                false, 
                SurfaceFormat.Color, 
                DepthFormat.None, 
                0, 
                RenderTargetUsage.DiscardContents);

            _graphics.GraphicsSettings.AddScreenResolutionChangeCallback(
               delegate ()
               {
                   finalPassTarget = new RenderTarget2D(
                       _graphics.GraphicsDeviceManager.GraphicsDevice, 
                       _graphics.GraphicsSettings.GameResolutionWidth, 
                       _graphics.GraphicsSettings.GameResolutionHeight);
               });

        }

        public void Render(GameTime gameTime,AeStateManager stateManager)
        {
            var graphicsSettings = _graphics.GraphicsSettings;
            var graphicsDeviceManager = _graphics.GraphicsDeviceManager;

            if (!graphicsSettings.Valid)
            {
                _graphics.GraphicsDeviceManager.ApplyChanges();
                _graphics.GraphicsDeviceManager.GraphicsDevice.Viewport = graphicsSettings.Viewport;
                _graphics.GraphicsSettings.Valid = true;
            }
            if (!graphicsSettings.ResolutionMatch)
            {
                //finalPassTarget = new RenderTarget2D(GraphicsDeviceManager.GraphicsDevice, GraphicsSettings.GameResolutionWidth, GraphicsSettings.GameResolutionHeight);
                graphicsDeviceManager.GraphicsDevice.SetRenderTarget(finalPassTarget);
                graphicsDeviceManager.GraphicsDevice.Clear(graphicsSettings.ClearColorFinalRenderTarget);
                _graphics.Batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                stateManager.Draw(gameTime, _graphics.Batch);
                _graphics.Batch.End();
                graphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
                graphicsDeviceManager.GraphicsDevice.Clear(graphicsSettings.ClearColorBackBuffer);
                _graphics.Batch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
                switch (graphicsSettings.ScalingMode)
                {
                    case GameToScreenScalingMode.NO_SCALING:
                        int noscaleX = (graphicsDeviceManager.PreferredBackBufferWidth - graphicsSettings.GameResolutionWidth) / 2 - finalPassTarget.Width / 2;
                        int noscaleY = (graphicsDeviceManager.PreferredBackBufferHeight - graphicsSettings.GameResolutionHeight) / -finalPassTarget.Height / 2;
                        _graphics.Batch.Draw(finalPassTarget, new Rectangle(noscaleX, noscaleY, graphicsSettings.GameResolutionWidth, graphicsSettings.GameResolutionHeight), Color.White);
                        break;
                    case GameToScreenScalingMode.UNIFORM_STRETCH:
                        float uniformScaleXTest = graphicsDeviceManager.PreferredBackBufferWidth / (float)graphicsSettings.GameResolutionWidth;
                        float uniformScaleYTest = graphicsDeviceManager.PreferredBackBufferHeight / (float)graphicsSettings.GameResolutionHeight;
                        float uniformScale;
                        if ((graphicsSettings.GameResolutionWidth * uniformScaleXTest <= graphicsDeviceManager.PreferredBackBufferWidth) &&
                            (graphicsSettings.GameResolutionHeight * uniformScaleXTest <= graphicsDeviceManager.PreferredBackBufferHeight))
                        {
                            uniformScale = uniformScaleXTest;
                        }
                        else
                        {
                            uniformScale = uniformScaleYTest;
                        }
                        int uniformWidth = (int)(graphicsSettings.GameResolutionWidth * uniformScale);
                        int uniformHeight = (int)(graphicsSettings.GameResolutionHeight * uniformScale);
                        int uniformX = (graphicsDeviceManager.PreferredBackBufferWidth - uniformWidth) / 2;
                        int uniformY = (graphicsDeviceManager.PreferredBackBufferHeight - uniformHeight) / 2;
                        _graphics.Batch.Draw(finalPassTarget, new Rectangle(uniformX, uniformY, uniformWidth, uniformHeight), Color.White);
                        break;
                    case GameToScreenScalingMode.CLOSEST_MULTIPLE_OF_2:
                        int closestMultipleOf2Width = graphicsSettings.GameResolutionWidth;
                        int closestMultipleOf2Height = graphicsSettings.GameResolutionHeight;
                        while ((closestMultipleOf2Width * 2 <= graphicsDeviceManager.PreferredBackBufferWidth) && (closestMultipleOf2Height * 2 <= graphicsDeviceManager.PreferredBackBufferHeight))
                        {
                            closestMultipleOf2Width *= 2;
                            closestMultipleOf2Height *= 2;
                        }
                        int closestMultipleOf2X = (graphicsDeviceManager.PreferredBackBufferWidth - closestMultipleOf2Width) / 2;
                        int closestMultipleOf2Y = (graphicsDeviceManager.PreferredBackBufferHeight - closestMultipleOf2Height) / 2;
                        _graphics.Batch.Draw(finalPassTarget, new Rectangle(closestMultipleOf2X, closestMultipleOf2Y, closestMultipleOf2Width, closestMultipleOf2Height), Color.White);
                        break;
                    case GameToScreenScalingMode.STRETCH:
                        _graphics.Batch.Draw(finalPassTarget, new Rectangle(0, 0, graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight), Color.White);
                        break;
                    default:
                        break;
                }
                _graphics.Batch.End();
            }
            else
            {
                graphicsDeviceManager.GraphicsDevice.Clear(graphicsSettings.ClearColorFinalRenderTarget);
                _graphics.Batch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                stateManager.Draw(gameTime, _graphics.Batch);
                _graphics.Batch.End();
            }
 

        }
    }
    /// <summary>
    /// Defines the scaling method when the resolution of the game doesn't match the resolution of the screen.
    /// </summary>
    public enum GameToScreenScalingMode
    {
        /// <summary>
        /// Performs no scaling and centers the screen in the window.
        /// </summary>
        NO_SCALING,
        /// <summary>
        /// Upscales to the closet multiple of two that isn't larger than the window. Centers the screen in the window after scaling.
        /// </summary>
        CLOSEST_MULTIPLE_OF_2,
        /// <summary>
        /// Stretches the image. If the window resolution has a different aspect ratio than the game resolution there will be distortion.
        /// </summary>
        STRETCH,
        /// <summary>
        /// Uniformly scales the game screen up until one of it's dimensions is as large as one of the window dimensions. Does not
        /// distort the image. Centers the screen in the window after scaling.
        /// </summary>
        UNIFORM_STRETCH
    }

    public delegate void OnScreenResolutionChange();
    public delegate void OnGameResolutionChange();
    public sealed class AeGraphicsSettings
    {
        private HashSet<OnScreenResolutionChange> screenResolutionChangeCallbacks = new HashSet<OnScreenResolutionChange>();
        private HashSet<OnGameResolutionChange> gameResolutionChangeCallbacks = new HashSet<OnGameResolutionChange>();
        private GraphicsDeviceManager graphicsDevice;
        /// <summary>
        /// Whether or not the graphics needs to have apply changes called. 
        /// 
        /// Acts as a dirty flag if the resolution has changed.
        /// </summary>
        public bool Valid { get; set; }

        public bool ResolutionMatch { get; private set; }

        public GameToScreenScalingMode ScalingMode { get; set; }

        public Color ClearColorFinalRenderTarget = Color.CornflowerBlue;
        public Color ClearColorBackBuffer = Color.CornflowerBlue;
        public int GameResolutionWidth { get; private set; }
        public int GameResolutionHeight { get; private set; }

        public Viewport Viewport { get; private set; }

        private AeGraphics _graphics;

        public AeGraphicsSettings(AeGraphics graphics)
        {
            _graphics = graphics;
            _graphics.GraphicsSettings = this;
            graphicsDevice = _graphics.GraphicsDeviceManager;
            GameResolutionWidth = graphicsDevice.PreferredBackBufferWidth;
            GameResolutionHeight = graphicsDevice.PreferredBackBufferHeight;
            CompareGameToScreenResolution();
            ClearColorFinalRenderTarget = Color.Black;
            ScalingMode = GameToScreenScalingMode.CLOSEST_MULTIPLE_OF_2;
            SetGameResolution(1280, 720);
            SetScreenResolution(1280, 720);
        }

        public void AddScreenResolutionChangeCallback(OnScreenResolutionChange callback)
        {
            screenResolutionChangeCallbacks.Add(callback);
        }

        public void AddGameResolutionChangeCallback(OnGameResolutionChange callback)
        {
            gameResolutionChangeCallbacks.Add(callback);
        }


        public void SetGameResolution(int width, int height)
        {
            GameResolutionWidth = width;
            GameResolutionHeight = height;

            CompareGameToScreenResolution();
            foreach (OnGameResolutionChange cb in gameResolutionChangeCallbacks)
            {
                cb();
            }
        }

        public bool FullScreen
        {
            get
            {
                return graphicsDevice.IsFullScreen;
            }
            set
            {
                graphicsDevice.IsFullScreen = value;
            }

        }

        public void SetScreenResolution(int width, int height)
        {
            graphicsDevice.PreferredBackBufferWidth = width;
            graphicsDevice.PreferredBackBufferHeight = height;
            Viewport = new Viewport(0, 0, width, height);
            Valid = false;
            CompareGameToScreenResolution();
            foreach (OnScreenResolutionChange cb in screenResolutionChangeCallbacks)
            {
                cb();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if resolutions match, false if they dont match</returns>
        private void CompareGameToScreenResolution()
        {
            if ((graphicsDevice.PreferredBackBufferWidth != GameResolutionWidth) || (graphicsDevice.PreferredBackBufferHeight != GameResolutionHeight))
            {
                ResolutionMatch = false;
            }
            else
            {
                ResolutionMatch = true;
            }
        }
    }

    public sealed class AeGraphics
    {
        public AeGraphicsSettings GraphicsSettings { get; set; }
        public AeRenderer Renderer { get; set; }
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public SpriteBatch Batch { get; private set; }

        public AeGraphics(GraphicsDeviceManager graphicsDeviceManager)
        {
            GraphicsDeviceManager = graphicsDeviceManager;
            Batch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
        }
    }

    public sealed class AeEngine
    {
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public AeTextureManager TextureManager { get; private set; }
        
        public AeGame GameReference;
        public AeStateManager StateManager;
        public AeGraphics Graphics;

        private void InitSubsystems()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(GameReference);
            TextureManager = new AeTextureManager(GameReference);
            
        }

        public void Load()
        {
            Graphics = new AeGraphics(GraphicsDeviceManager);
            AeGraphicsSettings settings = new AeGraphicsSettings(Graphics);
            AeRenderer renderer = new AeRenderer(Graphics);
            
            StateManager = new AeStateManager();
        }

        public void Update(GameTime gameTime)
        {
            StateManager.Update(gameTime);
        }

        public void Render(GameTime gameTime)
        {
            //TODO decide on Render or Draw. Not both
            Graphics.Renderer.Render(gameTime, StateManager);
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
