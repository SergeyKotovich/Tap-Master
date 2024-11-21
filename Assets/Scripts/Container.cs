using Cube;
using UnityEngine;

public class Container : MonoBehaviour
{
    [field:SerializeField]
    public SoundsManager SoundsManager { get; private set; }
    [field:SerializeField]
    public LevelsSwitcher LevelsSwitcher { get; private set; }
    [field:SerializeField]
    public ScreensController.ScreensController ScreensController { get; private set; }
    [field:SerializeField] public LevelsController levelsController { get; private set; }
    [field:SerializeField] public CubesController CubesController { get; private set; }
    
    private static Container _instance;
    public static Container Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Container>();
                
                if (_instance == null)
                {
                    var singleton = new GameObject("Container");
                    _instance = singleton.AddComponent<Container>();
                }

                if (_instance != null)
                {
                    DontDestroyOnLoad(_instance);
                }
            }
            
            return _instance;
        }
    }
    
    private void Awake()
    {
        
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
    }
    
}