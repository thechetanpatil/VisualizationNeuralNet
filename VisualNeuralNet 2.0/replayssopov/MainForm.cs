using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AForge.Neuro;
using AForge.Neuro.Learning;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using VNN;
using VNN.Structures;

namespace replayssopov
{
    public partial class MainForm : Form
    {
        #region INITIALIZATION
        #region Declaration
        #region Arrays
        private double[][] _input;
        private double[][] _output;
        /// <summary>
        /// массив для хранения количество нейроннов в каждом слое, а последний элемент-количество выходных слоев
        /// </summary>
        private int[] _netParams;
        //
        private List<double[]> trainingListData = new List<double[]>();
        private List<double[]> trainingListOut = new List<double[]>();
        private List<double[]> testingListData = new List<double[]>();
        private List<double[]> testingListOut = new List<double[]>();
        private double[,] _solution;
        private double[][] _trainingData;
        private double[][] _trainingOut;
        private double[,] _testingData;
        private double[,] _testingOut;
        #endregion
        #region Fields
        #region Поля диалогов
        private ProperitiesDialog propdlg;
        private PropNetDlg PropNet;
        #endregion
        private Thread _newCountThread;
        private double _indicatorOfUsingData;
        private Random _rand;
        private double _minZ;
        private double _maxZ;
        private double _learnRate;
        private int _numOfEpoch;
        private ActivationNetwork _network;
        private double _error;
        private string _formatError;
        private int _lenOfData;
        public VisualizationNeuralNet VisualizationNeuralNet;
        #endregion
        #region delegate
        private delegate void SetTextCallback(System.Windows.Forms.Control control, string text);
        delegate void UpdatePlotCallback();
        delegate void UpdateParamPlotCallback(int a);

        #endregion
        #endregion
        #region FORMINIT

        public MainForm()
        {
            // Создание диалога параметров данных
            propdlg = new ProperitiesDialog();



            // Создание диалога параметров сети
            PropNet = new PropNetDlg();


            // Окрашивание в голубой цвет
            base.BackColor = Color.LightBlue;

            // Настройки размеров окна
            //this.Size = new System.Drawing.Size(800, 388);
            // MinimumSize = Size;
            // MaximumSize = MinimumSize;
            InitializeComponent();
        }


