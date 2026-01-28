using UnityEngine;

public class CarController : MonoBehaviour
{
    AudioManager audioManager;

    [SerializeField]
    LevelManager levelManager;

    enum Arrows { FRONT, LEFT, RIGHT }

    [SerializeField]
    GameObject[] arrows;

    [SerializeField]
    float speed = 1;

    float initY;
    Vector3 position;
    Vector3 vel = new Vector3(0, 0, 0);

    bool hasToStop = false;
    Transform stopTr = null;
    Vector3 stopPos = new Vector3(0, 0, 0);
    Defs.StopType stopType = Defs.StopType.NONE;


    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.Instance;

        position = transform.position;
        initY = position.y;

        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
        arrows[(int)Arrows.FRONT].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (hasToStop && Vector3.Distance(transform.position, stopPos) < 0.05)
        {
            Stop();
            levelManager.ConsumeFuel(Vector3.Distance(transform.position, stopPos));
        }
        if (vel != Vector3.zero)
        {
            transform.position += vel.normalized * speed * Time.deltaTime;
            levelManager.ConsumeFuel(speed * Time.deltaTime);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (!hasToStop && other.transform.position.x != stopPos.x && other.transform.position.z != stopPos.z)
        {
            OnTriggerEnter(other);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }

        RoadStop stop = other.GetComponent<RoadStop>();
        if (stop != null)
        {
            stopTr = other.transform;
            stopPos = stopTr.position;
            stopPos.y = initY;
            hasToStop = true;
            stopType = stop.Type;
        }
    }

    public void MoveTowards(GameObject pressedArrow)
    {
        foreach (GameObject arrow in arrows)
        {
            arrow.SetActive(false);
        }
        vel = pressedArrow.transform.forward;
        transform.forward = pressedArrow.transform.forward;

        audioManager.Play(GameSound.Engine);
    }

    void Stop()
    {
        ForceStop();
        transform.position = stopPos;

        if (stopType == Defs.StopType.GOAL)
        {
            levelManager.Win();
            ForceStop();
        }
        else if (stopType == Defs.StopType.INTERSECTION)
        {
            foreach (GameObject arrow in arrows)
            {
                arrow.SetActive(true);
            }
        }
        else if (stopType == Defs.StopType.TSPLIT)
        {
            if (stopTr.forward == transform.forward)
            {
                arrows[(int)Arrows.FRONT].SetActive(true);
                arrows[(int)Arrows.LEFT].SetActive(true);
            }
            else if (stopTr.forward == -transform.forward)
            {
                arrows[(int)Arrows.FRONT].SetActive(true);
                arrows[(int)Arrows.RIGHT].SetActive(true);
            }
            else if (-stopTr.right == transform.forward)
            {
                arrows[(int)Arrows.FRONT].SetActive(true);
                arrows[(int)Arrows.LEFT].SetActive(true);
                arrows[(int)Arrows.RIGHT].SetActive(true);
            }
            else
            {
                arrows[(int)Arrows.LEFT].SetActive(true);
                arrows[(int)Arrows.RIGHT].SetActive(true);
            }
        }
        else if (stopType == Defs.StopType.CORNER)
        {
            if (stopTr.forward == transform.forward)
            {
                arrows[(int)Arrows.FRONT].SetActive(true);
                arrows[(int)Arrows.LEFT].SetActive(true);
            }
            else if (stopTr.forward == -transform.forward)
            {
                arrows[(int)Arrows.RIGHT].SetActive(true);
            }
            else if (-stopTr.right == transform.forward)
            {
                arrows[(int)Arrows.FRONT].SetActive(true);
                arrows[(int)Arrows.RIGHT].SetActive(true);
            }
            else
            {
                arrows[(int)Arrows.LEFT].SetActive(true);
            }
        }
    }

    public void ForceStop()
    {
        vel = Vector3.zero;
        hasToStop = false;
        audioManager.StopSFX();
    }
}
