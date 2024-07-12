using RAGE.Analytics;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xasu;

public class changeScene : MonoBehaviour
{
    public Animator transicion;
    /// <summary>
    /// Cambia a otra escena o sale del juego
    /// </summary>
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
    /// <summary>
    /// Carga los creditos
    /// </summary>
    public void LoadCredits()
    {
        if (XasuTracker.Instance.Status.State != TrackerState.Uninitialized)
            Xasu.HighLevel.AccessibleTracker.Instance.Accessed("Credits", Xasu.HighLevel.AccessibleTracker.AccessibleType.Screen);
      
    }

    /// <summary>
    /// Vuelve a cargar la escena
    /// </summary>
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// SCarga una escena
    /// </summary>
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