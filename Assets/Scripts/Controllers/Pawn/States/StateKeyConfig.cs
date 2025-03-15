using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "State Key", menuName = "Winter Universe/Pawn/State/New State Key")]
    public class StateKeyConfig : ScriptableObject
    {
        [SerializeField] private string _key;

        public string Key => _key;
    }
}