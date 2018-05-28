using System;
using System.Collections.Generic;
using Tao.OpenGl;
using VNN.Structures;

namespace VNN.Classes.NeuralNetModels
{
    [Serializable]
    internal class Neuron
    {
        #region Declaration
        private Vector3D _vector;
        public int Id;
        public string Identer;
        public List<Synapse> SynapsesList = new List<Synapse>();
        private readonly string _textid;
        [NonSerialized]
        private GlText _weightText;
        private float _color = 0.2f;
        float _lastcolor;
        private readonly double _size;
        #endregion
        #region Constructor
        public Neuron(double m, float posz, float posx, string textid, int id)
        {
            Identer = "ID" + id;

            Id = id;
            _textid = textid;
            _size = m;
            _vector.z = posz;
            _vector.x = posx;
            _vector.y = 2f;
            _lastcolor = _color;
        }
        #endregion
        #region Properties
        public float VectorX
        {
            get { return _vector.x; }
            set { _vector.x = value; }
        }
        public float Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public Vector3D Vector
        {
            get { return _vector; }
            set { _vector = value; }
        }

        #endregion
        public void Draw()
        {

            Gl.glPushName(Id);
            Gl.glColor3f(_color, 0.0f, 0.0f);
            Gl.glPushMatrix();
            Gl.glTranslated(_vector.x, _vector.y, _vector.z);
            Glu.GLUquadric pObj = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(pObj, Glu.GLU_SMOOTH);

            Glu.gluSphere(pObj, Math.Exp(_size) / 2.5, 12, 32);
            Glu.gluDeleteQuadric(pObj);

            Gl.glPopMatrix();


            Gl.glPopName();
            foreach (var s in SynapsesList)
                s.Draw();

            _weightText?.Draw();

            var vectext2 = _vector;
            vectext2.y = vectext2.y + 1.5f;
            var txt = new GlText("Neuron " + _textid, vectext2);
            txt.Draw();
        }
        public void Select()
        {
            _lastcolor = _color;
            _color = 1f;
            Vector3D vectext = _vector;
            vectext.y = vectext.y + 2f;
            vectext.z = vectext.z - 1.5f;
            var color= new GlText.GlColor {R = 0.3f, G = 0.5f, B = 0.5f};
            _weightText = new GlText("Weight: " + _size.ToString("0.000"), vectext, color);
            foreach (var s in SynapsesList)
                s.Select();

        }
        public void DefaultColor()
        {
            _color = _lastcolor;
            _weightText = null;
            foreach (var s in SynapsesList)
                s.DefaultColor();
        }

    }
}


