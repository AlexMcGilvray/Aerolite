using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aerolite.Util
{
    public static class AeUtil
    {
        public static IEnumerable<T> GetEnumValues<T>(T enumType)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Generic type argument for AeUtil.GetEnumValues must be System.Enum");
            }
            return Enum.GetValues(typeof(T)).OfType<T>();
        }
    }
}
