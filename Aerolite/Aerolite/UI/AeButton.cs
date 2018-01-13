using Aerolite.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Aerolite.UI
{
    public class AeButton : AeUIElement
    {
        public bool FitButtonSizeToText { get; set; } = false;
        public int FitButtonSizePadding { get; set; } = 20;
        private AeText _buttonTextControl;

        public string Text {
            get
            {
                return _buttonTextControl.Text;
            }
            set
            {
                _buttonTextControl.Text = value;
                if (FitButtonSizeToText)
                {
                    BoundingBox.Width = _buttonTextControl.TextWidth + FitButtonSizePadding;
                    BoundingBox.Height = _buttonTextControl.TextHeight + FitButtonSizePadding;
                }
            }
        }

        public AeColor FillColor { get; private set; } = new AeColor(Color.Black);
        public AeColor OutlineColor { get; private set; } = new AeColor(Color.White);
        public AeColor HoverFillColor { get; private set; } = new AeColor(Color.DarkGray);
        public AeColor HoverBorderFillColor { get; private set; } = new AeColor(Color.White);

        public Texture2D FillTexture { get; private set; }

        public int BorderSize { get; set; } = 1;
        public bool DrawBorder { get; set; } = true;
        public bool DrawMouseHoverHighlightBorder { get; set; } = true;

        public bool IsMouseHovering { get; private set; }

        Texture2D _drawTexture;

        Action _onClick;

        public AeButton()
        {
            BoundingBox.X = 0;
            BoundingBox.Y = 0;
            BoundingBox.Width = 64;
            BoundingBox.Height = 24;
            _drawTexture = Engine.TextureManager.GetFillTexture();

            _buttonTextControl = new AeText("test", Engine.DebugResources.DebugFont);

            AddChild(_buttonTextControl);
        }

        public void SetFillTexture(Texture2D texture,bool adjustSizeToFitImage = false)
        {
            FillTexture = texture;
            if (adjustSizeToFitImage)
            {
                BoundingBox.Width = FillTexture.Width;
                BoundingBox.Height = FillTexture.Height;
            }
        }

        public void SetOnClickCallback(Action onClick)
        {
            _onClick = onClick;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            BoundingBox.X = (int)Transform.X;
            BoundingBox.Y = (int)Transform.Y;
            int verticalOffset = (BoundingBox.Height - _buttonTextControl.TextHeight) / 2;
            int horizontalOffset = (BoundingBox.Width - _buttonTextControl.TextWidth) / 2;

            _buttonTextControl.Transform.X = Transform.X + horizontalOffset ;
            _buttonTextControl.Transform.Y = Transform.Y + verticalOffset;

            var mouse = Engine.Input.Mouse;
            if (BoundingBox.Contains(mouse.X,mouse.Y) && mouse.LeftClick && _onClick != null)
            {
                _onClick();
            }
            else
            {
                if (BoundingBox.Contains(mouse.X, mouse.Y))
                {
                    IsMouseHovering = true;
                }
                else
                {
                    IsMouseHovering = false;
                }
            }

            var touch = Engine.Input.Touch;

            var bounds = new AeAABB();
            bounds.SetPosition((int)Transform.X, (int)Transform.Y);
            bounds.SetSize(BoundingBox.Width, BoundingBox.Height);
            if (touch.IsTouched(bounds))
            {
                _onClick();
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            Rectangle destinationRect = BoundingBox;

            if (FillTexture != null)
            {
                if (IsMouseHovering)
                {
                    //TODO Make this not hideous
                    batch.Draw(FillTexture, BoundingBox, Color.Gray);
                }
                else
                {
                    batch.Draw(FillTexture, BoundingBox, Color.White);
                }
            }
            else
            {
                Color fillColor = IsMouseHovering ? HoverFillColor.CurrentColor : FillColor.CurrentColor;
                batch.Draw(_drawTexture, BoundingBox, fillColor);
            }

            if (IsMouseHovering && DrawMouseHoverHighlightBorder)
            {
                destinationRect.X = (int)Transform.X;
                destinationRect.Y = (int)Transform.Y;
                destinationRect.Width = BoundingBox.Width;
                destinationRect.Height = BorderSize;
                batch.Draw(_drawTexture, destinationRect, HoverBorderFillColor.CurrentColor);

                destinationRect.X = (int)Transform.X;
                destinationRect.Y = (int)Transform.Y + BoundingBox.Height - BorderSize;
                destinationRect.Width = BoundingBox.Width;
                destinationRect.Height = BorderSize;
                batch.Draw(_drawTexture, destinationRect, HoverBorderFillColor.CurrentColor);

                destinationRect.X = (int)Transform.X;
                destinationRect.Y = (int)Transform.Y;
                destinationRect.Width = BorderSize;
                destinationRect.Height = BoundingBox.Height;
                batch.Draw(_drawTexture, destinationRect, HoverBorderFillColor.CurrentColor);

                destinationRect.X = (int)Transform.X + BoundingBox.Width - BorderSize;
                destinationRect.Y = (int)Transform.Y;
                destinationRect.Width = BorderSize;
                destinationRect.Height = BoundingBox.Height;
                batch.Draw(_drawTexture, destinationRect, HoverBorderFillColor.CurrentColor);
            }
            else if (DrawBorder)
            {
                destinationRect.X = (int)Transform.X;
                destinationRect.Y = (int)Transform.Y;
                destinationRect.Width = BoundingBox.Width;
                destinationRect.Height = BorderSize;
                batch.Draw(_drawTexture, destinationRect, OutlineColor.CurrentColor);

                destinationRect.X = (int)Transform.X;
                destinationRect.Y = (int)Transform.Y + BoundingBox.Height - BorderSize;
                destinationRect.Width = BoundingBox.Width;
                destinationRect.Height = BorderSize;
                batch.Draw(_drawTexture, destinationRect, OutlineColor.CurrentColor);

                destinationRect.X = (int)Transform.X;
                destinationRect.Y = (int)Transform.Y;
                destinationRect.Width = BorderSize;
                destinationRect.Height = BoundingBox.Height;
                batch.Draw(_drawTexture, destinationRect, OutlineColor.CurrentColor);

                destinationRect.X = (int)Transform.X + BoundingBox.Width - BorderSize;
                destinationRect.Y = (int)Transform.Y;
                destinationRect.Width = BorderSize;
                destinationRect.Height = BoundingBox.Height;
                batch.Draw(_drawTexture, destinationRect, OutlineColor.CurrentColor);
            }

            _buttonTextControl.Draw(gameTime, batch);
        }
    }
}
