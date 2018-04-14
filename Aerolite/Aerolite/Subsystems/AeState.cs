using Aerolite.Components;
using Aerolite.Entity;
using Aerolite.Interfaces;
using Aerolite.Subsystems.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Aerolite.Subsystems
{
    public class AeState : IAeEntity
    {
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

        public void AddEntity(IAeEntity entity)
        {
            _entitiesToAdd.Add(entity);
        }

        public void RemoveEntity(IAeEntity entity)
        {
            _entitiesToRemove.Add(entity);
        }

        public bool ContainsEntity(IAeEntity entity) => _entities.Entities.Contains(entity);

        public virtual void Update(GameTime gameTime)
        {
            if (!_hasInited)
            {
                Init();
                _hasInited = true;
            }
            if (_entitiesToRemove.Count > 0)
            {
                foreach (var entity in _entitiesToRemove)
                {
                    _entities.Remove(entity);
                }
                _entitiesToRemove.Clear();
            }
            if (_entitiesToAdd.Count > 0)
            {
                foreach (var entity in _entitiesToAdd)
                {
                    _entities.Add(entity);
                }
                _entitiesToAdd.Clear();
            }
            Camera.Update(gameTime);
            foreach (var ent in _entities.Entities)
            {
                ent.Update(gameTime);
            }
        }

        public virtual void Init()
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch batch)
        {
            if (CameraEnabled)
            {
                batch.Begin(
                    SpriteSortMode.Deferred, 
                    BlendState.AlphaBlend, 
                    SamplerState.PointClamp, 
                    DepthStencilState.Default, 
                    RasterizerState.CullNone, 
                    null, 
                    Camera.GetTransform());
            }
            else
            {
                batch.Begin(
                    SpriteSortMode.Deferred, 
                    BlendState.AlphaBlend, 
                    SamplerState.PointClamp, 
                    DepthStencilState.Default, 
                    RasterizerState.CullNone);
            }
            foreach (var ent in _entities.Entities)
            {
                ent.Draw(gameTime, batch);
            }
            batch.End();
        }

        private AeEntityLayer _entities = new AeEntityLayer();
        private bool _hasInited = false;
        private List<IAeEntity> _entitiesToAdd = new List<IAeEntity>();
        private List<IAeEntity> _entitiesToRemove = new List<IAeEntity>();
    }
}