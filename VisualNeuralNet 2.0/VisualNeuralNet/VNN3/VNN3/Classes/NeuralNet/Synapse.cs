using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Tao.FreeGlut;
using VNN.Structures;

namespace VNN.Classes.NeuralNet
{
    [Serializable]
    struct MyColor
    {
        public float R, G, B;

    }
    [Serializable]
    class Synapse
    {
        Vector3D vec1;
        Vector3D vec2;
        static double radius = 0.05d;
        bool def = false;
        private MyColor color;
        private MyColor lastcolor;
        public bool isSelected=false;
        double height;

        public float ColorG
        {
            get { return color.G; }
            set
            {
                color.G = value;
 
            }
        }


        public float ColorB
        {
            get { return color.B; }
            set
            {
                color.B = value;

            }
        }
        public float ColorR
        {
            get { return color.R; }
            set
            {
                color.R = value;
            }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }
        double Fall;
        double Yaw;

        double[] angles = new double[3];


        public Synapse(Neuron n1, Neuron n2)
        {
            ColorR = 0.0f;
            ColorG = 0.0f;
            ColorB = 0.0f;

            vec1 = n1.Vector;
            vec2 = n2.Vector;
            Vector3D vec3 = vec2 - vec1;
            height = Math.Sqrt(Math.Pow(vec3.x, 2) + Math.Pow(vec3.y, 2) + Math.Pow(vec3.z, 2)); // высота цилиндра
            Fall = DegFromRad(Math.Acos(vec3.z / height));
            Yaw = DegFromRad(PolarAngle(vec3.x, vec3.y));
        }
        public void Draw()
        {
          
            Gl.glColor3f(color.R, color.G, color.B);
            Gl.glPushMatrix();
            Gl.glTranslated(vec1.x, vec1.y, vec1.z);
            Gl.glRotated(Yaw, 0, 0, 1); // преобразование координат № 2
            Gl.glRotated(Fall, 0, 1, 0); // преобразование координат № 1



            Glu.GLUquadric pObj = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(pObj, Glu.GLU_SMOOTH);
            Glu.gluCylinder(pObj, radius, radius, height, 15, 15);
            Glu.gluDeleteQuadric(pObj);

            Gl.glPopMatrix();

        }
        double DegFromRad(double Angle)
        {
            return (180 / Math.PI) * Angle;
        }
        double PolarAngle(double x, double y)
        {
            return Math.Atan2(y, x);
        }
        public void Select()
        {
            lastcolor = this.color;
            if (def == false)
            {
                def = true;
            }
            this.ColorB = 0.0f;
            this.ColorR = 1.0f;
            this.ColorG = 0.0f;

        }
        public void DefaultColor()
        {
       
           
            if (def == true)
            {
                this.color = lastcolor;
            }
        }
    }
}
