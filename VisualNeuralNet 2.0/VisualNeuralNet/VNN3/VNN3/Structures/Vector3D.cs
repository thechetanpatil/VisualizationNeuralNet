using System;

namespace VNN.Structures
{
    [Serializable]
    public struct Vector3D
    {
        public float x, y, z;
        public static Vector3D operator -(Vector3D vec1, Vector3D vec2)
        {
            Vector3D vec3;
            vec3.x = vec1.x - vec2.x;
            vec3.y = vec1.y - vec2.y;
            vec3.z = vec1.z - vec2.z;
            return vec3;
        }
    }
}