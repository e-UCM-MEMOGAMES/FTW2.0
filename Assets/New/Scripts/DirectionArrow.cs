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
    Ray ray;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f, arrowLayer))
            {
                if (hit.transform == transform)
                {
                    car.MoveTowards(gameObject);
                    playSound.PlaySound();
                    buttonTracker.Track();
                }
            }
        }
    }
}
