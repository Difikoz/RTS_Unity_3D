using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class OwnersManager : BasicComponent
    {
        private List<OwnerController> _controllersToAdd;
        private List<OwnerController> _controllersToRemove;

        public PlayerController PlayerController { get; private set; }
        public List<OwnerController> Controllers { get; private set; }

        public override void InitializeComponent()
        {
            _controllersToAdd = new();
            _controllersToRemove = new();
            Controllers = new();
            PlayerController = GameManager.StaticInstance.PrefabsManager.GetPlayer();
            AddController(PlayerController);
        }

        public override void EnableComponent()
        {
            foreach (OwnerController controller in Controllers)
            {
                controller.EnableComponent();
            }
        }

        public override void DisableComponent()
        {
            foreach (OwnerController controller in Controllers)
            {
                controller.DisableComponent();
            }
        }

        public override void UpdateComponent()
        {
            AddControllers();
            RemoveControllers();
            foreach (OwnerController controller in Controllers)
            {
                controller.UpdateComponent();
            }
        }

        public override void FixedUpdateComponent()
        {
            AddControllers();
            RemoveControllers();
            foreach (OwnerController controller in Controllers)
            {
                controller.FixedUpdateComponent();
            }
        }

        public override void LateUpdateComponent()
        {
            AddControllers();
            RemoveControllers();
            foreach (OwnerController controller in Controllers)
            {
                controller.LateUpdateComponent();
            }
        }

        public void AddController(OwnerController controller)
        {
            _controllersToAdd.Add(controller);
        }

        public void RemoveController(OwnerController controller)
        {
            _controllersToRemove.Add(controller);
        }

        private void AddControllers()
        {
            if (_controllersToAdd.Count > 0)
            {
                foreach (OwnerController controller in _controllersToAdd)
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
                foreach (OwnerController controller in _controllersToRemove)
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