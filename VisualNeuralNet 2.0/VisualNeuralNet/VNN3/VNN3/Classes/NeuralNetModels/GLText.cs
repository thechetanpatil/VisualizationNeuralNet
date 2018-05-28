using Tao.FreeGlut;
using Tao.OpenGl;
using VNN.Structures;

namespace VNN.Classes.NeuralNetModels
{
   
    public class GlText
    {
        private readonly string _text;
        private Vector3D _vector;
        private GlColor _color;

        public GlText(string text, Vector3D vector, GlColor color)
        {
            _text = text;
            _vector=vector;
            _color = color;
        }
        public GlText(string text, Vector3D vector)
        {
            _text = text;
            _vector = vector;
        }
        public void Draw()
        {
            Gl.glColor3f(_color.R, _color.G, _color.B);
            Gl.glRasterPos3f(_vector.x, _vector.y, _vector.z);
            for (int i = 0; i < _text.Length; i++)
            {
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_24, _text[i]);
            }

        }
        public struct  GlColor
        {
            public float R { get; set; }

            public float G { get; set; }

            public float B { get; set; }
        }
    }
}
