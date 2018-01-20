using Aerolite.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace Aerolite
{
    /// <summary>
    /// TODO this should be a string builder underneath if we're allowing the text to be mutable.
    /// </summary>
    public class AeText : AeEntity
    {
        private string _text;
        public SpriteFont Font { get; private set; }
        public int TextHeight { get; private set; }
        public int TextWidth { get; private set; }

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                RecalculateTextDimensions();
            }
        }

        private void RecalculateTextDimensions()
        {
            var textDimensions = Font.MeasureString(_text);
            TextWidth = (int)textDimensions.X;
            TextHeight = (int)textDimensions.Y;
        }

        public AeText(string text, SpriteFont font = null)
            :base()
        {
            if (font == null)
            {
                throw new NotImplementedException("TODO Make it use some library distributed default font");
            }
            else
            {
                Font = font;
            }
            Text = text;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            //TODO add support for color (interpolaters too!)
            Vector2 pos = Transform.Position;
            batch.DrawString(Font, _text, Transform.Position, Color.White,Transform.Orientation,Vector2.Zero,Transform.ScaleX,SpriteEffects.None,0.0f);
        }
    }
}
