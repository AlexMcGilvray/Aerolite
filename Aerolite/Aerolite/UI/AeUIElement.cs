using Aerolite.Entity;
using Aerolite.Subsystems.Input;
using Microsoft.Xna.Framework;

namespace Aerolite.UI
{
   
    /// <summary>
    /// TODO implement some kind of event system and have virtuals or callbacks for OnMouseHover etc etc
    /// </summary>
    public class AeUIElement : AeEntity
    {
        public Rectangle BoundingBox;

        private bool _mouseHovering;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            BoundingBox.X = (int)(Transform.X);
            BoundingBox.Y = (int)(Transform.Y);
            var mouse = Engine.Input.Mouse;
            if (BoundingBox.Contains(mouse.X,mouse.Y))
            {
                OnMouseHover(mouse.MouseData);
                _mouseHovering = true;
            }
            else
            {
                if (_mouseHovering)
                {
                    OnMouseExit(mouse.MouseData);
                }
                _mouseHovering = false;
            }
        }

        protected virtual void OnMouseHover(AeMouseData mousePosition)  { }
        protected virtual void OnMouseExit(AeMouseData mousePosition) { }



    }
}
