using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtons : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField]
    LevelManager levelManager;

    [SerializeField]
    GameObject overviewCameras,
               playerCameras;

    [SerializeField]
    GameObject[] topCameras,
                 isoCameras,
                 positionArrows;

    [SerializeField]
    GameObject objectiveInfo;

    /// <summary>
    /// Texto del indicador de usos restantes de la lista
    /// </summary>
    [SerializeField] TextMeshProUGUI remainingMapUsesText;


    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.Instance;

        overviewCameras.SetActive(true);
        playerCameras.SetActive(false);
        objectiveInfo.SetActive(true);
        foreach (GameObject arrow in positionArrows)
        {
            arrow.SetActive(true);
        }
        ToIso();
    }

    public void OpenMap()
    {
        // Si la vista del jugador esta activa y quedan usos del mapa
        if (playerCameras.activeSelf && levelManager.RemainingMapUses > 0)
        {
            // Se actualizan los intentos restantes y el texto
            levelManager.RemainingMapUses--;
            remainingMapUsesText.text = levelManager.RemainingMapUses.ToString();

            // Se activa la vista del nivel
            ToggleView();
        }
        // Si no, si la vista del nivel esta activa
        else if (overviewCameras.activeSelf)
        {
            // Se activa la vista del jugador
            ToggleView();
        }
    }
    void ToggleView()
    {
        overviewCameras.SetActive(!overviewCameras.activeSelf);
        playerCameras.SetActive(!playerCameras.activeSelf);
        objectiveInfo.SetActive(overviewCameras.activeSelf);

        foreach (GameObject arrow in positionArrows)
        {
            arrow.SetActive(overviewCameras.activeSelf);
        }
    }
    public void ToTop()
    {
        foreach (GameObject cam in isoCameras)
        {
            cam.SetActive(false);
        }
        foreach (GameObject cam in topCameras)
        {
            cam.SetActive(true);
        }
    }
    public void ToIso()
    {
        foreach (GameObject cam in isoCameras)
        {
            cam.SetActive(true);
        }
        foreach (GameObject cam in topCameras)
        {
            cam.SetActive(false);
        }
    }

    
    public void Replay()
    {
        GetComponent<MenuButtons>().ChangeScene(SceneManager.GetActiveScene().name);
        audioManager.StopSFX();
    }
    public void ExitLevel()
    {
        GetComponent<MenuButtons>().ChangeScene(Defs.LEVEL_SETTINGS_SCENE_NAME);
        audioManager.StopSFX();
        audioManager.Play(GameSound.MenuBGM);
    }
}
