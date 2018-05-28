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

        #endregion
        #region State/состояния

        #endregion
        #region _viewPort

        public static SimpleOpenGlControl ViewPort { get; private set; }

        #endregion
        #region Other
        public static MouseButtons Button;
        private static float CenterX { get; set; }

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
                _yCurrent = Convert.ToInt32((ViewPort.Height - value) - Centery);
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
            get => YNew1;
            set
            {
                YNewUnNormalize = value;
                YNew1 = Convert.ToInt32(ViewPort.Height - value - Centery);
            }
        }
        public static bool IsMouseUp { get; set; }

        public static bool IsMouseDown { get; set; }

        public static bool IsMouseMove { get; set; }

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
            IsMouseDown = true;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    Button = MouseButtons.Left; //Если нажата левая кнопка мыши
                    break;
                case MouseButtons.Middle:
                    Button = MouseButtons.Middle;
                    break;
            }
        }
        public static void MouseMove(object sender, MouseEventArgs e)
        {
            IsMouseMove = true;
            XNew = e.X;
            YNew = e.Y;


        }
        public static void MouseUp(object sender, MouseEventArgs e)
        {

            IsMouseUp = true;
            IsMouseDown = false;
            IsMouseMove = false;
        }
        #endregion
        public static void SetViewport(SimpleOpenGlControl viewport)
        {
            ViewPort = viewport;
            ViewPort.MouseClick += MouseClick;
            ViewPort.MouseDown += MouseDown;
            ViewPort.MouseUp += MouseUp;
            ViewPort.MouseMove += MouseMove;
            CenterX = (float)ViewPort.Width / 2;
            Centery = (float)ViewPort.Height / 2;
        }

        public static bool FreezeCam { get; set; }

        public static float Centery { get; set; }

        public static int YNew1 { get; set; }

        public static int XNew1 { get; set; }
    }
}
