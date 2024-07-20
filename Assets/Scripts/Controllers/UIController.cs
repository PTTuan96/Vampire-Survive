using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIController : MonoBehaviourSingleton<UIController>
{
    public DamageText numberToSpawn;
    public Transform numberCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SpawnDamage(57f, new Vector3(4, 3, 0 ));
        }
    }

    public void SpawnDamage(float damageAmount, Vector3 location)
    {
        int rounded =  Mathf.RoundToInt(damageAmount);
                                            //Quaternion.identity -> represents a rotation with no rotation
        DamageText newDamanage = Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);
        
        newDamanage.Setup(rounded);
        newDamanage.gameObject.SetActive(true);
    }
}
