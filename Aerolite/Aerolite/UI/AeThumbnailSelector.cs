using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aerolite.UI
{
    /// <summary>
    /// TODO
    ///  - add pagination logic
    ///  - add pagination controls
    ///  - debug bouinding box draw outline
    /// </summary>
    public class AeThumbnailSelector : AeUIElement
    {
        public AeThumbnailSelector(Rectangle boundingBox)
        {
            BoundingBox = boundingBox;
            Transform.X = boundingBox.X;
            Transform.Y = boundingBox.Y;
            BackgroundPanelColor = Color.Gray;
            _backgroundPanel.Transform.X = boundingBox.X;
            _backgroundPanel.Transform.Y = boundingBox.Y;
            _backgroundPanel.Width = boundingBox.Width;
            _backgroundPanel.Height = boundingBox.Height;
            AddChild(_backgroundPanel);
        }


        public Color BackgroundPanelColor
        {
            get
            {
                return _backgroundPanel.PanelColor;
            }
            set
            {
                _backgroundPanel.PanelColor = value;
            }

        }
        public bool BackgroundPanelVisible
        {
            get
            {
                return _backgroundPanel.Alive;
            }
            set
            {
                _backgroundPanel.Alive = value;
            }

        }

        public int Padding { get; set; } = 4;

        public void AddThumbnail(AeUIElement thumb)
        {
            _thumbnails.Add(thumb);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


            foreach (var thumb in _thumbnails)
            {
                thumb.Update(gameTime);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch batch)
        {
            base.Draw(gameTime, batch);
            if (_thumbnails.Count > 0)
            {
                int numColumns = BoundingBox.Width / (_thumbnails[0].BoundingBox.Width + Padding * 2);
                int numRows = BoundingBox.Height / (_thumbnails[0].BoundingBox.Height + Padding * 2);

                int thumbnailCount = Math.Min((numColumns * numRows), _thumbnails.Count);

                for (int i = 0; i < thumbnailCount; ++i)
                {
                    var pageOffset = (numRows * numColumns) * _page;
                    var thumb = _thumbnails[pageOffset + i];
                    thumb.Transform.X = BoundingBox.X + ((Padding + thumb.BoundingBox.Width + Padding) * (i % numColumns));
                    thumb.Transform.Y = BoundingBox.Y + ((Padding + thumb.BoundingBox.Height + Padding) * (i / numColumns));
                    thumb.Draw(gameTime, batch);
                }
            }
        }

        private int _page = 0;
        List<AeUIElement> _thumbnails = new List<AeUIElement>();
        private AePanel _backgroundPanel = new AePanel();

    }
}