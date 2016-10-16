using Aerolite.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Components
{
    public struct AeAnimationFrame
    {
        public int FrameX { get; private set; }
        public int FrameY { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }
        public int Duration { get; private set; }
        
        public AeAnimationFrame(int x, int y, int width, int height, int duration)
        {
            FrameX = x;
            FrameY = y;
            FrameWidth = width;
            FrameHeight = height;
            Duration = duration;
        }
    }

    public class AeAnimation
    {
        public AeAnimator Parent { get; private set; }
        public Texture2D Texture { get; private set; }
        public List<AeAnimationFrame> Frames { get; private set; } = new List<AeAnimationFrame>();
        public bool Looping { get; set; } = true;

        private int _currentElapsedTime = 0;
        private int _currentFrame = 0;

        public AeAnimation(Texture2D texture, AeAnimator parent,AeAnimationFrame[] frames = null)
        {
            Texture = texture;
            Parent = parent;
            AddFrames(frames);
        }

        public AeAnimation(string pathToTexture, AeAnimator parent,AeAnimationFrame[] frames = null)
            :this(AeEngine.Singleton().TextureManager.LoadTexture(pathToTexture),parent,frames)
        { }

        public void AddFrame(AeAnimationFrame frame)
        {
            Frames.Add(frame);
        }

        public void AddFrames(params AeAnimationFrame[] frames)
        {
            if (frames != null)
            {
                for (int i = 0; i < frames.Length; i++)
                {
                    Frames.Add(frames[i]);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Frames.Count == 0)
            {
                return;
            }
            _currentElapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            if (_currentElapsedTime >= Frames[_currentFrame].Duration)
            {
                _currentElapsedTime = 0;
                var nextFrame = _currentFrame + 1;
                if (nextFrame >= Frames.Count)
                {
                    if (Looping)
                    {
                        _currentFrame = 0;
                    }
                    else
                    {
                        //stay stuck on last frame
                    }
                }
                else
                {
                    _currentFrame = nextFrame;
                }
            }
        }

        public void Draw( SpriteBatch batch)
        {
            if (Frames.Count == 0)
            {
                return;
            }
            AeAnimationFrame frame = Frames[_currentFrame];
            Rectangle destinationRectangle;
            Rectangle sourceRectangle;
           
            destinationRectangle.X = (int)Math.Round(Parent.Owner.Transform.X);
            destinationRectangle.Y = (int)Math.Round(Parent.Owner.Transform.Y);
            destinationRectangle.Width = frame.FrameWidth;
            destinationRectangle.Height = frame.FrameHeight;

            sourceRectangle.X = frame.FrameX;
            sourceRectangle.Y = frame.FrameY;
            sourceRectangle.Width = frame.FrameWidth;
            sourceRectangle.Height = frame.FrameHeight;

            //TODO change this to use a color component
            batch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }

    public class AeAnimator : AeComponent
    {
        private Dictionary<string, AeAnimation> _animations = new Dictionary<string, AeAnimation>();
        public AeAnimation CurrentAnimation { get; private set; }
        
        public void Add(string name, AeAnimation animation)
        {
            _animations.Add(name, animation);
            if (CurrentAnimation == null)
            {
                CurrentAnimation = _animations[name];
            }
        }

        public void Play(string name)
        {
            if (_animations.ContainsKey(name))
            {
                CurrentAnimation = _animations[name];
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Draw(batch);
            }
        }
    }
}
