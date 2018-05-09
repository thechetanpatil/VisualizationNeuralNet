using System;
using System.Collections.Generic;
using VNN.Structures;

namespace VNN.Classes.NeuralNet
{
    [Serializable]
    internal class Layer
    {

        public List<Neuron> MassNeurons { get; set; }

        public Layer(NeuronsLayer inputLayer, float positionOnAxisZ, string layid, ref int startid)
        {
            double laycount = inputLayer.Neurons.Length;
            double length = laycount * 2 + laycount;
            var lengthitem = (float)length / (float)laycount;
            double min = 0 - length / 2;
            double max = 0 + (length / 2);
            int neuronid = 1;
            float positionOnAxisX = 0;
            MassNeurons = new List<Neuron>();
            if (laycount == 1.0)
            {
                positionOnAxisX = 0;
                var n = new Neuron(inputLayer.Neurons[0], positionOnAxisZ, positionOnAxisX +
                    (float)inputLayer.Neurons[0], layid + '.' + neuronid, startid);

                MassNeurons.Add(n);
            }
            else
            {
                for (int i = 0; i < laycount; i++)
                {
                    float lastpos;
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
                    positionOnAxisX += lengthitem + 1;
                    var n = new Neuron(inputLayer.Neurons[i], positionOnAxisZ, lastpos, layid + '.' + neuronid, startid);
                    neuronid++;
                    startid++;
                    MassNeurons.Add(n);
                }
            }
        }

        public void Draw()
        {
            foreach (var n in MassNeurons)
            {
                n.Draw();
            }
        }
        public void DefaultColor()
        {
            foreach (var n in MassNeurons)
            {
                n.DefaultColor();
            }
        }

    }
}
