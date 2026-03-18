using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;


public class LevelButton : MonoBehaviour
{
    /// <summary>
    /// Instancia del GameManager
    /// </summary>
    GameManager gameManager;

    [SerializeField]
    Image bg;

    [SerializeField]
    Color unlockedColor,
        lockedColor;

    /// <summary>
    /// Componente Button del objeto para bloquear la pulsacion si el nivel esta bloqueado
    /// </summary>
    [SerializeField]
    Button button;

    /// <summary>
    /// Texto con el numero del nivel
    /// </summary>
    [SerializeField]
    TextMeshProUGUI titleText;

    [SerializeField]
    LocalizedString localizedTitleText;

    /// <summary>
    /// Si el nivel viene desbloqueado por defecto
    /// </summary>
    [SerializeField]
    bool preUnlocked = false;

    [SerializeField]
    int levelNumber;


    void Start()
    {
        gameManager = GameManager.Instance;

        titleText.text = $"{localizedTitleText.GetLocalizedString()} {levelNumber.ToString()}";
        titleText.ForceMeshUpdate();
    }

    public void Unlock(bool unlocked)
    {
        // Hace el boton interactuable y oculta el icono de bloqueado (o viceversa)
        button.interactable = unlocked;
        if (unlocked || preUnlocked)
        {
            bg.color = unlockedColor;
        }
        else
        {
            bg.color = lockedColor;
        }
    }


    public void Select()
    {
        gameManager.Level = levelNumber;
    }
}
