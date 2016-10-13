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

        public AeTransform Transform { get; set; }
        
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime, SpriteBatch batch) { }

        
    }
}
