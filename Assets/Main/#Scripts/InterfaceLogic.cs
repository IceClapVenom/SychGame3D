using TMPro;
using UnityEngine;

public class InterfaceLogic : MonoBehaviour
{
    public static InterfaceLogic Instance;
    public GameObject healthbar;
    private TextMeshProUGUI text;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.LogError("More that 1 InterfaceLogic objects detected!");

        gameObject.SetActive(false);
        //if (healthbar != null) 
            text = healthbar.GetComponent<TextMeshProUGUI>();
    }

    public void Retry() => GameManager.ReloadScene();
    public void Exit() => GameManager.Exit();
    public void SetHealthBar(int health)
    {
        if (healthbar != null) 
            text.text = $"Health: {health}";
    }
}
