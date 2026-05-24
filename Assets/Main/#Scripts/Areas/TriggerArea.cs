using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerArea : MonoBehaviour
{
    private Dictionary<Entity, float> _spareEntitis= new(); // Избегаем инстакила от ловушки ДААА РАБОТАЕТ
    public float spareFor = 0;

    private void OnTriggerEnter(Collider collider) => ProcessCollision(collider);

    

    private void ProcessCollision(Collider collider)
    {
        Entity entity = GetCollidersEntity(collider);
        if (entity == null) return;


        if(spareFor > 0)
        {
            if (!_spareEntitis.ContainsKey(entity)) // Щадим если таймер идёт
            {
                OnEntityEnter(entity); // Не щадим если таймера нет
                _spareEntitis.Add(entity, spareFor);
            }
        }
        else
        {
            OnEntityEnter(entity); // Просто оюрабатываем если таймера нет
        }

    }

    private Entity GetCollidersEntity(Collider collider)
    {
        //Debug.Log("Touch!");
        GameObject target = collider.gameObject;
        Entity entity = target.GetComponent<Entity>();

        return entity;
    }

    private void Update()
    {
        List<Entity> markedRemoval = new();
        List<Entity> markedCount = new();

        foreach (KeyValuePair<Entity, float> pair in _spareEntitis) // Разрешаем если таймер прошел
        {
            Entity entity = pair.Key;
            if (pair.Value < 0) markedRemoval.Add(entity);
            else markedCount.Add(entity);
        }

        for (int i = 0; i < markedCount.Count; i++)
        {
            _spareEntitis[markedCount[i]] -= Time.deltaTime;
        }
        for (int i = 0; i < markedRemoval.Count; i++)
        {
            _spareEntitis.Remove(markedRemoval[i]);
        }
    }



    public virtual void OnEntityEnter(Entity entity) { }
}
