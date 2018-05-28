using System;
using System.Collections.Generic;
using System.Linq;
using Tao.OpenGl;
using VNN.Interfaces;
using VNN.Structures;

namespace VNN.Classes.Scene
{
    public class Scene 
    {
        private readonly Camera _camera;
        public IMainView MainView;
        private List<ISceneObject> _sceneObjects;
        private const uint BufferLength = 64;
        private uint[] _selectBuff; //буфер для выбора

        public Scene(List<NeuronsLayer> customLayers)
        {
            _camera = new Camera();
            ConfigurationSceneObjects(
                new List<ISceneObject> { new SceneSpace(), new NeuralNetModels.NeuralNet(customLayers) });
        }
        public Scene()
        {
            _camera = new Camera();
            ConfigurationSceneObjects(new List<ISceneObject> { new SceneSpace() });
        }

        public bool Showing => ((MainView) MainView).Visible;

        public void AddSceneObject(ISceneObject obj)
        {
            _sceneObjects.Add(obj);
        }
        public void Draw()
        {

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glLoadIdentity();
            Gl.glColor3i(255, 0, 0);
            _camera.Look(); //обновляем взгляд камеры
            foreach (var obj in _sceneObjects)
                obj.Draw();
 
            MainView.UpdateView();

        }
        public void Render()
        {
            MouseEvents();
            Draw();
        }
        public void SetupScene(int[] viewport)
        {
            int height = viewport[0];
            int width = viewport[1];

            Gl.glClearColor(255, 255, 255, 1);
            Glu.gluOrtho2D(0.0, width, height, 0.0);
            Gl.glViewport(0, 0, width, height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Glu.gluPerspective(45, width / (float)height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);

            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glLineWidth(1.0f);
        }
        public void ReViewportScene(int[] viewport)
        {
            int height = viewport[0];
            int width = viewport[1];
            Gl.glClearColor(255, 255, 255, 1);

            Gl.glViewport(0, 0, width, height);

            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            Glu.gluPerspective(45, (float)width / height, 0.1, 200);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);

            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
            Gl.glHint(Gl.GL_LINE_SMOOTH_HINT, Gl.GL_NICEST);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glEnable(Gl.GL_LINE_SMOOTH);
            Gl.glLineWidth(1.0f);
        }
        public void MouseEvents()
        {
            _camera.ActionCamera();
            _camera.SelectAction();
        }
        public void SaveNeuralNet(string fileName)
        {
            foreach (var sceneObject in _sceneObjects)
            {
                if (!sceneObject.GetType().ToString().Equals("VNN.Classes.NeuralNet.NeuralNet")) continue;
                var g = (NeuralNetModels.NeuralNet)sceneObject;
                g.Save(fileName);
                break;
            }
        }
        public bool ProcessSelection(int positionAxisX, int positionAxisY)
        {
            // Space for selection buffer
            _selectBuff = new uint[BufferLength];

            // Hit counter and viewport storage
            var viewport = new int[4];

            // Setup selection buffer
            Gl.glSelectBuffer(64, _selectBuff);

            // Get the viewport
            Gl.glGetIntegerv(Gl.GL_VIEWPORT, viewport);

            // Switch to projection and save the matrix
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPushMatrix();

            // Change render mode
            Gl.glRenderMode(Gl.GL_SELECT);

            // Establish new clipping volume to be unit cube around
            // mouse cursor point (positionAxisX, yPos) and extending two pixels
            // in the vertical and horizontal direction
            Gl.glLoadIdentity();
            Glu.gluPickMatrix(positionAxisX, viewport[3] - positionAxisY, 2, 2, viewport);

            // Apply perspective matrix 
            var fAspect = viewport[2] / (float)viewport[3];
            Glu.gluPerspective(45.0f, fAspect, 1.0, 425.0);

            // Draw the scene
            ReDraw();

            // Collect the hits
            Gl.glRenderMode(Gl.GL_RENDER);
            var net =(NeuralNetModels.NeuralNet) 
                _sceneObjects.FirstOrDefault(s => s.GetType().Name == nameof(NeuralNetModels.NeuralNet));

            // If a single hit occurred, display the info.
            var isSelected = false;
            if (net != null && net.Selectobj(Convert.ToInt32(_selectBuff[3]), positionAxisX, positionAxisY))
            {
                net.CurrentId = Convert.ToInt32(_selectBuff[3]);
                isSelected = true;
            }
            else
            {
                if (net != null) net.CurrentId = -1;
            }


            // Restore the projection matrix
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPopMatrix();

            // Go back to modelview for normal rendering
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            return isSelected;
        }
        private void ReDraw()
        {
            // Clear the window with current clearing color
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // Save the matrix state and do the rotations
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPushMatrix();
            _camera.Look(); 
            Gl.glPopMatrix();

            foreach (var sceneObject in _sceneObjects)
                sceneObject.Draw();

            Gl.glPopMatrix();
            MainView.UpdateView();
        }


        private void ConfigurationSceneObjects(List<ISceneObject> sceneObjects)
        {
            MainView = new MainView(this);
            _sceneObjects = sceneObjects;

            _camera.Look();
            SetupScene(MainView.GetViewPortValues());
        }

        public void Show()
        {
            MainView.ShowView();
        }

        public int[] GetViewPort()
        {
           return MainView.GetViewPortValues();
        }
    }
}
