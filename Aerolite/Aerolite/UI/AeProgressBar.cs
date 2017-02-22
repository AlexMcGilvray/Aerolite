using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.UI
{
    public enum AeProgressBarType
    {
        Horizontal,
        Vertical
    }

    public class AeProgressBar : AeUIElement
    {
        public float CurrentValue
        {
            get
            {
                return _currentValue;
            }
            set
            {
                if (value > 1.0f || value < 0.0f)
                {
                    throw new ArgumentOutOfRangeException("value not within 0-1 range");
                }
                _currentValue = value;
            }
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public int BorderSize { get; set; } = 2;
        public Color BackgroundColor { get; set; }
        public Color FillColor { get; set; }
        public Color BorderColor { get; set; }

        public AeProgressBarType BarType { get; set; } = AeProgressBarType.Horizontal;

        private float _currentValue = 0.5f;

        private Texture2D _drawTexture;

        public AeProgressBar(int width, int height, int borderWidth = 2, int borderHeight = 2)
        {
            _drawTexture = Engine.TextureManager.CreateFilledRectangle(1, 1, Color.White);
            Width = width;
            Height = height;
            
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            Rectangle destinationRect;

            destinationRect.X = (int)Transform.X;
            destinationRect.Y = (int)Transform.Y;
            destinationRect.Width = Width;
            destinationRect.Height = Height;
            batch.Draw(_drawTexture, destinationRect, BackgroundColor);

            switch (BarType)
            {
                case AeProgressBarType.Horizontal:
                    destinationRect.X = (int)Transform.X;
                    destinationRect.Y = (int)Transform.Y;
                    destinationRect.Width = (int)(Width * CurrentValue);
                    destinationRect.Height = Height;
                    batch.Draw(_drawTexture, destinationRect, FillColor);
                    break;
                case AeProgressBarType.Vertical:
                    destinationRect.X = (int)Transform.X;
                    destinationRect.Y = (int)Transform.Y + Height - (int)(Height * CurrentValue);
                    destinationRect.Width = Width;
                    destinationRect.Height = (int)(Height * CurrentValue);
                    batch.Draw(_drawTexture, destinationRect, FillColor);
                    break;
                default:
                    break;
            }

            destinationRect.X = (int)Transform.X;
            destinationRect.Y = (int)Transform.Y;
            destinationRect.Width = Width;
            destinationRect.Height = BorderSize;
            batch.Draw(_drawTexture, destinationRect, BorderColor);

            destinationRect.X = (int)Transform.X;
            destinationRect.Y = (int)Transform.Y + Height - BorderSize;
            destinationRect.Width = Width;
            destinationRect.Height = BorderSize;
            batch.Draw(_drawTexture, destinationRect, BorderColor);

            destinationRect.X = (int)Transform.X;
            destinationRect.Y = (int)Transform.Y;
            destinationRect.Width = BorderSize;
            destinationRect.Height = Height;
            batch.Draw(_drawTexture, destinationRect, BorderColor);

            destinationRect.X = (int)Transform.X + Width - BorderSize;
            destinationRect.Y = (int)Transform.Y;
            destinationRect.Width = BorderSize;
            destinationRect.Height = Height;
            batch.Draw(_drawTexture, destinationRect, BorderColor);
        }
    }
}
