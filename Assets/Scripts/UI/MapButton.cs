using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MapButton : MonoBehaviour
{
    /// <summary>
    /// Instancia del GameManager
    /// </summary>
    GameManager gameManager;

    /// <summary>
    /// Componente Button del objeto para bloquear la pulsacion si el mapa esta bloqueado
    /// </summary>
    [SerializeField]
    Button button;

    /// <summary>
    /// Texto con el numero del mapa
    /// </summary>
    [SerializeField]
    TextMeshProUGUI numberTxt;

    [SerializeField]
    Image[] stars;

    Color starUnlockedColor = Color.white;
    [SerializeField]
    Color starLockedColor;


    /// <summary>
    /// Si el nivel viene desbloqueado por defecto
    /// </summary>
    [SerializeField]
    bool preUnlocked = false;

    [SerializeField]
    int mapNumber = 0;

    int minStarsForNextLevel;


    private void Awake()
    {
        gameManager = GameManager.Instance;
    }
    void Start()
    {
        numberTxt.text = mapNumber.ToString();
        numberTxt.ForceMeshUpdate();
    }

    public void Setup(int minStarsForNext)
    {
        minStarsForNextLevel = minStarsForNext;
    }

    private void OnEnable()
    {
        string currLvl = Defs.GetLevelKey(gameManager.Level, mapNumber);

        bool unlocked = true;
        if (mapNumber > 1)
        {
            string prevLvl = Defs.GetLevelKey(gameManager.Level, mapNumber - 1);
            unlocked = (PlayerPrefs.HasKey(prevLvl) && PlayerPrefs.GetInt(prevLvl) >= minStarsForNextLevel) || preUnlocked;
        }

        // Hace el boton interactuable y oculta el icono de bloqueado (o viceversa)
        button.interactable = unlocked;

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].color = starLockedColor;
        }

        if (unlocked)
        {
            int numUnlockedStars = PlayerPrefs.GetInt(currLvl);
            for (int i = 0; i < stars.Length; i++)
            {
                if (i < numUnlockedStars)
                {
                    stars[i].color = starUnlockedColor;
                }
            }
        }
    }


    // Llamado al pulsar el boton
    public void Select()
    {
        gameManager.Map = mapNumber;
    }
}
