using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Animator AnimatorTutorial;
    public Animator AnimatorRegister;
    public Button startButton;
    public Button btnSave;
    public Button BackButton;
    [SerializeField] private Button[] btnReset;
    public float tiempoInicial = 60.0f; // Tiempo inicial en segundos
    public TMP_Text textoTiempo; // Referencia al objeto TextMeshPro donde se mostrar� el tiempo
    public AudioSource audioSource; // Componente AudioSource para reproducir m�sica

    private float tiempoRestante; // Tiempo restante en segundos
    private bool temporizadorActivo = false; // Variable para controlar si el temporizador est� activo
    private float tiempoTranscurrido = 0f; // Tiempo transcurrido desde el inicio del temporizador
    private float tiempoUltimaActualizacion = 0f; // Tiempo del �ltimo ajuste de velocidad de audio

    public GameObject tutorial;

    private void Start()
    {
        tiempoRestante = tiempoInicial;
        ActualizarTextoTiempo();
        startButton.onClick.AddListener(IniciarTemporizador);
        BackButton.onClick.AddListener(DetenerTemporizador);
        BackButton.onClick.AddListener(ReiniciarTiempo);
        btnSave.onClick.AddListener(ExitRegister);

        foreach (Button button in btnReset)
        {
            button.onClick.AddListener(ReiniciarTiempo);
        }
    }

    private void Update()
    {
        if (temporizadorActivo)
        {
            tiempoRestante -= Time.deltaTime;
            tiempoTranscurrido += Time.deltaTime;

            // Aumentar la velocidad de la m�sica cada 10 segundos
            if (tiempoTranscurrido - tiempoUltimaActualizacion >= 10f)
            {
                audioSource.pitch += 0.2f; // Aumentar la velocidad en 0.2
                tiempoUltimaActualizacion = tiempoTranscurrido;
            }

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                temporizadorActivo = false;
                // Aqu� puedes agregar cualquier acci�n que quieras realizar cuando el temporizador llegue a cero
                AnimatorRegister.SetBool("RegisterEnter", true);
                AnimatorRegister.SetBool("RegisterExit", false);

                // Detener la reproducci�n de audio cuando el temporizador llegue a cero
                audioSource.Stop();
            }
            ActualizarTextoTiempo();
        }
    }

    // M�todo para iniciar el temporizador
    public void IniciarTemporizador()
    {
        tiempoRestante = tiempoInicial;
        temporizadorActivo = true;
        AnimatorTutorial.SetBool("ExitTutorial", true);
        AnimatorTutorial.SetBool("EnterTutorial", false);

        // Comenzar la reproducci�n de la m�sica al iniciar el temporizador
        audioSource.Play();
    }

    // M�todo para detener el temporizador
    public void DetenerTemporizador()
    {
        temporizadorActivo = false;

        // Detener la reproducci�n de la m�sica cuando se detiene el temporizador
        audioSource.Stop();
    }

    // M�todo para actualizar el texto del tiempo mostrado en pantalla
    private void ActualizarTextoTiempo()
    {
        int minutos = Mathf.FloorToInt(tiempoRestante / 60);
        int segundos = Mathf.FloorToInt(tiempoRestante % 60);
        textoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void ReiniciarTiempo()
    {
        tiempoRestante = tiempoInicial;
        tiempoTranscurrido = 0f; // Reiniciar el tiempo transcurrido
        tiempoUltimaActualizacion = 0f; // Reiniciar el tiempo de la �ltima actualizaci�n de velocidad
        audioSource.pitch = 1f; // Restaurar la velocidad de la m�sica a su valor original
        ActualizarTextoTiempo();
    }

    private void ExitRegister()
    {
        AnimatorRegister.SetBool("RegisterEnter", false);
        AnimatorRegister.SetBool("RegisterExit", true);
    }
}
