using System;
using System.Windows.Forms;
using Tao.OpenGl;
using VNN.Structures;

namespace VNN.Classes
{

    class Camera
    {
        #region Declaration
        #region Fields Vectors
        private Vector3D _position;   // Вектор позиции камеры
        private Vector3D _view;   // Направление, куда смотрит камера
        private Vector3D _up;     // Вектор верхнего направления
        private Vector3D _strafe; // Вектор для стрейфа (движения влево и вправо) камеры
        #endregion
        private bool _cameraMove;
        private bool _cameraRotate;
        float _rotCamX;
        #endregion

        public bool CameraRotate
        {
            get => _cameraRotate;
            set => _cameraRotate = value;
        }
        public bool CameraMove
        {
            get => _cameraMove;
            set => _cameraMove = value;
        }
        public void ActionCamera()
        {
            _cameraRotate = Mouse.Button == MouseButtons.Left;
            _cameraMove = Mouse.Button == MouseButtons.Middle;

            if (_cameraRotate || _cameraMove)
            {
                SelectAction();
            }
        }
        public void SelectAction()
        {
            if (Mouse.FreezeCam != true) { 
            if (Mouse.Mdown && _cameraRotate) //Если нажата левая кнопка мыши
            {
                Mouse._viewPort.Cursor = Cursors.SizeAll; //меняем указатель
                Rotate_Position(Mouse.XNewUnNormalize - Mouse.XCurrentUnNormalize, 0, 1, 0); //крутим камеру
                _rotCamX = _rotCamX + (Mouse.YNewUnNormalize - Mouse.YCurrentUnNormalize);
                if ((_rotCamX > -40) && (_rotCamX < 40))
                    UpDown(((float)(Mouse.YNewUnNormalize - Mouse.YCurrentUnNormalize)) / 1);
                Mouse.XCurrentUnNormalize=Mouse.XNewUnNormalize;
                Mouse.YCurrentUnNormalize = Mouse.YNewUnNormalize;
            }
            else
            {
                if (Mouse.Mmove && _cameraMove && Mouse.Mdown)
                {
                    Mouse._viewPort.Cursor = Cursors.SizeAll;

                    Move_Camera((float)(Mouse.YNewUnNormalize - Mouse.YCurrentUnNormalize) / 50);
                    Strafe(-((float)(Mouse.XNewUnNormalize - Mouse.XCurrentUnNormalize) / 50));
                    Mouse.XCurrentUnNormalize = Mouse.XNewUnNormalize;
                    Mouse.YCurrentUnNormalize = Mouse.YNewUnNormalize;
                }
                else
                {
                    Mouse._viewPort.Cursor = Cursors.Default;//возвращаем курсор
                }
            }
            }
        }

  

        #region Constructor
        public Camera()
        {
            _position.x = 5; // Позиция камеры
            _position.y = 2; //
            _position.z = 10; //
            _view.x = 0; // Куда смотрит, т.е. взгляд
            _view.y = 0; //
            _view.z = 0; //
            _up.x = 0; // Вертикальный вектор камеры
            _up.y = 1; //
            _up.z = 0; //
        }
        #endregion
        #region Methods

