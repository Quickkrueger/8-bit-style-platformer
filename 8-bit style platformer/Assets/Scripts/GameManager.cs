using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]public class EventGameState : UnityEvent<GameManager.GameState, GameManager.GameState>
{

}

public class GameManager : Singleton<GameManager>
{
    //control sprite palettes
    //keep track of game state

    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }

    public GameObject[] SystemPrefabs;
    public EventGameState OnGameStateChanged;

    List<GameObject> _instancedSystemPrefabs;
    List<AsyncOperation> _loadOperations;

    GameState _currentGameState = GameState.PREGAME;

    string _currentLevelName = string.Empty;

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        private set { _currentGameState = value; }
    }

    

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        _instancedSystemPrefabs = new List<GameObject>();
        _loadOperations = new List<AsyncOperation>();

        InstantiateSystemPrefabs();
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);
            
            if(_loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }

        Debug.Log("Load Complete");
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
    }

    void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                break;

            case GameState.RUNNING:
                break;

            case GameState.PAUSED:
                break;

            default:
                break;
        }

        OnGameStateChanged.Invoke(_currentGameState, previousGameState);



    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for(int i = 0; i < SystemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to load level " + levelName);
            return;
        }
        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);
        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to unload level " + levelName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for(int i = 0; i < _instancedSystemPrefabs.Count; i++)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();
    }

    public void StartGame()
    {
        LoadLevel("Main");
    }
}
