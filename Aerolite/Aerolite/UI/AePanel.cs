using Aerolite.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aerolite.UI
{
    public class AePanel : AeUIElement
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color PanelColor { get; set; }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
       
                AeDrawUtil.DrawRectangle(batch, (int)Transform.X, (int)Transform.Y, Width, Height, PanelColor);
        }
    }
}