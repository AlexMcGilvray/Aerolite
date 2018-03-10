using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Aerolite.Util;

namespace Aerolite.UI
{
    public class AePanel : AeUIElement
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color PanelColor { get; set; }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            AeDrawUtil.DrawRectangle(batch, (int)Transform.X, (int)Transform.Y, Width, Height, Color.DarkGray);
        }
    }
}
