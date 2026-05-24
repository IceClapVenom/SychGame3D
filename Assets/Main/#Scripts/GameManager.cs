using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    static void ResetStatic() { }

    public static void ReloadScene() 
    {
        Debug.Log("@Reload scene!");
        InterfaceLogic.Instance.gameObject.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
    public static void Finish()
    {
        Debug.Log("@Finish!");
        InterfaceLogic.Instance.gameObject.SetActive(true);
    }
    public static void Exit()
    {
        Debug.Log("@Exit!");
        Application.Quit();
    }
}
