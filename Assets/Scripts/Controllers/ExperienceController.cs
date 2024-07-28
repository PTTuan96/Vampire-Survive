using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static WeaponEnums;

public class ExperienceController : MonoBehaviourSingleton<ExperienceController>
{
    private UIController uIController;

    [SerializeField] private ExpItem expItem;

    [SerializeField] private Weapon assignWeapon;
    private List<WeaponProduct> availableWeaponProducts;
    private List<IWeaponProduct> weaponProducts;

    [SerializeField] private int levelCount = 100;
    private int p_CurrentPlayerExp;
    private int[] p_PlayerExpLevels;
    private int p_PlayerCurrentLvl = 1;
    


    private void Awake()
    {
        uIController = UIController.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {   
        InitiatePlayerExp();

        // Show level up panel
        uIController.levelUpPanel.SetActive(true);
        Time.timeScale = 0f; // pause the game

        weaponProducts = assignWeapon.CreateAllWeapons();
        ShowUpdateWeapon();
    }

    private void InitiatePlayerExp()
    {
        p_PlayerExpLevels = new int[levelCount]; // Number of level -> level Count = 100  
        p_PlayerExpLevels[0] = 10; // Exp for Level up -> after 10 exp will level up from 0 -> 1
        for(int i = 1; i < p_PlayerExpLevels.Length - 1; i ++)
        {   
            p_PlayerExpLevels[i] = Mathf.CeilToInt(p_PlayerExpLevels[i - 1] * 1.1f); // Each time level up Exp need to up * 1.1
        }
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

    public void ShowUpdateWeapon()
    {
        UpdateAvailableWeaponProducts();
        foreach (LevelUpSelectionButton button in uIController.LevelUpButtons)
        {
            WeaponProduct? randomWeaponProduct = GetRandomWeaponProduct();
            
            if(randomWeaponProduct.HasValue)
            {   
                IWeaponProduct weaponProduct = weaponProducts.FirstOrDefault(weapon => weapon.WeaponTypeSelected == randomWeaponProduct.Value);;
                int weaponLevel = assignWeapon.GetWeaponLevel(randomWeaponProduct.Value) + 1;
                if(weaponProduct != null && weaponLevel > 0)
                {
                    WeaponFactory weaponFactory = assignWeapon.GetWeaponFactory(randomWeaponProduct.Value);
                    button.UpdateButtonDisplay(weaponFactory.Stats[weaponLevel].UpgradeText, weaponLevel, weaponProduct);
                } else 
                {
                    Debug.Log("randomWeaponProduct.Value: " + randomWeaponProduct.Value + "weaponLevel: " + weaponLevel);
                }
            }
            else
            {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void GetExp(int amount)
    {
        p_CurrentPlayerExp += amount;

        if(p_CurrentPlayerExp >= p_PlayerExpLevels[p_PlayerCurrentLvl])
        {
            PlayerLevelUp();
        }

        uIController.UpdateExp(p_CurrentPlayerExp, p_PlayerExpLevels[p_PlayerCurrentLvl], p_PlayerCurrentLvl);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(expItem, position, Quaternion.identity).expValue = expValue;
    }

    public void PlayerLevelUp()
    {
        p_CurrentPlayerExp -= p_PlayerExpLevels[p_PlayerCurrentLvl];
        
        p_PlayerCurrentLvl++;

        if(p_PlayerCurrentLvl >= p_PlayerExpLevels.Length)
        {
            p_PlayerCurrentLvl = p_PlayerExpLevels.Length - 1;
        }

        // Show level up panel
        uIController.levelUpPanel.SetActive(true);

        Time.timeScale = 0f; // pause the game

        ShowUpdateWeapon();
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
        assignWeapon.TrySpawnWeapon(weaponProduct);
    }
}
