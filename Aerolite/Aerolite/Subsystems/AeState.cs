using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aerolite.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Aerolite.Components;
using Aerolite.Interfaces;
using Aerolite.Subsystems.Input;

namespace Aerolite.Subsystems
{
    public class AeState : IAeEntity
    {
        private AeEntityLayer _entities = new AeEntityLayer();
        public AeCamera Camera { get; private set; }
        public bool CameraEnabled { get; set; }
        public AeEngine Engine { get; private set; }
        public AeInput Input { get { return Engine.Input; } }

        public AeState() : base()
        {
            Engine = AeEngine.Singleton();
            Camera = new AeCamera();
            CameraEnabled = true;
        }

        protected void AddEntity(IAeEntity entity)
        {
            _entities.Add(entity);
        }

        public virtual void Update(GameTime gameTime)
        {
            Camera.Update(gameTime);
            foreach (var ent in _entities.Entities)
            {
                ent.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch batch)
        {
            if (CameraEnabled)
            {
                batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone,null,Camera.GetTransform());
            }
            else
            {
                batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            }
            foreach (var ent in _entities.Entities)
            {
                ent.Draw(gameTime, batch);
            }
            batch.End();
        }
    }
}
