using TMPro;
using UnityEngine;

public class OverviewCamera : MonoBehaviour
{
    [SerializeField]
    Camera cam;

    [SerializeField]
    GameObject resetButton;

    [SerializeField]
    float zoomSpeed = 2.0f, minCamSize = 10.0f;

    Vector3 initPos;
    float initSize;
    Vector3 touchStart;

    float minX, maxX, minZ, maxZ = 50f;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        initSize = cam.orthographicSize;

        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * cam.aspect;

        minX = transform.position.x - horzExtent;
        maxX = transform.position.x + horzExtent;
        minZ = transform.position.z - vertExtent;
        maxZ = transform.position.z + vertExtent;
    }

    // Update is called once per frame
    void Update()
    {
        // Zoom con controles tactiles
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }
        else
        {
            Zoom(Input.GetAxis("Mouse ScrollWheel") * zoomSpeed);
        }

        if (initSize != cam.orthographicSize)
        {
            //Pulsar
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
            }
            // Mantener pulsado
            if (Input.GetMouseButton(0))
            {
                Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
                direction.y = 0;
                cam.transform.position += direction;
            }
        }

        float vertExtent = cam.orthographicSize;
        float horzExtent = vertExtent * cam.aspect;

        float minXPos = minX + horzExtent;
        float maxXPos = maxX - horzExtent;
        float minZPos = minZ + vertExtent;
        float maxZPos = maxZ - vertExtent;

        Vector3 newPos = cam.transform.position;
        newPos.x = Mathf.Clamp(newPos.x, minXPos, maxXPos);
        newPos.z = Mathf.Clamp(newPos.z, minZPos, maxZPos);
        cam.transform.position = newPos;

        resetButton.SetActive(initPos != cam.transform.position || initSize != cam.orthographicSize);
    }

    void Zoom(float amount)
    {
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize - amount, minCamSize, initSize);
    }

    public void ResetPos()
    {
        transform.position = initPos;
        cam.orthographicSize = initSize;

        resetButton.SetActive(false);
    }
}
