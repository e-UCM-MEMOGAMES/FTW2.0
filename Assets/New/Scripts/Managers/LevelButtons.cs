using UnityEngine;

public class LevelButtons : MonoBehaviour
{
    [SerializeField]
    GameObject overviewCameras,
               playerCameras;

    [SerializeField]
    GameObject[] orthoCameras,
                 perspectiveCameras,
                 positionArrows;


    // Start is called before the first frame update
    void Start()
    {
        //overviewCameras.SetActive(true);
        //playerCameras.SetActive(false);
        foreach (GameObject arrow in positionArrows)
        {
            arrow.SetActive(true);
        }
        ToPerspective();
    }


    public void ToggleView()
    {
        overviewCameras.SetActive(!overviewCameras.activeSelf);
        playerCameras.SetActive(!playerCameras.activeSelf);
        foreach (GameObject arrow in positionArrows)
        {
            arrow.SetActive(!arrow.activeSelf);
        }
    }
    public void ToOrtho()
    {
        foreach (GameObject cam in perspectiveCameras)
        {
            cam.SetActive(false);
        }
        foreach (GameObject cam in orthoCameras)
        {
            cam.SetActive(true);
        }
    }
    public void ToPerspective()
    {
        foreach (GameObject cam in perspectiveCameras)
        {
            cam.SetActive(true);
        }
        foreach (GameObject cam in orthoCameras)
        {
            cam.SetActive(false);
        }
    }
}
