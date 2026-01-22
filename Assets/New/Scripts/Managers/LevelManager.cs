using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    enum Direction { FORWARD, RIGHT, BACK, LEFT} 

    [SerializeField]
    Transform startingTile,
               goalTile,
               playerTr,
               playerArrowTr,
               goalArrowTr;

    [SerializeField]
    Direction initialDirection;

    [SerializeField]
    int totalFuel = 100;

    [SerializeField]
    GameObject fuelBar;



    // Start is called before the first frame update
    void Start()
    {
        Vector3 initPos = startingTile.position;
        initPos.y = playerTr.position.y;
        playerTr.position = initPos;
        initPos.y = playerArrowTr.position.y;
        playerArrowTr.position = initPos;

        Vector3 initRot = playerTr.rotation.eulerAngles;
        initRot.y += 90 * (int) initialDirection;
        playerTr.eulerAngles = initRot;


        Vector3 endPos = goalTile.position;
        endPos.y = goalArrowTr.position.y;
        goalArrowTr.position = endPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
