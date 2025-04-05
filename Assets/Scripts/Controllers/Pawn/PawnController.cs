using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace WinterUniverse
{
    [RequireComponent(typeof(PawnAnimator))]
    [RequireComponent(typeof(PawnCombat))]
    [RequireComponent(typeof(PawnEquipment))]
    [RequireComponent(typeof(PawnFaction))]
    [RequireComponent(typeof(PawnInventory))]
    [RequireComponent(typeof(PawnLocomotion))]
    [RequireComponent(typeof(PawnStatus))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    public class PawnController : BasicComponent
    {
        private List<PawnComponent> _components;

        public PawnAnimator Animator { get; private set; }
        public NavMeshAgent Agent { get; private set; }
        public Rigidbody RB { get; private set; }
        public CapsuleCollider Collider { get; private set; }
        public PawnCombat Combat { get; private set; }
        public PawnEquipment Equipment { get; private set; }
        public PawnFaction Faction { get; private set; }
        public PawnInventory Inventory { get; private set; }
        public PawnLocomotion Locomotion { get; private set; }
        public PawnStatus Status { get; private set; }

        public override void InitializeComponent()
        {
            _components = new();
            Animator = GetComponent<PawnAnimator>();
            Agent = GetComponent<NavMeshAgent>();
            RB = GetComponent<Rigidbody>();
            Collider = GetComponent<CapsuleCollider>();
            Combat = GetComponent<PawnCombat>();
            Equipment = GetComponent<PawnEquipment>();
            Faction = GetComponent<PawnFaction>();
            Inventory = GetComponent<PawnInventory>();
            Locomotion = GetComponent<PawnLocomotion>();
            Status = GetComponent<PawnStatus>();
            _components.Add(Animator);
            _components.Add(Combat);
            _components.Add(Equipment);
            _components.Add(Faction);
            _components.Add(Inventory);
            _components.Add(Locomotion);
            _components.Add(Status);
            foreach (PawnComponent component in _components)
            {
                component.InitializeComponent();
            }
        }

        public override void DestroyComponent()
        {
            foreach (PawnComponent component in _components)
            {
                component.DestroyComponent();
            }
        }

        public override void EnableComponent()
        {
            foreach (PawnComponent component in _components)
            {
                component.EnableComponent();
            }
        }

        public override void DisableComponent()
        {
            foreach (PawnComponent component in _components)
            {
                component.DisableComponent();
            }
        }

        public override void UpdateComponent()
        {
            foreach (PawnComponent component in _components)
            {
                component.UpdateComponent();
            }
        }

        public override void FixedUpdateComponent()
        {
            foreach (PawnComponent component in _components)
            {
                component.FixedUpdateComponent();
            }
        }

        public override void LateUpdateComponent()
        {
            foreach (PawnComponent component in _components)
            {
                component.LateUpdateComponent();
            }
        }
    }
}