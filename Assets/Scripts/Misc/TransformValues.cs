using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class TransformValues
    {
        public float PosX { get; private set; }
        public float PosY { get; private set; }
        public float PosZ { get; private set; }

        public float RotX { get; private set; }
        public float RotY { get; private set; }
        public float RotZ { get; private set; }

        public void SetPosition(Vector3 position)
        {
            PosX = position.x;
            PosY = position.y;
            PosZ = position.z;
        }

        public void SetRotation(Vector3 eulerAngles)
        {
            RotX = eulerAngles.x;
            RotY = eulerAngles.y;
            RotZ = eulerAngles.z;
        }

        public void SetPositionAndRotation(Vector3 position, Vector3 eulerAngles)
        {
            SetPosition(position);
            SetRotation(eulerAngles);
        }

        public Vector3 GetPosition()
        {
            return new(PosX, PosY, PosZ);
        }

        public Quaternion GetRotation()
        {
            return Quaternion.Euler(RotX, RotY, RotZ);
        }
    }
}