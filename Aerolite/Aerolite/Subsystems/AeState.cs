using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aerolite.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aerolite.Subsystems
{
    public class AeState : AeEntity
    {
        private List<AeEntity> _entities = new List<AeEntity>();

        public AeState() : base()
        {

        }

        protected void AddEntity(AeEntity entity)
        {
            _entities.Add(entity);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (var ent in _entities)
            {
                ent.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            foreach (var ent in _entities)
            {
                ent.Draw(gameTime, batch);
            }
        }
    }
}
