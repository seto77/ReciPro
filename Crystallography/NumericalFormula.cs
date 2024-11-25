using System;
using System.Collections.Generic;

namespace Crystallography;

public class NumericalFormula
{
    public static double GetNumetricValue(string[] str)
    {
        try
        {
            //for (int i=0; i<str.Length ; i++)
            //�X�y�[�X���폜����
            //�萔���v�Z����
            for (int i = 0; i < str.Length; i++)
                if (str[i].Contains('=')) // '=' �̕����񂪂݂�������
                {
                    string leftString = str[i].Split(["="], StringSplitOptions.RemoveEmptyEntries)[0].Replace(" ", ""); ;
                    string rightString = str[i].Split(["="], StringSplitOptions.RemoveEmptyEntries)[1].Replace(" ", ""); ;

                    for (int j = i + 1; j < str.Length; j++)
                        for (int l = 0; l < str[j].Length; l++)
                        {
                            if (str[j].IndexOf(leftString, l, StringComparison.Ordinal) >= 0)//�����񂪌��������Ƃ�
                            {
                                int k = str[j].IndexOf(leftString, StringComparison.Ordinal);
                                bool flag = true;

                                if (k + leftString.Length < str[j].Length)//����ɗ]�T�̂���Ƃ�
                                    if ('A' <= str[j][k + leftString.Length] && 'z' >= str[j][k + leftString.Length])//���オ������̂Ƃ���
                                        flag = false;

                                if (k > 0)//�O���ɗ]�T�̂���Ƃ�
                                    if ('A' <= str[j][k - 1] && 'z' >= str[j][k - 1])//���O��������̂Ƃ���
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

        //"e" �������Ē��O���オ���l�̂Ƃ��� "*10^"�ɕϊ����Ă���
        for (int i = 0; i < str.Length - 1; i++)
            if (str.Substring(i, 1).ToLower() == "e" && (str[i + 1] == '-' || str[i + 1] == '+' || (str[i + 1] >= '0' && str[i + 1] <= '9')))
            {
                str = str.Remove(i, 1);
                if (i == 0)
                    str = str.Insert(i, "10^");
                else
                    str = str.Insert(i, "*10^");
            }

        var list = new List<object>();

        for (int i = 0; i < str.Length; i++)
        {
            if (i + 1 < str.Length)//2�����̊֐��A�萔������
            {
                string func = str.Substring(i, 2).ToLower();
                if (func == "pi" || func == "ln")
                {
                    list.Add(func);
                    str = str.Remove(i, 2);
                    i = 0;
                }
            }
            if (i + 2 < str.Length)//3�����̊֐��A�萔������
            {
                string func = str.Substring(i, 3).ToLower();
                if (func == "sin" || func == "cos" || func == "tan" || func == "exp" || func == "log" || func == "abs")
                {
                    list.Add(func);
                    str = str.Remove(i, 3);
                    i = 0;
                }
            }
            if (i + 3 < str.Length)//4�����̊֐��A�萔������
            {
                string func = str.Substring(i, 4).ToLower();
                if (func == "asin" || func == "acos" || func == "atan" || func == "sqrt")
                {
                    list.Add(func);
                    str = str.Remove(i, 4);
                    i = 0;
                }
            }

            if (i < str.Length && str[i] == '(')  //�������̎n�܂肪���ꂽ��
            {
                int count = 1;
                for (int j = 1; j < str.Length; j++)//�Ή����邩�����̏I����݂���
                {
                    if (str[j] == '(')
                        count++;
                    else if (str[j] == ')')
                        count--;

                    if (count == 0)//����������
                    {
                        list.Add(NumericalValue(str[1..j]));
                        str = str.Remove(0, j + 1);
                        i = 0; //����0�ɖ߂�
                        break;
                    }
                }
            }

            if (i < str.Length && (str[i] == '+' || str[i] == '-' || str[i] == '*' || str[i] == '/' || str[i] == '^')) //���Z�q�����ꂽ��
            {
                if (i != 0)
                    list.Add(Convert.ToDouble(str[..i]));//���Z�q�̒��O�܂ł𐔒l�ɕϊ����i�[

                list.Add(str[i]);//���Z�q���i�[

                str = str.Remove(0, i + 1);
                i = -1;//����0�ɖ߂�
            }
        }
        //�Ō��str�ɗ]�肪����΂���𐔒l�ɕϊ�1
        if (str.Length > 0)
            list.Add(Convert.ToDouble(str));

        if (list.Count == 0)
            return 0;

        //�ŏ��Ɋ֐����`�F�b�N
        for (int i = 0; i < list.Count; i++)
            if (list[i].GetType() == typeof(string))
            {
                if ((string)list[i] == "pi")//�萔�̏ꍇ
                    list[i] = Math.PI;
                else if (i + 1 < list.Count && list[i + 1].GetType() == typeof(double))
                {
                    switch (list[i])
                    {
                        case "ln": list[i] = Math.Log((double)list[i + 1]); break;
                        case "sin": list[i] = Math.Sin((double)list[i + 1] / 180 * Math.PI); break;
                        case "cos": list[i] = Math.Cos((double)list[i + 1] / 180 * Math.PI); break;
                        case "tan": list[i] = Math.Tan((double)list[i + 1] / 180 * Math.PI); break;
                        case "exp": list[i] = Math.Exp((double)list[i + 1]); break;
                        case "log": list[i] = Math.Log10((double)list[i + 1]); break;
                        case "abs": list[i] = Math.Abs((double)list[i + 1]); break;
                        case "asin": list[i] = Math.Asin((double)list[i + 1]) / Math.PI * 180; break;
                        case "acos": list[i] = Math.Acos((double)list[i + 1]) / Math.PI * 180; break;
                        case "atan": list[i] = Math.Atan((double)list[i + 1]) / Math.PI * 180; break;
                        case "sqrt": list[i] = Math.Sqrt((double)list[i + 1]); break;
                    }

                    list.RemoveAt(i + 1);
                }
                else
                    return double.NaN;
            }

        //�擪��'-'���邢��'+'�Ŏn�܂�ꍇ���l������
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
                    return double.NaN;//�擪��'-'�Ȃ̂Ɏ������l�łȂ��Ƃ���NaN��Ԃ�
            }

        //���� ���Z�q�̌��"-"���邢��'+'������ꍇ�ɑΏ�
        if (list.Count > 2)
            for (int i = 0; i < list.Count - 2; i++)
            {
                if (list[i].GetType() == typeof(char) && list[i + 1].GetType() == typeof(char))//2���Z�q�������Ă���Ƃ���
                {
                    if (list[i + 2].GetType() != typeof(double))
                        return double.NaN;//2���������Ƃ̂�����l�łȂ��Ƃ���NaN��Ԃ�

                    if ((char)list[i + 1] == '-')// '-'�������畄����ς���
                        list[i + 2] = -(double)list[i + 2];
                    else if ((char)list[i + 1] != '+')// '-'�ł�'+'�ł��Ȃ��Ƃ���NaN��������
                        return double.NaN;
                    list.RemoveAt(i + 1);
                }
            }

        //�����܂łŐ��l�͂��Ȃ炸���Z�q��ɋ��܂��悤�ɂȂ��Ă���͂��Ȃ̂Ń`�F�b�N
        //list�̃J�E���g����ŁA�������Ԗڂ����l�A������Z�q�ɂȂ�͂�
        if (list.Count % 2 != 1)
            return double.NaN;

        for (int i = 0; i < list.Count; i++)
            if ((i % 2 == 0 && list[i].GetType() != typeof(double)) && (i % 2 == 1 && list[i].GetType() != typeof(char)))
                return double.NaN;

        //�܂�^����납��T��
        for (int i = list.Count - 1; i >= 0; i--)
            if (list[i].GetType() == typeof(char) && (char)list[i] == '^')
            {
                double v = Math.Pow((double)list[i - 1], (double)list[i + 1]);
                list.RemoveRange(i, 2);
                list[i - 1] = v;
            }

        //����*��/��T��
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

        //�Ō��+��-��T��
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
