using System;
using System.Collections;
using System.Collections.Generic;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAimWeapon : MonoBehaviour
{
    // public UnityEvent<OnThrowEventArgs> OnThrow;
    public delegate void ThrowKnife(OnThrowEventArgs onThrowEventArgs);
    public event ThrowKnife OnThrow;
    public class OnThrowEventArgs : EventArgs {
        public Vector3 endPointPositions;
        public Vector3 throwPosition;
    }

    private Transform aimTransform;

    private void Awake()
    {
        aimTransform = FindChildByName(transform, "ProjectileWeapons");
    }

    private void Update()
    {
        HandleAiming();
        HandleThrowing();
    }

    private void HandleAiming()
    {
        // Persoective -> aim by movement
        // Orthographic -> aim by mouse
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDerection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(-aimDerection.x, aimDerection.y) * Mathf.Rad2Deg; // oposite -> change place aimDerection.y and aimDerection.x

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        aimTransform.rotation = Quaternion.Slerp(aimTransform.rotation, targetRotation, 10f * Time.deltaTime);

        Debug.Log(angle);
    }

    [SerializeField] private ProjectileWeaponsFactory knifePool;

    private void HandleThrowing()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            // Get a knife from the pool
            Knife knife = knifePool.GetKnife();

            // Set the position and direction of the knife
            knife.transform.position = transform.position;
            // Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 direction = aimTransform.position;

            
            // Launch the knife
            knife.Launch(direction);
            

            // Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            // OnThrow?.Invoke(new OnThrowEventArgs {
            //     endPointPositions = aimTransform.position,
            //     throwPosition = mousePosition
            // });
        }
    }

    private Transform FindChildByName(Transform parent, string name)
    {
        // Iterate through each child of the parent transform
        foreach (Transform child in parent)
        {
            // Check if the child's name matches the name we are looking for
            if (child.name == name)
                return child;

            // Recursively search in the child's children
            Transform result = FindChildByName(child, name);
            if (result != null)
                return result;
        }
        return null; // Return null if no child with the specified name is found
    }
}
