using UnityEngine;

public class PlayerEntity : Entity
{
    private float _defaultHealth;

    private void Awake() => _defaultHealth = health;
    private void Start() => InterfaceLogic.Instance.SetHealthBar((int)_defaultHealth);

    public override void Kill()
    {
        InterfaceLogic.Instance.SetHealthBar((int)_defaultHealth);
        GameManager.ReloadScene();
    }

    public override void OnTakeDamage(float amount) => InterfaceLogic.Instance.SetHealthBar((int)health);
}
