using Cinemachine;
using System.Collections;
using UnityEngine;

public class CharacterSelect : MonoBehaviour {
    // Reference to cameras
    [SerializeField] private CinemachineFreeLook[] _cameras;

    // Reference to character models
    [SerializeField] private GameObject[] _characterModels;

    // Reference to characters
    [SerializeField] private GameObject[] _playerCharacters;

    [SerializeField] private GameManager _gameManager;

    private int _currentlyEquippedCamera = 0;
    

    private void Start() {
        CharacterSelectHUD.NextCharacterClicked += NextCharacter;
        CharacterSelectHUD.PreviousCharacterClicked += PreviousCharacter;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        _characterModels[0] = GameObject.Find("Heavy");
        _characterModels[1] = GameObject.Find("Infantry");
        _characterModels[2] = GameObject.Find("Recon");

        CinemachineFreeLook[] camsTemp = FindObjectsOfType<CinemachineFreeLook>();

        foreach (CinemachineFreeLook cam in camsTemp) {
            if (cam.name == "FreeLook Heavy") {
                _cameras[0] = cam;
            }

            if (cam.name == "FreeLook Camera") {
                _cameras[1] = cam;
            }

            if (cam.name == "FreeLook Recon") {
                _cameras[2] = cam;
            }
        }

        _currentlyEquippedCamera = 0;
    }

    private void Update() {
        if (_characterModels[0] == null) {
            _characterModels[0] = GameObject.Find("Heavy");
        }

        if (_characterModels[1] == null) {
            _characterModels[1] = GameObject.Find("Infantry");
        }

        if (_characterModels[2] == null) {
            _characterModels[2] = GameObject.Find("Recon");
        }

        if (_cameras[0] == null) {

            CinemachineFreeLook[] camsTemp = FindObjectsOfType<CinemachineFreeLook>();

            foreach (CinemachineFreeLook cam in camsTemp) {
                if (cam.name == "FreeLook Heavy") {
                    _cameras[0] = cam;
                }

                if (cam.name == "FreeLook Camera") {
                    _cameras[1] = cam;
                }

                if (cam.name == "FreeLook Recon") {
                    _cameras[2] = cam;
                }
            }
        }
    }

    public void NextCharacter() {
        if (_currentlyEquippedCamera == 2) {
            _currentlyEquippedCamera = 0;
        } else {
            _currentlyEquippedCamera++;
        }

        SwitchCamera();
    }

    public void PreviousCharacter() {
        if (_currentlyEquippedCamera == 0) {
            _currentlyEquippedCamera = 2;
        } else {
            _currentlyEquippedCamera--;
        }

        SwitchCamera();
    }

    private void SwitchCamera() {
        if (_characterModels[0] == null) {
            _characterModels[0] = GameObject.Find("Heavy");
        }

        if (_characterModels[1] == null) {
            _characterModels[1] = GameObject.Find("Infantry");
        }

        if (_characterModels[2] == null) {
            _characterModels[2] = GameObject.Find("Recon");
        }

        if (_cameras[0] == null) {

            CinemachineFreeLook[] camsTemp = FindObjectsOfType<CinemachineFreeLook>();

            foreach (CinemachineFreeLook cam in camsTemp) {
                if (cam.name == "FreeLook Heavy") {
                    _cameras[0] = cam;
                }

                if (cam.name == "FreeLook Camera") {
                    _cameras[1] = cam;
                }

                if (cam.name == "FreeLook Recon") {
                    _cameras[2] = cam;
                }
            }
        }
        foreach (CinemachineFreeLook camera in _cameras) {
            camera.enabled = false;
        }
        _cameras[_currentlyEquippedCamera].enabled = true;
    }

    public void StartGame() {
        GameManager.Instance.LoadFirstLevel(_playerCharacters[_currentlyEquippedCamera]);
    }

    private IEnumerator DestroyAfter(float time) {
        yield return new WaitForSeconds(time);

        foreach (GameObject model in _characterModels) {
            Destroy(model);
        }
    }
}
