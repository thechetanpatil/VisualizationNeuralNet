using System;
using System.Windows.Forms;
using Tao.Platform.Windows;

namespace VNN.Classes
{
    class Mouse
    {
        #region Declaration
        #region Coords field

        public static int XCurrentUnNormalize { get; set; }
        public static int YCurrentUnNormalize { get; set; }
        public static int XNewUnNormalize { get; private set; }
        public static int YNewUnNormalize { get; private set; }
        private static int _xCurrent;
        private static int _yCurrent;
        private static int _xNew;
        private static int _yNew;
        #endregion
        #region State/состояния
        private static bool _mdown;
        private static bool _mmove;
        private static bool _mup;
        private static bool _freezecam;
        #endregion
        #region _viewPort

        public static SimpleOpenGlControl _viewPort { get; private set; }

        #endregion
        #region Other
        public static MouseButtons Button;
        private static float CenterX { get; set; }

        private static float _centerY;

        #endregion
        #endregion
        #region Properties

        public static int XCurrent
        {
            get => _xCurrent;
            set
            {
                XCurrentUnNormalize = value;
                _xCurrent = Convert.ToInt32(value - CenterX);
            }

        }
        public static int YCurrent
        {
            get => _yCurrent;
            set
            {
                YCurrentUnNormalize = value;
                _yCurrent = Convert.ToInt32((_viewPort.Height - value) - Centery);
            }
        }
        public static int XNew
        {
            get => XNew1;
            set
            {
                XNewUnNormalize = value;
                XNew1 = Convert.ToInt32(value - CenterX);
            }
        }
        public static int YNew
        {
            get { return YNew1; }
            set
            {
                YNewUnNormalize = value;
                YNew1 = Convert.ToInt32((_viewPort.Height - value) - Centery);
            }
        }
        public static bool Mup
        {
            get { return _mup; }
            set { _mup = value; }
        }
        public static bool Mdown
        {
            get { return _mdown; }
            set { _mdown = value; }
        }
        public static bool Mmove
        {
            get { return _mmove; }
            set { _mmove = value; }
        }
        #endregion
        #region Events
        public static void MouseClick(object sender, MouseEventArgs e)
        {
            //XCurrent = e.X;
            //YCurrent = e.Y;
        }
        public static void MouseDown(object sender, MouseEventArgs e)
        {
            XCurrent = e.X;
            YCurrent = e.Y;
            Mdown = true;
            if (e.Button == MouseButtons.Left)
                Button = MouseButtons.Left; //Если нажата левая кнопка мыши
            if (e.Button == MouseButtons.Middle)
            {
                Button = MouseButtons.Middle;
            }
        }
        public static void MouseMove(object sender, MouseEventArgs e)
        {
            Mmove = true;
            XNew = e.X;
            YNew = e.Y;


        }
        public static void MouseUp(object sender, MouseEventArgs e)
        {

            Mup = true;
            Mdown = false;
            Mmove = false;
        }
        #endregion
        public static void SetViewport(SimpleOpenGlControl viewport)
        {
            _viewPort = viewport;
            _viewPort.MouseClick += MouseClick;
            _viewPort.MouseDown += MouseDown;
            _viewPort.MouseUp += MouseUp;
            _viewPort.MouseMove += MouseMove;
            CenterX = _viewPort.Width / 2;
            Centery = _viewPort.Height / 2;
        }

        public static bool FreezeCam
        {

            get
            {
                return _freezecam;
            }
            set
            {
                _freezecam = value;
            }
        }

        public static float Centery { get => _centerY; set => _centerY = value; }
        public static int YNew1 { get => _yNew; set => _yNew = value; }
        public static int XNew1 { get => _xNew; set => _xNew = value; }
    }
}
