using UnityEngine;
using UnityEngine.Localization;

public class Building : MonoBehaviour
{
    [SerializeField]
    LocalizedString localizedName;

    LevelManager levelManager;
    public void InitialSetup(LevelManager lvlMngr)
    {
        levelManager = lvlMngr;
    }

    private void OnMouseEnter()
    {
        levelManager.ShowBuildingInfo(localizedName.GetLocalizedString());
    }
    private void OnMouseExit()
    {
        levelManager.HideBuildingInfo();
    }
}