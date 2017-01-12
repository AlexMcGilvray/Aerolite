using Aerolite.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Components
{
    public abstract class AeComponent 
    {
        public AeEntity Owner { get; set; }
        private List<AeComponent> _components = new List<AeComponent>();
        protected AeEngine Engine;

        public AeComponent()
        {
            Engine = AeEngine.Singleton();
        }

        public void AddComponent(AeComponent comp)
        {
            if (comp != null)
            {
                _components.Add(comp);
            }
        }

        public void RemoveComponent(AeComponent comp)
        {
            if (comp != null)
            {
                _components.Remove(comp);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var component in _components)
            {
                component.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch batch)
        {
            foreach (var component in _components)
            {
                component.Draw(gameTime, batch);
            }
        }
    }
}
