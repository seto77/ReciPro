using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Crystallography
{
    public static class Miscellaneous
    {
        public static string Ordinal(this int number)
        {
            var ones = number % 10;
            var tens = Math.Floor(number / 10f) % 10;
            if (tens == 1)
                return number + "th";
            switch (ones)
            {
                case 1: return number + "st";
                case 2: return number + "nd";
                case 3: return number + "rd";
                default: return number + "th";
            }
        }

        public static bool IsFiniteNumber(params double[] d)
        {
            foreach (var value in d)
            {
                if (double.IsNaN(value))
                    return false;
                if (double.IsInfinity(value))
                    return false;
            }
            return true;
        }

        private static bool isDecimalPointCommaFlag = true;
        private static bool isDecimalPointComma = false;

        public static bool IsDecimalPointComma
        {
            get
            {
                if (isDecimalPointCommaFlag)
                {
                    double temp;
                    isDecimalPointComma = double.TryParse("1.000,01", out temp);
                    isDecimalPointCommaFlag = false;
                }
                return isDecimalPointComma;
            }
        }


        public static ParallelQuery<int> Sequence(int n) => Enumerable.Range(0, n).ToList().AsParallel();

    }



    /// <summary>
    /// プロパティグリッドのプロパティの並び順をソート
    /// </summary>
    public class DefinitionOrderTypeConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            // TypeDescriptorを使用してプロパティ一覧を取得する
            var pdc = TypeDescriptor.GetProperties(value, attributes);

            // プロパティ一覧をリフレクションから取得
            var type = value.GetType();
            var list = new List<string>();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
                list.Add(propertyInfo.Name);
            // リフレクションから取得した順でソート
            return pdc.Sort(list.ToArray());
        }

        /// <summary>
        /// GetPropertiesをサポートしていることを表明する。
        /// </summary>
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
    }
}