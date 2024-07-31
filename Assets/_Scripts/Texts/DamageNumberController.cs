using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviourSingleton<DamageNumberController>
{
    public DamageText numberToSpawn;
    public Transform numberCanvas;

    public void SpawnDamage(float damageAmount, Vector3 location)
    {
        int rounded =  Mathf.RoundToInt(damageAmount);
                                            //Quaternion.identity -> represents a rotation with no rotation
        DamageText newDamanage = Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);
        
        newDamanage.Setup(rounded);
        newDamanage.gameObject.SetActive(true);
    }
}
