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
        public bool Alive { get; set; }
        public bool IsInitialized { get; private set; }

        public AeTransform Transform { get; private set; }
        private List<AeComponent> Components { get; set; }

        protected AeEngine Engine { get; private set; }
        
        public AeEntity()
        {
            Engine = AeEngine.Singleton();
            Transform = new AeTransform();
            Components = new List<AeComponent>();
        }

        public void AddComponent(AeComponent component)
        {
            component.Owner = this;
            Components.Add(component);
        }
        
        public virtual void Update(GameTime gameTime)
        {
            foreach (var cmp in Components)
            {
                cmp.Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch batch)
        {
            foreach (var cmp in Components)
            {
                cmp.Draw(gameTime,batch);
            }
        }
    }
}
