using System.IO;
using System.Xml.Serialization;

namespace Crystallography
{
    /*
    static class CloneUtils
    {
        /// <summary>
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static object Copy(this object target)
        {
            object result;
            BinaryFormatter b = new BinaryFormatter();
            MemoryStream mem = new MemoryStream();
            try
            {
                b.Serialize(mem, target);
                mem.Position = 0;
                result = b.Deserialize(mem);
            }
            finally
            {
                mem.Close();
            }
            return result;
        }
    }
     */

    /// <summary>
    ///
    /// </summary>
    public static class Deep
    {
        //Element element = Deep.Copy<Element>(e);みたいな感じで使う
        public static T Copy<T>(T target)
        {
            T result;
            var b = new XmlSerializer(typeof(T));
            var mem = new MemoryStream();
            try
            {
                b.Serialize(mem, target);
                mem.Position = 0;
                result = (T)b.Deserialize(mem);
            }
            finally
            {
                mem.Close();
            }
            return result;
        }
    }
}