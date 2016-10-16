﻿using Aerolite.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.HighLevel2D
{
    public class AeSprite : AeEntity
    {
        public Texture2D Texture { get; private set; }
        //public RsColor RenderColor { get; private set; }
        private Vector2 _size;
        public float SizeX { get { return _size.X; } set { _size.X = value; } }
        public float SizeY { get { return _size.Y; } set { _size.Y = value; } }
        public Vector2 Size { get { return _size; } set { _size = value; } }
        
        public AeSprite(string texturePath) : base()
        {
            Texture = Engine.TextureManager.LoadTexture(texturePath);
            SizeX = Texture.Width;
            SizeY = Texture.Height;
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            Rectangle destinationRectangle;
            destinationRectangle.X = (int)Math.Floor(Transform.X);
            destinationRectangle.Y = (int)Math.Floor(Transform.Y);
            destinationRectangle.Width = (int)Size.X;
            destinationRectangle.Height = (int)Size.Y;
            batch.Draw(Texture, destinationRectangle, Color.White);
        }


    }
}
