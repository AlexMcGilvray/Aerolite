using Aerolite.Components;
using Aerolite.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.HighLevel2D
{
    public class AeSprite : AeEntity
    {
        public Texture2D Texture { get; private set; }
        //public RsColor RenderColor { get; private set; }
        private Vector2 _size;
        public float SizeX { get { return _size.X; } set { _size.X = value; } }
        public float SizeY { get { return _size.Y; } set { _size.Y = value; } }
        public Vector2 Size { get { return _size; } set { _size = value; } }

        public AeAnimator Animator { get; private set; }
        public AeColor RenderColor { get; set; }

        public AeSprite(string texturePath = null) : base()
        {
            Animator = new AeAnimator();
            RenderColor = new AeColor();
            if (!string.IsNullOrEmpty(texturePath))
            {
                AeAnimation animation = new AeAnimation(texturePath, Animator);
                AeAnimationFrame frame1 = new AeAnimationFrame(0, 0, animation.Texture.Width, animation.Texture.Height, -1);
                SizeX = animation.Texture.Width;
                SizeY = animation.Texture.Height;
                animation.AddFrame(frame1);
                Animator.Add("default",animation);
            }
            AddComponent(Animator);
            AddComponent(RenderColor);
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            Animator.RenderColor = RenderColor;
            base.Draw(gameTime, batch);
        }
    }
}
