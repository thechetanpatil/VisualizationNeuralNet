using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;


namespace replayssopov
{
    public partial class ProperitiesDialog : Form
    {
        #region Initialization
        public ProperitiesDialog()
        {
            Text = "Data Properities";
            //            FormBorderStyle = FormBorderStyle.FixedDialog;
            ControlBox = true;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            BackColor = Color.LightBlue;


            InitializeComponent();

            // Установка начальных значений элементов управления
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
        }
        #endregion
        #region Methods
            #region Indicators
        public bool Indicator
        {
            set
            {
                radioButton1.Checked = value;
            }

            get
            {
                return radioButton1.Checked;
            }
        }
        public bool Indicator_Second
        {
            set
            {
                radioButton2.Checked = value;
            }

            get
            {
                return radioButton2.Checked;
            }
        }

        public bool Indicator_Third
        {
            set
            {
                radioButton3.Checked = value;
            }

            get
            {
                return radioButton3.Checked;
            }
        }
        #endregion
            #region maths methods
        // Задаём, получаем ошибку накладываемую на данные
        public double sError
        {
            set
            {
                percentOf.Text = value.ToString();
            }
            get
            {
                return double.Parse(percentOf.Text);
            }
        }
        // Задаём, получаем минимальный X
                 #region x2
        public int X0min
        {
            set
            {
                xXmin.Text = value.ToString();
            }
            get
            {
                return int.Parse(xXmin.Text);
            }
        }
        // Задаём, получаем максимальный X
        public int X0max
        {
            set
            {
                xXmax.Text = value.ToString();
            }
            get
            {
                return int.Parse(xXmax.Text);
            }
        }
        #endregion
                 #region sin1
        // Задаём, получаем минимальный X
        public int X1min
        {
            set
            {
                sinXmin.Text = value.ToString();
            }
            get
            {
                return int.Parse(sinXmin.Text);
            }
        }
        // Задаём, получаем максимальный X
        public int X1max
        {
            set
            {
                sinXmax.Text = value.ToString();
            }
            get
            {
                return int.Parse(sinXmax.Text);
            }
        }
        // Задаём, получаем число точек
        #endregion
                 #region sin2
        public int X2min
        {
            set
            {
                sin2Xmin.Text = value.ToString();
            }
            get
            {
                return int.Parse(sin2Xmin.Text);
            }
        }
        // Задаём, получаем максимальный X2
        public int X2max
        {
            set
            {
                sin2Xmax.Text = value.ToString();
            }
            get
            {
                return int.Parse(sin2Xmax.Text);
            }
        }
        // Задаём, получаем минимальный Y
        public int Ymin
        {
            set
            {
                sin2Ymin.Text = value.ToString();
            }
            get
            {
                return int.Parse(sin2Ymin.Text);
            }
        }
        // Задаём, получаем максимальный Y
        public int Ymax
        {
            set
            {
                sin2Ymax.Text = value.ToString();
            }
            get
            {
                return int.Parse(sin2Ymax.Text);
            }
        }
        #endregion
            #endregion
            public int allPoints
        {
            set
            {
                XPoints.Text = value.ToString();
            }
            get
            {
                return int.Parse(XPoints.Text);
            }
      
   
        }
        #endregion
        #region Buttons
            private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
