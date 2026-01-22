using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    /// <summary>
    /// Instancia del GameManager
    /// </summary>
    GameManager gameManager;

    [SerializeField] 
    Transition transition;

    [SerializeField] 
    GameObject transitionBgBlock;


    //Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        transitionBgBlock.SetActive(false);
    }

    public void ChangeScene(string sceneName)
    {
        transitionBgBlock.SetActive(true);
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
