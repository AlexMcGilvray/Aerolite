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
        public SpriteFont Font { get; private set; }

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
                Font = font;
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

            batch.DrawString(Font, _text, Transform.Position, Color.White,Transform.Orientation,Vector2.Zero,Transform.ScaleX,SpriteEffects.None,0.0f);
        }
    }
}
