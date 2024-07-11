using UnityEngine;

/// <summary>
/// Script que se encarga del movimiento de la cámara detrás de un target.
/// </summary>
public class CameraControl : MonoBehaviour
{
    public GameObject target;
    Vector3 offset;

    public float posXmax;
    public float posXmin;
    public float posYmax;
    public float posYmin;
    public float offsetXmax = 6;
    public float offsetXmin = 6;
    public float offsetYmax = 15;
    public float vel;
    public GameObject resetCamButton;
    public GameObject isoCam;
    public GameObject cenCam;

    
    Vector3 touchStart;
    Vector3 cameraIsoOffset;
    Vector3 cameraCenOffset;
    float initIsoZoom;
    float initCenZoom;
    //private float zoom = 3.84f;
    //private float zoomAmount = 40f;

    // ==============================
    void Start()
    {
        posXmax = 261 - offsetXmax;
        posXmin = 0 + offsetXmin;
        posYmax = 50;// 0 - offsetYmax;
        posYmin = -57;
        initIsoZoom = isoCam.GetComponent<Camera>().orthographicSize;
        initCenZoom = cenCam.GetComponent<Camera>().orthographicSize;
        resetCamButton.SetActive(false);

        offset = target.transform.position - transform.position;
        
        cameraIsoOffset = target.transform.position - isoCam.transform.position;
        cameraCenOffset = target.transform.position - cenCam.transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            resetCamButton.SetActive(true);
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Camera.main.transform.position += direction;
        }
        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void zoom(float increment)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, 3f, 10f);
    }

    // ==========================
    void LateUpdate()
    {
        // Limite del offset vertical del jugador y la cámara
        if (offset.y > 1) offset.y = 1;
        if (offset.y < -2) offset.y = -2;

        Vector3 orig = transform.position;
        Vector3 destino = target.transform.position - offset;

        // Limites de movimiento de la cámara
        if (destino.x < posXmin) destino.x = posXmin;
        if (destino.x > posXmax) destino.x = posXmax;
        if (destino.y < posYmin) destino.y = posYmin;
        if (destino.y > posYmax) destino.y = posYmax;

        Vector3.Lerp(orig, destino, 1 / 20f);
        Vector3 despl = Vector3.Lerp(orig, destino, Time.deltaTime*vel);
        transform.position = despl;
    }

    public void ResetCamera()
    {
        isoCam.transform.position = target.transform.position - cameraIsoOffset;
        cenCam.transform.position = target.transform.position - cameraCenOffset;
        isoCam.GetComponent<Camera>().orthographicSize = initIsoZoom;
        cenCam.GetComponent<Camera>().orthographicSize = initCenZoom;
        resetCamButton.SetActive(false);
    }
}