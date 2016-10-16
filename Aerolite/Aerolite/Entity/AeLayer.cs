using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Aerolite.Entity
{
    public class AeLayer : AeEntity
    {
        private const int InitialLayerSize = 64;

        private List<AeEntity> _entities;
        
        public AeLayer(int initialCollectionSize = InitialLayerSize)
        {
            _entities = new List<AeEntity>(InitialLayerSize);
        }

        public void Add(AeEntity entity)
        {
            _entities.Add(entity);
        }

        public void Remove(AeEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach(var ent in _entities)
            {
                ent.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            foreach(var ent in _entities)
            {
                ent.Draw(gameTime, batch);
            }
        }
    }
}
