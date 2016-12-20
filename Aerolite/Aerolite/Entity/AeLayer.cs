using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Aerolite.Entity
{
    //todo add enumerable support
    public class AeLayer<T> : AeEntity where T:AeEntity
    {
        private const int InitialLayerSize = 64;

        private List<T> _entities;

        public List<T> Children { get { return _entities; } }
        
        public AeLayer(int initialCollectionSize = InitialLayerSize)
        {
            _entities = new List<T>(InitialLayerSize);
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Remove(T entity)
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
