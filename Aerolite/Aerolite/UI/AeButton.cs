using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Aerolite.Components;

namespace Aerolite.UI
{
    public class AeButton : AeUIElement
    {
        public bool FitButtonSizeToText { get; set; } = false;

        public Rectangle BoundingBox;
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
                    BoundingBox.Width = _buttonTextControl.TextWidth + 4;
                    BoundingBox.Height = _buttonTextControl.TextHeight + 4;
                }
            }
        }

        public AeColor FillColor { get; private set; } = new AeColor(Color.Black);
        public AeColor OutlineColor { get; private set; } = new AeColor(Color.White);
        public int BorderSize { get; set; } = 1;

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
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            Rectangle destinationRect = BoundingBox;

            batch.Draw(_drawTexture, BoundingBox, FillColor.CurrentColor);

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


            _buttonTextControl.Draw(gameTime, batch);
        }

    }
}
