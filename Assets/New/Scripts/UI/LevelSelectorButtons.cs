using UnityEngine;

public class LevelSelectorButtons : MonoBehaviour
{
    /// <summary>
    /// Instancia del GameManager
    /// </summary>
    GameManager gameManager;

    [SerializeField]
    /// <summary>
    /// Elementos de la seleccion de nivel
    /// </summary>
    GameObject levels,
    /// <summary>
    /// Elementos de la seleccion de mapa
    /// </summary>
    maps;

    int selectedLevel = 0,
        selectedMap = 0;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        levels.SetActive(true);
        maps.SetActive(false);
    }


    /// <summary>
    /// Llamado por el boton de volver
    /// </summary>
    public void Return()
    {
        // Si se esta eligiendo mapa, se ocultan los elementos de la
        // seleccion de mapa y se muestran los de la seleccion de nivel
        if (maps.activeSelf)
        {
            levels.SetActive(true);
            maps.SetActive(false);
        }
        // Si no, se vuelve al menu principal
        else
        {
            gameManager.ChangeScene(Defs.MENU_SCENE_NAME);
        }
    }

    /// <summary>
    /// Llamado por el boton de tutorial
    /// </summary>
    public void StartTutorial()
    {
        SelectMap();
    }

    /// <summary>
    /// Llamado por los botones de nivel
    /// </summary>
    public void SelectLevel(int level)
    {
        selectedLevel = level;
        // Se ocultan los elementos de seleccion de nivel y se muestran los de seleccion de mapa
        levels.SetActive(false);
        maps.SetActive(true);
    }

    /// <summary>
    /// Llamado por los botones de los mapas
    /// </summary>
    public void SelectMap()
    {
        // Cambia a la escena de juego
        gameManager.ChangeScene(Defs.GetLevelKey(selectedLevel, selectedMap));
    }
}
