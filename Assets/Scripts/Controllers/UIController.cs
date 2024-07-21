using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviourSingleton<UIController>
{
    public Slider expLvlSilder;
    public TMP_Text expLvlText;

    public void UpdateExp(int currentExp, int lvlExp, int currentLvl)
    {
        expLvlSilder.maxValue = lvlExp;
        expLvlSilder.value = currentExp;

        expLvlText.text = "Level: " + currentLvl;
    }
}
