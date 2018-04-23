using Aerolite.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.UI
{
    public enum AeGroupPanelLayoutOrder
    {
        Horizontal,
        Vertical
    }

    public enum AeGroupPanelLayoutSizing
    {
        None,
        ScaleToLargestElement
    }

    public class AeGroupPanel : AeUIElement
    {
        //TODO make the layer enumerable ASAP because this 2 list stuff is getting tiresome
        public List<AeUIElement> Elements { get; private set; }
        public AeGroupPanelLayoutOrder LayoutOrder { get; set; }
        public AeGroupPanelLayoutSizing LayoutSizing { get; set; }
        public int Padding { get; set; } = 2;

        public int BorderSize { get; set; } = 2;

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

        public Color BorderPanelColor
        {
            get
            {
                return _borderPanel.PanelColor;
            }
            set
            {
                _borderPanel.PanelColor = value;
            }

        }
        public bool BorderPanelVisible
        {
            get
            {
                return _borderPanel.Alive;
            }
            set
            {
                _borderPanel.Alive = value;
            }
        }

        public AeGroupPanel()
        {
            Elements = new List<AeUIElement>();
            LayoutOrder = AeGroupPanelLayoutOrder.Horizontal;
            LayoutSizing = AeGroupPanelLayoutSizing.None;

            BackgroundPanelColor = Color.DarkGray;
            BorderPanelColor = Color.Gray;

            AddChild(_borderPanel);
            AddChild(_backgroundPanel);
            AddChild(_elements);
        }

        public void Clear()
        {
            Elements.Clear();
            _elements.Clear();
        }

        public void Add(AeUIElement element)
        {
            Elements.Add(element);
            _elements.Add(element);
        }

        public void Layout()
        {
            //used as the current position when placing an element while iterating through all the elements
            Vector2 currentPosition;
            currentPosition.X = Transform.X;
            currentPosition.Y = Transform.Y;
            int largestWidth = 0;
            int largestHeight = 0;

            switch (LayoutOrder)
            {
                case AeGroupPanelLayoutOrder.Horizontal:
                    currentPosition.X += Padding;
                    break;
                case AeGroupPanelLayoutOrder.Vertical:
                    currentPosition.Y += Padding;
                    break;
                default:
                    break;
            }

            foreach (var element in Elements)
            {
                element.Transform.X = currentPosition.X;
                element.Transform.Y = currentPosition.Y;
                if (largestWidth < element.BoundingBox.Width)
                {
                    largestWidth = element.BoundingBox.Width;
                }
                if (largestHeight < element.BoundingBox.Height)
                {
                    largestHeight = element.BoundingBox.Height;
                }
                //add to the current position the width/height of the previous element
                switch (LayoutOrder)
                {
                    case AeGroupPanelLayoutOrder.Horizontal:
                        currentPosition.X += element.BoundingBox.Width + Padding;
                        break;
                    case AeGroupPanelLayoutOrder.Vertical:
                        currentPosition.Y += element.BoundingBox.Height + Padding;
                        break;
                    default:
                        break;
                }
            }

            if (LayoutSizing == AeGroupPanelLayoutSizing.ScaleToLargestElement)
            {
                foreach(var element in Elements)
                {
                    element.BoundingBox.Width = largestWidth;
                    element.BoundingBox.Height = largestHeight;
                }
            }

            BoundingBox.Width = largestWidth;
            BoundingBox.Height = largestHeight;

            {
                int totalWidth = 0;
                int totalHeight = 0;

                switch (LayoutOrder)
                {
                    case AeGroupPanelLayoutOrder.Horizontal:
                        totalWidth = (largestWidth * Elements.Count) + (Padding * Elements.Count) + Padding;
                        totalHeight = largestHeight;
                        break;
                    case AeGroupPanelLayoutOrder.Vertical:
                        totalWidth = largestWidth;
                        totalHeight = (largestHeight * Elements.Count) + (Padding * Elements.Count) + Padding;
                        break;
                    default:
                        break;
                }

                _backgroundPanel.Transform.X = Transform.X;
                _backgroundPanel.Transform.Y = Transform.Y;
                _backgroundPanel.Width = totalWidth;
                _backgroundPanel.Height = totalHeight;

                _borderPanel.Transform.X = _backgroundPanel.Transform.X - BorderSize;
                _borderPanel.Transform.Y = _backgroundPanel.Transform.Y - BorderSize;
                _borderPanel.Width = totalWidth + BorderSize * 2;
                _borderPanel.Height = totalHeight + BorderSize * 2;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Layout();
        }

        private readonly AePanel _backgroundPanel = new AePanel();
        private readonly AePanel _borderPanel = new AePanel();
        AeLayer<AeUIElement> _elements = new AeLayer<AeUIElement>();
    }
}
