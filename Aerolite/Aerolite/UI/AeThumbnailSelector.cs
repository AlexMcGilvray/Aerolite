using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Aerolite.UI
{
    public class AeThumbnailSelector : AeThumbnailSelector<AeUIElement>
    {
        public AeThumbnailSelector() : base(new Rectangle()) { }
        public AeThumbnailSelector(Rectangle boundingBox) :base(boundingBox) { }
    }
    /// <summary>
    /// TODO
    ///  - add pagination logic
    ///  - add pagination controls
    ///  - debug bouinding box draw outline
    /// </summary>
    public class AeThumbnailSelector<T> : AeUIElement where T : AeUIElement
    {
        public AeThumbnailSelector()
            :this(new Rectangle()) { }

        public AeThumbnailSelector(Rectangle boundingBox)
        {
            BoundingBox = boundingBox;
            Transform.X = BoundingBox.X;
            Transform.Y = BoundingBox.Y;
            BackgroundPanelColor = Color.Gray;
            AddChild(_backgroundPanel);
        }

        public Color BackgroundPanelColor
        {
            get => _backgroundPanel.PanelColor;
            set => _backgroundPanel.PanelColor = value;
        }

        public bool BackgroundPanelVisible
        {
            get => _backgroundPanel.Alive;
            set => _backgroundPanel.Alive = value;
        }

        public int Padding { get; set; } = 4;

        public void AddThumbnail(T thumb)
        {
            _thumbnails.Add(thumb);
        }

        public List<T> ThumbnailCollection => _thumbnails;

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

            _backgroundPanel.Transform.X = BoundingBox.X;
            _backgroundPanel.Transform.Y = BoundingBox.Y;
            _backgroundPanel.Width = BoundingBox.Width;
            _backgroundPanel.Height = BoundingBox.Height;

            if (_thumbnails.Count > 0)
            {
                int numColumns = BoundingBox.Width / (_thumbnails[0].BoundingBox.Width + Padding * 2) + Padding;
                int numRows = BoundingBox.Height / (_thumbnails[0].BoundingBox.Height + Padding * 2) + Padding;
                int thumbnailCount = Math.Min((numColumns * numRows), _thumbnails.Count);

                for (int i = 0; i < thumbnailCount; ++i)
                {
                    var pageOffset = (numRows * numColumns) * _page;
                    var thumb = _thumbnails[pageOffset + i];
                    thumb.Transform.X = Padding + BoundingBox.X + ((Padding + thumb.BoundingBox.Width + Padding) * (i % numColumns));
                    thumb.Transform.Y = Padding + BoundingBox.Y + ((Padding + thumb.BoundingBox.Height + Padding) * (i / numColumns));
                    thumb.Draw(gameTime, batch);
                }
            }
        }

        private int _page = 0;
        private List<T> _thumbnails = new List<T>();
        private AePanel _backgroundPanel = new AePanel();
    }
}