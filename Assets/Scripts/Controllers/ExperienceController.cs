using System;
using System.Collections.Generic;
using UnityEngine;
using static WeaponEnums;

public class ExperienceController : MonoBehaviourSingleton<ExperienceController>
{
    private UIController uIController;

    [SerializeField] private ExpItem expItem;
    [SerializeField] private Weapon assignWeapon;
    
    private int currentExp;
    private int[] expLevels;
    private int currentLv = 1;
    private int levelCount = 100;

    private List<WeaponProduct> availableWeaponProducts;

    private void Awake()
    {
        uIController = UIController.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {   
        // expLevels = new int[levelCount];
        // expLevels[0] = 10;
        // for(int i = 1; i < expLevels.Length - 1; i ++)
        // {   
        //     expLevels[i] = Mathf.CeilToInt(expLevels[i - 1] * 1.1f);
        // }

        // Show level up panel
        uIController.levelUpPanel.SetActive(true);

        Time.timeScale = 0f; // pause the game
        UpdateWeapon();
    }

    private void UpdateAvailableWeaponProducts()
    {
        availableWeaponProducts = new List<WeaponProduct>();

        foreach (WeaponProduct weapon in Enum.GetValues(typeof(WeaponProduct)))
        {
            if (assignWeapon.GetWeaponLevel(weapon) < assignWeapon.GetWeaponFactory(weapon).Stats.Count)
            {
                availableWeaponProducts.Add(weapon);
            }
        }
    }

    public void UpdateWeapon()
    {
        UpdateAvailableWeaponProducts();
        foreach (LevelUpSelectionButton button in uIController.LevelUpButtons)
        {
            WeaponProduct? randomWeaponProduct = GetRandomWeaponProduct();
            
            if (randomWeaponProduct.HasValue)
            {   
                IWeaponProduct weaponProduct = assignWeapon.GetWeaponInfo(randomWeaponProduct.Value);
                if(weaponProduct != null)
                {
                    WeaponFactory weaponFactory = assignWeapon.GetWeaponFactory(randomWeaponProduct.Value);
                    int weaponLevel = assignWeapon.GetWeaponLevel(randomWeaponProduct.Value);
                    button.UpdateButtonDisplay(weaponFactory.Stats[weaponLevel].UpgradeText, weaponLevel, weaponProduct);
                    if(weaponLevel == Level_1)
                    {
                        assignWeapon.SetActiveWeapon(randomWeaponProduct.Value, SET_DEACTIVE);
                    }
                }
            }
            else
            {
                // Handle the case when no valid weapon products are left
                Debug.Log("No more valid WeaponProducts available.");
                button.gameObject.SetActive(false);; // Example method to handle this case
            }
        }
    }

    public void GetExp(int amount)
    {
        currentExp += amount;

        if(currentExp >= expLevels[currentLv])
        {
            PlayerLevelUp();
        }

        uIController.UpdateExp(currentExp, expLevels[currentLv], currentLv);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(expItem, position, Quaternion.identity).expValue = expValue;
    }

    public void PlayerLevelUp()
    {
        currentExp -= expLevels[currentLv];
        
        currentLv++;

        if(currentLv >= expLevels.Length)
        {
            currentLv = expLevels.Length - 1;
        }

        // Show level up panel
        uIController.levelUpPanel.SetActive(true);

        Time.timeScale = 0f; // pause the game

        UpdateWeapon();

        // uIController.LevelUpButtons[1].UpdateButtonDisplay(weaponFactories[0]);
    }

    public WeaponProduct? GetRandomWeaponProduct()
    {
        // If no valid weapon products are left, return null
        if (availableWeaponProducts.Count == 0)
        {
            return null;
        }

        // Pick a random index from the available list
        int randomIndex = UnityEngine.Random.Range(0, availableWeaponProducts.Count);
        WeaponProduct selectedWeapon = availableWeaponProducts[randomIndex];

        // Remove the selected weapon from the list to avoid repeating
        availableWeaponProducts.RemoveAt(randomIndex);

        return selectedWeapon;
    }

    public void WeaponLevelUp(WeaponProduct weaponProduct)
    {
        Debug.Log(weaponProduct);
    }
}
