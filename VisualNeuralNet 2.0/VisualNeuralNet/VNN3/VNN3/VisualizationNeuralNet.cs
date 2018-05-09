using System.Collections.Generic;
using System.Windows.Forms;
using Tao.FreeGlut;
using VNN.Classes.Scene;
using VNN.Properties;
using VNN.Structures;


namespace VNN
{
    public class VisualizationNeuralNet
    {
        private Scene _scene;

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
            if (!IsShowing)
                _scene.MainForm.ShowDialog();
        }
        public void Load(string fileName)
        {
            if (!IsShowing)
            {
                _scene = new Scene();
                _scene.SetupScene(_scene.MainForm.GetViewPortValues());
                _scene.LoadNeuralNet(fileName);
            }
            else
            {
                MessageBox.Show(Resources.VisualNeuralNet_Load_ClosePreviousNeuralNet);
            }
        }
        public void SetNeuralNet(List<NeuronsLayer> customLayers)
        {
            if (!IsShowing)
            {
                _scene = new Scene(customLayers);
                _scene.SetupScene(_scene.MainForm.GetViewPortValues());
            }
            else
            {
                MessageBox.Show(Resources.VisualNeuralNet_Load_ClosePreviousNeuralNet);
            }
        }
        public bool IsShowing => _scene.MainForm.Showing();

        private static void GlutInit()
        {
            Glut.glutInit();
            Glut.glutInitDisplayMode(Glut.GLUT_RGB | Glut.GLUT_DOUBLE | Glut.GLUT_DEPTH);
        }

    }
}
