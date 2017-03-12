using Aerolite.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Subsystems.Input
{
    public class AeTouch 
    {
        TouchCollection _touchCollection;

        public void Poll()
        {
            _touchCollection = TouchPanel.GetState();
        }

        public bool IsTouched(AeAABB boundingBox)
        {
            foreach (var touch in _touchCollection)
            {
                if (boundingBox.Overlaps(new Vector2(touch.Position.X, touch.Position.Y)))
                {
                    return true;
                }
            }
            return false;
        }

        public Vector2? GetFirstTouchPosition()
        {
            var touch = _touchCollection.FirstOrDefault();
          
            if (touch != null && touch.State == TouchLocationState.Moved)
            {
                var touchModifierX = AeEngine.Singleton().Graphics.GraphicsSettings.GameResolutionWidth  / (float)AeEngine.Singleton().Graphics.GraphicsSettings.ScreenResolutionWidth;
                var touchModifierY = AeEngine.Singleton().Graphics.GraphicsSettings.GameResolutionHeight  / (float)AeEngine.Singleton().Graphics.GraphicsSettings.ScreenResolutionHeight;
                var position = touch.Position;
                position.X *= touchModifierX;
                position.Y *= touchModifierY;
                return position;
            }
            return null;
        }

        public bool AnyTouched()
        {
            return _touchCollection.Count > 0;
        }
    }
}
