using Tao.FreeGlut;
using Tao.OpenGl;

namespace VNN.Classes.Scene
{
    internal class SceneSpace : ISceneObject
    {

        public void Draw()//отрисовка пространства, в котором находятся объекты
        {
            float[] matrixColorOx = { 1, 0, 1, 1 };
            Gl.glPushMatrix();
            DrawGrid(15, 1);//Нарисуем сетку
            Gl.glBegin(Gl.GL_LINE_STRIP);

            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, matrixColorOx);

            Gl.glVertex3d(2, 0, 0);
            Gl.glVertex3d(0, 2, 0);
            Gl.glEnd();
        }
        private static void DrawGrid(int x, double quadSize)
        {
            float[] matrixColorOx = { 1, 0, 0, 1 };
            float[] matrixColorOy = { 0, 1, 0, 1 };
            float[] matrixColorOz = { 0, 0, 1, 1 };
            float[] matrixColorOt = { 0, 0, 0, 1 };
            //x - количество или длина сетки, quad_size - размер клетки
            Gl.glPushMatrix(); //Рисуем оси координат, цвет объявлен в самом начале
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, matrixColorOx);
            Gl.glTranslated(-x * 2 / 2, 0, 0);
            Gl.glRotated(90, 0, 1, 0);
            Glut.glutSolidCylinder(0.02, x * 2, 12, 12);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, matrixColorOz);
            Gl.glTranslated(0, 0, (-x * 2) / 2);
            Glut.glutSolidCylinder(0.02, x * 2, 12, 12);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, matrixColorOy);
            Gl.glTranslated(0, x / 2, 0);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3d(0, 0, 22);
            Gl.glVertex3d(0, 0, -8);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, matrixColorOt);
            Gl.glBegin(Gl.GL_LINES);
            DrawGridAlongAxises(x, quadSize);
        }

        private static void DrawGridAlongAxises(int x, double quadSize)
        {
            // Рисуем сетку 1х1 вдоль осей
            for (double i = -x; i <= x; i += 1)
            {
                Gl.glBegin(Gl.GL_LINES);
                // Ось Х
                Gl.glVertex3d(-x * quadSize, 0, i * quadSize);
                Gl.glVertex3d(x * quadSize, 0, i * quadSize);

                // Ось Z
                Gl.glVertex3d(i * quadSize, 0, -x * quadSize);
                Gl.glVertex3d(i * quadSize, 0, x * quadSize);
                Gl.glEnd();
            }
        }
    }
}
