using System;
using System.Globalization;
using System.Windows.Forms;

namespace Dec2Hex
{
    public partial class Form1 : Form
    {

        private bool suppressed;
        public Form1()
        {
            InitializeComponent();
            toolTip1.SetToolTip(textBox1, "Decimal");
            toolTip1.SetToolTip(textBox2, "Hexadecimal");
            toolTip1.SetToolTip(textBox3, "Press <Enter> to add all current values to this log");
            toolTip1.SetToolTip(textBox4, "Binary");

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                textBox3.AppendText(textBox1.Text + " = 0x" + textBox2.Text + " = b'" + textBox4.Text + "'\n");
                e.SuppressKeyPress = true;
            }
        }

        private static int safeIntParse(string s)
        {
            int result;
            int.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
            return result;
        }

        private static int safeHexParse(string s)
        {
            int result;
            int.TryParse(s, NumberStyles.HexNumber, NumberFormatInfo.CurrentInfo, out result);
            return result;
        }

        private static int safeBinParse(string s)
        {
            int result;
            try
            {
                result = Convert.ToInt32(s, 2);
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!suppressed) ParseAll(textBox1);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (!suppressed) ParseAll(textBox2);
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (!suppressed) ParseAll(textBox4);
        }

        private void ParseAll(TextBox origin)
        {
            suppressed = true;
            if (origin == textBox1)
            {
                var val = safeIntParse(textBox1.Text);

                if (val > 65535)
                {
                    textBox2.Text = val.ToString("X8");
                    textBox4.Text = Convert.ToString(val, 2).PadLeft(32, '0');
                }
                else if (val > 255)
                {
                    textBox2.Text = val.ToString("X4");
                    textBox4.Text = Convert.ToString(val, 2).PadLeft(16, '0');
                }
                else
                {
                    textBox2.Text = val.ToString("X2");
                    textBox4.Text = Convert.ToString(val, 2).PadLeft(8, '0');
                }
            }
            else if (origin == textBox2)
            {
                var val = safeHexParse(textBox2.Text);

                if (val > 65535)
                {
                    textBox1.Text = val.ToString("D");
                    textBox4.Text = Convert.ToString(val, 2).PadLeft(32, '0');
                }
                else if (val > 255)
                {
                    textBox1.Text = val.ToString("D");
                    textBox4.Text = Convert.ToString(val, 2).PadLeft(16, '0');
                }
                else
                {
                    textBox1.Text = val.ToString("D");
                    textBox4.Text = Convert.ToString(val, 2).PadLeft(8, '0');
                }

            }
            else if (origin == textBox4)
            {
                var val = safeBinParse(textBox4.Text);

                if (val > 65535)
                {
                    textBox1.Text = val.ToString("D");
                    textBox2.Text = val.ToString("X8");
                }
                else if (val > 255)
                {
                    textBox1.Text = val.ToString("D");
                    textBox2.Text = val.ToString("X4");
                }
                else
                {
                    textBox1.Text = val.ToString("D");
                    textBox2.Text = val.ToString("X2");
                }

            }
            suppressed = false;
        }
    }
}


