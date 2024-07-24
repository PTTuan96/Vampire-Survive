using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviourSingleton<UIController>
{
    public Slider ExpLvlSilder;
    public TMP_Text ExpLvlText;

    public LevelUpSelectionButton[] LevelUpButtons;

    public GameObject levelUpPanel;

    public void UpdateExp(int currentExp, int lvlExp, int currentLvl)
    {
        ExpLvlSilder.maxValue = lvlExp;
        ExpLvlSilder.value = currentExp;

        ExpLvlText.text = "Level: " + currentLvl;
    }

    public void SkipLevelUp()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
