using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// TODO Stop managing delegates yourself and use the event keyword
/// </summary>
namespace Aerolite.Subsystems.Graphics
{
    /// <summary>
    /// Defines the scaling method when the resolution of the game doesn't match the resolution of the screen.
    /// </summary>
    public enum AeScalingMode
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
        public AeScalingMode ScalingMode { get; set; }
        public Color ClearColorFinalRenderTarget = Color.CornflowerBlue;
        public Color ClearColorBackBuffer = Color.CornflowerBlue;
        public int GameResolutionWidth { get; private set; }
        public int GameResolutionHeight { get; private set; }
        public int ScreenResolutionWidth { get { return graphicsDevice.PreferredBackBufferWidth; } }
        public int ScreenResolutionHeight { get { return graphicsDevice.PreferredBackBufferHeight; } }

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
            ScalingMode = AeScalingMode.CLOSEST_MULTIPLE_OF_2;
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
            Valid = false;

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
        /// <returns>True if resolutions match, false if they don't match</returns>
        private void CompareGameToScreenResolution()
        {
            if ((graphicsDevice.PreferredBackBufferWidth != GameResolutionWidth) || 
                (graphicsDevice.PreferredBackBufferHeight != GameResolutionHeight))
            {
                ResolutionMatch = false;
            }
            else
            {
                ResolutionMatch = true;
            }
        }
    }
}
