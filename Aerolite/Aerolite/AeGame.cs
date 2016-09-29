using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite
{

    public class AeGame : Game
    {
        AeEngine _engine;
        public AeGame():base()
        {
            Content.RootDirectory = "Content";
        }
        protected override sealed void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            AeEngine.Initalize(this);
        }
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _engine.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _engine.Render(gameTime);
        }
    }
}
