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
        TouchCollection _lastTouchCollection;

        public void Poll()
        {
            Func<bool> calculateJustTouched = () =>
            {
                if (_touchCollection.Count != 0 && _lastTouchCollection.Count == 0)
                {
                    return true;
                }
                return false;
            };

            Func<bool> calculateJustReleased = () =>
            {
                if (_touchCollection.Count == 0 && _lastTouchCollection.Count != 0)
                {
                    return true;
                }
                return false;
            };

            _lastTouchCollection = _touchCollection;
            _touchCollection = TouchPanel.GetState();
            JustTouched = calculateJustTouched();
            JustReleased = calculateJustReleased();


#if DEBUG
            if (JustTouched)
            {
                Console.WriteLine("Just touched.");
            }
            if (JustReleased)
            {
                Console.WriteLine("Just released.");
            }
#endif 
        }

        public bool IsTouched(AeAABB boundingBox)
        {
            //todo position adjustment relative to game/screen resolution
            int gameResolutionWidth = AeEngine.Singleton().Graphics.GraphicsSettings.GameResolutionWidth;
            int gameResolutionHeight = AeEngine.Singleton().Graphics.GraphicsSettings.GameResolutionHeight;
            int screenResolutionWidth = AeEngine.Singleton().Graphics.GraphicsSettings.ScreenResolutionWidth;
            int screenResolutionHeight = AeEngine.Singleton().Graphics.GraphicsSettings.ScreenResolutionHeight;
            foreach (var touch in _touchCollection)
            {
                float touchModifierX = gameResolutionWidth / (float)screenResolutionWidth;
                float touchModifierY = gameResolutionHeight / (float)screenResolutionHeight;
                var position = touch.Position;
                position.X *= touchModifierX;
                position.Y *= touchModifierY;
                if (boundingBox.Overlaps(new Vector2(position.X, position.Y)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsTouched(Rectangle boundingBox)
        {
            return IsTouched(new AeAABB(boundingBox));
        }

        public bool JustTouched { get; private set; }
        public bool JustReleased { get; private set; }

        public Vector2? GetFirstTouchPosition()
        {
            if (_touchCollection.Count == 0)
            {
                return null;
            }
            var touch = _touchCollection.FirstOrDefault();
            if (touch != null && touch.State == TouchLocationState.Moved)
            {
                int gameResolutionWidth = AeEngine.Singleton().Graphics.GraphicsSettings.GameResolutionWidth;
                int gameResolutionHeight = AeEngine.Singleton().Graphics.GraphicsSettings.GameResolutionHeight;
                int screenResolutionWidth = AeEngine.Singleton().Graphics.GraphicsSettings.ScreenResolutionWidth;
                int screenResolutionHeight = AeEngine.Singleton().Graphics.GraphicsSettings.ScreenResolutionHeight;
                float touchModifierX = gameResolutionWidth / (float)screenResolutionWidth;
                float touchModifierY = gameResolutionHeight / (float)screenResolutionHeight;
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