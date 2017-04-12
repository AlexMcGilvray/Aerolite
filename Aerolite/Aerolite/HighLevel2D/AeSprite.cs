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
    public class AeSpriteDebugVizualizer : AeComponent
    {
        private AeSprite _sprite;
        public AeColor DebugColor { get; private set; }
        private AeText _debugTransformText;

        public AeSpriteDebugVizualizer(AeSprite sprite)
        {
            _sprite = sprite;
            DebugColor = new AeColor(Color.Red);


            // we need to make some kind of resource injection interface to pass into the engine :(
            _debugTransformText = new AeText("debugTransform",Engine.DebugResources.DebugFont);
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            Texture2D fill = Engine.TextureManager.GetFillTexture();

            Rectangle rect;
            rect.X = (int)_sprite.Transform.X;
            rect.Y = (int)_sprite.Transform.Y;
            rect.Width = (int)_sprite.SizeX;
            rect.Height = 1;
            batch.Draw(fill, rect, DebugColor.CurrentColor);
             
            rect.X = (int)_sprite.Transform.X;
            rect.Y = (int)(_sprite.Transform.Y + _sprite.SizeY - 1);
            rect.Width = (int)_sprite.SizeX;
            rect.Height = 1;
            batch.Draw(fill, rect, DebugColor.CurrentColor);

            rect.X = (int)_sprite.Transform.X;
            rect.Y = (int)_sprite.Transform.Y;
            rect.Width = 1;
            rect.Height = (int)_sprite.SizeY;
            batch.Draw(fill, rect, DebugColor.CurrentColor);

            rect.X = (int)(_sprite.Transform.X + _sprite.SizeX);
            rect.Y = (int)_sprite.Transform.Y;
            rect.Width = 1;
            rect.Height = (int)_sprite.SizeY;
            batch.Draw(fill, rect, DebugColor.CurrentColor);

            _debugTransformText.Text = "x : " + Math.Floor(_sprite.Transform.X) + " y : " + Math.Floor(_sprite.Transform.Y);
            _debugTransformText.Transform.X = _sprite.Transform.X;
            _debugTransformText.Transform.Y = _sprite.Transform.Y - _debugTransformText.Font.LineSpacing;

            _debugTransformText.Draw(gameTime, batch);
        }

    }


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

        public void SetupDebugVizualization()
        {
            AeSpriteDebugVizualizer debugViz = new AeSpriteDebugVizualizer(this);
            AddComponent(debugViz);
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            Animator.RenderColor = RenderColor;
            base.Draw(gameTime, batch);
        }
    }
}
