using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Util
{
    public static class AeDrawUtil
    {
        public static void DrawHollowRectangle(SpriteBatch batch, Rectangle sourceRect, int border = 1)
        {
            DrawHollowRectangle(batch, sourceRect, Color.White, border);
        }

        public static void DrawHollowRectangle(SpriteBatch batch, Rectangle sourceRect, Color color ,int border = 1)
        {
            Texture2D fill = AeEngine.Singleton().TextureManager.GetFillTexture();

            Rectangle rect;
            rect.X = sourceRect.X;
            rect.Y = sourceRect.Y;
            rect.Width = sourceRect.Width;
            rect.Height = border;
            batch.Draw(fill, rect, color);

            rect.X = sourceRect.X;
            rect.Y = (sourceRect.Y + sourceRect.Height - 1);
            rect.Width = sourceRect.Width;
            rect.Height = border;
            batch.Draw(fill, rect, color);

            rect.X = sourceRect.X;
            rect.Y = sourceRect.Y;
            rect.Width = border;
            rect.Height = sourceRect.Height;
            batch.Draw(fill, rect, color);

            rect.X = sourceRect.X + sourceRect.Width;
            rect.Y = sourceRect.Y;
            rect.Width = border;
            rect.Height = sourceRect.Height;
            batch.Draw(fill, rect, color);
        }


    }
}
