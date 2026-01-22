using UnityEngine;


public class Transition : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField]
    Animator animator;

    [SerializeField]
    AnimationClip exitClip;

    string nextScene = "";
    
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void ChangeScene(string sceneName)
    {
        nextScene = sceneName;
        animator.Play(exitClip.name);
    }

    public void OnAnimationEnd()
    {
        gameManager.ChangeScene(nextScene);
    }
}
