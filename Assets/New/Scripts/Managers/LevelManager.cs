using UnityEngine;

public class LevelManager : MonoBehaviour
{
    enum Directions { FORWARD, RIGHT, BACK, LEFT }
    [Header("Level Specific Configuration")]
    [SerializeField] Transform startingTile;
    [SerializeField] Directions initialDirection;
    [SerializeField] Transform goalTile;

    [SerializeField] float totalFuel = 100;
    [SerializeField] float fuelConsumptionPerTile = 1;


    [Header("Elements Depending on Player Movement")]
    [SerializeField] CarController player;
    [SerializeField] Transform playerTr;
    [SerializeField] Transform goalArrowTr;
    [SerializeField] Transform playerCameras;
    [SerializeField] float cameraFollowSmoothness = 1;
    float playerInitY;
    Vector3 playerPos;


    [Header("UI Elements")]
    [SerializeField]
    RectTransform fuelBar;
    float fullFuelBarWidth;

    [SerializeField]
    GameObject[] unlockedStars;

    bool ended = false;

    // Start is called before the first frame update
    void Start()
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
        initRot.y += 90 * (int) initialDirection;
        playerTr.eulerAngles = initRot;

        fullFuelBarWidth = fuelBar.sizeDelta.x;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = playerTr.position;
        playerPos.y = 0;
        playerCameras.position = Vector3.Lerp(playerCameras.position, playerPos, cameraFollowSmoothness * Time.deltaTime);
    }

    public void Win()
    {
        ended = true;
        Debug.Log("Win");
        player.ForceStop();
    }

    private void Lose()
    {
        ended = true;
        Debug.Log("Lose");
        player.ForceStop();
    }

    public void ConsumeFuel(float amount)
    {
        totalFuel -= amount * fuelConsumptionPerTile;
        fuelBar.sizeDelta *= new Vector2(totalFuel / 100.0f, 1);

        if (!ended && totalFuel < 0)
        {
            Lose();
        }
    }
}
