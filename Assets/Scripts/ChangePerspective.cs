using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangePerspective : MonoBehaviour {
    /// <summary>
    /// Sprite del boton para cambiar de perspectiva
    /// </summary>
    public Sprite isoSprite, cenitSprite;
    /// <summary>
    /// Camaras del juego
    /// </summary>
    public Camera cameraPauseIso, cameraPauseCenit, cameraGameIso, cameraGameCenit;
    /// <summary>
    /// Indica si la vista esta en isometrica
    /// </summary>
    bool iso = false;
    /// <summary>
    /// Boton para cambiar de perspectiva
    /// </summary>
    public Button button;

	void Start () {
        Change_Perspective();
	}

    /// <summary>
    /// Pone vista en isometrica
    /// </summary>
    public void ResetCamera()
    {
        if (!iso)
            Change_Perspective();
    }

    /// <summary>
    /// Cambia la perspectiva de la camara
    /// </summary>
    public void Change_Perspective() {
        iso = !iso;
        cameraPauseCenit.gameObject.SetActive(!iso);
        cameraGameCenit.gameObject.SetActive(!iso);
        cameraPauseIso.gameObject.SetActive(iso);
        cameraGameIso.gameObject.SetActive(iso);
        if (!iso) button.GetComponent<Image>().sprite = isoSprite;
        else button.GetComponent<Image>().sprite = cenitSprite;
    }
}
