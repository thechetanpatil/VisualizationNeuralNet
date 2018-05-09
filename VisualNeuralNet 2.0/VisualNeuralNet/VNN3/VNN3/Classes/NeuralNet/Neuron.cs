using System;
using System.Collections.Generic;
using Tao.OpenGl;
using VNN.Structures;

namespace VNN.Classes.NeuralNet
{
    [Serializable]
    class Neuron
    {
        #region Declaration
        private Vector3D vector;
        public int id;
        public string identer;
        public List<Synapse> syn= new List<Synapse>();
        private string textid;
         [NonSerialized]
        private GLText WeightText;
        float color = 0.2f;
        float lastcolor;
        private double size;
        #endregion
        #region Constructor
        public Neuron(double m, float posz, float posx,string textid,int id)
        {
            this.identer = "ID" + id; ;
  
            this.id = id;
            this.textid = textid;
            size = m;
            vector.z = posz;
            vector.x = posx;
            vector.y = 2f;
            lastcolor = color;
        }
        #endregion
        #region Properties
        public float VectorX
        {
            get { return vector.x; }
            set { vector.x = value; }
        }
        public float Color
        {
            get { return color; }
            set { color = value; }
        }
        public Vector3D Vector
        {
            get { return vector; }
            set { vector = value; }
        }

        #endregion
        public void Draw()
        {

            Gl.glPushName(id);
            Gl.glColor3f(color, 0.0f, 0.0f);
            Gl.glPushMatrix();
            Gl.glTranslated(vector.x, vector.y, vector.z);
            Glu.GLUquadric pObj = Glu.gluNewQuadric();
            Glu.gluQuadricNormals(pObj, Glu.GLU_SMOOTH);

            Glu.gluSphere(pObj, Math.Exp(size) / 2.5, 12, 32);
            Glu.gluDeleteQuadric(pObj);

            Gl.glPopMatrix();
    
       
             Gl.glPopName();
             foreach (Synapse s in syn)
             {
                 s.Draw();
             }

             if (WeightText != null)
             {
                 WeightText.Draw();

             }
             Vector3D vectext2 = vector;
             vectext2.y = vectext2.y + 1.5f;
             GLText txt = new GLText("Neuron " + textid, vectext2);
            txt.Draw();
        }
        public void Select()
        {
            lastcolor = this.color;
            this.color = 1f;
            Vector3D vectext = vector;
            vectext.y = vectext.y + 2f;
            vectext.z = vectext.z - 1.5f;
            WeightText = new GLText("Weight: " + size.ToString("0.000"), vectext);
            WeightText.R = 0.3f;
            WeightText.G = 0.5f;
            WeightText.B = 0.5f;
            foreach (Synapse s in syn)
            {
   
     
                s.Select();
            
           
            }

        }
        public void DefaultColor()
        {
  
            this.color = lastcolor;
            WeightText = null;
            foreach (Synapse s in syn)
            {


                s.DefaultColor();


            }


        }

    }
}
    

