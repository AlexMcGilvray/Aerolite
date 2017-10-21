using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aerolite.Components
{
    public class AeTransformVizualizer : AeComponent
    {
        public AeColor HorizontalCrossColor { get; private set; }
        public AeColor VerticalCrossColor { get; private set; }
        public int Size { get; set; } = 19;
        public AeTransform Transform { get; private set; }
        public bool Visible { get; set; } = true;

        public AeTransformVizualizer(AeTransform tranform)
            : base()
        {
            Transform = tranform;
            HorizontalCrossColor = new AeColor(Color.Red);
            VerticalCrossColor = new AeColor(Color.Green);
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            if (Visible)
            {
                Texture2D fill = Engine.TextureManager.GetFillTexture();
                Rectangle rect;
                rect.X = (int)Transform.X - Size / 2;
                rect.Y = (int)Transform.Y;
                rect.Width = Size;
                rect.Height = 1;
                batch.Draw(fill, rect, HorizontalCrossColor.CurrentColor);
                rect.X = (int)Transform.X;
                rect.Y = (int)Transform.Y - Size / 2;
                rect.Width = 1;
                rect.Height = Size;
                batch.Draw(fill, rect, VerticalCrossColor.CurrentColor);
            }
        }
    }

    public class AeTransform : AeComponent
    {
        private float positionX, positionY;
        private float orientation;
        private float scaleX, scaleY;
        public Vector2 RotationCenter { get; set; } //todo : warn if set outside of the 0-1.0f range

        /// <summary>
        /// When set to true, modifcations to the transforms properties have no effect.
        /// </summary>
        public bool Lock { get; set; } = false;

        public AeTransform()
        {
            scaleX = 1.0f;
            scaleY = 1.0f;
            RotationCenter = new Vector2(0.0f);
        }

        public void SetupDebugVizualization()
        {
            AeTransformVizualizer debugViz = new AeTransformVizualizer(this);
            AddComponent(debugViz);
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(positionX, positionY);
            }
            set
            {
                if (!Lock)
                {
                    positionX = value.X;
                    positionY = value.Y;
                }
            }
        }

        public float X { get { return positionX; } set { if (!Lock) { positionX = value; } } }
        public float Y { get { return positionY; } set { if (!Lock) { positionY = value; } } }

        public float Orientation { get { return orientation; } set { if (!Lock) { orientation = value; } } }

        public float ScaleX { get { return scaleX; } set { if (!Lock) { scaleX = value; } } }
        public float ScaleY { get { return scaleY; } set { if (!Lock) { scaleY = value; } } }
    }
}
