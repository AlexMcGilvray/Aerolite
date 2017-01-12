﻿using Aerolite.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Entity
{
    public class AeEntity
    {
        public bool Alive { get; set; } = true;
        public bool IsInitialized { get; private set; }

        public AeTransform Transform { get; private set; }
        public AeAABB CollisionHull { get; private set; }
        private List<AeComponent> Components { get; set; }
        private List<AeEntity> Entities { get; set; }

        private List<AeComponent> _privateComponents;

        protected AeEngine Engine { get; private set; }

        
        public AeEntity()
        {
            Engine = AeEngine.Singleton();
            Transform = new AeTransform();
            CollisionHull = new AeAABB();
            Components = new List<AeComponent>();
            Entities = new List<AeEntity>();

            _privateComponents = new List<AeComponent>() { Transform, CollisionHull };
        }

        public void AddComponent(AeComponent component)
        {
            component.Owner = this;
            Components.Add(component);
        }

        public void AddChild(AeEntity entity)
        {
            Entities.Add(entity);
        }
        //TODO switch to update/doupdate pattern so alive bool will work
        public virtual void Update(GameTime gameTime)
        {
            if (Alive)
            {
                CollisionHull.SetPosition((int)Transform.X, (int)Transform.Y);
                foreach(var cmp in _privateComponents)
                {
                    cmp.Update(gameTime);
                }
                foreach (var cmp in Components)
                {
                    cmp.Update(gameTime);
                }
                foreach (var child in Entities)
                {
                    child.Update(gameTime);
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch batch)
        {
            if (Alive)
            {
                foreach (var cmp in _privateComponents)
                {
                    cmp.Draw(gameTime, batch);
                }
                foreach (var cmp in Components)
                {
                    cmp.Draw(gameTime, batch);
                }
                foreach (var child in Entities)
                {
                    child.Draw(gameTime, batch);
                }
            }
            
        }
    }
}
