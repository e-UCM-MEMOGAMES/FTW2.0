using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    [SerializeField]
    CarController car;

    [SerializeField]
    ButtonTracker buttonTracker;

    [SerializeField]
    PlaySoundComponent playSound;

    private void OnMouseDown()
    {
        car.MoveTowards(gameObject);
        buttonTracker.Track();
        playSound.PlaySound();
    }
}
