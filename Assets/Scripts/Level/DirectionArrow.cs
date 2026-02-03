using UnityEngine;
using UnityEngine.EventSystems;

public class DirectionArrow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    CarController car;

    [SerializeField]
    ButtonTracker buttonTracker;

    [SerializeField]
    PlaySoundComponent playSound;


    public void OnPointerClick(PointerEventData eventData)
    {
        car.MoveTowards(gameObject);
        buttonTracker.Track();
        playSound.PlaySound();
    }
}
