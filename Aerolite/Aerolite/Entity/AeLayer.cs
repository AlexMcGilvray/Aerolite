﻿using Aerolite.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Aerolite.Entity
{
    //todo add enumerable support
    //todo this inherits from entity which also has children. Either remove the concept of layers or extract a
    //updatable/renderable interface layer can inherit from where it won't have things like 2 collections of entities
    public class AeLayer<T> : IAeEntity where T: IAeEntity
    {
        private const int InitialLayerSize = 64;

        private List<T> _entities;

        public List<T> Entities { get { return _entities; } }

        public AeLayer() : this(InitialLayerSize) { }
        public bool Alive { get; set; } = true;
        
        public AeLayer(int initialCollectionSize)
        {
            _entities = new List<T>(InitialLayerSize);
        }

        public void Reverse()
        {
            _entities.Reverse();
        }

        public void Clear()
        {
            _entities.Clear();
        }

        /// <summary>
        /// What this should really do is put this in a defferred buffer so the entities can be added at a safe time.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public virtual void Init() { }

        public virtual void Update(GameTime gameTime)
        {
            if (!Alive)
            {
                return;
            }
            foreach (var ent in _entities)
            {
                ent.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch batch)
        {
            if (!Alive)
            {
                return;
            }
            foreach (var ent in _entities)
            {
                ent.Draw(gameTime, batch);
            }
        }
    }
}
