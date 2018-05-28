using System;
using System.Windows.Forms;
using VNN.Classes;
using VNN.Classes.Scene;
using VNN.Interfaces;

namespace VNN
{

    public partial class MainView : Form, IMainView
    {
        #region Declaration
        private readonly Scene _scene;
        private delegate DialogResult ShowSaveFileDialogInvoker();
        #endregion
        public MainView(Scene scene)
        {
            InitializeComponent();
            TaoControlInit();
            Mouse.SetViewport(TaoControl);
            _scene = scene;
        }
        #region INIT
        private void TaoControlInit()
        {
            TaoControl.InitializeContexts();
            TaoControl.Width = Width - 42;
            TaoControl.Height = Height - 60;
        }
        #endregion


        public void UpdateView()
        {
            TaoControl.Invalidate();
        }

        public int[] GetViewPortValues()
        {
            return new[] { TaoControl.Height, TaoControl.Width };
        }

        public void ShowView()
        {
            ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _scene.Render();
        }

        private void FVisual_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void FVisual_Resize(object sender, EventArgs e)
        {
            RefreshViewport();
        }
        private void RefreshViewport()
        {
            TaoControl.Width = Width - 42;
            TaoControl.Height = Height - 60;
            _scene.ReViewportScene(GetViewPortValues());
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = @"*.vnn";
            saveFileDialog1.Filter = @"VNN Files | *.vnn";
            saveFileDialog1.Title = @"Сохранение нейронной сети ";

            ShowSaveFileDialogInvoker invoker = saveFileDialog1.ShowDialog;
            if (Invoke(invoker).Equals(DialogResult.OK))
                _scene.SaveNeuralNet(saveFileDialog1.FileName);

        }

        private void TaoControl_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse.FreezeCam = _scene.ProcessSelection(e.X, e.Y);
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //OpenFileDialog dlg = new OpenFileDialog();
            //dlg.DefaultExt = "*.vnn";
            //dlg.Filter = "VNN Files | *.vnn";
            //dlg.Title = "Открытие нейронной сети ";
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    _scene.LoadNeuralNet(dlg.FileName);
            //}
        }

        private void VisualForm_Shown(object sender, EventArgs e)
        {
                TaoControl.Invalidate();
        }

    }
}
