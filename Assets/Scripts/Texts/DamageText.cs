using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TMP_Text damageText; 

    public float lifetime;
    private float lifeCounter; 

    // Start is called before the first frame update
    void Start()
    {
        lifeCounter = lifetime;
    }

    // Update is called once per frame
    void Update()
    {
        if(lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;
            if(lifeCounter <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void Setup(int damageDisplay)
    {
        lifeCounter = lifetime;

        damageText.text = damageDisplay.ToString();
    }
}
