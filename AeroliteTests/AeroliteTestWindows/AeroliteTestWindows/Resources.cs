using Aerolite;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroliteTestWindows
{
    public class Resources
    {
        public SpriteFont FontGame { get; private set; }

        private AeEngine _engine;

        private const string FONT_GAME = @"default";

        public Resources(AeEngine engine)
        {
            _engine = engine;
        }

        public void LoadGlobalResources()
        {
            FontGame = _engine.GameReference.Content.Load<SpriteFont>(FONT_GAME);
        }
    }
}
