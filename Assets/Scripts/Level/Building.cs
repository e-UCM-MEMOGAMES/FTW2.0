using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Localization;

public class Building : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    LocalizedString localizedName;

    LevelManager levelManager;
    
    public void InitialSetup(LevelManager lvlMngr)
    {
        levelManager = lvlMngr;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        levelManager.ShowBuildingInfo(localizedName.GetLocalizedString());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        levelManager.HideBuildingInfo();
    }

    // El collider de los edificios puede bloquear el de las flechas, por lo que necesita implementar la interfaz
    // IPointerClickHandler para poder propagar el evento por el resto de objetos aunque lo este bloqueandovel
    public void OnPointerClick(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject != gameObject)
            {
                ExecuteEvents.Execute(result.gameObject, eventData, ExecuteEvents.pointerClickHandler);
            }
        }
    }
}