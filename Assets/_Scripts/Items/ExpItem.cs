using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : MonoBehaviour
{
    private ExperienceController experienceController;

    public int expValue;

    [SerializeField] private float moveSpeed;
    private bool movingToPlayer;

    [SerializeField] private float timeBetweenChecks = .2f;
    private float checkCounter;

    private Vector3 playerPosition;

    private void Awake()
    {
        experienceController = ExperienceController.Instance;
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (player.TryGetComponent<PlayerMovement>(out var playerMovement))
            {   
                playerPosition = playerMovement.transform.position;
                if(movingToPlayer)
                {
                    transform.position = Vector3.MoveTowards(transform.position, playerPosition, moveSpeed * Time.deltaTime);
                } else
                {
                    checkCounter -= Time.deltaTime;
                    if(checkCounter <= 0)
                    {
                        checkCounter = timeBetweenChecks;

                        if(Vector3.Distance(transform.position, playerPosition) < playerMovement.pickUpRange)
                        {
                            movingToPlayer = true;
                            moveSpeed += playerMovement.m_Speed;
                        }
                    }
                }
            }
            else
            {
                Debug.LogWarning("PlayerMovement component not found on Player.");
            } 
        } else
        {
            Debug.LogWarning("Player not found!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D) 
    {
        if(collider2D.tag == "Player")
        {
            experienceController.GetExp(expValue);

            Destroy(gameObject);
        }
    }
}
