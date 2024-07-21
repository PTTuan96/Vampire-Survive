using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviourSingleton<ExperienceController>
{
    private UIController uiController;

    [SerializeField]  private ExpItem expItem;
    public int currentExp;

    public int[] expLevels;
    public int currentLv = 1;
    public int levelCount = 100;

    private void Awake()
    {
        uiController = UIController.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {   
        expLevels = new int[levelCount];
        expLevels[0] = 10;
        for(int i = 1; i < expLevels.Length - 1; i ++)
        {   
            expLevels[i] = Mathf.CeilToInt(expLevels[i - 1] * 1.1f);
        }
    }

    public void GetExp(int amount)
    {
        currentExp += amount;

        if(currentExp >= expLevels[currentLv])
        {
            LevelUp();
        }

        uiController.UpdateExp(currentExp, expLevels[currentLv], currentLv);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(expItem, position, Quaternion.identity).expValue = expValue;
    }

    public void LevelUp()
    {
        currentExp -= expLevels[currentLv];
        
        currentLv++;

        if(currentLv >= expLevels.Length)
        {
            currentLv = expLevels.Length - 1;
        }
    }
}
