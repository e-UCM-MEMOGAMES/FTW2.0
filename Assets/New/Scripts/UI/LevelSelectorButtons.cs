using UnityEngine;

public class LevelSelectorButtons : MonoBehaviour
{
    /// <summary>
    /// Instancia del GameManager
    /// </summary>
    GameManager gameManager;

    /// <summary>
    /// Elementos de la seleccion de nivel
    /// </summary>
    [SerializeField]
    GameObject levels,
    /// <summary>
    /// Elementos de la seleccion de mapa
    /// </summary>
    maps;

    [SerializeField]
    int minStarsForNextLevel = 0,
        neededLevelsWithMinStars = 2,
        minStarsPerLevel = 0;

    [SerializeField]
    LevelButton[] levelButtons;

    [SerializeField]
    MapButton[] mapButtons;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;

        levels.SetActive(true);
        maps.SetActive(false);

        string prevLvl = "";
        string currLvl = "";

        int beatenMapsPrevLevel = 0;

        for (int levelNumber = 0; levelNumber < levelButtons.Length; levelNumber++)
        {
            for (int mapNumber = 0; mapNumber < mapButtons.Length; mapNumber++)
            {
                mapButtons[mapNumber].Setup(minStarsForNextLevel);

                prevLvl = Defs.GetLevelKey(levelNumber, mapNumber);
                currLvl = Defs.GetLevelKey(levelNumber, mapNumber + 1);

                bool mapUnlocked = mapNumber == 0 || (PlayerPrefs.HasKey(prevLvl) && PlayerPrefs.GetInt(prevLvl) >= minStarsForNextLevel);

                if (mapUnlocked && PlayerPrefs.GetInt(currLvl) >= minStarsPerLevel)
                {
                    beatenMapsPrevLevel++;
                }
            }
            bool levelUnlocked = levelNumber == 0 || beatenMapsPrevLevel >= neededLevelsWithMinStars;

            levelButtons[levelNumber].Unlock(levelUnlocked);
        }
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
        gameManager.Level = 0;
        gameManager.Map = 0;

        gameManager.ChangeScene(Defs.TUTORIAL_SCENE_NAME);
    }

    /// <summary>
    /// Llamado por los botones de nivel
    /// </summary>
    public void SelectLevel(int level)
    {
        gameManager.Level = level;
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
        gameManager.ChangeScene(Defs.GetLevelKey(gameManager.Level, gameManager.Map));
    }
}
