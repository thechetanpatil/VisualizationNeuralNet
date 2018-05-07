using System;
using System.Windows.Forms;
using VNN.Classes;
using VNN.Classes.Scene;

namespace VNN
{

    public partial class VisualForm : Form
    {
        #region Declaration
        private Scene scene;
        private bool showing;
        private delegate DialogResult ShowSaveFileDialogInvoker();
        #endregion
        public VisualForm(Scene scene)
        {

            InitializeComponent();
            TaoControlInit();
            Mouse.SetViewport(TaoControl);
            this.scene = scene;
        }
        #region INIT
        private void TaoControlInit()
        {
            TaoControl.InitializeContexts();
            TaoControl.Width = Width - 42;
            TaoControl.Height = Height - 60;
        }
        #endregion


        public int[] GetViewPortValues()
        {
            return new int[] { TaoControl.Height, TaoControl.Width };
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            scene.Render();
        }

        private void FVisual_Load(object sender, EventArgs e)
        {
            showing = true;
            timer1.Start();
        }

        private void FVisual_Resize(object sender, EventArgs e)
        {
            ReViewport(sender, e);
        }
        private void ReViewport(object sender, EventArgs e)
        {
            TaoControl.Width = this.Width - 42;
            TaoControl.Height = this.Height - 60;
            scene.ReViewportScene(GetViewPortValues());
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "*.vnn";
            saveFileDialog1.Filter = "VNN Files | *.vnn";
            saveFileDialog1.Title = "Сохранение нейронной сети ";

            ShowSaveFileDialogInvoker invoker = new ShowSaveFileDialogInvoker(saveFileDialog1.ShowDialog);
            if (this.Invoke(invoker).Equals(DialogResult.OK))
            {
                scene.SaveNeuralNet(saveFileDialog1.FileName);
            }

        }
        public bool Showing()
        {
            if (showing == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void FVisual_FormClosing(object sender, FormClosingEventArgs e)
        {
            showing = false;
        }

        private void TaoControl_MouseDown(object sender, MouseEventArgs e)
        {

            if (scene.ProcessSelection(e.X, e.Y) == true)
            {
                Mouse.FreezeCam = true;
            }
            else
            {
                Mouse.FreezeCam = false;
            }

        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "*.vnn";
            dlg.Filter = "VNN Files | *.vnn";
            dlg.Title = "Открытие нейронной сети ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                scene.LoadNeuralNet(dlg.FileName);
            }
        }



        //////////////end VisualForm
    }
}
