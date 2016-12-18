using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Graphics
{

    public sealed class AeGraphics
    {
        public AeGraphicsSettings GraphicsSettings { get; set; }
        public AeRenderer Renderer { get; set; }
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public SpriteBatch Batch { get; private set; }

        public AeGraphics(GraphicsDeviceManager graphicsDeviceManager)
        {
            GraphicsDeviceManager = graphicsDeviceManager;
            //graphicsDeviceManager.PreferMultiSampling = false;
        }

        public void Load()
        {
            Batch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
        }
    }
}
