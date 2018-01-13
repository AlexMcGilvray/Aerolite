using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Aerolite.Util
{
    public static class AeUtil
    {
        public static IEnumerable<T> GetEnumValues<T>()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Generic type argument for AeUtil.GetEnumValues must be System.Enum");
            }
            return Enum.GetValues(typeof(T)).OfType<T>();
        }


        //this wont work unless I made 2 aerolite projects (windows/andriod) because it would need the xamarin dll's not the .net
        //public static XDocument LoadXmlDocument(string path)
        //{
        //    XDocument doc;
        //    using (var stream =
        //    {
        //        doc = XDocument.Load(stream);
        //    }
        //    return doc;
        //}
    }
}