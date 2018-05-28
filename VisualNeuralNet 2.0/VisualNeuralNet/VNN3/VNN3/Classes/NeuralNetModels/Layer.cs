using System;
using System.Collections.Generic;
using VNN.Classes.Extensions;
using VNN.Structures;

namespace VNN.Classes.NeuralNetModels
{
    [Serializable]
    internal class Layer
    {
        public List<Neuron> Neurons { get; }

        public Layer(NeuronsLayer inputLayer, float positionOnAxisZ, string layid)
        {
            double laycount = inputLayer.Neurons.Length;
            double length = laycount * 2 + laycount;
            var lengthitem = (float)length / (float)laycount;
            double min = 0 - length / 2;
            double max = 0 + length / 2;
            int neuronid = 1;
            float positionOnAxisX = 0;
            Neurons = new List<Neuron>();
            if (laycount.AlmostEquals(1.0))
            {
                positionOnAxisX = 0;
                var n = new Neuron(inputLayer.Neurons[0], positionOnAxisZ, positionOnAxisX +
                    (float)inputLayer.Neurons[0], layid + '.' + neuronid, 0);

                Neurons.Add(n);
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
                        if ((laycount-1).AlmostEquals(i))
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
                    var n = new Neuron(inputLayer.Neurons[i], positionOnAxisZ, lastpos, layid + '.' + neuronid, 0);
                    neuronid++;
                    Neurons.Add(n);
                }
            }
        }

        public Layer()
        {
        }

        public Layer GetInputLayer(float positionZ)
        {
            return GetDefaultLayer(positionZ, "Input", 1f);
        }
        public Layer GetOutputLayer(float positionZ)
        {
            return GetDefaultLayer(positionZ, "Output", 0.1f);
        }
        private  Layer GetDefaultLayer(float positionZ, string infoText, float color)
        {
            var neuronsLayer = new NeuronsLayer { Neurons = new[] { 0.5 } };
            var layer = new Layer(neuronsLayer, positionZ, infoText);
            layer.Neurons[0].Color = color;
            return layer;
        }
        public void Draw()
        {
            foreach (var neuron in Neurons)
            {
                neuron.Draw();
            }
        }
    }
}
