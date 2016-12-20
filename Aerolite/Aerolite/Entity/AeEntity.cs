using Aerolite.Components;
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
        private List<AeEntity> Children { get; set; }

        protected AeEngine Engine { get; private set; }
        
        public AeEntity()
        {
            Engine = AeEngine.Singleton();
            Transform = new AeTransform();
            CollisionHull = new AeAABB();
            Components = new List<AeComponent>();
            Children = new List<AeEntity>();
        }

        public void AddComponent(AeComponent component)
        {
            component.Owner = this;
            Components.Add(component);
        }

        public void AddChild(AeEntity entity)
        {
            Children.Add(entity);
        }
        //TODO switch to update/doupdate pattern so alive bool will work
        public virtual void Update(GameTime gameTime)
        {
            if (Alive)
            {
                CollisionHull.SetPosition((int)Transform.X, (int)Transform.Y);
                foreach (var cmp in Components)
                {
                    cmp.Update(gameTime);
                }
                foreach (var child in Children)
                {
                    child.Update(gameTime);
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch batch)
        {
            if (Alive)
            {
                foreach (var cmp in Components)
                {
                    cmp.Draw(gameTime, batch);
                }
                foreach (var child in Children)
                {
                    child.Draw(gameTime, batch);
                }
            }
            
        }
    }
}
