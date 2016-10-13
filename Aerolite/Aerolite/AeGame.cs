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
            AeEngine.Initalize(this);
            _engine = AeEngine.Singleton();
        }
        protected override sealed void Initialize()
        {
            base.Initialize();
        }

        protected override sealed void LoadContent()
        {
            base.LoadContent();
            Load();
        }

        protected virtual void Load()
        {

        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override sealed void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _engine.Update(gameTime);
        }
        protected override sealed void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            _engine.Render(gameTime);
            
        }
    }
}
