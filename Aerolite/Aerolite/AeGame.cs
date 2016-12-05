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
        public AeEngine Engine { get; private set; }

        public AeGame():base()
        {
            Content.RootDirectory = "Content";
            AeEngine.Initalize(this);
            Engine = AeEngine.Singleton();
        }
        protected override sealed void Initialize()
        {
            base.Initialize();
        }

        protected override sealed void LoadContent()
        {
            base.LoadContent();
            Engine.Load();
            Load();
        }

        protected virtual void Load() { }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override sealed void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Engine.Input.PollInput();
            Engine.StateManager.Update(gameTime);
        }
        protected override sealed void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Engine.Graphics.Renderer.Render(gameTime, Engine.StateManager);
        }
    }
}
