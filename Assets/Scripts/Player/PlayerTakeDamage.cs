using UnityEngine;

// This class will apply for multiple players
public class PlayerTakeDamage : ObjectTakeDamage, IPlayerDieAnimation
{
    [Tooltip("Effect to instantiate on explosion")]
    // [SerializeField] GameObject m_ExplosionPrefab;

    public override void Die()
    {
        base.Die();
        DieAnimation();
    }

    public void DieAnimation()
    {
        // if (m_ExplosionPrefab)
        // {
        //     GameObject instance = Instantiate(m_ExplosionPrefab, transform.position, quaternion.identity);
        // }

        // Add custom explosion logic here
    }
}
