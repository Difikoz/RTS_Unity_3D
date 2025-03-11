using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PrefabsManager : MonoBehaviour
    {
        [SerializeField] private GameObject _pawnPrefab;
        [SerializeField] private GameObject _npcControllerPrefab;

        public PawnController GetPawn(Transform point)
        {
            return GetPawn(point.position, point.rotation);
        }

        public PawnController GetPawn(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(_pawnPrefab, position, rotation).GetComponent<PawnController>();
        }

        public NPCController GetNPC(Transform point)
        {
            return GetNPC(point.position, point.rotation);
        }

        public NPCController GetNPC(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(_pawnPrefab, position, rotation).GetComponent<NPCController>();
        }

        public void DespawnObject(GameObject go, float delay = 0f)
        {
            LeanPool.Despawn(go, delay);
        }
    }
}