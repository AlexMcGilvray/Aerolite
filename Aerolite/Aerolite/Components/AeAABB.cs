using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Components
{
    public class AeAABB : AeComponent
    {
        Rectangle _rect = new Rectangle();
        private Texture2D _debugTexture;
        public bool DebugDraw { get; set; } = false;

        public AeAABB()
        {
            _debugTexture = Engine.TextureManager.CreateFilledRectangle(1, 1, Color.Pink);
        }

        public void SetSize(int width, int height)
        {
            _rect.Width = width;
            _rect.Height = height;
        }

        public void SetPosition(int x, int y)
        {
            _rect.X = x;
            _rect.Y = y;
        }

        public bool Overlaps(AeAABB otherHull)
        {
            if (this._rect.Intersects(otherHull._rect))
            {
                return true;
            }
            return false;
        }

        public bool Overlaps(Vector2 point)
        {
            bool xIntersects = point.X > _rect.X && point.X < _rect.X + _rect.Width ? true : false;
            bool yIntersects = point.Y > _rect.Y && point.Y < _rect.Y + _rect.Height ? true : false;
            return xIntersects && yIntersects;
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            if (DebugDraw)
            {
                batch.Draw(_debugTexture, _rect, Color.White);
            }
        }
    }
}