        #endregion
        #endregion
        #region BUTTONS EVENTS
        private void StandardF_Click(object sender, EventArgs e)
        {

            if (_newCountThread != null)
            {
                _newCountThread.Abort();
                //GraphClear();
            }
            #region Формирование выборки
            ///формируется на основе выбранного варианта функции, количества точек и процента ошибки
            if (propdlg.ShowDialog() == DialogResult.OK)
            {

                label4.Text = "FinalError";
                label7.Visible = false;
                LearnErBox.Enabled= true;

                _indicatorOfUsingData = 0;

                // Если в диалоге был выбрал первый вариант
                if (propdlg.Indicator)
                {
                    PropNet.NumInp = 1;

                    // Вызываем функцию, формирующую выборку
                    make_data_X_2(propdlg.X0min, propdlg.X0max, propdlg.allPoints, propdlg.sError);

                    // Активируем флажок исполнения реального времени график
                    chart1.Visible = true;
                    checkBoxRTi.Visible = true;
                }

                else if (propdlg.Indicator_Second)
                {
                    PropNet.NumInp = 1;
                    // Вызываем функцию, формирующую выборку
                    make_data_sin_1(propdlg.X1min, propdlg.X1max, propdlg.allPoints, propdlg.sError);

                    // Активируем флажок исполнения реального времени график
                    chart1.Visible = true;
                    checkBoxRTi.Visible = true;
                }
                else if (propdlg.Indicator_Third)
                {
                    chart1.Visible = true;
                    checkBoxRTi.Visible = false;
                    PropNet.NumInp = 2;
                    make_data_sin_1(propdlg.X2min, propdlg.X2max, propdlg.Ymin, propdlg.Ymax, propdlg.allPoints, propdlg.sError);

                    //Очистить график
                    chart1.Series["MyChart"].Points.Clear();
                    chart1.Series["PointChart"].Points.Clear();

                }
            }
            #endregion
            // Активизируем кнопку параметров сети
            PropertiesNet_button.Enabled = true;

            //
            PropNet.RealStateOfProcess = false;

            PropNet.RealStateOfProcess_1 = false;
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void PropertiesNet_button_Click(object sender, EventArgs e)
        {
            if (_newCountThread != null)
            {
                _newCountThread.Abort();
                //GraphClear();
            }



            if (PropNet.ShowDialog() == DialogResult.OK)
            {
                // Активируем кнопку Start
                Start_button.Enabled = true;

                // активировать поле ввода числа итераций
                textBox1.Enabled = true;

                // создание временного массива расширяемого размера
                List<int> temp_A = new List<int>();
                // заполнение этого массива параметрами сети
                for (int i = 0; i < PropNet.NumLayers; i++)
                {
                    temp_A.Add(PropNet.NumNeyrons);
                }
                temp_A.Add(PropNet.NumOut);

                _netParams = new int[temp_A.Count];

                for (int i = 0; i < temp_A.Count; i++)
                {
                    _netParams[i] = temp_A[i];
                }

            }
        }
        private void Start_button_Click(object sender, EventArgs e)
        {
            if (_newCountThread != null)
            {
                _newCountThread.Abort();
                //GraphClear();
            }

            try
            {
                _learnRate = Math.Min(1, Math.Max(0, double.Parse(textBox3.Text)));
                //                learnRate = double.Parse(textBox3.Text);
                _numOfEpoch = int.Parse(textBox1.Text);

                if (_numOfEpoch >= 1)
                {

                    if (propdlg.Indicator)
                    {

                        _newCountThread = new Thread(new ThreadStart(SearchSolution));
                        _newCountThread.Start();
                    }
                    else
                        if (propdlg.Indicator_Second)
                        {

                            _newCountThread = new Thread(new ThreadStart(SearchSolution_1));
                            _newCountThread.Start();
                        }
                        else
                            if (propdlg.Indicator_Third)
                            {
                                _newCountThread = new Thread(new ThreadStart(SearchSolution_2));
                                _newCountThread.Start();
                            }
                            else
                                if (_indicatorOfUsingData == 1)
                                {
                                    chart1.Visible = true;
                                    _newCountThread = new Thread(new ThreadStart(SearchSolution_Data));
                                    _newCountThread.Start();
                                }

                }
                else
                {
                    MessageBox.Show("The number of Epoch must be positive and greater than 1");
                    textBox1.Text = "";
                }


            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }

            button2.Visible = true;
        }
        #endregion
        #region MathMethods
        public void make_data_X_2(int Xmin, int Xmax, int PointsCount, double errorPercent)
        {
            _rand = new Random();

            _input = new double[PointsCount][];
            _output = new double[PointsCount][];
            //            output_copy = new double[PointsCount][];

            // Ищем центральную точку и отклонение от неё для входной переменной X
            int centrePoint_X = (Xmax + Xmin) / 2;
            int PartLength_X = (Xmax - Xmin) / 2;

            // Задаем максимально и минимально возможные хранимые значения в типе double
            double minY = 0;
            double maxY = 0;
            if (Math.Abs(Xmax) >= Math.Abs(Xmin))
            {
                maxY = Math.Pow(Xmax, 2);
            }
            else
            {
                maxY = Math.Pow(Xmin, 2);
            }

            for (int i = 0; i < PointsCount; i++)
            {
                _input[i] = new double[1];
                _output[i] = new double[1];

                // МЕДЛЕННЫЙ КОД! НУЖНО ПЕРЕПИСАТЬ!
                _input[i][0] = Xmin + (double)i * (double)(Xmax - Xmin) / (double)(PointsCount - 1);
                _output[i][0] = Math.Pow(_input[i][0], 2);
            }

            if (errorPercent != 0)
            {
                for (int i = 0; i < PointsCount; i++)
                {
                    _output[i][0] += _output[i][0] * (_rand.NextDouble() / errorPercent);
                }
            }

            for (int i = 0; i < propdlg.allPoints; i++)
            {

                _input[i][0] = (_input[i][0] - centrePoint_X) / PartLength_X;
                _output[i][0] = (_output[i][0] - minY) / (maxY - minY);
            }

        }
        public void make_data_sin_1(int Xmin, int Xmax, int PointsCount, double errorPercent)
        {
            _rand = new Random();

            _input = new double[PointsCount][];
            _output = new double[PointsCount][];
            //            output_copy = new double[PointsCount][];

            // Ищем центральную точку и отклонение от неё для входной переменной X
            int centrePoint_X = (propdlg.X1max + propdlg.X1min) / 2;
            int PartLength_X = (propdlg.X1max - propdlg.X1min) / 2;

            // Задаем максимально и минимально возможные хранимые значения в типе double
            double minY = -1;
            double maxY = 1;

            for (int i = 0; i < PointsCount; i++)
            {
                _input[i] = new double[1];
                _output[i] = new double[1];
                //                output_copy[i] = new double[1];



                // МЕДЛЕННЫЙ КОД! НУЖНО ПЕРЕПИСАТЬ!
                _input[i][0] = Xmin + (double)i * (double)(Xmax - Xmin) / (double)(PointsCount - 1);//rand.Next(Xmin, Xmax + 1);
                _output[i][0] = Math.Sin(_input[i][0]);
            }

            if (errorPercent != 0)
            {
                for (int i = 0; i < PointsCount; i++)
                {
                    _output[i][0] += _output[i][0] * (_rand.NextDouble() / errorPercent);
                }
            }



            for (int i = 0; i < propdlg.allPoints; i++)
            {

                _input[i][0] = (_input[i][0] - centrePoint_X) / PartLength_X;
                _output[i][0] = (_output[i][0] - minY) / (maxY - minY);
            }

        }

        public void make_data_sin_1(int Xmin, int Xmax, int Ymin, int Ymax, int PointsCount, double errorPercent)
        {
            _rand = new Random();

            int centrePoint_X = (Xmax + Xmin) / 2;
            int PartLength_X = (Xmax - Xmin) / 2;

            int centrePoint_Y = (Ymax + Ymin) / 2;
            int PartLength_Y = (Ymax - Ymin) / 2;

            _input = new double[PointsCount][];
            _output = new double[PointsCount][];


            for (int i = 0; i < PointsCount; i++)
            {
                // Выделяем память под входы и выходы
                _input[i] = new double[2];
                _output[i] = new double[1];

                // Формируем входы
                _input[i][0] = _input[i][0] = Xmin + (double)i * (double)(Xmax - Xmin) / (double)(PointsCount - 1);
                _input[i][1] = _input[i][0] = Ymin + (double)i * (double)(Ymax - Ymin) / (double)(PointsCount - 1);

                _output[i][0] = Math.Pow(_input[i][0], 2) * Math.Sin(_input[i][0]) + Math.Pow(_input[i][1], 2) * Math.Sin(_input[i][1]);
            }

            if (errorPercent != 0)
            {
                for (int i = 0; i < PointsCount; i++)
                {
                    _output[i][0] += _output[i][0] * (_rand.NextDouble() / errorPercent);
                }
            }


            // Вновь задаём максимум и минимум выхода, дабы не глобализовывать эти значения

            _minZ = Math.Pow(Xmin, 2) * (-1) + Math.Pow(Ymin, 2) * (-1);
            _maxZ = Math.Pow(Xmax, 2) + Math.Pow(Ymax, 2);

            for (int i = 0; i < propdlg.allPoints; i++)
            {
                _input[i][0] = (_input[i][0] - centrePoint_X) / PartLength_X;
                _input[i][1] = (_input[i][1] - centrePoint_Y) / PartLength_Y;

                _output[i][0] = (_output[i][0] - _minZ) / (_maxZ - _minZ);
            }

        }
        #endregion
        #region Solutions
        private void SearchSolution()
        {
            // Вспомогательные локальные переменные

            double[] networkInput = new double[1];
            _solution = new double[propdlg.allPoints, 2];
            double lenghtofX = (double)(propdlg.X0max - propdlg.X0min);
            double enotherLocalValue = (double)(propdlg.allPoints - 1);

            // Создание объекта класса сети 
            _network = new ActivationNetwork(new BipolarSigmoidFunction(2), PropNet.NumInp, _netParams);


            // Ищем центральную точку и отклонение от неё для входной переменной X
            int centrePoint_X = (propdlg.X0max + propdlg.X0min) / 2;
            int PartLength_X = (propdlg.X0max - propdlg.X0min) / 2;

            // Задаем максимально и минимально возможные хранимые значения в типе double
            double minY = 0;
            double maxY = 0;
            if (Math.Abs(propdlg.X0max) >= Math.Abs(propdlg.X0min))
            {
                maxY = Math.Pow(propdlg.X0max, 2);
            }
            else
            {
                maxY = Math.Pow(propdlg.X0min, 2);
            }


            BackPropagationLearning teacher = new BackPropagationLearning(_network);

            teacher.LearningRate = _learnRate;

            if (checkBoxRTi.Checked)
            {
                for (int i = 0; i < _numOfEpoch; i++)
                {
                    _error = teacher.RunEpoch(_input, _output);
                    SetText(LearnErBox, _formatError);
                    // вывод ошибки в реальном времени
                    SetText(CurrentItBox, (i + 1).ToString());

                    for (int j = 0; j < propdlg.allPoints; j++)
                    {
                        _solution[j, 0] = (double)propdlg.X0min + (double)j * (lenghtofX) / enotherLocalValue;
                    }

                    for (int j = 0; j < propdlg.allPoints; j++)
                    {
                        networkInput[0] = (_solution[j, 0] - centrePoint_X) / (double)PartLength_X;
                        _solution[j, 1] = (double)(maxY - minY) * (_network.Compute(networkInput)[0]) + minY;
                    }

                    // Важная функция, позволяющая вывод ошибки в реальном времени
                    _formatError = string.Format("{0:f4}", _error);
                    SetText(LearnErBox, _formatError);

                    // Важная функция, позволяющая вывод графика в реальном времени
                    UpdateXPlot();


                }
            }
            else
            {
                for (int i = 0; i < _numOfEpoch; i++)
                {
                    _error = teacher.RunEpoch(_input, _output) / 100;

                    // вывод ошибки в реальном времени
                    SetText(CurrentItBox, (i + 1).ToString());

                    //UpdatepBar();
                }
                for (int j = 0; j < propdlg.allPoints; j++)
                {
                    _solution[j, 0] = (double)propdlg.X0min + (double)j * (lenghtofX) / enotherLocalValue;
                }

                for (int j = 0; j < propdlg.allPoints; j++)
                {
                    networkInput[0] = (_solution[j, 0] - centrePoint_X) / (double)PartLength_X;
                    _solution[j, 1] = (double)(maxY - minY) * (_network.Compute(networkInput)[0]) + minY;
                }

                // Важная функция, позволяющая вывод ошибки в реальном времени
                _formatError = string.Format("{0:f4}", _error);
                SetText(LearnErBox, _formatError);

                // Важная функция, позволяющая вывод графика в реальном времени
                UpdateXPlot();

            }
            ShowNeuralNet();
        }


        private void SearchSolution_1()
        {
            // Вспомогательные локальные переменные

            double[] networkInput = new double[1];
            _solution = new double[propdlg.allPoints, 2];
            double lenghtofX = (double)(propdlg.X1max - propdlg.X1min);
            double enotherLocalValue = (double)(propdlg.allPoints - 1);

            // Создание объекта класса сети 
            _network = new ActivationNetwork(new BipolarSigmoidFunction(2), PropNet.NumInp, _netParams);

            // Ищем центральную точку и отклонение от неё для входной переменной X
            int centrePointX = (propdlg.X1max + propdlg.X1min) / 2;
            int partLengthX = (propdlg.X1max - propdlg.X1min) / 2;

            // Задаем максимально и минимально возможные хранимые значения в типе double
            double minY = -1;
            double maxY = 1;


            BackPropagationLearning teacher = new BackPropagationLearning(_network);

            teacher.LearningRate = _learnRate;

            if (checkBoxRTi.Checked)
            {
                for (int i = 0; i < _numOfEpoch; i++)
                {
                    _error = teacher.RunEpoch(_input, _output);

                    // вывод ошибки в реальном времени
                    SetText(CurrentItBox, (i + 1).ToString());

                    for (int j = 0; j < propdlg.allPoints; j++)
                    {
                        _solution[j, 0] = (double)propdlg.X1min + (double)j * (lenghtofX) / enotherLocalValue;
                    }

                    for (int j = 0; j < propdlg.allPoints; j++)
                    {
                        networkInput[0] = (_solution[j, 0] - centrePointX) / (double)partLengthX;
                        _solution[j, 1] = (double)(maxY - minY) * (_network.Compute(networkInput)[0]) + minY;
                    }

                    // Важная функция, позволяющая вывод ошибки в реальном времени
                    _formatError = string.Format("{0:f4}", _error);
                    SetText(LearnErBox, _formatError);

                    // Важная функция, позволяющая вывод графика в реальном времени
                    UpdatePlot();

                    //UpdatepBar();
                }
            }
            else
            {
                for (int i = 0; i < _numOfEpoch; i++)
                {
                    _error = teacher.RunEpoch(_input, _output) / 100;

                    // вывод ошибки в реальном времени
                    SetText(CurrentItBox, (i + 1).ToString());

                    //UpdatepBar();
                }
                for (int j = 0; j < propdlg.allPoints; j++)
                {
                    _solution[j, 0] = (double)propdlg.X1min + (double)j * (lenghtofX) / enotherLocalValue;
                }

                for (int j = 0; j < propdlg.allPoints; j++)
                {
                    networkInput[0] = (_solution[j, 0] - centrePointX) / (double)partLengthX;
                    _solution[j, 1] = (double)(maxY - minY) * (_network.Compute(networkInput)[0]) + minY;
                }

                // Важная функция, позволяющая вывод ошибки в реальном времени
                _formatError = string.Format("{0:f4}", _error);
                SetText(LearnErBox, _formatError);

                // Важная функция, позволяющая вывод графика в реальном времени
                UpdatePlot();

            }
            ShowNeuralNet();
        }
        private void SearchSolution_2()
        {
            // Вспомогательные локальные переменные

            double[] networkInput = new double[2];
            _solution = new double[propdlg.allPoints, 3];
            double lenghtofX = (double)(propdlg.X2max - propdlg.X2min);
            double lenghtofY = (double)(propdlg.Ymax - propdlg.Ymin);
            double enotherLocalValue = (double)(propdlg.allPoints - 1);

            // Создание объекта класса сети 
            _network = new ActivationNetwork(new BipolarSigmoidFunction(2), PropNet.NumInp, _netParams);

            // Ищем центральную точку и отклонение от неё для входной переменной X
            int centrePoint_X = (propdlg.X2max + propdlg.X2min) / 2;
            int PartLength_X = (propdlg.X2max - propdlg.X2min) / 2;

            int centrePoint_Y = (propdlg.Ymax + propdlg.Ymin) / 2;
            int PartLength_Y = (propdlg.Ymax - propdlg.Ymin) / 2;



            BackPropagationLearning teacher = new BackPropagationLearning(_network);

            teacher.LearningRate = _learnRate;

            for (int i = 0; i < _numOfEpoch; i++)
            {
                _error = teacher.RunEpoch(_input, _output);

                // вывод ошибки в реальном времени
                SetText(CurrentItBox, (i + 1).ToString());

            }
            for (int j = 0; j < propdlg.allPoints; j++)
            {
                _solution[j, 0] = (double)propdlg.X2min + (double)j * (lenghtofX) / enotherLocalValue;
                _solution[j, 1] = (double)propdlg.Ymin + (double)j * (lenghtofY) / enotherLocalValue;
            }

            for (int j = 0; j < propdlg.allPoints; j++)
            {
                networkInput[0] = _input[j][0];//(solution[j, 0] - centrePoint_X) / (double)PartLength_X;
                networkInput[1] = _input[j][1];//(solution[j, 1] - centrePoint_Y) / (double)PartLength_Y;
                _solution[j, 2] = (double)(_maxZ - _minZ) * (_network.Compute(networkInput)[0]) + _minZ;
            }

            // Важная функция, позволяющая вывод ошибки в реальном времени
            _formatError = string.Format("{0:f4}", _error);
            SetText(LearnErBox, _formatError);

            // Важная функция, позволяющая вывод графика в реальном времени
            UpdateSecondPlot(0);
            ShowNeuralNet();
        }
        private void SearchSolution_Data()
        {
            trainingListData.Clear();
            trainingListOut.Clear();
            testingListData.Clear();
            testingListOut.Clear();

            // Вспомогательные локальные переменные
            _lenOfData = 270;
            double[] networkInput = new double[27];

            Random rand = new Random();
            int randnum;
            //     solution = new double[270, 28];

            // необходимо изменить размерность
            _solution = new double[_lenOfData - 15, 2];
            _testingData = new double[15, 27];
            _testingOut = new double[15, 1];
            _trainingData = new double[_lenOfData - 15][];
            _trainingOut = new double[_lenOfData - 15][];



            // Создание объекта класса сети 
            _network = new ActivationNetwork(new BipolarSigmoidFunction(2), PropNet.NumInp, _netParams);

            BackPropagationLearning teacher = new BackPropagationLearning(_network);

            // этта коэффициент скорости обучения
            teacher.LearningRate = _learnRate;

            // перепишем исходные массивы входных и выходных данных в списки
            for (int i = 0; i < _lenOfData; i++)
            {
                trainingListData.Add(_input[i]);

                trainingListOut.Add(_output[i]);
            }

            // из массива input и output в список для формирования случайным образом тестовой и обучающей выборки

            for (int i = 0; i < 15; i++)
            {
                randnum = rand.Next(0, trainingListData.Count);

                testingListData.Add(trainingListData[randnum]);
                testingListOut.Add(trainingListOut[randnum]);

                trainingListData.RemoveAt(randnum);
                trainingListOut.RemoveAt(randnum);
            }

            // вновь перепишем в массив типа double[][] данные из списков
            for (int i = 0; i < _lenOfData - 15; i++)
            {
                _trainingData[i] = new double[27];
                _trainingOut[i] = new double[1];

                _trainingData[i] = trainingListData[i];
                _trainingOut[i] = trainingListOut[i];
            }

            // обучение сети
            for (int i = 0; i < _numOfEpoch; i++)
            {
                _error = teacher.RunEpoch(_trainingData, _trainingOut);

                // вывод ошибки в реальном времени
                SetText(CurrentItBox, (i + 1).ToString());
            }

            // расчет выходных значений сети после обучения для тестовой выборки
            for (int j = 0; j < 15; j++)
            {
                for (int i = 0; i < 27; i++)
                {
                    networkInput[i] = testingListData[j][i];
                }

                _solution[j, 1] = (_network.Compute(networkInput)[0]);
            }

            // расчет выходных значений сети после обучения для обучающей выборки
            for (int j = 0; j < _lenOfData - 15; j++)
            {
                for (int i = 0; i < 27; i++)
                {
                    networkInput[i] = trainingListData[j][i];
                }

                _solution[j, 0] = (_network.Compute(networkInput)[0]);
            }



            int Er = 0;
            // расчет ошибки по тестовой выборке
            for (int j = 0; j < 15; j++)
            {
                if ((Math.Abs(_solution[j, 1] - testingListOut[j][0]) >= 0.5))
                {
                    Er += 1;

                }
            }

            // потокобезопасность
            _formatError = string.Format("{0:f0}", Er);
            SetText(LearnErBox, _formatError);

            Er = 0;
            // расчет ошибки по обучающей выборке
            for (int j = 0; j < _lenOfData - 15; j++)
            {
                if ((Math.Abs(_solution[j, 0] - trainingListOut[j][0]) >= 0.5))
                {
                    Er += 1;

                }
            }

            // потокобезопасность
            _formatError = string.Format("{0:f0}", Er);
            SetText(LearnErBox, _formatError);

            // вывести результат
            UpdateDataPlot(0);



        }
        #endregion
        #region ForSolutions(Methods)
        private void SetText(System.Windows.Forms.Control control, string text)
        {
            if (control.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                Invoke(d, new object[] { control, text });
            }
            else
            {
                control.Text = text;
            }
        }
        private void UpdatePlot()
        {
            if (this.chart1.InvokeRequired)
            {
                UpdatePlotCallback d = new UpdatePlotCallback(UpdatePlot);
                this.Invoke(d, null);

            }
            else
            {
                ShowResult();
            }
        }
        private void UpdateXPlot()
        {
            if (this.chart1.InvokeRequired)
            {
                UpdatePlotCallback d = new UpdatePlotCallback(UpdateXPlot);
                this.Invoke(d, null);

            }
            else
            {
                ShowXResult();
            }
        }
        private void UpdateDataPlot(int a)
        {
            if (this.chart1.InvokeRequired)
            {
                UpdateParamPlotCallback d = new UpdateParamPlotCallback(UpdateDataPlot);
                this.Invoke(d, a);

            }
            else
            {
                ShowDataResult(a);
            }
        }
        private void UpdateSecondPlot(int a)
        {
            if (this.chart1.InvokeRequired)
            {
                UpdateParamPlotCallback d = new UpdateParamPlotCallback(UpdateSecondPlot);
                this.Invoke(d, a);

            }
            else
            {
                ShowSecondResult(0);
            }
        }
        #endregion
        #region ShowResult
        private void ShowDataResult(int a)
        {
            chart1.Series["MyChart"].ChartType = SeriesChartType.Point;
            chart1.Series["PointChart"].ChartType = SeriesChartType.Point;
            chart1.Series["MyChart"].MarkerStyle = MarkerStyle.Circle;
            chart1.Series["PointChart"].MarkerStyle = MarkerStyle.Circle;


            chart1.Series["MyChart"].Points.Clear();
            chart1.Series["PointChart"].Points.Clear();


            if (a == 0)
            {
                for (int pointIndex = 0; pointIndex < _lenOfData - 15; pointIndex++)
                {
                    chart1.Series["MyChart"].Points.AddXY(pointIndex, _solution[pointIndex, 0]);

                    chart1.Series["PointChart"].Points.AddXY(pointIndex, trainingListOut[pointIndex][0]);

                }
            }
            else
            {
                for (int pointIndex = 0; pointIndex < 15; pointIndex++)
                {
                    chart1.Series["MyChart"].Points.AddXY(pointIndex, _solution[pointIndex, 1]);

                    chart1.Series["PointChart"].Points.AddXY(pointIndex, testingListOut[pointIndex][0]);

                }
            }
        }
        private void ShowXResult()
        {
            chart1.Series["MyChart"].ChartType = SeriesChartType.Spline;
            chart1.Series["PointChart"].ChartType = SeriesChartType.Point;
            chart1.Series["PointChart"].MarkerStyle = MarkerStyle.Circle;


            chart1.Series["MyChart"].Points.Clear();
            chart1.Series["PointChart"].Points.Clear();


            for (int pointIndex = 0; pointIndex < propdlg.allPoints; pointIndex++)
            {
                chart1.Series["MyChart"].Points.AddXY(_solution[pointIndex, 0], _solution[pointIndex, 1]);

                chart1.Series["PointChart"].Points.AddXY(_solution[pointIndex, 0], Math.Pow(_solution[pointIndex, 0], 2));

            }
        }
        private void ShowSecondResult(int param)
        {

            chart1.Series["MyChart"].ChartType = SeriesChartType.Spline;
            chart1.Series["PointChart"].ChartType = SeriesChartType.Point;
            chart1.Series["PointChart"].MarkerStyle = MarkerStyle.Circle;


            chart1.Series["MyChart"].Points.Clear();
            chart1.Series["PointChart"].Points.Clear();


            for (int pointIndex = 0; pointIndex < propdlg.allPoints; pointIndex++)
            {
                chart1.Series["MyChart"].Points.AddXY(_solution[pointIndex, param], _solution[pointIndex, 2]);

                chart1.Series["PointChart"].Points.AddXY(_solution[pointIndex, param], (double)(_maxZ - _minZ) * (_output[pointIndex][0]) + _minZ);//output[pointIndex][0]);

            }
        }
        private void ShowResult()
        {
            chart1.Series["MyChart"].ChartType = SeriesChartType.Spline;
            chart1.Series["PointChart"].ChartType = SeriesChartType.Point;
            chart1.Series["PointChart"].MarkerStyle = MarkerStyle.Circle;


            chart1.Series["MyChart"].Points.Clear();
            chart1.Series["PointChart"].Points.Clear();


            for (int pointIndex = 0; pointIndex < propdlg.allPoints; pointIndex++)
            {
                chart1.Series["MyChart"].Points.AddXY(_solution[pointIndex, 0], _solution[pointIndex, 1]);

                chart1.Series["PointChart"].Points.AddXY(_solution[pointIndex, 0], Math.Sin(_solution[pointIndex, 0]));

            }
        }
        #endregion

        private void ShowNeuralNet()
        {
            if (cbIsVisualization.Checked != true) return;
            
            Layer[] custom = _network.GetLayers;
            List<NeuronsLayer> neuronsLayers = new List<NeuronsLayer>();
            //перевод вес.коэф. нейроннов в необходимую структуру NeuronsLayer для создания объекта "VisualizationNeuralNet"
            for (var i = 0; i < custom.Count() - 1; i++)
            {
                var mass = new double[custom[i].NeuronsCount];
                for (var j = 0; j < custom[i].NeuronsCount; j++)
                {
                    mass[j] = custom[i][j].Output;
                }
                NeuronsLayer nl = new NeuronsLayer {Neurons = mass};
                neuronsLayers.Add(nl);
            }
            //запускаем в потоке
            var thr = new Thread(delegate() { StartNewVisualization(neuronsLayers); });
            thr.SetApartmentState(ApartmentState.STA);
            thr.Start();
        }

   

        private void StartNewVisualization(List<NeuronsLayer> neuronsLayers)//запуск новой визуализации
        {
            if (VisualizationNeuralNet != null)
                VisualizationNeuralNet.SetNeuralNet(neuronsLayers);
            else
                VisualizationNeuralNet = new VisualizationNeuralNet(neuronsLayers);

            VisualizationNeuralNet.ShowNeuralNet();
        }
        private void StartVisualizationFromFile(string fileName)//запуск визуализации  из файла
        {
            if (VisualizationNeuralNet == null)
                VisualizationNeuralNet = new VisualizationNeuralNet();

            VisualizationNeuralNet.Load(fileName);
            VisualizationNeuralNet.ShowNeuralNet();
        }
        private void BOpenVNN_Click(object sender, EventArgs e)
        {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.DefaultExt = "*.vnn";
                dlg.Filter = "VNN Files | *.vnn";
                dlg.Title = "Открытие нейронной сети ";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Thread thr = new Thread(delegate() { StartVisualizationFromFile(dlg.FileName); });
                thr.SetApartmentState(ApartmentState.STA);
                thr.Start();
            }
        }

   

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
       

    }
}
