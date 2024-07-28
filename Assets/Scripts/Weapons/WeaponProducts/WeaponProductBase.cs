using UnityEngine;
using static WeaponEnums;

public abstract class WeaponProductBase : MonoBehaviour
{
    // [Tooltip("Why this cannot attached by any Gameobject?")]
    // [SerializeField] private GameObject holderPrefab;
    [SerializeField] protected ParticleSystem p_ParticleSystem;
    [SerializeField] protected Sprite p_SpriteRenderer; 
    [SerializeField] protected float s_DamageMultiple;
    [SerializeField] protected float s_RangeMultiple;
    [SerializeField] protected float s_SpeedMultiple;   

    protected bool CheckIfChildAtPosition(Transform parent, Vector3 position)
    {
        foreach (Transform child in parent)
        {
            if (child.position == position)
            {
                return true;
            }

            // Optionally, check the child's children recursively
            if (CheckIfChildAtPosition(child, position))
            {
                return true;
            }
        }
        return false;
    }

    public void Initialize(string p_ProductName)
    {
        // Add any unique set up logic here
        gameObject.name = p_ProductName;
        gameObject.transform.localScale = Vector3.zero;

        p_ParticleSystem = GetComponentInChildren<ParticleSystem>();

        if (p_ParticleSystem != null)
        {
            p_ParticleSystem.Stop();
            p_ParticleSystem.Play();
        }
    }
}
