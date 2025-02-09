using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HudUpdater : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI _magazineAmmoText;
    [SerializeField] private TextMeshProUGUI _playerAmmoText;
    [SerializeField] private TextMeshProUGUI _healthPacksText;

    [SerializeField] private Image _healthBar;
    [SerializeField] private Image _staminaBar;

    [SerializeField] private GameObject _crosshairsRed;

    [SerializeField] private Image[] _weaponIcons;

    private void Awake () {
        // Firearm UI events
        FirearmManager.AmmoUpdated += UpdateAmmoText;
        FirearmManager.WeaponSwitched += UpdateWeaponIcon;
        FirearmManager.IsReloading += DisplayReloading;
        FirearmManager.InRangeStatusChanged += SetRedCrosshairs;

        // Player stats UI events
        Player.HealthUpdated += UpdateHealthBar;
        Player.HealthPacksUpdated += UpdateHealthPackText;

        MovementManager.StaminaUpdated += UpdateStaminaBar;
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
}
