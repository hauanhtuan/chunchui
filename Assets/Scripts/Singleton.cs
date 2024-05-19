using UnityEngine;


    public class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool dontDestroyOnLoad;
        protected static T _instance = null;
        public static T Instance
        {
            get
            {
                return _instance;
            }
        }
        public virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = this as T;
            if (dontDestroyOnLoad)
                DontDestroyOnLoad(this.gameObject);
        }
    }
