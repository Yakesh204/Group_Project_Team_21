using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    public static GameManager Instance {
        get {
            if (_instance == null) {
                Debug.LogError("Game Manager is Null"); ;
            }

            return _instance;
        }
    }

    [SerializeField] private Transform[] _spawnPoints;
    private int _activeSpawnPoint;

    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private bool _dontDestroyOnLoad;

    private int _score;
    private GameObject _currentlyActivePlayer;

    public static event Action CharacterSpawned;
    public static event Action<int> ScoreUpdated;
    public static event Action<int> LevelSwitched;
    private GameObject _playerPrefab;


    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(this.gameObject);
        }

        if (_dontDestroyOnLoad) {
            DontDestroyOnLoad(this.gameObject);
        }

        _activeSpawnPoint = 0;
        _spawnPoint = _spawnPoints[_activeSpawnPoint];
    }


    public void LoadStagingArea() {
        SceneManager.LoadScene("StagingArea");
    }

    public void LoadFirstLevel(GameObject character) {
        _activeSpawnPoint = 0;
        SceneManager.LoadScene("Stage01");
        _playerPrefab = character;

        _spawnPoint = _spawnPoints[_activeSpawnPoint];
        _currentlyActivePlayer = Instantiate(character, _spawnPoint.position, _spawnPoint.rotation);

        _activeSpawnPoint++;
        _spawnPoint = _spawnPoints[_activeSpawnPoint];
        LevelSwitched?.Invoke(SceneManager.GetActiveScene().buildIndex - 2);
    }


    public void LoadNextScene() {
        _currentlyActivePlayer.SetActive(false);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);

        _spawnPoint = _spawnPoints[_activeSpawnPoint];
        
        _currentlyActivePlayer.transform.position = _spawnPoint.position;
        
        _activeSpawnPoint++;

        // Match position
        _currentlyActivePlayer.transform.position = _spawnPoint.position;

        // Match rotation
        _currentlyActivePlayer.transform.rotation = _spawnPoint.rotation;

        _currentlyActivePlayer.SetActive(true);
        LevelSwitched?.Invoke(SceneManager.GetActiveScene().buildIndex - 2);
    }














    public void AddToScore(int scoreToBeAdded) {
        _score += scoreToBeAdded;

        ScoreUpdated?.Invoke(_score);
    }

    public void GameOver() {
        _score = 0;
        SceneManager.LoadScene("GameOver");
    }

    public void RestartGame() {
        _currentlyActivePlayer.SetActive(false);
        Destroy(_currentlyActivePlayer);
        _activeSpawnPoint = 0;
        SceneManager.LoadScene("Stage01");


        _spawnPoint = _spawnPoints[_activeSpawnPoint];
        _currentlyActivePlayer = Instantiate(_playerPrefab, _spawnPoint.position, _spawnPoint.rotation);

        _activeSpawnPoint++;
        _spawnPoint = _spawnPoints[_activeSpawnPoint];
    }

    public void Exit() {
        Application.Quit();
    }
}
