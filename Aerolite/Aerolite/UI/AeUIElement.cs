using Aerolite.Entity;
using Aerolite.Subsystems.Input;
using Microsoft.Xna.Framework;

namespace Aerolite.UI
{
    public delegate void OnMouseHoverCB(AeMouseData mousePosition);
    public delegate void OnMouseClickCB(AeMouseData mousePosition);
    public delegate void OnMouseExitCB(AeMouseData mousePosition);

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
                if (mouse.LeftClick)
                {
                    OnMouseClick(mouse.MouseData);
                    OnMouseClickEvent?.Invoke(mouse.MouseData);
                }
                else
                {
                    _mouseHovering = true;
                    OnMouseHover(mouse.MouseData);
                    OnMouseHoverEvent?.Invoke(mouse.MouseData);
                }
            }
            else
            {
                if (_mouseHovering)
                {
                    _mouseHovering = false;
                    OnMouseExit(mouse.MouseData);
                    OnMouseExitEvent?.Invoke(mouse.MouseData);
                }
            }
        }

        public event OnMouseHoverCB OnMouseHoverEvent;
        public event OnMouseHoverCB OnMouseExitEvent;
        public event OnMouseHoverCB OnMouseClickEvent;

        protected virtual void OnMouseHover(AeMouseData mousePosition)  { }
        protected virtual void OnMouseExit(AeMouseData mousePosition) { }
        protected virtual void OnMouseClick(AeMouseData mousePosition) { }
    }
}
