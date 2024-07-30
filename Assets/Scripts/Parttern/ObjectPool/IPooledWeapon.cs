using UnityEngine.Pool;

public interface IPooledWeapon
{
    IObjectPool<IPooledWeapon> Pool { get; set; }
}
