using System;
using Tao.OpenGl;
using VNN.Structures;

namespace VNN.Classes.NeuralNetModels
{
    [Serializable]
    internal struct MyColor
    {
        public float R, G, B;

    }
    [Serializable]
    internal class Synapse
    {
        private Vector3D _vector1;
        private const double Radius = 0.05d;
        private bool _isDefault;
        private MyColor _color;
        private MyColor _lastcolor;
        public bool IsSelected=false;
        private readonly double _fall;
        private readonly double _yaw;

        public double[] Angles { get; }

        public float ColorG
        {
            get => _color.G;
            set => _color.G = value;
        }

        public float ColorB
        {
            get => _color.B;
            set => _color.B = value;
        }
        public float ColorR
        {
            get => _color.R;
            set => _color.R = value;
        }

        public double Height { get; set; }

        public Synapse(Neuron n1, Neuron n2)
        {
            Angles = new double[3];
            ColorR = 0.0f;
            ColorG = 0.0f;
            ColorB = 0.0f;

            _vector1 = n1.Vector;
            Vector3D vector2 = n2.Vector;
            var vec3 = vector2 - _vector1;
            Height = Math.Sqrt(Math.Pow(vec3.x, 2) + Math.Pow(vec3.y, 2) + Math.Pow(vec3.z, 2)); // высота цилиндра
            _fall = DegFromRad(Math.Acos(vec3.z / Height));
            _yaw = DegFromRad(PolarAngle(vec3.x, vec3.y));
        }
        public void Draw()
        {
          
            Gl.glColor3f(_color.R, _color.G, _color.B);
            Gl.glPushMatrix();
            Gl.glTranslated(_vector1.x, _vector1.y, _vector1.z);
            Gl.glRotated(_yaw, 0, 0, 1); // преобразование координат № 2
            Gl.glRotated(_fall, 0, 1, 0); // преобразование координат № 1



            Glu.GLUquadric pObj = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(pObj, Glu.GLU_SMOOTH);
            Glu.gluCylinder(pObj, Radius, Radius, Height, 15, 15);
            Glu.gluDeleteQuadric(pObj);

            Gl.glPopMatrix();

        }
        private static double DegFromRad(double angle)
        {
            return (180 / Math.PI) * angle;
        }
        private static double PolarAngle(double x, double y)
        {
            return Math.Atan2(y, x);
        }
        public void Select()
        {
            _lastcolor = _color;
            if (_isDefault == false)
            {
                _isDefault = true;
            }
            ColorB = 0.0f;
            ColorR = 1.0f;
            ColorG = 0.0f;

        }
        public void DefaultColor()
        {      
            if (_isDefault)
                _color = _lastcolor;
        }
    }
}
