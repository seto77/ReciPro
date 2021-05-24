using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Crystallography;

namespace Crystallography.Controls
{
    public partial class NumericTextBox : UserControl
    {

        public delegate void MyEventHandler(object sender, EventArgs e);
        public event MyEventHandler ValueChanged;

        public NumericTextBox()
        {
            InitializeComponent();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13 && ModifierKeys == Keys.Shift) || (e.KeyChar == 10 && ModifierKeys == Keys.Control))
                e.Handled = true;
            
      }

        private bool skipTextChangeEvent = false;//テキストチェンジイベント自体をキャンセルする　
        private bool renewValue = true;//イベントはキャンセルしないが、numericalValueは変更しない
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (skipTextChangeEvent)
                return;
            try
            {
             
                int count = 0;
                int selectionLine = 0;
                for (int i = 0; i < textBox.Lines.Length; i++)
                {
                    count += textBox.Lines[i].Length + 2;
                    if (count > textBox.SelectionStart)
                    {
                        selectionLine = i;
                        break;
                    }
                }
                string[] formula = new string[selectionLine + 1];
                Array.Copy(textBox.Lines, formula, selectionLine + 1);
                double d = NumericalFormula.GetNumetricValue(formula);
                if (!double.IsNaN(d))
                {
                    if (d != this.numericalValue)
                    {
                        if(renewValue)
                            this.numericalValue = d;//NumericalValueを外部から変更された時は値を変更しない
                        ValueChanged?.Invoke(this, e);
                    }
                }
            }
            catch { }
        }

        
        private double  numericalValue= 0;
        /// <summary>
        /// コントロールが保持している値
        /// </summary>
        public double NumericalValue
        {
            set
            {
                renewValue = false;
                this.numericalValue = value;

                textBox.Text = GetString();

                renewValue = true;
            }
            get { return numericalValue; }
        }

        private int decimalPlaces = -1;
        /// <summary>
        /// 小数点以下の桁数
        /// </summary>
        public int DecimalPlaces
        {
            set
            {
                decimalPlaces = value;
                textBox.Text = GetString();
            }
            get
            {
                return decimalPlaces;
            }
        }

        /// <summary>
        /// 読み取り専用
        /// </summary>
        public bool ReadOnly
        {
            set { textBox.ReadOnly = value; }
            get { return textBox.ReadOnly; }
        }

        /// <summary>
        /// 複数行表示
        /// </summary>
        public bool Multiline
        {
            set { textBox.Multiline = value; }
            get { return textBox.Multiline; }
        }

        private bool showFraction = false;
        public bool ShowFraction
        {
            set { showFraction = value; }
            get { return showFraction; }
        }


        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control || e.Shift) && e.KeyCode == Keys.Return)
            {
                double d = 0;
                //現在のカーソル位置のテキストを計算する
                int count = 0;
                int selectionLine=0;
                for (int i = 0; i < textBox.Lines.Length; i++)
                {
                    count += textBox.Lines[i].Length + 2;
                    if (count > textBox.SelectionStart)
                    {
                        selectionLine = i;
                        break;
                    }
                }
                string[] formula = new string[selectionLine + 1];
                Array.Copy(textBox.Lines, formula, selectionLine + 1);
                d = NumericalFormula.GetNumetricValue(formula);
                if (!double.IsNaN(d))
                {
                    skipTextChangeEvent = true;
                    this.numericalValue = d;
                    if (textBox.Multiline)
                    {
                        if (textBox.Text.IndexOf("\r\n", textBox.SelectionStart) >= 0)
                            textBox.Text = textBox.Text.Remove(textBox.Text.IndexOf("\r\n", textBox.SelectionStart));

                        textBox.Text += "\r\n" + GetString();
                    }
                    else
                    {
                        textBox.Text = GetString();
                    }

                    this.numericalValue = d;
                    ValueChanged?.Invoke(this, e);

                    skipTextChangeEvent = false;
                    textBox.SelectionStart = textBox.Text.Length;
                }
                
            }
        }

        /// <summary>
        /// 現在のnumericalValueからテキストボックスの文字列を設定する
        /// </summary>
        /// <returns></returns>
        private string GetString()
        {
            string text = "";
            
            if (showFraction) //分数で表示するとき
            {
                int j = (int)Math.Ceiling(numericalValue-1);
                if (Math.Abs(numericalValue - j - 1.0 / 2.0) < 0.0000000001) text = (1 + 2 * j).ToString() + "/2";
                else if (Math.Abs(numericalValue - j - 1.0 / 3.0) < 0.0000000001) text = (1 + 3 * j).ToString() + "/3";
                else if (Math.Abs(numericalValue - j - 2.0 / 3.0) < 0.0000000001) text = (2 + 3 * j).ToString() + "/3";
                else if (Math.Abs(numericalValue - j - 1.0 / 4.0) < 0.0000000001) text = (1 + 4 * j).ToString() + "/4";
                else if (Math.Abs(numericalValue - j - 3.0 / 4.0) < 0.0000000001) text = (3 + 4 * j).ToString() + "/4";
                else if (Math.Abs(numericalValue - j - 1.0 / 6.0) < 0.0000000001) text = (1 + 6 * j).ToString() + "/6";
                else if (Math.Abs(numericalValue - j - 5.0 / 6.0) < 0.0000000001) text = (5 + 6 * j).ToString() + "/6";
                else if (Math.Abs(numericalValue - j - 1.0 / 12.0) < 0.0000000001) text = (1 + 12 * j).ToString() + "/12";
                else if (Math.Abs(numericalValue - j - 5.0 / 12.0) < 0.0000000001) text = (5 + 12 * j).ToString() + "/12";
                else if (Math.Abs(numericalValue - j - 7.0 / 12.0) < 0.0000000001) text = (7 + 12 * j).ToString() + "/12";
                else if (Math.Abs(numericalValue - j - 11.0 / 12.0) < 0.0000000001) text = (11 + 12 * j).ToString() + "/12";
            }
            
            if(text=="")
            {
                if (decimalPlaces >= 0)
                    text = numericalValue.ToString("f" + decimalPlaces.ToString());
                else
                    text = numericalValue.ToString();
            }
            return text;
        }



    }
}
