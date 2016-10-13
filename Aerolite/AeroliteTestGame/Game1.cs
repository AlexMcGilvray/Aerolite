using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Aerolite;

namespace AeroliteTestGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : AeGame
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1():base()
        {
            Content.RootDirectory = "Content";
        }
 
        protected override void Load()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}
