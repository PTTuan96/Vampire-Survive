using UnityEngine;
using static WeaponEnums;

public interface IWeaponProduct
{
    string HolderWeaponName { get; set; }
    string ProductWeaponName { get; set; }
    Sprite SpriteRenderer  { get; set;}
    WeaponProduct WeaponTypeSelected { get; set; }

    void Initialize();
    // void UpdateStats(float angle, float damage, float range, float speed);
    bool IsSelectedWeapon(WeaponProduct weaponProduct);
    void UpdateStats(float angle, Attributes attributes);
}
