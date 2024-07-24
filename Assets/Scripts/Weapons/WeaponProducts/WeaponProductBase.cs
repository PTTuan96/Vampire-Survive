using UnityEngine;
using static WeaponEnums;

public class WeaponProductBase : MonoBehaviour
{
    // [Tooltip("Why this cannot attached by any Gameobject?")]
    // [SerializeField] private GameObject holderPrefab;
    [SerializeField] protected ParticleSystem p_ParticleSystem;
    [SerializeField] protected Sprite p_SpriteRenderer; 
    [SerializeField] protected float s_DamageMultiple;
    [SerializeField] protected float s_RangeMultiple;
    [SerializeField] protected float s_SpeedMultiple;
    [SerializeField] protected bool shouldKnockBack; 

    public void Initialize(string p_ProductName)
    {
        // Add any unique set up logic here
        gameObject.name = p_ProductName;

        p_ParticleSystem = GetComponentInChildren<ParticleSystem>();

        if (p_ParticleSystem != null)
        {
            p_ParticleSystem.Stop();
            p_ParticleSystem.Play();
        }
    }
}
