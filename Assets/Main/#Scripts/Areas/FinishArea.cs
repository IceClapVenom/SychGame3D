using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FinishArea: TriggerArea
{
    public override void OnEntityEnter(Entity entity)
    {
        Debug.Log("d");
        if(entity.GetType() == typeof(PlayerEntity))
            GameManager.Finish();
    }
}
