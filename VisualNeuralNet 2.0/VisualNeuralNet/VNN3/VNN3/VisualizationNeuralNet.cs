using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Tao.FreeGlut;
using VNN.Properties;
using VNN.Classes.Scene;
using VNN.Structures;


namespace VNN
{
    [Serializable]
    public class VisualizationNeuralNet
    {
        private Scene _scene;
        private bool _isShowing;
        public bool IsShowing
        {
            get => _isShowing;
            set
            {
                _isShowing = value;
                if (_scene != null && (bool)_scene?.Showing)
                    _isShowing = true;
                else
                    _isShowing = false;
            }
        }
        public VisualizationNeuralNet(List<NeuronsLayer> customLayers)
        {
            GlutInit();

            _scene = new Scene(customLayers);

        }

        public VisualizationNeuralNet()
        {
            GlutInit();

            _scene = new Scene();
        }
        public void ShowNeuralNet()
        {
            if (IsShowing) return;
            _scene.Show();
            _isShowing = true;
        }
        //public VisualizationNeuralNet(string fileName)
        //{
        //    GlutInit();
        //    if (!IsShowing)
        //    {
        //        var binFormat = new BinaryFormatter();
        //         Сохранить объект в локальном файле.
        //        using (Stream fStream = new FileStream(fileName,
        //            FileMode.Open, FileAccess.Write, FileShare.None))
        //        {
        //            = (VisualizationNeuralNet)binFormat.Deserialize(fStream);
        //        }

        //    }
        //    else
        //    {
        //        MessageBox.Show(Resources.VisualNeuralNet_Load_ClosePreviousNeuralNet);
        //    }
        //}
        public void SetNeuralNet(List<NeuronsLayer> customLayers)
        {
            if (!IsShowing)
            {
                _scene = new Scene(customLayers);
                _scene.SetupScene(_scene.GetViewPort());
            }
            else
            {
                MessageBox.Show(Resources.VisualNeuralNet_Load_ClosePreviousNeuralNet);
            }
        }


        private static void GlutInit()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
        }

    }
}
