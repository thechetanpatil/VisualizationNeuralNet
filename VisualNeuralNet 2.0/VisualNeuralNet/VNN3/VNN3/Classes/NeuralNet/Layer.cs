using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using VNN.Structures;

namespace VNN.Classes.NeuralNet
{
    [Serializable]
    class Layer 
    {
        List<Neuron> massNeurons;
        private string layid;
        public double maxlen=0;
         public  double minlen=0;
        float positionOnAxisZ;
        public Layer(NeuronsLayer lay, float positionOnAxisZ,string layid,ref int startid)
        {

             this.layid = layid;
            double length;
            this.positionOnAxisZ = positionOnAxisZ;
            massNeurons = new List<Neuron>();
            float positionOnAxisX = 0;
            double laycount = lay.neurons.Count();
            double centeritem = Convert.ToDouble(laycount / 2);
            length = laycount * 2 + laycount;
            float lengthitem = (float)length / (float)laycount;
            double min = 0 - length / 2;
            double max = 0 + (length / 2);
            int neuronid=1;
            if (laycount == 1.0)
            {
                positionOnAxisX =0;
                Neuron n = new Neuron(lay.neurons[0], positionOnAxisZ, positionOnAxisX + (float)lay.neurons[0],layid+'.'+neuronid,startid);

                massNeurons.Add(n);
            }
            else { 

            float lastpos = 0;

            for (int i = 0; i < laycount; i++)
            {

                if (i == 0)
                {
                    positionOnAxisX = (float)min;
                    lastpos = positionOnAxisX;
                }
                else
                {
                    if (i == (laycount - 1))
                    {
                        positionOnAxisX = (float)max;
                        lastpos = positionOnAxisX;
                    }
                    else
                    {
                        lastpos = positionOnAxisX;
                    }


                }
                positionOnAxisX += lengthitem+1;
                Neuron n = new Neuron(lay.neurons[i], positionOnAxisZ, lastpos,layid + '.' + neuronid,startid);
                neuronid++;
                startid++;
                massNeurons.Add(n);
            }
            }
        }
        internal List<Neuron> MassNeurons
        {
            get { return massNeurons; }
            set { massNeurons = value; }
        }
        public void Draw()
        {
            foreach (Neuron n in massNeurons)
            {
                n.Draw();
            }
        }
        public void DefaultColor()
        {
            foreach(Neuron n in massNeurons){
                n.DefaultColor();
            }
        }
        public  void SetColorSynapse(Synapse syn)
        {
             if (syn.Height > maxlen)
                    {
                        maxlen = syn.Height;
                      
                    }
                    else
                    {
                        if (syn.Height < minlen)
                            minlen = syn.Height;
                    }

                    float col = (float)((syn.Height - minlen) / (float)(maxlen - minlen));
                    if (col > 0.5)
                    {
                        syn.ColorB = col;
                        syn.ColorG = 1 - col;
                        syn.ColorR = syn.ColorG - syn.ColorB;
                    }

                    else
                    {
                        syn.ColorB = col;
                        syn.ColorG = 1 - col; ;
                        syn.ColorR = syn.ColorB - syn.ColorG;
                    }
        }


    }
}
