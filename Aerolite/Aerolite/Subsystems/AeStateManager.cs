using Aerolite.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems
{
    public enum FadingDirection
    {
        NotFading,
        In,
        Out
    }

    public class AeCurtainManager : AeEntity
    {
        public FadingDirection FadeDirection { get; private set; }
        Texture2D _curtainsTexture;
        float CurrentAlpha { get; set; }
        bool IsActive { get; set; }

        Action _fadeInCallback;
        Action _fadeOutCallback;

        Rectangle _screenRect;
        public AeCurtainManager()
        {
            _curtainsTexture = Engine.TextureManager.CreateFilledRectangle(1, 1, Color.Black);
        }

        public void FadeIn(Action callback = null)
        {
            CurrentAlpha = 1.0f;
            FadeDirection = FadingDirection.In;
                _fadeInCallback = callback;
        }

        public void FadeOut(Action callback = null)
        {
            CurrentAlpha = 0;
            FadeDirection = FadingDirection.Out;
            _fadeOutCallback = callback;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (FadeDirection == FadingDirection.In)
            {
                if (CurrentAlpha > 0)
                {
                    CurrentAlpha -= 0.1f;
                }
                else
                {
                    if (_fadeInCallback != null)
                    {
                        _fadeInCallback();
                        _fadeInCallback = null;
                    }
                    FadeDirection = FadingDirection.NotFading;
                }
            }
            else if (FadeDirection == FadingDirection.Out)
            {
                if (CurrentAlpha < 1.0f)
                {
                    CurrentAlpha += 0.1f;
                }
                else
                {
                    if (_fadeOutCallback != null)
                    {
                        _fadeOutCallback();
                        _fadeOutCallback = null;
                    }
                    FadeDirection = FadingDirection.NotFading;
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            _screenRect.X = 0;
            _screenRect.Y = 0;
            _screenRect.Width = Engine.Graphics.GraphicsSettings.GameResolutionWidth;
            _screenRect.Height = Engine.Graphics.GraphicsSettings.GameResolutionHeight;

            batch.Draw(_curtainsTexture, _screenRect, Color.White * CurrentAlpha);
        }
    }


    public class AeStateManager
    {
        private List<AeState> _states = new List<AeState>();

        /// <summary>
        /// state changes are usually done during update which means we would modify the state collection while
        /// we're iterating on it. So we defer all state manipulations until after all state updates are complete. 
        /// </summary>
        private List<Action> _stateManiulationDeferredOperations = new List<Action>();

        private AeCurtainManager _curtains = new AeCurtainManager();

        public AeStateManager()
        { }

        public void PushState(AeState state)
        {
            _stateManiulationDeferredOperations.Add(() =>
           {
               _states.Add(state);
           });
        }

        public void PopState()
        {
            _stateManiulationDeferredOperations.Add(() =>
            {
                _states.RemoveAt(_states.Count - 1);
            });
        }

        public void PopState(AeState state)
        {
            _stateManiulationDeferredOperations.Add(() =>
            {
                var indexOfStateToRemove = _states.FindIndex(x => x == state);
                if (indexOfStateToRemove != -1)
                {
                    _states.RemoveAt(indexOfStateToRemove);
                }
            });
        }

        public void ChangeState(AeState state)
        {
            _stateManiulationDeferredOperations.Add(() =>
            {
                _states.Clear();
                _states.Add(state);
            });
        }

        public void ChangeStateWithCurtains(AeState state)
        {
            _stateManiulationDeferredOperations.Add(() =>
            {
                _curtains.FadeOut(() => 
                {
                    _stateManiulationDeferredOperations.Add(() =>
                    {
                        _states.Clear();
                        _states.Add(state);
                        _curtains.FadeIn();
                    });
                });
            });
        }

        public void Update(GameTime gameTime)
        {
            if (_curtains.FadeDirection != FadingDirection.NotFading)
            {
                _curtains.Update(gameTime);
            }
            //states shouldn't bleed into each other which technically means 
            //I might be able to do this with a parallel for....
            for (int i = _states.Count - 1; i >= 0; --i)
            {
                _states[i].Update(gameTime);
            }
            foreach (var operation in _stateManiulationDeferredOperations)
            {
                operation.Invoke();
            }
            _stateManiulationDeferredOperations.Clear();
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _states.Count; ++i)
            {
                _states[i].Draw(gameTime, spriteBatch);
            }
            if (_curtains.FadeDirection != FadingDirection.NotFading)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                _curtains.Draw(gameTime, spriteBatch);
                spriteBatch.End();
            }
        }
    }
}
