using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DamageArea : TriggerArea
{
    public float damage = 10;

    public override void OnEntityEnter(Entity entity)
    {
        entity.Damage(damage);
    }
}
