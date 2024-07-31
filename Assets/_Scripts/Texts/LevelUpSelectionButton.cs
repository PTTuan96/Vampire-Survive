using TMPro;
using UnityEngine.UI;
using UnityEngine;
using static WeaponEnums;

public class LevelUpSelectionButton : MonoBehaviour
{
    private UIController uIController;
    private ExperienceController experienceController; 
    public TMP_Text upgradeDescText, nameLevelText;
    public Image weaponIcon;
    private WeaponProduct weaponProduct;
    private int currentWeaponLevel;

    private void Awake()
    {
        uIController = UIController.Instance;
        experienceController = ExperienceController.Instance;
    }

    public void UpdateButtonDisplay(string text, int weaponLevel, IWeaponProduct weaponInfo)
    {
        upgradeDescText.text = text;

        weaponIcon.sprite = weaponInfo.SpriteRenderer;

        nameLevelText.text = weaponInfo.ProductWeaponName + " - Lvl " + weaponLevel;

        weaponProduct = weaponInfo.WeaponTypeSelected;

        currentWeaponLevel =  weaponLevel;
    }

    public void SeletedUpdate()
    {
        experienceController.WeaponLevelUp(weaponProduct);
        uIController.levelUpPanel.SetActive(false);

        Time.timeScale = 1f;
    }
}
