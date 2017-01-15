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

namespace Aerolite.Subsystems
{
    //todo just like layer this has the issue of have 2 entity collections because it inherits from AeEntity rather than from an updatable/renderable interface
    //actually.. in this case maybe just get rid of the list and use the collections built into entity
    public class AeState : AeEntity
    {
        private AeEntityLayer _entities = new AeEntityLayer();
        public AeCamera Camera { get; private set; }
        public bool CameraEnabled { get; set; }

        public AeState() : base()
        {
            Camera = new AeCamera();
            AddComponent(Camera);
            CameraEnabled = true;
        }

        protected void AddEntity(IAeEntity entity)
        {
            _entities.Add(entity);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var ent in _entities.Entities)
            {
                ent.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            if (CameraEnabled)
            {
                batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone,null,Camera.GetTransform());
            }
            else
            {
                batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
            }
            base.Draw(gameTime, batch);
            foreach (var ent in _entities.Entities)
            {
                ent.Draw(gameTime, batch);
            }
            batch.End();
        }
    }
}
