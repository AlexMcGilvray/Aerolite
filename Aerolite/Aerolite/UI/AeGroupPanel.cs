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
        AeLayer<AeUIElement> _elements = new AeLayer<AeUIElement>();
        public AeGroupPanelLayoutOrder LayoutOrder { get; set; }
        public AeGroupPanelLayoutSizing LayoutSizing { get; set; }
        public int Padding { get; set; } = 2;

        public AeGroupPanel()
        {
            Elements = new List<AeUIElement>();
            LayoutOrder = AeGroupPanelLayoutOrder.Horizontal;
            LayoutSizing = AeGroupPanelLayoutSizing.None;
            AddChild(_elements);
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
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Layout();
        }
    }
}
