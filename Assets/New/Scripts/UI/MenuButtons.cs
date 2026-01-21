using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    /// <summary>
    /// Instancia del GameManager
    /// </summary>
    GameManager gameManager;

    [SerializeField] Transition transition;


    //Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void ChangeScene(string sceneName)
    {
        transition.ChangeScene(sceneName);
    }

    public void ExitGame()
    {
        gameManager.ExitGame();
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
