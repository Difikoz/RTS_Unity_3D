using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private CameraManager _cameraManager;
        private ControllersManager _controllersManager;
        private LayersManager _layersManager;
        private PrefabsManager _prefabsManager;

        public CameraManager CameraManager => _cameraManager;
        public ControllersManager ControllersManager => _controllersManager;
        public LayersManager LayersManager => _layersManager;
        public PrefabsManager PrefabsManager => _prefabsManager;

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
        }

        private void Start()
        {
            InitializeComponents();
        }

        private void Update()
        {
            UpdateComponents(Time.deltaTime);
        }

        private void GetComponents()
        {
            _cameraManager = GetComponentInChildren<CameraManager>();
            _controllersManager = GetComponentInChildren<ControllersManager>();
            _layersManager = GetComponentInChildren<LayersManager>();
            _prefabsManager = GetComponentInChildren<PrefabsManager>();
        }

        private void InitializeComponents()
        {
            _cameraManager.InitializeComponent();
            _controllersManager.InitializeComponent();
        }

        private void UpdateComponents(float deltaTime)
        {
            _cameraManager.OnUpdate(deltaTime);
            _controllersManager.OnUpdate(deltaTime);
        }
    }
}