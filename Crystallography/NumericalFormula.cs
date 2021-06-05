using System;
using System.Collections.Generic;

namespace Crystallography
{
    public class NumericalFormula
    {
        public static double GetNumetricValue(string[] str)
        {
            try
            {
                //for (int i=0; i<str.Length ; i++)
                //スペースを削除する
                //定数を計算する
                for (int i = 0; i < str.Length; i++)
                    if (str[i].Contains("=")) // '=' の文字列がみつかったら
                    {
                        string leftString = str[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", ""); ;
                        string rightString = str[i].Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", ""); ;

                        for (int j = i + 1; j < str.Length; j++)
                            for (int l = 0; l < str[j].Length; l++)
                            {
                                if (str[j].IndexOf(leftString, l) >= 0)//文字列が見つかったとき
                                {
                                    int k = str[j].IndexOf(leftString);
                                    bool flag = true;

                                    if (k + leftString.Length < str[j].Length)//後方に余裕のあるとき
                                        if ('A' <= str[j][k + leftString.Length] && 'z' >= str[j][k + leftString.Length])//直後が文字列のときは
                                            flag = false;

                                    if (k > 0)//前方に余裕のあるとき
                                        if ('A' <= str[j][k - 1] && 'z' >= str[j][k - 1])//直前が文字列のときは
                                            flag = false;

                                    if (flag)
                                    {
                                        str[j] = str[j].Remove(k, leftString.Length);
                                        str[j] = str[j].Insert(k, "(" + rightString + ")");
                                    }
                                }
                            }
                    }
                return NumericalValue(str[^1]);
            }
            catch
            {
                return double.NaN;
            }
        }

        private static double NumericalValue(string str)
        {
            str = str.Replace(" ", "");

            //"e" があって直前直後が数値のときは "*10^"に変換しておく
            for (int i = 0; i < str.Length - 1; i++)
                if (str.Substring(i, 1).ToLower() == "e" && (str[i + 1] == '-' || str[i + 1] == '+' || (str[i + 1] >= '0' && str[i + 1] <= '9')))
                {
                    str.Remove(i, 1);
                    if (i == 0)
                        str.Insert(i, "10^");
                    else
                        str.Insert(i, "*10^");
                }

            List<object> list = new List<object>();

            for (int i = 0; i < str.Length; i++)
            {
                if (i + 1 < str.Length)//2文字の関数、定数を検索
                {
                    string func = str.Substring(i, 2).ToLower();
                    if (func == "pi" || func == "ln")
                    {
                        list.Add(func);
                        str = str.Remove(i, 2);
                        i = 0;
                    }
                }
                if (i + 2 < str.Length)//3文字の関数、定数を検索
                {
                    string func = str.Substring(i, 3).ToLower();
                    if (func == "sin" || func == "cos" || func == "tan" || func == "exp" || func == "log" || func == "abs")
                    {
                        list.Add(func);
                        str = str.Remove(i, 3);
                        i = 0;
                    }
                }
                if (i + 3 < str.Length)//4文字の関数、定数を検索
                {
                    string func = str.Substring(i, 4).ToLower();
                    if (func == "asin" || func == "acos" || func == "atan" || func == "sqrt")
                    {
                        list.Add(func);
                        str = str.Remove(i, 4);
                        i = 0;
                    }
                }

                if (i < str.Length && str[i] == '(')  //かっこの始まりが現れたら
                {
                    int count = 1;
                    for (int j = 1; j < str.Length; j++)//対応するかっこの終りをみつける
                    {
                        if (str[j] == '(')
                            count++;
                        else if (str[j] == ')')
                            count--;

                        if (count == 0)//見つかったら
                        {
                            list.Add(NumericalValue(str[1..j]));
                            str = str.Remove(0, j + 1);
                            i = 0; //次を0に戻す
                            break;
                        }
                    }
                }

                if (i < str.Length && (str[i] == '+' || str[i] == '-' || str[i] == '*' || str[i] == '/' || str[i] == '^')) //演算子が現れたら
                {
                    if (i != 0)
                        list.Add(Convert.ToDouble(str.Substring(0, i)));//演算子の直前までを数値に変換し格納

                    list.Add(str[i]);//演算子を格納

                    str = str.Remove(0, i + 1);
                    i = -1;//次を0に戻す
                }
            }
            //最後にstrに余りがあればそれを数値に変換1
            if (str.Length > 0)
                list.Add(Convert.ToDouble(str));

            if (list.Count == 0)
                return 0;

            //最初に関数をチェック
            for (int i = 0; i < list.Count; i++)
                if (list[i].GetType() == typeof(string))
                {
                    if ((string)list[i] == "pi")//定数の場合
                        list[i] = Math.PI;
                    else if (i + 1 < list.Count && list[i + 1].GetType() == typeof(double))
                    {
                        if ((string)list[i] == "ln")
                            list[i] = Math.Log((double)list[i + 1]);
                        else if ((string)list[i] == "sin")
                            list[i] = Math.Sin((double)list[i + 1] / 180 * Math.PI);
                        else if ((string)list[i] == "cos")
                            list[i] = Math.Cos((double)list[i + 1] / 180 * Math.PI);
                        else if ((string)list[i] == "tan")
                            list[i] = Math.Tan((double)list[i + 1] / 180 * Math.PI);
                        else if ((string)list[i] == "exp")
                            list[i] = Math.Exp((double)list[i + 1]);
                        else if ((string)list[i] == "log")
                            list[i] = Math.Log10((double)list[i + 1]);
                        else if ((string)list[i] == "abs")
                            list[i] = Math.Abs((double)list[i + 1]);
                        else if ((string)list[i] == "asin")
                            list[i] = Math.Asin((double)list[i + 1]) / Math.PI * 180;
                        else if ((string)list[i] == "acos")
                            list[i] = Math.Acos((double)list[i + 1]) / Math.PI * 180;
                        else if ((string)list[i] == "atan")
                            list[i] = Math.Atan((double)list[i + 1]) / Math.PI * 180;
                        else if ((string)list[i] == "sqrt")
                            list[i] = Math.Sqrt((double)list[i + 1]);

                        list.RemoveAt(i + 1);
                    }
                    else
                        return double.NaN;
                }

            //先頭が'-'あるいは'+'で始まる場合を考慮する
            if (list.Count > 1)
                if (list[0].GetType() == typeof(char) && ((char)list[0] == '-' || (char)list[0] == '+'))
                {
                    if (list[1].GetType() == typeof(double))
                    {
                        if ((char)list[0] == '-')
                            list[1] = -(double)list[1];
                        list.RemoveAt(0);
                    }
                    else
                        return double.NaN;//先頭が'-'なのに次が数値でないときはNaNを返す
                }

            //次に 演算子の後に"-"あるいは'+'が来る場合に対処
            if (list.Count > 2)
                for (int i = 0; i < list.Count - 2; i++)
                {
                    if (list[i].GetType() == typeof(char) && list[i + 1].GetType() == typeof(char))//2個演算子が続いているところ
                    {
                        if (list[i + 2].GetType() != typeof(double))
                            return double.NaN;//2個続いたあとのやつが数値でないときはNaNを返す

                        if ((char)list[i + 1] == '-')// '-'だったら符号を変える
                            list[i + 2] = -(double)list[i + 2];
                        else if ((char)list[i + 1] != '+')// '-'でも'+'でもないときはNaNをかえす
                            return double.NaN;
                        list.RemoveAt(i + 1);
                    }
                }

            //ここまでで数値はかならず演算子一つに挟まれるようになっているはずなのでチェック
            //listのカウントが奇数個で、かつ偶数番目が数値、奇数が演算子になるはず
            if (list.Count % 2 != 1)
                return double.NaN;

            for (int i = 0; i < list.Count; i++)
                if ((i % 2 == 0 && list[i].GetType() != typeof(double)) && (i % 2 == 1 && list[i].GetType() != typeof(char)))
                    return double.NaN;

            //まず^を後ろから探す
            for (int i = list.Count - 1; i >= 0; i--)
                if (list[i].GetType() == typeof(char) && (char)list[i] == '^')
                {
                    double v = Math.Pow((double)list[i - 1], (double)list[i + 1]);
                    list.RemoveRange(i, 2);
                    list[i - 1] = v;
                }

            //次に*と/を探す
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].GetType() == typeof(char) && (char)list[i] == '*')
                {
                    double v = (double)list[i - 1] * (double)list[i + 1];
                    list.RemoveRange(i, 2);
                    list[i - 1] = v;
                    i = 0;
                }
                else if (list[i].GetType() == typeof(char) && (char)list[i] == '/')
                {
                    double v = (double)list[i - 1] / (double)list[i + 1];
                    list.RemoveRange(i, 2);
                    list[i - 1] = v;
                    i = 0;
                }
            }

            //最後に+と-を探す
            for (int i = 1; i < list.Count; i++)
            {
                if (list[i].GetType() == typeof(char) && (char)list[i] == '+')
                {
                    double v = (double)list[i - 1] + (double)list[i + 1];
                    list.RemoveRange(i, 2);
                    list[i - 1] = v;
                    i = 0;
                }
                else if (list[i].GetType() == typeof(char) && (char)list[i] == '-')
                {
                    double v = (double)list[i - 1] - (double)list[i + 1];
                    list.RemoveRange(i, 2);
                    list[i - 1] = v;
                    i = 0;
                }
            }

            if (list.Count == 1)
                return (double)list[0];
            else
                return double.NaN;
        }
    }
}