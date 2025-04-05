using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PrefabsManager : BasicComponent
    {
        [SerializeField] private GameObject _playerControllerPrefab;
        [SerializeField] private GameObject _npcControllerPrefab;
        [SerializeField] private GameObject _pawnControllerPrefab;

        public PlayerController GetPlayer()
        {
            return LeanPool.Spawn(_playerControllerPrefab).GetComponent<PlayerController>();
        }

        public NPCController GetNPC()
        {
            return LeanPool.Spawn(_pawnControllerPrefab).GetComponent<NPCController>();
        }

        public PawnController GetPawn()
        {
            return GetPawn(Vector3.zero, Quaternion.identity);
        }

        public PawnController GetPawn(Transform point)
        {
            return GetPawn(point.position, point.rotation);
        }

        public PawnController GetPawn(Vector3 position, Quaternion rotation)
        {
            return LeanPool.Spawn(_pawnControllerPrefab, position, rotation).GetComponent<PawnController>();
        }

        public void DespawnObject(GameObject go, float delay = 0f)
        {
            LeanPool.Despawn(go, delay);
        }
    }
}