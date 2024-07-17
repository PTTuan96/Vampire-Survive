using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogicTest : MonoBehaviour
{
    // public Enemy enemy;
    public Player player;

    private void Update()
    {
        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     enemy.ApplyDamage(10);
        // }

        // if (Input.GetKeyDown(KeyCode.H))
        // {
        //     enemy.ApplyHeal(5);
        // }

        if (Input.GetKeyDown(KeyCode.J))
        {
            // player.ApplyDamage(10);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            // player.ApplyHeal(5);
        }
    }
}
