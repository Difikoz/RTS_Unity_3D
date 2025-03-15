using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimator))]
    [RequireComponent(typeof(PawnEffects))]
    [RequireComponent(typeof(PawnEquipment))]
    [RequireComponent(typeof(PawnFaction))]
    [RequireComponent(typeof(PawnInput))]
    [RequireComponent(typeof(PawnInteraction))]
    [RequireComponent(typeof(PawnInventory))]
    [RequireComponent(typeof(PawnLocomotion))]
    [RequireComponent(typeof(PawnStatus))]
    public class PawnController : MonoBehaviour
    {
        private PawnAnimator _animator;
        private PawnEffects _effects;
        private PawnEquipment _equipment;
        private PawnFaction _faction;
        private PawnInput _input;
        private PawnInteraction _interaction;
        private PawnInventory _inventory;
        private PawnLocomotion _locomotion;
        private PawnStatus _status;

        public PawnAnimator Animator => _animator;
        public PawnEffects Effects => _effects;
        public PawnEquipment Equipment => _equipment;
        public PawnFaction Faction => _faction;
        public PawnInput Input => _input;
        public PawnInteraction Interaction => _interaction;
        public PawnInventory Inventory => _inventory;
        public PawnLocomotion Locomotion => _locomotion;
        public PawnStatus Status => _status;

        public void Create()
        {
            GetComponents();
            InitializeComponents();
        }

        private void GetComponents()
        {
            _animator = GetComponent<PawnAnimator>();
            _effects = GetComponent<PawnEffects>();
            _equipment = GetComponent<PawnEquipment>();
            _faction = GetComponent<PawnFaction>();
            _input = GetComponent<PawnInput>();
            _interaction = GetComponent<PawnInteraction>();
            _inventory = GetComponent<PawnInventory>();
            _locomotion = GetComponent<PawnLocomotion>();
            _status = GetComponent<PawnStatus>();
        }

        private void InitializeComponents()
        {
            _animator.Initialize();
            _effects.Initialize();
            _equipment.Initialize();
            _faction.Initialize();
            _input.Initialize();
            _interaction.Initialize();
            _inventory.Initialize();
            _locomotion.Initialize();
            _status.Initialize();
            //_faction.ChangeConfig(null);
        }

        public void OnTick(float deltaTime)
        {
            _animator.OnTick(deltaTime);
            _effects.OnTick(deltaTime);
            _interaction.OnTick(deltaTime);
            _locomotion.OnTick(deltaTime);
            _status.OnTick(deltaTime);
        }
    }
}