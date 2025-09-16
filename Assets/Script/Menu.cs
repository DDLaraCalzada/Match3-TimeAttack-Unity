using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject panelMenu;
    public Button btnSave;
    public Button startButton;
    public Button exitButton;
    public Button StarTutorialBtn;
    public Button[] backButton;

    private Animator panelAnimator;
    public Animator AmimatorTutorial;

    private void Start()
    {
        panelAnimator = panelMenu.GetComponent<Animator>();

        // Asignar la función al evento OnClick del botón de inicio
        startButton.onClick.AddListener(StartAnimation);

        // Asignar la función al evento OnClick del botón de salida
        exitButton.onClick.AddListener(ExitApplication);

        btnSave.onClick.AddListener(ReturntAnimation);

        // Iterar sobre cada botón en la lista backButton
        foreach (Button button in backButton)
        {
            // Agregar un listener de evento para el clic del botón actual
            button.onClick.AddListener(() => ReturntAnimation());
        }
    }

    // Método para activar la animación asociada al bool "Intro"
    public void StartAnimation()
    {
        panelAnimator.SetBool("Start", true);
        panelAnimator.SetBool("Intro", false);
        panelAnimator.SetBool("Return", false);

        //Animador tutorial

        AmimatorTutorial.SetBool("EnterTutorial", true);
        AmimatorTutorial.SetBool("ExitTutorial", false); 
    }

    public void ReturntAnimation()
    {
        panelAnimator.SetBool("Return", true);
        panelAnimator.SetBool("Start", false);
    }

    private void Update()
    {
        //if (panelMenu.activeSelf)
        //{
        //    panelAnimator.SetBool("Intro", true);
        //}
    }

    // Método para cerrar la aplicación
    public void ExitApplication()
    {
        Application.Quit();
    }
}
