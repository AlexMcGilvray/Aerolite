using Aerolite.Entity;
using Aerolite.HighLevel2D;
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

        public AeAnimationFrame(int x, int y, int width, int height, int duration = 0)
        {
            FrameX = x;
            FrameY = y;
            FrameWidth = width;
            FrameHeight = height;
            Duration = duration;
        }
    }

    public delegate void AeEventOnAnimationComplete(AeAnimation sender);

    public class AeLinearAnimationGenerationParams
    {
        public int Width { get;  set; }
        public int Height { get;  set; }
        public int FrameCount { get;  set; }
        public int FrameTime { get;  set; }
    }

    public class AeAnimation
    {
        public AeAnimator Parent { get; private set; }
        public Texture2D Texture { get; private set; }
        public List<AeAnimationFrame> Frames { get; private set; } = new List<AeAnimationFrame>();
        public bool Looping { get; set; } = true;

        private int _currentElapsedTime = 0;
        private int _currentFrame = 0;

        public bool CompleteAnimation { get; private set; } = false;

        public event AeEventOnAnimationComplete OnAnimationComplete;

        public void ClearOnAnimationComplete() => OnAnimationComplete = null;

        public AeAnimation(Texture2D texture, AeAnimator parent, AeLinearAnimationGenerationParams linearAnimGenerationParams)
        {
            Texture = texture;
            Parent = parent;

            int currentX = 0;
            int width = linearAnimGenerationParams.Width;
            int height = linearAnimGenerationParams.Height;
            List<AeAnimationFrame> frames = new List<AeAnimationFrame>();
            for( int i = 0; i < linearAnimGenerationParams.FrameCount; ++i)
            {
                frames.Add(new AeAnimationFrame(currentX, 0, width, height, linearAnimGenerationParams.FrameTime));
                currentX += width;
            }

            AddFrames(frames.ToArray());
        }

        public AeAnimation(Texture2D texture, AeAnimator parent, AeAnimationFrame[] frames = null)
        {
            Texture = texture;
            Parent = parent;
            AddFrames(frames);
        }

        public AeAnimation(string pathToTexture, AeAnimator parent, AeAnimationFrame[] frames = null)
            : this(AeEngine.Singleton().TextureManager.LoadTexture(pathToTexture), parent, frames)
        { }

        ~AeAnimation()
        {
            ClearOnAnimationComplete();
        }

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

        public void ResetAnimation()
        {
            _currentElapsedTime = 0;
            _currentFrame = 0;
            CompleteAnimation = false;
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
                        if (!CompleteAnimation)
                        {
                            // stay stuck on last frame
                            CompleteAnimation = true;
                            OnAnimationComplete?.Invoke(this);
                        }
                    }
                }
                else
                {
                    _currentFrame = nextFrame;
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if (Frames.Count == 0 || CompleteAnimation)
            {
                return;
            }
            AeAnimationFrame frame = Frames[_currentFrame];
            Rectangle destinationRectangle;
            Rectangle sourceRectangle;

            destinationRectangle.X = (int)Math.Floor(Parent.Owner.Transform.X);
            destinationRectangle.Y = (int)Math.Floor(Parent.Owner.Transform.Y);
            destinationRectangle.Width = frame.FrameWidth;
            destinationRectangle.Height = frame.FrameHeight;

            sourceRectangle.X = frame.FrameX;
            sourceRectangle.Y = frame.FrameY;
            sourceRectangle.Width = frame.FrameWidth;
            sourceRectangle.Height = frame.FrameHeight;

            Vector2 rotationCenter = Vector2.Zero;
            rotationCenter.X = Parent.Owner.Transform.RotationCenter.X * sourceRectangle.Width;
            rotationCenter.Y = Parent.Owner.Transform.RotationCenter.Y * sourceRectangle.Height;

            //fix their positioning if they have a rotation offset
            destinationRectangle.X += (int)rotationCenter.X;
            destinationRectangle.Y += (int)rotationCenter.Y;

            batch.Draw(
                Texture, 
                destinationRectangle, 
                sourceRectangle, 
                Parent.RenderColor.CurrentColor,
                Parent.Owner.Transform.Orientation,
                rotationCenter,
                SpriteEffects.None,
                0);
        }
    }

    public class AeAnimator : AeComponent
    {
        private Dictionary<string, AeAnimation> _animations = new Dictionary<string, AeAnimation>();
        public AeAnimation CurrentAnimation { get; private set; }
        public AeColor RenderColor { get; set; }

        public AeAnimator()
        {
            RenderColor = new AeColor();
            AddComponent(RenderColor);
        }

        public AeAnimation Get(string key)
        {
            if (_animations.ContainsKey(key))
            {
                return _animations[key];
            }
            else
            {
                throw new ArgumentException("Key does not exist in the animations dictionary.");
            }
        }

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
                CurrentAnimation.ResetAnimation();
            }
        }

        /// <summary>
        /// Sets the animation to null which I guess makes the animator invisible? 
        /// </summary>
        public void ClearCurrentAnimation()
        {
            CurrentAnimation = null;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            if (CurrentAnimation != null)
            {
                CurrentAnimation.Draw(batch);
            }
        }
    }
}