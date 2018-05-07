using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VNN.NSceneObject;
using Tao.OpenGl;
using Tao.FreeGlut;
namespace VNN.NScene
{
    class SceneSpace:ISceneObject
    {
 
        public void Draw()//отрисовка пространства, в котором находятся объекты
        {
            float[] MatrixColorOX = { 1, 0, 1, 1 };
            Gl.glPushMatrix();
            DrawGrid(15, 1);//Нарисуем сетку
            Gl.glBegin(Gl.GL_LINE_STRIP);

            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOX);

            Gl.glVertex3d(2, 0, 0);
            Gl.glVertex3d(0, 2, 0);
            Gl.glEnd();
        }
        private void DrawGrid(int x, double quad_size)
        {
            float[] MatrixColorOX = { 1, 0, 0, 1 };
            float[] MatrixColorOY = { 0, 1, 0, 1 };
            float[] MatrixColorOZ = { 0, 0, 1, 1 };
            float[] MatrixColorOT = { 0, 0, 0, 1 };
            //x - количество или длина сетки, quad_size - размер клетки
            Gl.glPushMatrix(); //Рисуем оси координат, цвет объявлен в самом начале
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOX);
            Gl.glTranslated((-x * 2) / 2, 0, 0);
            Gl.glRotated(90, 0, 1, 0);
            Glut.glutSolidCylinder(0.02, x * 2, 12, 12);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOZ);
            Gl.glTranslated(0, 0, (-x * 2) / 2);
            Glut.glutSolidCylinder(0.02, x * 2, 12, 12);
            Gl.glPopMatrix();

            Gl.glPushMatrix();
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOY);
            Gl.glTranslated(0, x / 2, 0);
            Gl.glRotated(90, 1, 0, 0);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glVertex3d(0, 0, 22);
            Gl.glVertex3d(0, 0, -8);
            Gl.glEnd();
            Gl.glPopMatrix();
            Gl.glMaterialfv(Gl.GL_FRONT, Gl.GL_AMBIENT_AND_DIFFUSE, MatrixColorOT);
            Gl.glBegin(Gl.GL_LINES);



            // Рисуем сетку 1х1 вдоль осей
            for (double i = -x; i <= x; i += 1)
            {
                Gl.glBegin(Gl.GL_LINES);
                // Ось Х
                Gl.glVertex3d(-x * quad_size, 0, i * quad_size);
                Gl.glVertex3d(x * quad_size, 0, i * quad_size);

                // Ось Z
                Gl.glVertex3d(i * quad_size, 0, -x * quad_size);
                Gl.glVertex3d(i * quad_size, 0, x * quad_size);
                Gl.glEnd();
            }
        }

    }
}
