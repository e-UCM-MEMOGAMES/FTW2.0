using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField]
    CarController car;

    [SerializeField]
    ButtonTracker buttonTracker;

    [SerializeField]
    PlaySoundComponent playSound;

    int arrowLayer = 1 << 10;
    RaycastHit hit;


    // La deteccion de los edificios bloquea la deteccion de pulsacion en las
    // flechas, por lo que es preferible detectar la pulsacion con raycasts
    /*
    private void OnMouseDown()
    {
        car.MoveTowards(gameObject);
        buttonTracker.Track();
        playSound.PlaySound();
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100.0f, arrowLayer))
            {
                if (hit.transform == transform)
                {
                    car.MoveTowards(gameObject);
                    buttonTracker.Track();
                    playSound.PlaySound();
                }
            }
        }
    }
}
