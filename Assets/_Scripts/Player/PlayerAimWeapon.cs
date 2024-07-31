using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAimWeapon : MonoBehaviour
{
    // public UnityEvent<OnThrowEventArgs> OnThrow;
    // public delegate void ThrowKnife(OnThrowEventArgs onThrowEventArgs);
    // public event ThrowKnife OnThrow;
    // public class OnThrowEventArgs : EventArgs {
    //     public Vector3 endPointPositions;
    //     public Vector3 throwPosition;
    // }

    private Transform aimTransform;
    private bool isThrowing = false; // Flag to check if throwing is already in progress

    [SerializeField] private ProjectileWeaponsFactory knifePool;
    [SerializeField] private float timeoutDelay = 0.5f; // Delay before throwing the knife

    private void Awake()
    {
        aimTransform = FindChildByName(transform, "ProjectileWeapons");
    }

    private void Update()
    {
        HandleAiming();

        if (Input.GetKeyDown(KeyCode.Space) && !isThrowing)
        {
            StartCoroutine(HandleThrowing());
        }
    }

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(-aimDirection.x, aimDirection.y) * Mathf.Rad2Deg; // Opposite -> change place aimDirection.y and aimDirection.x

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        aimTransform.rotation = Quaternion.Slerp(aimTransform.rotation, targetRotation, 10f * Time.deltaTime);
    }

    private IEnumerator HandleThrowing()
    {
        isThrowing = true;

        // Get a knife from the pool
        Knife knife = knifePool.GetKnife();

        // Set the position and direction of the knife
        knife.transform.position = transform.position;
        Vector3 direction = (aimTransform.position - transform.position).normalized;

        // Launch the knife
        knife.Launch(direction);

        isThrowing = false; // Reset the flag to allow the next throw

        yield return new WaitForSeconds(timeoutDelay);
    }

    private Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindChildByName(child, name);
            if (result != null)
                return result;
        }
        return null;
    }
}
