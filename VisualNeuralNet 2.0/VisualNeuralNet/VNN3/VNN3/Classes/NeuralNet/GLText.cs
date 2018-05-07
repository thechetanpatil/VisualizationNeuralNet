using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Tao.FreeGlut;
using VNN.Structures;

namespace VNN.Classes.NeuralNet
{
   
    class GLText
    {
        private string text;
        Vector3D vec;
        float r, g, b;

        public float B
        {
            get { return b; }
            set { b = value; }
        }

        public float G
        {
            get { return g; }
            set { g = value; }
        }

        public float R
        {
            get { return r; }
            set { r = value; }
        }
        public GLText(string text,Vector3D vec)
        {
            this.text = text;
            this.vec=vec;
        }
        public void Draw()
        {
            Gl.glColor3f(R, G, B);
            Gl.glRasterPos3f(vec.x, vec.y, vec.z);
            for (int i = 0; i < text.Count(); i++)
            {
                Glut.glutBitmapCharacter(Glut.GLUT_BITMAP_TIMES_ROMAN_24, (int)text[i]);
            }

        }

    }
}