        private Vector3D Cross(Vector3D vV1, Vector3D vV2, Vector3D vVector2)
        {
            Vector3D vNormal;
            Vector3D vVector1;
            vVector1.x = vV1.x - vV2.x;
            vVector1.y = vV1.y - vV2.y;
            vVector1.z = vV1.z - vV2.z;

            // Если у нас есть 2 вектора (вектор взгляда и вертикальный вектор), 
            // У нас есть плоскость, от которой мы можем вычислить угол в 90 градусов.
            // Рассчет cross'a прост, но его сложно запомнить с первого раза. 
            // Значение X для вектора = (V1.y * V2.z) - (V1.z * V2.y)
            vNormal.x = ((vVector1.y * vVector2.z) - (vVector1.z * vVector2.y));

            // Значение Y = (V1.z * V2.x) - (V1.x * V2.z)
            vNormal.y = ((vVector1.z * vVector2.x) - (vVector1.x * vVector2.z));

            // Значение Z = (V1.x * V2.y) - (V1.y * V2.x)
            vNormal.z = ((vVector1.x * vVector2.y) - (vVector1.y * vVector2.x));

            
            return vNormal;
        }
        private float Magnitude(Vector3D vNormal)
        {
            // Это даст нам величину нашей нормали, 
         

            return (float)Math.Sqrt((vNormal.x * vNormal.x) +
                    (vNormal.y * vNormal.y) +
                    (vNormal.z * vNormal.z));
        }
        private Vector3D Normalize(Vector3D vVector)
        {
         

            // Вычислим величину нормали
            float magnitude = Magnitude(vVector);

          
            vVector.x = vVector.x / magnitude;
            vVector.y = vVector.y / magnitude;
            vVector.z = vVector.z / magnitude;

            return vVector;
        }
        public void Position_Camera(float posX, float posY, float posZ,
                     float viewX, float viewY, float viewZ,
                     float upX, float upY, float upZ)
        {
            _position.x = posX; // Позиция камеры
            _position.y = posY; //
            _position.z = posZ; //
            _view.x = viewX; // Куда смотрит, т.е. взгляд
            _view.y = viewY; //
            _view.z = viewZ; //
            _up.x = upX; // Вертикальный вектор камеры
            _up.y = upY; //
            _up.z = upZ; //
        }
        public void Rotate_View(float speed)
        {
            Vector3D vVector; // Полчим вектор взгляда
            vVector.x = _view.x - _position.x;
            vVector.y = _view.y - _position.y;
            vVector.z = _view.z - _position.z;



            _view.z = (float)(_position.z + Math.Sin(speed) * vVector.x + Math.Cos(speed) * vVector.z);
            _view.x = (float)(_position.x + Math.Cos(speed) * vVector.x - Math.Sin(speed) * vVector.z);
        }
        public void Rotate_Position(float angle, float x, float y, float z)
        {
            _position.x = _position.x - _view.x;
            _position.y = _position.y - _view.y;
            _position.z = _position.z - _view.z;

            Vector3D vVector = _position;
            Vector3D aVector;

            float sinA = (float)Math.Sin(Math.PI * angle / 180.0);
            float cosA = (float)Math.Cos(Math.PI * angle / 180.0);

            // Найдем новую позицию X для вращаемой точки 
            aVector.x = (cosA + (1 - cosA) * x * x) * vVector.x;
            aVector.x += ((1 - cosA) * x * y - z * sinA) * vVector.y;
            aVector.x += ((1 - cosA) * x * z + y * sinA) * vVector.z;

            // Найдем позицию Y 
            aVector.y = ((1 - cosA) * x * y + z * sinA) * vVector.x;
            aVector.y += (cosA + (1 - cosA) * y * y) * vVector.y;
            aVector.y += ((1 - cosA) * y * z - x * sinA) * vVector.z;

            // И позицию Z 
            aVector.z = ((1 - cosA) * x * z - y * sinA) * vVector.x;
            aVector.z += ((1 - cosA) * y * z + x * sinA) * vVector.y;
            aVector.z += (cosA + (1 - cosA) * z * z) * vVector.z;

            _position.x = _view.x + aVector.x;
            _position.y = _view.y + aVector.y;
            _position.z = _view.z + aVector.z;
        }
        public void Move_Camera(float speed) // Задаем скорость
        {
            Vector3D vVector; // Получаем вектор взгляда
            vVector.x = _view.x - _position.x;
            vVector.y = _view.y - _position.y;
            vVector.z = _view.z - _position.z;

            vVector.y = 0.0f; // Это запрещает камере подниматься вверх
            vVector = Normalize(vVector);

            _position.x += vVector.x * speed;
            _position.z += vVector.z * speed;
            _view.x += vVector.x * speed;
            _view.z += vVector.z * speed;
        }
        public void Strafe(float speed)
        {
            // добавим вектор стрейфа к позиции
            _position.x += _strafe.x * speed;
            _position.z += _strafe.z * speed;

            // Добавим теперь к взгляду
            _view.x += _strafe.x * speed;
            _view.z += _strafe.z * speed;
        }

        public void Update()
        {
            Vector3D vCross = Cross(_view, _position, _up);

            // Нормализуем вектор стрейфа
            _strafe = Normalize(vCross);
        }
        public void UpDown(float speed)
        {
            _position.y += speed;
        }
        public void Look()
        {
            Glu.gluLookAt(_position.x, _position.y, _position.z, // Ранее упомянутая команда 
                          _view.x, _view.y, _view.z,
                          _up.x, _up.y, _up.z);
        }
        #region Getters
        public double GetPosX() // Возвращает позицию камеры по Х
        {
            return _position.x;
        }

        public double GetPosY() // Возвращает позицию камеры по Y
        {
            return _position.y;
        }

        public double GetPosZ() // Возвращает позицию камеры по Z
        {
            return _position.z;
        }

        public double GetViewX() // Возвращает позицию взгляда по Х
        {
            return _view.x;
        }

        public double GetViewY() // Возвращает позицию взгляда по Y
        {
            return _view.y;
        }

        public double GetViewZ() // Возвращает позицию взгляда по Z
        {
            return _view.z;
        }
        #endregion

        #endregion

        ///////////////

    }


}
