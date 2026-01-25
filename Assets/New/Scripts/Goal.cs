using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField]
    LevelManager levelManager;

    public void OnTriggerEnter(Collider other)
    {
        levelManager.Win();
    }
}
