using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aerolite.Util
{
    public static class AeDrawUtil
    {
        public static void DrawRectangle(SpriteBatch batch, int x, int y, int width, int height)
        {
            DrawRectangle(batch, x, y, width, height, Color.White);
        }

        public static void DrawRectangle(SpriteBatch batch,int x, int y, int width, int height, Color color)
        {
            Texture2D fill = AeEngine.Singleton().TextureManager.GetFillTexture();
            Rectangle dest;
            dest.X = x;
            dest.Y = y;
            dest.Width = width;
            dest.Height = height;
            batch.Draw(fill, dest, color);
        }

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
