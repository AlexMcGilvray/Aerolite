using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.UI
{
    public class AeUISettings
    {
        /// <summary>
        /// When this is enabled UI controls will throw exceptions when they get invalid values. For example
        /// the AeProgress bar will throw if it gets a values outside of the range 0.0f-1.0f for its 
        /// property "CurrentValue".
        /// </summary>
        public bool ShouldThrowOnInvalidValues { get; set; } = false;
    }
}
