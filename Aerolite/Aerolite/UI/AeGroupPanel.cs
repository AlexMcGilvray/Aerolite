using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.UI
{
    public enum AeGroupPanelLayout
    {
        Horizontal,
        Vertical
    }

    public class AeGroupPanel : AeUIElement
    {
        public List<AeUIElement> Elements { get; private set; }
        public AeGroupPanelLayout LayoutOrder { get; set; }

        public AeGroupPanel()
        {
            Elements = new List<AeUIElement>();
            LayoutOrder = AeGroupPanelLayout.Horizontal;
        }


    }
}
