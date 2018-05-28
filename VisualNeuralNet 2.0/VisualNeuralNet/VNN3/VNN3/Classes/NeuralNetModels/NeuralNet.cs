using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Tao.OpenGl;
using VNN.Classes.Extensions;
using VNN.Classes.Scene;
using VNN.Structures;

namespace VNN.Classes.NeuralNetModels
{
    [Serializable]
    internal class NeuralNet : ISceneObject
    {
        private readonly List<Layer> _layers;
        private readonly List<Synapse> _synapses;

        public int CurrentId { get; set; }

        public NeuralNet(IReadOnlyCollection<NeuronsLayer> layers)
        {
            CurrentId = -1;
            _layers = new List<Layer>();
            _synapses = new List<Synapse>();


            float sizeGrid = 26; //размер сетки
            float positionOnAxisZ = sizeGrid / 2;
            float max = 0 - positionOnAxisZ;

            float itemlength = sizeGrid / (layers.Count + 1);

            _layers.Add(new Layer().GetInputLayer(positionOnAxisZ));
            positionOnAxisZ = positionOnAxisZ - itemlength;

            var layerIndex = 1;
            foreach (var nl in layers)
            {
                _layers.Add(new Layer(nl, positionOnAxisZ, layerIndex.ToString()));
                positionOnAxisZ = positionOnAxisZ - itemlength;
                ++layerIndex;
            }

            _layers.Add(new Layer().GetOutputLayer(max));
            for (int i = 0; i < _layers.Count - 1; i++)
                CreateSynapses(_layers[i], _layers[i + 1]);

        }

        public void CreateSynapses(Layer layer1, Layer layer2)
        {

            double maxlen = 0;
            double minlen = 0;
            foreach (var neuronByLayer1 in layer1.Neurons)
            {
                foreach (var neuronByLayer2 in layer2.Neurons)
                {
                    var syn = new Synapse(neuronByLayer1, neuronByLayer2);
                    if (syn.Height > maxlen)
                    {
                        if (maxlen.AlmostEquals(minlen))
                            minlen = syn.Height;

                        maxlen = syn.Height;

                    }
                    else
                    {
                        if (syn.Height < minlen)
                            minlen = syn.Height;
                    }
                    neuronByLayer1.SynapsesList.Add(syn);
                    neuronByLayer2.SynapsesList.Add(syn);
                    _synapses.Add(syn);
                }
            }
            foreach (var syn in _synapses)
            {

                var col = (float)((syn.Height - minlen) / (maxlen - minlen));
                if (col > 0.5)
                {
                    syn.ColorG = col;
                    syn.ColorB = 1 - col;
                    syn.ColorR = syn.ColorG - syn.ColorB;
                }

                else
                {
                    syn.ColorB = col;
                    syn.ColorG = 1 - col;
                    syn.ColorR = syn.ColorB - syn.ColorG;
                }

            }
        }
        public bool Selectobj(int id, int xpos, int ypos)
        {
            var indexselected = -1;
            var lc = 0;
            var selectlayer = -1;
            if (Convert.ToInt32(id) == CurrentId) return false;
            foreach (var layer in _layers)
            {

                for (int i = 1; i < layer.Neurons.Count + 1; i++)
                {
                    if (id == layer.Neurons[i - 1].Id)
                    {
                        selectlayer = lc;
                        CurrentId = Convert.ToInt32(id);
                        indexselected = i - 1;

                    }
                    layer.Neurons[i - 1].DefaultColor();
                }
                lc++;
            }

            if (indexselected == -1) return false;
            _layers[selectlayer].Neurons[indexselected].Select();
            return true;
        }
        public void Draw()
        {
            Gl.glInitNames();
            float[] color = { 1, 0, 0, 1 }; // красный цвет
            float[] shininess = { 90 };
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glMaterialfv(Gl.GL_FRONT_AND_BACK, Gl.GL_DIFFUSE, color); // цвет
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SPECULAR, color); // отраженный свет
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_SHININESS, shininess); // степень отраженного света

            _layers.ForEach(layer => layer.Draw());

        }

        public void Save(string fileName)
        {
            var binFormat = new BinaryFormatter();
            // Сохранить объект в локальном файле.
            using (Stream fStream = new FileStream(fileName,
               FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, this);
            }
        }

        public static NeuralNet Load(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            IFormatter formatter = new BinaryFormatter();
            var network = (NeuralNet)formatter.Deserialize(stream);
            stream.Close();

            return network;
        }

    }
}
