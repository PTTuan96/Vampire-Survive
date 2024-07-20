using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleToUnderStand
{
    void Start()
    {
        BigFactory zombieSpawner = new ZombiSmallFactorySpawner();
        IProductActions zombie = zombieSpawner.EachProductActions();
        zombie.Attack();  // ----->  Outputs: Zombie attacks!

        BigFactory alienSpawner = new AlienSmallFactorySpawner();
        IProductActions alien = alienSpawner.EachProductActions();
        alien.Attack();  // ----->  Outputs: Alien attacks!
    }
}

public interface IProductActions
{
    // Combat actions
    void Attack();
    // void Defend();

    // // State management
    // void TakeDamage(int damage);
    // void Heal(int amount);
    // bool IsAlive();

    // // Movement and positioning
    // void Move(Vector3 direction, float speed);
    // void Stop();
    // Vector3 GetPosition();

    // // Interactions
    // void Interact(GameObject other);

    // // Utility methods
    // void LogStatus();
    // string GetName();
}

public interface ISomething
{
    void DoSomething();
}

public abstract class BigFactory : MonoBehaviour
{
    // Abstract method to get a product instance.
    public abstract IProductActions EachProductActions();

    //If related -> add to this factory
    //Else just create another factory
    public abstract ISomething Something();

    // OR
    public ISomething CreateSomething()
    {
        // return new Something();
        return null;
    }







    // Health property
    public int Health { get; protected set; }

    // Reference to Animator component
    protected Animator animator;

    // Method called when the entity is spawned
    public virtual void OnSpawn()
    {
        Debug.Log("Entity has spawned.");
    }

    // Method called when the entity is destroyed
    public virtual void OnDestroy()
    {
        Debug.Log("Entity is destroyed.");
    }

    // Method to take damage
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        Debug.Log($"Entity took {damage} damage, remaining health: {Health}");
        if (Health <= 0)
        {
            OnDeath();
        }
    }

    // Method to handle death
    protected virtual void OnDeath()
    {
        Debug.Log("Entity died.");
        // Additional death handling logic
    }

    // Method to heal
    public virtual void Heal(int amount)
    {
        Health += amount;
        Debug.Log($"Entity healed by {amount}, total health: {Health}");
    }

    // Method to move the entity
    public virtual void Move(Vector3 direction, float speed)
    {
        transform.position += direction * speed * Time.deltaTime;
        Debug.Log($"Entity moved in direction {direction} with speed {speed}");
    }

    // Method to stop the entity
    public virtual void Stop()
    {
        Debug.Log("Entity stopped.");
        // Additional stop handling logic
    }

    // Method to initialize the animator
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Method to play an animation
    public virtual void PlayAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.Play(animationName);
            Debug.Log($"Playing animation: {animationName}");
        }
        else
        {
            Debug.LogWarning("Animator component is missing.");
        }
    }

    // Method to stop an animation
    public virtual void StopAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.StopPlayback();
            Debug.Log($"Stopped animation: {animationName}");
        }
        else
        {
            Debug.LogWarning("Animator component is missing.");
        }
    }

    // Method to log entity info
    public virtual void LogInfo()
    {
        Debug.Log($"Entity: {gameObject.name}, Position: {transform.position}");
    }

    // Method to reset the entity's position
    public virtual void ResetPosition()
    {
        transform.position = Vector3.zero;
        Debug.Log("Entity position reset.");
    }
}

public class ZombiSmallFactorySpawner : BigFactory
{
    public override IProductActions EachProductActions()
    {
        return new Zombie();
    }

    public override ISomething Something()
    {
        throw new System.NotImplementedException();
    }
}

public class AlienSmallFactorySpawner : BigFactory
{
    public override IProductActions EachProductActions()
    {
        return new Alien();
    }

    public override ISomething Something()
    {
        throw new System.NotImplementedException();
    }
}

public class Zombie : MonoBehaviour, IProductActions
{
    private int health = 100;

    public void Attack()
    {
        Debug.Log("Zombie attacks!");
    }

    public void Defend()
    {
        Debug.Log("Zombie defends!");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Zombie took {damage} damage, remaining health: {health}");
    }

    public void Heal(int amount)
    {
        health += amount;
        Debug.Log($"Zombie healed by {amount}, total health: {health}");
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void Move(Vector3 direction, float speed)
    {
        transform.position += direction * speed * Time.deltaTime;
        Debug.Log($"Zombie moved in direction {direction} with speed {speed}");
    }

    public void Stop()
    {
        Debug.Log("Zombie stopped.");
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Interact(GameObject other)
    {
        Debug.Log("Zombie interacts with " + other.name);
    }

    public void LogStatus()
    {
        Debug.Log($"Zombie status: {health} health.");
    }

    public string GetName()
    {
        return "Zombie";
    }
}

public class Alien : MonoBehaviour, IProductActions
{
    private int health = 150;

    public void Attack()
    {
        Debug.Log("Alien attacks!");
    }

    public void Defend()
    {
        Debug.Log("Alien defends!");
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"Alien took {damage} damage, remaining health: {health}");
    }

    public void Heal(int amount)
    {
        health += amount;
        Debug.Log($"Alien healed by {amount}, total health: {health}");
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void Move(Vector3 direction, float speed)
    {
        transform.position += direction * speed * Time.deltaTime;
        Debug.Log($"Alien moved in direction {direction} with speed {speed}");
    }

    public void Stop()
    {
        Debug.Log("Alien stopped.");
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Interact(GameObject other)
    {
        Debug.Log("Alien interacts with " + other.name);
    }

    public void LogStatus()
    {
        Debug.Log($"Alien status: {health} health.");
    }

    public string GetName()
    {
        return "Alien";
    }
}



