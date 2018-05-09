using System;
using System.Collections.Generic;
using System.Text;
using Tao.OpenGl;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using VNN.Classes.Scene;
using VNN.Structures;

namespace VNN.Classes.NeuralNet
{
    [Serializable]
    class NeuralNet : ISceneObject
    {
        List<Layer> massLayers;
        List<Synapse> massSynapses;
        private int _currentId;

        public int CurrentId
        {
            get => _currentId;
            set => _currentId = value;
        }
        public NeuralNet()
        {

        }
        public NeuralNet(List<NeuronsLayer> layers)
        {
            CurrentId = -1;
            massLayers = new List<Layer>();
            massSynapses = new List<Synapse>();

            int id = 5;
            int layid = 2 ;
            float sizegrid = 26;
            float posz = sizegrid / 2;
            float min = posz;
            float max = 0 - posz;
            float itemlength = sizegrid / (layers.Count + 1);
            var nlinput = new NeuronsLayer {Neurons = new[] {0.5}};
            Layer input = new Layer(nlinput, min,"Input",ref id);
            input.MassNeurons[0].Color = 1f;
            massLayers.Add(input);
            posz = posz - itemlength;
            id++;
            foreach (var nl in layers)
            {

                Layer l = new Layer(nl, posz, layid.ToString(),ref id);
                massLayers.Add(l);
                posz = posz - itemlength;
                layid++;
            }

            var nloutput = new NeuronsLayer {Neurons = new[] {0.5}};
            var output = new Layer(nloutput, max, "Output", ref id);
            output.MassNeurons[0].Color = 0.1f;
            massLayers.Add(output);
            for (int i = 0; i < massLayers.Count - 1; i++)
            {
                CreateSynapses(massLayers[i], massLayers[i + 1]);
            }



        }
        public void CreateSynapses(Layer l1, Layer l2)
        {

            double maxlen = 0;
            double minlen = 0;
            for (int i = 0; i < l1.MassNeurons.Count; i++)
            {
                foreach (Neuron n in l2.MassNeurons)
                {
                    Synapse syn = new Synapse(l1.MassNeurons[i], n);
                    if (syn.Height > maxlen)
                    {
                        if (maxlen == minlen)
                            minlen = syn.Height;
                        maxlen = syn.Height;

                    }
                    else
                    {
                        if (syn.Height < minlen)
                            minlen = syn.Height;
                    }
                    l1.MassNeurons[i].syn.Add(syn);
                    n.syn.Add(syn);
                    massSynapses.Add(syn);
                }

            }
            foreach (Synapse syn in massSynapses)
            {

                float col = (float)((syn.Height - minlen) / (maxlen - minlen));
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
        public bool Selectobj(uint id, int xpos, int ypos)
        {
            bool gg = false;
            int indexselected = -1;
            int lc = 0;
            int selectlayer=-1;
            if (Convert.ToInt32(id) != CurrentId)
            {

                foreach (Layer mass in massLayers)
                {

                    for (int i = 1; i < mass.MassNeurons.Count + 1; i++)
                    {


                        if (id == (uint)mass.MassNeurons[i - 1].id)
                        {
                            selectlayer = lc;
                            _currentId = Convert.ToInt32(id);
                            indexselected = i - 1;

                        }
                        mass.MassNeurons[i - 1].DefaultColor();
                    }
                    lc++;
                }
                if (indexselected != -1)
                {
                    massLayers[selectlayer].MassNeurons[indexselected].Select();
                    gg = true;
                }
                
            }
            return gg;
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

            foreach (Layer l in massLayers)
            {
                l.Draw();
            }


        }
        public void Save(string fileName)
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            // Сохранить объект в локальном файле.
            using (Stream fStream = new FileStream(fileName,
               FileMode.Create, FileAccess.Write, FileShare.None))
            {
                binFormat.Serialize(fStream, this);
            }
        }
        public static NeuralNet Load(string fileName)
        {
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            NeuralNet network = Load(stream);
            stream.Close();

            return network;
        }
        private static NeuralNet Load(Stream stream)
        {
            IFormatter formatter = new BinaryFormatter();
            NeuralNet network = (NeuralNet)formatter.Deserialize(stream);
            return network;
        }



    }
}
