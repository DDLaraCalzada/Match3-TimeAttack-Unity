using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Button[] btnReset;
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private Button btnSave;
    [SerializeField] private TMP_Text playerListText;
    public static ScoreCounter Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI[] scoreText;

    private int _score;
    private List<PlayerScore> playerScores = new List<PlayerScore>();

    // Estructura de datos para almacenar el nombre del jugador y su puntaje
    private struct PlayerScore : IComparable<PlayerScore>
    {
        public string playerName;
        public int score;

        public PlayerScore(string name, int score)
        {
            this.playerName = name;
            this.score = score;
        }

        // Implementación de IComparable para comparar PlayerScores por puntaje
        public int CompareTo(PlayerScore other)
        {
            return other.score.CompareTo(score);
        }
    }
    public int Score
    {
        get => _score;

        set
        {
            if (_score == value) return;
            _score = value;

            // Actualizar cada objeto TextMeshProUGUI en el array
            for (int i = 0; i < scoreText.Length; i++)
            {
                scoreText[i].SetText($"Puntaje = {_score}");
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Agregar un listener de eventos a cada botón de reinicio
        foreach (Button button in btnReset)
        {
            button.onClick.AddListener(ResetScore);
        }

        // Agregar un listener de eventos para el botón de guardar
        btnSave.onClick.AddListener(GuardarNombreJugador);
    }

    // Método para reiniciar el puntaje a cero
    private void ResetScore()
    {
        Score = 0;
    }

    // Método para guardar el nombre del jugador y su puntaje
    private void GuardarNombreJugador()
    {
        string playerName = playerNameInput.text;
        if (!string.IsNullOrEmpty(playerName))
        {
            // Agregar el nombre del jugador y su puntaje a la lista
            playerScores.Add(new PlayerScore(playerName, _score));

            // Ordenar la lista por puntaje (mayor a menor)
            playerScores.Sort();
            ActualizarListaJugadores();
            playerNameInput.text = ""; // Limpiar el campo de entrada después de guardar el nombre
        }
    }

    // Método para actualizar la lista de jugadores en el texto
    private void ActualizarListaJugadores()
    {
        playerListText.text = "Top 5 Jugadores:\n";
        for (int i = 0; i < Mathf.Min(playerScores.Count, 5); i++)
        {
            playerListText.text += $"{i + 1}. {playerScores[i].playerName} - Puntaje: {playerScores[i].score}\n";
        }
    }

}
