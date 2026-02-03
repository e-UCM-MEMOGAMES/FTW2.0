using UnityEngine;

public class TutorialManager : LevelManager
{
    /// <summary>
    /// Estados del tutorial (en que momentos se muestra cada panel)
    /// </summary>
    enum States
    {
        PANEL1, PANEL2, PANEL3, PANEL4, PANEL5, PANEL6, PANEL7, PANEL8, PANEL9, LAST
    };
    States currState = States.PANEL1;

    [Header("Tutorial")]
    /// <summary>
    /// Paneles con la informacion de cada estado del tutorial
    /// </summary>
    [SerializeField] GameObject[] statePanels;

    [SerializeField] GameObject toTopButton;
    [SerializeField] GameObject mapButton;
    [SerializeField] GameObject mapCameras;

    /// <summary>
    /// Animator de la mano (para cambiar las animaciones segun el estado)
    /// </summary>
    [SerializeField] Animator handAnimator;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Se desactivan todos los paneles
        foreach (GameObject panel in statePanels)
        {
            panel.SetActive(false);
        }
        toTopButton.SetActive(false);
        mapButton.SetActive(false);
        UpdateState(0);
    }

    // Update is called once per frame
    protected override void Update()
    {
        // Si se ha pulsado la pantalla y no se ha llegado al ultimo panel
        if (Input.GetMouseButtonDown(0) && (int)currState < statePanels.Length)
        {
            if (currState <= States.PANEL4 || (currState > States.PANEL6))
            {
                UpdateState();

                if (currState == States.PANEL5)
                {
                    toTopButton.SetActive(true);
                }
            }
            else if (currState == States.PANEL5 && !toTopButton.activeSelf)
            {
                UpdateState();

                if (currState == States.PANEL6)
                {
                    mapButton.SetActive(true);
                }
            }
            else if (currState == States.PANEL6 && mapCameras.activeSelf)
            {
                UpdateState();
            }
        }
        else
        {
            base.Update();
        }
    }

    /// <summary>
    /// Actualiza el estado actual, ocultando el panel actual y mostrando el siguiente
    /// </summary>
    void UpdateState(int increment = 1)
    {
        // Oculta el panel del estado actual
        if (ValidState())
        {
            statePanels[(int)currState].SetActive(false);
        }
        // Actualiza el estado
        currState += increment;
        // Muestra el panel del nuevo estado actual
        if (ValidState())
        {
            statePanels[(int)currState].SetActive(true);
        }
        // Hace la transicion de la animacion a la del nuevo estado actual
        handAnimator.SetInteger("step", (int)currState);

        //trackerManager.TrySendStatement(CompletableTracker.Instance.Progressed("Tutorial", COMPLETABLE_TYPE, (float)currState / (int)States.LAST));
    }

    /// <summary>
    /// Devuelve si el estado actual, es valido
    /// </summary>
    bool ValidState()
    {
        return currState >= 0 && (int)currState < statePanels.Length;
    }
}
