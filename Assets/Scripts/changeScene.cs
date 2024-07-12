using RAGE.Analytics;
using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xasu;

public class changeScene : MonoBehaviour
{
    public Animator transicion;
    public void Change(string scene)
    {
        if (scene != "exit")
            StartCoroutine(LoadScene(scene));
        else
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
    }
    public void LoadCredits()
    {
        if (XasuTracker.Instance.Status.State != TrackerState.Uninitialized)
            Xasu.HighLevel.AccessibleTracker.Instance.Accessed("Credits", Xasu.HighLevel.AccessibleTracker.AccessibleType.Screen);
      
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadScene(string scene)
    {
        if(SceneManager.GetActiveScene().name != "Start" && XasuTracker.Instance.Status.State != TrackerState.Uninitialized)
            Xasu.HighLevel.AccessibleTracker.Instance.Accessed(scene, Xasu.HighLevel.AccessibleTracker.AccessibleType.Screen);
        if(!transicion.gameObject.activeInHierarchy)
            transicion.gameObject.SetActive(true);
        transicion.Play("ExitScene");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }
 


}