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

        protected override sealed void LoadContent()
        {
            base.LoadContent();
            AeEngine.Initalize(this);
            _engine = AeEngine.Singleton();
            Load();
        }

        protected virtual void Load()
        {

        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
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
