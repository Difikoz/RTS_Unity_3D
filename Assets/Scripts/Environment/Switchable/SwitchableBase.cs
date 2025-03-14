using UnityEngine;

namespace WinterUniverse
{
    public abstract class SwitchableBase : MonoBehaviour
    {
        [SerializeField] protected bool _switched;

        protected virtual void Awake()
        {
            if (_switched)
            {
                SwitchOn();
            }
            else
            {
                SwitchOff();
            }
        }

        public void Switch()
        {
            if (_switched)
            {
                _switched = false;
                SwitchOff();
            }
            else
            {
                _switched = true;
                SwitchOn();
            }
        }

        public abstract void SwitchOn();
        public abstract void SwitchOff();
    }
}