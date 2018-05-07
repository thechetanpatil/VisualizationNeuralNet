using System;
using System.Collections.Generic;
using Tao.OpenGl;
using VNN.NScene;
using VNN.NSceneObject;
using VNN.Structures;

namespace VNN.Classes.Scene
{
    public class Scene
    {
        private readonly Camera _camera;
        public VisualForm MainForm;
        private readonly List<ISceneObject> _sceneObjects;
        private const uint BufferLength = 64;
        uint[] _selectBuff;//буфер для выбора
        public int IndexNn;


        public Scene(List<NeuronsLayer> customLayers)
        {
            MainForm = new VisualForm(this);
            _camera = new Camera();
            _sceneObjects = new List<ISceneObject>();
            ISceneObject scspace = new SceneSpace();
            _sceneObjects.Add(scspace);
            ISceneObject neuralnet = new NeuralNet.NeuralNet(customLayers);
            _sceneObjects.Add(neuralnet);
            IndexNn = _sceneObjects.Count - 1;
            _camera.Look();
            SetupScene(MainForm.GetViewPortValues());
        }
        public Scene()
        {
            MainForm = new VisualForm(this);
            _sceneObjects = new List<ISceneObject>();
            ISceneObject scspace = new SceneSpace();
            _sceneObjects.Add(scspace);
            IndexNn = _sceneObjects.Count - 1;
            _camera.Look();
            SetupScene(MainForm.GetViewPortValues());
        }
        public void AddSceneObject(ISceneObject obj)
        {
            _sceneObjects.Add(obj);
        }
        public void Draw()
        {

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            //Glu.gluOrtho2D(0.0, (double)viewport.Width, (double)viewport.Height, 0.0);
            Gl.glLoadIdentity();
            Gl.glColor3i(255, 0, 0);
            _camera.Look(); //Обновляем взгляд камеры
            foreach (ISceneObject obj in _sceneObjects)
            {
                obj.Draw();
            }
            MainForm.TaoControl.Invalidate();
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

            foreach (ISceneObject so in _sceneObjects)
            {
                if (so.GetType().ToString().Equals("VNN.Classes.NeuralNet.NeuralNet"))
                {
                    NeuralNet.NeuralNet g = (NeuralNet.NeuralNet)so;
                    g.Save(fileName);
                    break;
                }
            }
        }
        public void LoadNeuralNet(string fileName)
        {

            foreach (ISceneObject so in _sceneObjects)
            {
                if (so.GetType().ToString().Equals("VNN.Classes.NeuralNet.NeuralNet"))
                {
                    _sceneObjects.Remove(so);
                    break;
                }
            }
            ISceneObject neuralnet2 = NeuralNet.NeuralNet.Load(fileName);
            _sceneObjects.Add(neuralnet2);
            IndexNn = _sceneObjects.Count - 1;
        }

        public bool ProcessSelection(int xPos, int yPos)
        {
            // Space for selection buffer
            _selectBuff = new uint[BufferLength];

            // Hit counter and viewport storage
            int[] viewport = new int[4];

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
            // mouse cursor point (xPos, yPos) and extending two pixels
            // in the vertical and horizontal direction
            Gl.glLoadIdentity();
            Glu.gluPickMatrix(xPos, viewport[3] - yPos, 2, 2, viewport);

            // Apply perspective matrix 
            var fAspect = viewport[2] / (float)viewport[3];
            Glu.gluPerspective(45.0f, fAspect, 1.0, 425.0);

            // Draw the scene
            ReDraw();

            // Collect the hits
            Gl.glRenderMode(Gl.GL_RENDER);
            NeuralNet.NeuralNet net = _sceneObjects[IndexNn] as NeuralNet.NeuralNet;
            bool h = false;


            // If a single hit occurred, display the info.

            if (net != null && net.Selectobj(_selectBuff[3], xPos, yPos))
            {
                net.CurrentID = Convert.ToInt32(_selectBuff[3]);
                h = true;
            }
            else
            {
                if (net != null) net.CurrentID = -1;
            }


            // Restore the projection matrix
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glPopMatrix();

            // Go back to modelview for normal rendering
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            return h;
        }

        private void ReDraw()
        {
            // Clear the window with current clearing color
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            // Save the matrix state and do the rotations
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glPushMatrix();
            _camera.Look(); //Обновляем взгляд камеры
            Gl.glPopMatrix();

            foreach (ISceneObject obj in _sceneObjects)
            {
                obj.Draw();
            }

            Gl.glPopMatrix();
            MainForm.TaoControl.Invalidate();
        }



        public List<ISceneObject> GetMass()
        {
            return _sceneObjects;
        }

    }
}
