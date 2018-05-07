using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace replayssopov
{
    public partial class PropNetDlg : Form
    {
        #region INITIALIZATION
        #region Forminit
        public PropNetDlg()
        {
            BackColor = Color.LightBlue;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            InitializeComponent();
        }
        #endregion
        #endregion
        #region Getters/Setters
        #region Neuro Getters/Setters
        public int NumInp
        {
            set
            {
                NumberOfInputs.Text = value.ToString();
            }
            get
            {
                return int.Parse(NumberOfInputs.Text);
            }
        }
        public int NumOut
        {
            set
            {
                NumberOfOutputs.Text = value.ToString();
            }
            get
            {
                return int.Parse(NumberOfOutputs.Text);
            }
        }

        public int NumLayers
        {
            set
            {
                NumberOfLayers.Text = value.ToString();

            }
            get
            {


                return int.Parse(NumberOfLayers.Text);

            }
        }

        public int NumNeyrons
        {
            set
            {
                NumberOfNeyrons.Text = value.ToString();
            }
            get
            {
                return int.Parse(NumberOfNeyrons.Text);
            }
        }
        #endregion
        public bool RealStateOfProcess
        {
            set
            {
                NumberOfInputs.Enabled = value;
            }

            get
            {
                return NumberOfInputs.Enabled;
            }
        }
        public bool RealStateOfProcess_1
        {
            set
            {
                NumberOfOutputs.Enabled = value;
            }

            get
            {
                return NumberOfOutputs.Enabled;
            }
        }
        #endregion
        private void OK_button_Click(object sender, EventArgs e)
        {
            // Условия проверки правильности введенных параметров   
            if (NumLayers > 5)
            {
                MessageBox.Show("The advisable number of layers is 5");
                DialogResult = DialogResult.OK;
            }
            else
                if (NumLayers < 0)
                {
                    NumLayers = 1;
                    MessageBox.Show("Minimum number of layers 1!!!");
                }
                else
                    if (NumNeyrons > 50)
                    {
                        NumNeyrons = 50;
                        MessageBox.Show("Maximum number of neyrons 50!!!");
                    }
                    else
                        if (NumNeyrons < 0)
                        {
                            NumNeyrons = 1;
                            MessageBox.Show("Minimum number of neyrons 1!!!");
                        }

                        else
                            DialogResult = DialogResult.OK;
        }
    }
}
