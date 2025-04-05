using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private List<BasicComponent> _components;

        public AudioManager AudioManager { get; private set; }
        public CameraManager CameraManager { get; private set; }
        public ConfigsManager ConfigsManager { get; private set; }
        public OwnersManager OwnersManager { get; private set; }
        public PawnsManager PawnsManager { get; private set; }
        public LayersManager LayersManager { get; private set; }
        public PrefabsManager PrefabsManager { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _components = new();
            AudioManager = GetComponentInChildren<AudioManager>();
            CameraManager = GetComponentInChildren<CameraManager>();
            ConfigsManager = GetComponentInChildren<ConfigsManager>();
            OwnersManager = GetComponentInChildren<OwnersManager>();
            PawnsManager = GetComponentInChildren<PawnsManager>();
            LayersManager = GetComponentInChildren<LayersManager>();
            PrefabsManager = GetComponentInChildren<PrefabsManager>();
            _components.Add(AudioManager);
            _components.Add(CameraManager);
            _components.Add(ConfigsManager);
            _components.Add(OwnersManager);
            _components.Add(PawnsManager);
            _components.Add(LayersManager);
            _components.Add(PrefabsManager);
            foreach (BasicComponent component in _components)
            {
                component.InitializeComponent();
            }
        }

        private void OnEnable()
        {
            foreach (BasicComponent component in _components)
            {
                component.EnableComponent();
            }
        }

        private void OnDisable()
        {
            foreach (BasicComponent component in _components)
            {
                component.DisableComponent();
            }
        }

        private void Update()
        {
            foreach (BasicComponent component in _components)
            {
                component.UpdateComponent();
            }
        }

        private void FixedUpdate()
        {
            foreach (BasicComponent component in _components)
            {
                component.FixedUpdateComponent();
            }
        }

        private void LateUpdate()
        {
            foreach (BasicComponent component in _components)
            {
                component.LateUpdateComponent();
            }
        }
    }
}