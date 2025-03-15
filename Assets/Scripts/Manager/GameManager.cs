using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private AudioManager _audioManager;
        private CameraManager _cameraManager;
        private ControllersManager _controllersManager;
        private LayersManager _layersManager;
        private PrefabsManager _prefabsManager;

        public AudioManager AudioManager => _audioManager;
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
            _audioManager = GetComponentInChildren<AudioManager>();
            _cameraManager = GetComponentInChildren<CameraManager>();
            _controllersManager = GetComponentInChildren<ControllersManager>();
            _layersManager = GetComponentInChildren<LayersManager>();
            _prefabsManager = GetComponentInChildren<PrefabsManager>();
        }

        private void InitializeComponents()
        {
            _audioManager.InitializeComponent();
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