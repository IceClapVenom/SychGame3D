using UnityEngine;

public class Entity : MonoBehaviour
{
    public float health = 100;



    public void Damage(float amount)
    {
        health -= amount;
        OnTakeDamage(amount);

        if (health <= 0) Kill();

    }

    public virtual void OnTakeDamage(float amount) { }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }
}
