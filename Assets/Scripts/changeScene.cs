using RAGE.Analytics;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadScene(string scene)
    {
        //Tracker.T.Accessible.Accessed(scene, AccessibleTracker.Accessible.Screen);
        if(!transicion.gameObject.activeInHierarchy)
            transicion.gameObject.SetActive(true);
        transicion.Play("ExitScene");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(scene);
    }

}