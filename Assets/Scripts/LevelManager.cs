using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Niveles del juego.
    /// </summary>
    public List<GameObject> niveles;

    /// <summary>
    /// Conjuntos de mapas de los niveles
    /// </summary>
    public List<GameObject> mapas;

    /// <summary>
    /// Nivel.
    /// </summary>
    public int level;
    IEnumerator fadeIn()
    {
        while (aS.volume <= 0.6f)
        {
            aS.volume += 0.005f;
            yield return null;
        }

    }
    AudioSource aS;
    void Start()
    {
        aS = GameObject.Find("SoundManager").GetComponent<AudioSource>();
        StartCoroutine(fadeIn());
        int index = 0;
        foreach (GameObject nivel in niveles)
        {
            nivel.gameObject.GetComponent<Button>().interactable = false;
            if (index == 0)
                nivel.gameObject.GetComponent<Button>().interactable = true;
            else
            {
                ///* Cogemos los mapas del nivel anterior donde se han superado con al menos 2 estrellas. Ejemplo: Nivel1 */
                //string mapa2Star = string.Concat("Nivel", index);

                ///* Comprobamos las condiciones para desbloquear el nivel */
                //bool desbloqueo = PlayerPrefs.HasKey(mapa2Star) && PlayerPrefs.GetInt(mapa2Star) >= 2;

                string mapa3NivAnt = string.Concat("N", index, "mapa", 3);
                bool desbloqueo = PlayerPrefs.HasKey(mapa3NivAnt);
                nivel.gameObject.GetComponent<Button>().interactable = desbloqueo;
                nivel.transform.Find("Block").gameObject.SetActive(!desbloqueo);
            }
            ++index;
        }

        /* Recorremos los conjuntos de mapas de los diferentes niveles */
        foreach (GameObject cjtoMapa in mapas)
        {
            int numMapa = 0;
            int numNivelesPasados = 0;
            string nivAnt = "";
            string nivAct = "";

            /* Recorremos cada mapa (cada botón) */
            foreach (Button mapa in cjtoMapa.transform.GetComponentsInChildren<Button>())
            {
                mapa.gameObject.GetComponent<Button>().interactable = false;
                nivAnt = string.Concat("N", level, "mapa", numMapa);
                nivAct = string.Concat("N", level, "mapa", numMapa + 1);

                if (numMapa == 0)
                {
                    mapa.gameObject.GetComponent<Button>().interactable = true;
                    numNivelesPasados++;
                }
                else
                {

                    bool desbloqueo = PlayerPrefs.HasKey(nivAnt) /*&& PlayerPrefs.GetInt(nivAnt) >= 2*/;
                    mapa.gameObject.GetComponent<Button>().interactable = desbloqueo;

                    if (desbloqueo)
                    {
                        ++numNivelesPasados;
                    }
                }

                if (PlayerPrefs.HasKey(nivAct))
                {
                    /* Recorremos todas las estrellas conseguidas en ese mapa. */
                    for (int j = 0; j < PlayerPrefs.GetInt(nivAct); ++j)
                    {
                        mapa.transform.GetChild(j).transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                ++numMapa;
            }
            string nombreNivel = string.Concat("Nivel", level);
            PlayerPrefs.SetInt(nombreNivel, numNivelesPasados);
        }
    }
}
