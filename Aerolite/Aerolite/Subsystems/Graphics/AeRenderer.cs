﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Graphics
{
    public sealed class AeRenderer
    {
        private AeGraphics _graphics;
        private RenderTarget2D finalPassTarget;
        
        public AeRenderer(AeGraphics graphics)
        {
            _graphics = graphics;
            _graphics.Renderer = this;

            finalPassTarget = new RenderTarget2D(
                _graphics.GraphicsDeviceManager.GraphicsDevice,
                _graphics.GraphicsSettings.GameResolutionWidth,
                _graphics.GraphicsSettings.GameResolutionHeight,
                false,
                SurfaceFormat.Color,
                DepthFormat.None,
                0,
                RenderTargetUsage.DiscardContents);

            _graphics.GraphicsSettings.AddScreenResolutionChangeCallback(
               delegate ()
               {
                   finalPassTarget = new RenderTarget2D(
                       _graphics.GraphicsDeviceManager.GraphicsDevice,
                       _graphics.GraphicsSettings.GameResolutionWidth,
                       _graphics.GraphicsSettings.GameResolutionHeight);
               });
        }

        public void Render(GameTime gameTime, AeStateManager stateManager)
        {
            var graphicsSettings = _graphics.GraphicsSettings;
            var graphicsDeviceManager = _graphics.GraphicsDeviceManager;

            if (!graphicsSettings.Valid)
            {
                _graphics.GraphicsDeviceManager.ApplyChanges();
                _graphics.GraphicsDeviceManager.GraphicsDevice.Viewport = graphicsSettings.Viewport;
                _graphics.GraphicsSettings.Valid = true;
            }
            if (!graphicsSettings.ResolutionMatch)
            {
                graphicsDeviceManager.GraphicsDevice.SetRenderTarget(finalPassTarget);
                graphicsDeviceManager.GraphicsDevice.Clear(graphicsSettings.ClearColorFinalRenderTarget);
                _graphics.Batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                stateManager.Draw(gameTime, _graphics.Batch);
                _graphics.Batch.End();
                graphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
                graphicsDeviceManager.GraphicsDevice.Clear(graphicsSettings.ClearColorBackBuffer);
                _graphics.Batch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);
                switch (graphicsSettings.ScalingMode)
                {
                    case AeScalingMode.NO_SCALING:
                        int noscaleX = (graphicsDeviceManager.PreferredBackBufferWidth - graphicsSettings.GameResolutionWidth) / 2 - finalPassTarget.Width / 2;
                        int noscaleY = (graphicsDeviceManager.PreferredBackBufferHeight - graphicsSettings.GameResolutionHeight) / -finalPassTarget.Height / 2;
                        _graphics.Batch.Draw(finalPassTarget, new Rectangle(noscaleX, noscaleY, graphicsSettings.GameResolutionWidth, graphicsSettings.GameResolutionHeight), Color.White);
                        break;
                    case AeScalingMode.UNIFORM_STRETCH:
                        float uniformScaleXTest = graphicsDeviceManager.PreferredBackBufferWidth / (float)graphicsSettings.GameResolutionWidth;
                        float uniformScaleYTest = graphicsDeviceManager.PreferredBackBufferHeight / (float)graphicsSettings.GameResolutionHeight;
                        float uniformScale;
                        if ((graphicsSettings.GameResolutionWidth * uniformScaleXTest <= graphicsDeviceManager.PreferredBackBufferWidth) &&
                            (graphicsSettings.GameResolutionHeight * uniformScaleXTest <= graphicsDeviceManager.PreferredBackBufferHeight))
                        {
                            uniformScale = uniformScaleXTest;
                        }
                        else
                        {
                            uniformScale = uniformScaleYTest;
                        }
                        int uniformWidth = (int)(graphicsSettings.GameResolutionWidth * uniformScale);
                        int uniformHeight = (int)(graphicsSettings.GameResolutionHeight * uniformScale);
                        int uniformX = (graphicsDeviceManager.PreferredBackBufferWidth - uniformWidth) / 2;
                        int uniformY = (graphicsDeviceManager.PreferredBackBufferHeight - uniformHeight) / 2;
                        _graphics.Batch.Draw(finalPassTarget, new Rectangle(uniformX, uniformY, uniformWidth, uniformHeight), Color.White);
                        break;
                    case AeScalingMode.CLOSEST_MULTIPLE_OF_2:
                        int closestMultipleOf2Width = graphicsSettings.GameResolutionWidth;
                        int closestMultipleOf2Height = graphicsSettings.GameResolutionHeight;
                        while (
                            (closestMultipleOf2Width * 2 <= graphicsDeviceManager.PreferredBackBufferWidth) && 
                            (closestMultipleOf2Height * 2 <= graphicsDeviceManager.PreferredBackBufferHeight))
                        {
                            closestMultipleOf2Width *= 2;
                            closestMultipleOf2Height *= 2;
                        }
                        int closestMultipleOf2X = (graphicsDeviceManager.PreferredBackBufferWidth - closestMultipleOf2Width) / 2;
                        int closestMultipleOf2Y = (graphicsDeviceManager.PreferredBackBufferHeight - closestMultipleOf2Height) / 2;
                        _graphics.Batch.Draw(finalPassTarget, new Rectangle(closestMultipleOf2X, closestMultipleOf2Y, closestMultipleOf2Width, closestMultipleOf2Height), Color.White);
                        break;
                    case AeScalingMode.STRETCH:
                        _graphics.Batch.Draw(finalPassTarget, new Rectangle(0, 0, graphicsDeviceManager.PreferredBackBufferWidth, graphicsDeviceManager.PreferredBackBufferHeight), Color.White);
                        break;
                    default:
                        break;
                }
                _graphics.Batch.End();
            }
            else
            {
                graphicsDeviceManager.GraphicsDevice.Clear(graphicsSettings.ClearColorFinalRenderTarget);
                _graphics.Batch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                stateManager.Draw(gameTime, _graphics.Batch);
                _graphics.Batch.End();
            }
        }
    }
}