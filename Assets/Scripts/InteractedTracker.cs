﻿//using RAGE.Analytics;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Xasu;

public class InteractedTracker : MonoBehaviour
{

    // Use this for initialization
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Se comprueba si en el punto del mouse al hacer click hay colisión con algún objeto. Se devuelven todos los objetos en result.
            Collider2D[] result = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

            int i = result.Length;
            if (i == 0)
            {
                //Tracker.T.setVar("empty", 1);
            }
            else
            {
                while (i > 0)
                {
                    i--;
                    string objName = result[i].name;
                    if (objName != null) { }
                        //Tracker.T.setVar(objName, 1);
                }
            }

            //Return the current Active Scene in order to get the current Scene's name
            Scene scene = SceneManager.GetActiveScene();
            string name = scene.name;

            Xasu.HighLevel.GameObjectTracker.Instance.Interacted(name);
            //Tracker.T.GameObject.Interacted(name);
        }
    }

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