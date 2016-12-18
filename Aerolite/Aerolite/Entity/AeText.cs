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
    public class AeText : AeEntity
    {
        private string _text;
        private SpriteFont _font;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public AeText(string text, SpriteFont font = null)
            :base()
        {
            _text = text;
            if (font == null)
            {
                throw new NotImplementedException("TODO Make it use some library distributed default font");
            }
            else
            {
                _font = font;
            }
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

            batch.DrawString(_font, _text, Transform.Position, Color.White);
        }
    }
}
