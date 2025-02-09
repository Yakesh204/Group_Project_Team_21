using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudUpdater : MonoBehaviour {
    private static HudUpdater _instance;
    public static HudUpdater Instance {
        get {
            if (_instance == null) {
                Debug.LogError("Hud Updater is Null");
            }

            return _instance;
        }
    }

    [SerializeField] private TextMeshProUGUI _magazineAmmoText;
    [SerializeField] private TextMeshProUGUI _playerAmmoText;
    [SerializeField] private TextMeshProUGUI _healthPacksText;

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _staminaBar;

    [SerializeField] private GameObject _crosshairsRed;

    [SerializeField] private Image[] _weaponIcons;

    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private TextMeshProUGUI[] _objectives;

    private int _currentBuildIndex;

    private void Awake() {
        if (_instance == null) {
            _instance = this;
        } else {
            Destroy(this.gameObject);
        }

        // Firearm UI events
        FirearmManager.AmmoUpdated += UpdateAmmoText;
        FirearmManager.WeaponSwitched += UpdateWeaponIcon;
        FirearmManager.IsReloading += DisplayReloading;
        FirearmManager.InRangeStatusChanged += SetRedCrosshairs;

        // Player stats UI events
        Player.HealthUpdated += UpdateHealthBar;
        Player.HealthPacksUpdated += UpdateHealthPackText;

        MovementManager.StaminaUpdated += UpdateStaminaBar;

        GameManager.ScoreUpdated += SetScore;
        GameManager.LevelSwitched += SwitchObjectives;
    }

    private void Start () {
        DontDestroyOnLoad(this.gameObject);

        _currentBuildIndex = SceneManager.GetActiveScene().buildIndex - 2;
        SwitchObjectives(_currentBuildIndex);
    }

    private void Update () {
        if (_currentBuildIndex != SceneManager.GetActiveScene().buildIndex - 2) {
            _currentBuildIndex = SceneManager.GetActiveScene().buildIndex - 2;
            SwitchObjectives(_currentBuildIndex);
        }
    }


    private void UpdateAmmoText(float bulletsLeftInMag, float bulletsLeftOnPlayer) {
        _magazineAmmoText.text = bulletsLeftInMag.ToString();
        _playerAmmoText.text = bulletsLeftOnPlayer.ToString();
    }

    private void DisplayReloading() {
        _magazineAmmoText.text = "RLD";
        _playerAmmoText.text = "- - -";
    }

    private void UpdateWeaponIcon(int weaponCode) {
        for (int i = 0; i < _weaponIcons.Length; i++) {
            if (i == weaponCode) {
                _weaponIcons[i].enabled = true;
            } else {
                _weaponIcons[i].enabled = false;
            }
        }
    }

    private void UpdateHealthBar(float healthDecimalAmount) {
        _healthBar.fillAmount = healthDecimalAmount;
    }

    private void UpdateStaminaBar(float staminaDecimalAmount) {
        _staminaBar.fillAmount = staminaDecimalAmount;
    }

    private void UpdateHealthPackText(int healthPacks) {
        _healthPacksText.text = healthPacks.ToString();
    }

    private void SetRedCrosshairs(bool enable) {
        _crosshairsRed.SetActive(enable);
    }

    private void SetScore(int score) {
        _scoreText.text = score.ToString();
    }

    private void SwitchObjectives(int objectiveIndex) {
        foreach (TextMeshProUGUI txt in _objectives) {
            txt.gameObject.SetActive(false);
        }

        _objectives[objectiveIndex].gameObject.SetActive(true);
    }
}
