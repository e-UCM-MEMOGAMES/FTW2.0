using System;
using System.Threading.Tasks;
using UnityEngine;
using Xasu;
using Xasu.Config;

public class InitTracker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    /// <summary>
    /// Inicializa el tracker
    /// </summary>
    private async void Init()
    {
        bool hasConfig = false;
        try
        {
            var trackerConfig = await TrackerConfigLoader.LoadLocalAsync();
            hasConfig = true;
        }
        catch { Debug.Log("Tracker config not found."); }
        if(hasConfig)
        {
            await XasuTracker.Instance.Init();
            await Task.Yield();
            while (XasuTracker.Instance.Status.State == TrackerState.Uninitialized)
            {
                await Task.Yield();
            }
        }
    }

    private async void OnApplicationQuit()
    {
        if (XasuTracker.Instance.Status.State != TrackerState.Uninitialized)
            await CloseTracker();
    }

    /// <summary>
    /// Cierra el tracker
    /// </summary>
    private async Task CloseTracker() 
    {
        var progress = new Progress<float>();
        progress.ProgressChanged += (_, p) =>
        {
            Debug.Log("Finalization progress: " + p);
        };
        await XasuTracker.Instance.Finalize(progress);
        Debug.Log("Tracker finalized");
        Application.Quit();
    }
}
