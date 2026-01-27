using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script Game Manager. Se encarga de centralizar operaciones como llamar al canvas, poner el juego en pausa, controlar el estado de la partida...
/// </summary>
public class GMTutorial : LevelManager
{
    [SerializeField]
    GameObject manoMapa;

    public GameObject exitButton, recenterButton, mapButton;
    public GameObject manoPath, manoCombustible, manoPerspectiva, manoRecentrar;
    public GameObject[] cartelesTutorial;
    float timeBlocked = 25;

    /// <summary>
    /// Cartel recordatorio por si el usuario se bloquea.
    /// </summary>
    public GameObject recordatorio;
    int indTutorial;

    void Awake()
    {
        recenterButton.SetActive(false);
        manoMapa.SetActive(false);
        manoPath.SetActive(false);
        manoPerspectiva.SetActive(false);
        manoCombustible.SetActive(false);
        manoRecentrar.SetActive(false);
        foreach (GameObject go in cartelesTutorial) go.gameObject.SetActive(false);
        indTutorial = 0;
    }


    /// <summary>
    /// Se llama cuando se pulsa el botón del mapa
    /// </summary>
    //public void OnMapClicked(GameObject texto)
    //{
    //    //Debug.Log(indTutorial);
    //    if (indTutorial == 0 || indTutorial == 6 || indTutorial >= cartelesTutorial.Length)
    //    {
    //        actualizaTutorial();
    //    }
    //    else return;

    //    if (num > 0 && !finished)
    //    {
    //        paused = !paused;
    //        car.GetComponent<Car>().OnPause();
    //        car.transform.Find("Posicion").gameObject.SetActive(paused);
    //        cameraPausa.SetActive(paused);
    //        cameraPrincipal.SetActive(!paused);


    //        if (paused)
    //        {
    //            ImageConsumo.SetActive(false);
    //            metaO.GetComponent<MeshRenderer>().enabled = true;
    //            x = Mathf.FloorToInt(car.transform.position.x);
    //            y = Mathf.FloorToInt(-car.transform.position.y);
    //            Posicion pos = car.GetComponentInChildren<Car>().UltimaCasilla();

    //            if (texto != null) texto.GetComponent<Text>().text = (num - 1).ToString();

    //            mapa[pos.y, pos.x] = 100000;
    //            Find(x, y, true);
    //        }
    //        else
    //        {
    //            manoMapa.SetActive(false);
    //            ImageConsumo.SetActive(true);
    //            Find(x, y, false);
    //            Posicion pos = car.GetComponentInChildren<Car>().UltimaCasilla();
    //            mapa[pos.y, pos.x] = 1;
    //            if (!first)
    //                num--;
    //            else
    //                first = false;

    //            contexto.SetActive(false);
    //            metaO.GetComponent<MeshRenderer>().enabled = false;

    //        }

    //    }

    //}

    /// <summary>
    /// Actualiza el tutorial mostrando y ocultando los elementos opertunos
    /// </summary>
    void actualizaTutorial()
    {
        switch (indTutorial)
        {
            case 0:
                cartelesTutorial[indTutorial].SetActive(true);
                break;
            case 1:
                cartelesTutorial[indTutorial - 1].SetActive(false);
                cartelesTutorial[indTutorial].SetActive(true);
                break;
            case 2:
                manoMapa.SetActive(false);
                cartelesTutorial[indTutorial - 1].SetActive(false);
                manoPath.SetActive(true);
                cartelesTutorial[indTutorial].SetActive(true);
                break;
            case 3:
                cartelesTutorial[indTutorial - 1].SetActive(false);
                manoPath.SetActive(false);
                cartelesTutorial[indTutorial].SetActive(true);
                break;
            case 4:
                cartelesTutorial[indTutorial - 1].SetActive(false);
                manoPath.SetActive(false);
                manoPerspectiva.SetActive(true);
                cartelesTutorial[indTutorial].SetActive(true);
                break;
            case 5:
                cartelesTutorial[indTutorial - 1].SetActive(false);
                manoMapa.SetActive(true);
                manoPerspectiva.SetActive(false);
                cartelesTutorial[indTutorial].SetActive(true);
                break;
            case 6:
                cartelesTutorial[indTutorial - 1].SetActive(false);
                manoMapa.SetActive(false);
                cartelesTutorial[indTutorial].SetActive(true);
                break;
            case 7:
                cartelesTutorial[indTutorial - 1].SetActive(false);
                manoCombustible.SetActive(true);
                cartelesTutorial[indTutorial].SetActive(true);
                break;
            case 8:
                cartelesTutorial[indTutorial - 1].SetActive(false);
                manoCombustible.SetActive(false);
                manoRecentrar.SetActive(true);
                recenterButton.SetActive(true);
                cartelesTutorial[indTutorial].SetActive(true);
                break;
            case 9:
                cartelesTutorial[indTutorial - 1].SetActive(false);
                recenterButton.SetActive(false);
                manoRecentrar.SetActive(false);
                exitButton.SetActive(true);
                break;

        }
        indTutorial++;
    }
    private void Update()
    {
        if (indTutorial <= cartelesTutorial.Length && indTutorial != 6 && Input.GetMouseButtonDown(0)) actualizaTutorial();
        if (Input.GetMouseButtonDown(0))
        {
            timeBlocked = 10;
            recordatorio.SetActive(false);
        }
        if (timeBlocked <= 0 && indTutorial < cartelesTutorial.Length)
        {
            recordatorio.SetActive(true);
            timeBlocked = 10;
        }
        else
        {
            timeBlocked -= Time.deltaTime;
        }
    }
}
