using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    private static PlayerSingleton _instance;
    public static PlayerSingleton Instance {
        get {
            if (_instance == null) {
                Debug.LogError("Player is Null");
            }

            return _instance;
        }
    }

    [SerializeField] private GameObject _player;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update() {
        //this.transform.position = _player.transform.position;
    }
}
