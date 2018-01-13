using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Interfaces
{
    public interface IAeEntity
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime, SpriteBatch batch);
        void Init();
    }
}
