using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterSelectHUD : MonoBehaviour {

    // Scriptable Object References
    [SerializeField] private PlayerStats[] _allPlayerStats = new PlayerStats[3];
    [SerializeField] private PlayerStats _referenceStats;

    // Player Stat
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Slider _stamSlider;

    [SerializeField] private Slider _primDamageSlider;
    [SerializeField] private Slider _primRangeSlider;
    [SerializeField] private Slider _primRofSlider;
    [SerializeField] private Slider _primMagSlider;

    [SerializeField] private Slider _secDamageSlider;
    [SerializeField] private Slider _secRangeSlider;
    [SerializeField] private Slider _secRofSlider;
    [SerializeField] private Slider _secMagSlider;

    [SerializeField] private TextMeshProUGUI _characterName;

    // Buttons
    [SerializeField] private Button _prevButton;
    [SerializeField] private Button _nextButton;

    [SerializeField] private CharacterSelect _characterSelect;

    [SerializeField] private GameObject _hud;

    // Private fields
    private int _playerIndex = 0;
    private Input _input;


    // Events
    public static event Action NextCharacterClicked;
    public static event Action PreviousCharacterClicked;

    private void Start() {
        _playerIndex = 0;
        _input = new Input();
        _input.characterSelect.Enable();
        _input.player.Disable();

        _input.characterSelect.nextCharacter.performed += NextCharacter_performed; ;
        _input.characterSelect.previousCharacter.performed += PreviousCharacter_performed; ;
        _input.characterSelect.start.performed += Start_performed; ;


        UpdateSliders();
    }
        
    private void OnDisable() {
        _input.characterSelect.nextCharacter.performed -= NextCharacter_performed; ;
        _input.characterSelect.previousCharacter.performed -= PreviousCharacter_performed; ;
        _input.characterSelect.start.performed -= Start_performed; ;
    }

    private void NextCharacter_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        
        OnNextClicked();
    }

    private void PreviousCharacter_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnPrevClicked();
    }

    private void Start_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnStartClicked();
    }

    public void OnNextClicked() {
        NextCharacterClicked?.Invoke();

        if (_playerIndex == 2) {
            _playerIndex = 0;
        } else {
            _playerIndex++;
        }

        UpdateSliders();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnPrevClicked() {
        PreviousCharacterClicked?.Invoke();

        if (_playerIndex == 0) {
            _playerIndex = 2;
        } else {
            _playerIndex--;
        }

        UpdateSliders();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void OnStartClicked() {
        EventSystem.current.SetSelectedGameObject(null);
        _input.characterSelect.Disable(); // Disable this action map
        _input.player.Enable();


        // Start the game
        _characterSelect.StartGame();
    }



    private void UpdateSliders() {
        PlayerStats _currentPlayerStats = _allPlayerStats[_playerIndex];

        _healthSlider.value = _currentPlayerStats.health/_referenceStats.health;
        _stamSlider.value = _currentPlayerStats.stamina/_referenceStats.stamina;

        _primDamageSlider.value = _currentPlayerStats.primaryDamage/_referenceStats.primaryDamage;
        _primRangeSlider.value = _currentPlayerStats.primaryRange/_referenceStats.primaryRange;
        _primRofSlider.value = _currentPlayerStats.primaryRof / _referenceStats.primaryRof;
        _primMagSlider.value = _currentPlayerStats.primaryMagSize / _referenceStats.primaryMagSize;

        _secDamageSlider.value = _currentPlayerStats.secondaryDamage / _referenceStats.secondaryDamage;
        _secRangeSlider.value = _currentPlayerStats.secondaryRange / _referenceStats.secondaryRange;
        _secRofSlider.value = _currentPlayerStats.secondaryRof / _referenceStats.secondaryRof;
        _secMagSlider.value = _currentPlayerStats.secondaryMagSize / _referenceStats.secondaryMagSize;

        _characterName.text = _currentPlayerStats.name;
    }

    private void OnEnable() {
        _input = new Input();
        _input.characterSelect.Enable();
        _input.player.Disable();
    }
}
