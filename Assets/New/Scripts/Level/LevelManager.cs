using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using Xasu.HighLevel;

public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// Instancia del GameManager
    /// </summary>
    GameManager gameManager;
    /// <summary>
    /// Instancia del AudioManager
    /// </summary>
    AudioManager audioManager;
    /// <summary>
    /// Instancia del TrackerManager
    /// </summary>
    TrackerManager trackerManager;

    /// <summary>
    /// Temporizador para medir el tiempo que se tarda en completar el nivel
    /// </summary>
    Stopwatch watch = Stopwatch.StartNew();
    //CompletableTracker.CompletableType COMPLETABLE_TYPE = CompletableTracker.CompletableType.Level;

    [Header("Level Solver")]
    [SerializeField] BFS bfsSolver;

    [Header("Level Specific Configuration")]
    [SerializeField] Transform startingTile;
    [SerializeField] Defs.CarDirections initialDirection;
    [SerializeField] Transform goalTile;
    [SerializeField] float initialFuel = 100;
    [SerializeField] float fuelConsumptionPerTile = 1;
    float totalFuelInTiles = 0;
    int minDistance = 0;
    float traversedDistance = 0;
    [SerializeField] Transform buildingsParentObj;
    [SerializeField] LocalizedString localizedObjectiveText;


    [Header("Elements Depending on Player Movement")]
    [SerializeField] CarController player;
    [SerializeField] Transform playerTr;
    [SerializeField] Transform goalArrowTr;
    [SerializeField] Transform playerCameras;
    [SerializeField] float cameraFollowSmoothness = 1;
    float playerInitY;
    Vector3 playerPos;


    [Header("UI Elements")]
    [SerializeField] GameObject playPanel;
    [SerializeField] RectTransform fuelBar;
    float initialFuelBarWidth;
    Vector2 fuelBarSize;
    [SerializeField] TextMeshProUGUI objectiveText;
    [SerializeField] GameObject buildingInfoPanel;
    [SerializeField] TextMeshProUGUI buildingInfoText;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject[] unlockedStars;
    [SerializeField] GameObject losePanel;
    bool ended = false;


    /// <summary>
    /// Numero maximo de usos del mapa
    /// </summary>
    const int MAX_MAP_USES = 3;
    /// <summary>
    /// Usos del mapa restantes
    /// </summary>
    int remainingMapUses = MAX_MAP_USES;
    public int RemainingMapUses
    {
        get { return remainingMapUses; }
        set { remainingMapUses = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        audioManager = AudioManager.Instance;
        trackerManager = TrackerManager.Instance;


        audioManager.Play(GameSound.LevelBGM);
        watch.Start();

        InitialSetup();

        totalFuelInTiles = initialFuel;
        fuelBarSize = fuelBar.sizeDelta;
        initialFuelBarWidth = fuelBar.sizeDelta.x;

        bfsSolver.InitialSetup(playerTr, goalTile);
        minDistance = bfsSolver.GetShortestPath();

        objectiveText.text = localizedObjectiveText.GetLocalizedString();

        playPanel.SetActive(true);
        HideBuildingInfo();
        winPanel.SetActive(false);
        losePanel.SetActive(false);

        foreach (Transform tr in buildingsParentObj)
        {
            Building building = tr.GetComponent<Building>();
            if (building != null)
            {
                building.InitialSetup(this);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = playerTr.position;
        playerPos.y = 0;
        playerCameras.position = Vector3.Lerp(playerCameras.position, playerPos, cameraFollowSmoothness * Time.deltaTime);
    }

    
    void InitialSetup()
    {
        playerInitY = playerTr.position.y;
        float goalArrowY = goalArrowTr.position.y;

        playerPos = startingTile.position;
        playerPos.y = playerInitY;
        playerTr.position = playerPos;

        playerPos.y = 0;
        playerCameras.position = playerPos;

        Vector3 endPos = goalTile.position;
        endPos.y = goalArrowTr.position.y;
        goalArrowTr.position = endPos;

        Vector3 initRot = playerTr.rotation.eulerAngles;
        initRot.y += 90 * (int)initialDirection;
        playerTr.eulerAngles = initRot;


        BoxCollider goalCollider = goalTile.GetComponent<BoxCollider>();
        if (goalCollider != null)
        {
            goalCollider.enabled = true;
        }
        RoadStop goal = goalTile.GetComponent<RoadStop>();
        if (goal == null)
        {
            goal = goalTile.gameObject.AddComponent<RoadStop>();
        }
        goal.Type = Defs.StopType.GOAL;
    }

    public void ShowBuildingInfo(string buildingName)
    {
        buildingInfoPanel.SetActive(true);
        buildingInfoText.text = buildingName;
    }
    public void HideBuildingInfo()
    {
        buildingInfoPanel.SetActive(false);
        buildingInfoText.text = "";
    }

    public void Win()
    {
        ended = true;
        playPanel.SetActive(false);
        winPanel.SetActive(true);

        if (totalFuelInTiles / initialFuel >= 0.5)
        {
            unlockedStars[0].SetActive(true);
        }
        if (remainingMapUses == MAX_MAP_USES)
        {
            unlockedStars[1].SetActive(true);
        }
        if ((int)traversedDistance <= minDistance)
        {
            unlockedStars[2].SetActive(true);
        }
    }

    private void Lose()
    {
        ended = true;
        player.ForceStop();
        playPanel.SetActive(false);
        losePanel.SetActive(true);
    }

    public void ConsumeFuel(float tilesTraversed)
    {
        traversedDistance += tilesTraversed;
        totalFuelInTiles -= tilesTraversed * fuelConsumptionPerTile;

        fuelBarSize.x = initialFuelBarWidth * (totalFuelInTiles / initialFuel);
        fuelBar.sizeDelta = fuelBarSize;

        if (!ended && totalFuelInTiles < 0)
        {
            Lose();
        }
    }
}
