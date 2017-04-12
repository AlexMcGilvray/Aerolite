using Aerolite;
using Aerolite.Subsystems;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroliteTestWindows
{
    class DebugResources : AeIDebugResources
    {
        Resources _resources;
        public DebugResources(Resources resources)
        {
            _resources = resources;
        }

        public SpriteFont DebugFont
        {
            get
            {
                return _resources.FontGame;
            }
        }
    }


    public class Resources
    {
        public SpriteFont FontGame { get; private set; }

        private AeEngine _engine;

        DebugResources _debugResources;

        private const string FONT_GAME = @"default";

        public Resources(AeEngine engine)
        {
            _engine = engine;
        }

        public void LoadGlobalResources()
        {
            FontGame = _engine.GameReference.Content.Load<SpriteFont>(FONT_GAME);
            _debugResources = new DebugResources(this);
            _engine.DebugResources = _debugResources;
        }
    }
}
