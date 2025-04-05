using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnsManager : BasicComponent
    {
        private List<PawnController> _controllersToAdd;
        private List<PawnController> _controllersToRemove;

        public List<PawnController> Controllers { get; private set; }

        public override void InitializeComponent()
        {
            _controllersToAdd = new();
            _controllersToRemove = new();
            Controllers = new();
        }

        public override void EnableComponent()
        {
            foreach (PawnController controller in Controllers)
            {
                controller.EnableComponent();
            }
        }

        public override void DisableComponent()
        {
            foreach (PawnController controller in Controllers)
            {
                controller.DisableComponent();
            }
        }

        public override void UpdateComponent()
        {
            AddControllers();
            RemoveControllers();
            foreach (PawnController controller in Controllers)
            {
                controller.UpdateComponent();
            }
        }

        public override void FixedUpdateComponent()
        {
            AddControllers();
            RemoveControllers();
            foreach (PawnController controller in Controllers)
            {
                controller.FixedUpdateComponent();
            }
        }

        public override void LateUpdateComponent()
        {
            AddControllers();
            RemoveControllers();
            foreach (PawnController controller in Controllers)
            {
                controller.LateUpdateComponent();
            }
        }

        public void AddController(PawnController controller)
        {
            _controllersToAdd.Add(controller);
        }

        public void RemoveController(PawnController controller)
        {
            _controllersToRemove.Add(controller);
        }

        private void AddControllers()
        {
            if (_controllersToAdd.Count > 0)
            {
                foreach (PawnController controller in _controllersToAdd)
                {
                    controller.InitializeComponent();
                    controller.EnableComponent();
                    Controllers.Add(controller);
                }
                _controllersToAdd.Clear();
            }
        }

        private void RemoveControllers()
        {
            if (_controllersToRemove.Count > 0)
            {
                foreach (PawnController controller in _controllersToRemove)
                {
                    Controllers.Remove(controller);
                    controller.DisableComponent();
                    //controller.Destroy();
                    GameManager.StaticInstance.PrefabsManager.DespawnObject(controller.gameObject);
                }
                _controllersToRemove.Clear();
            }
        }
    }
}