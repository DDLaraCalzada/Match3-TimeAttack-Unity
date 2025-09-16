using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class ScoreCounter : MonoBehaviour
{
    [Header("Botones")]
    [SerializeField] private Button[] btnResetScore;        // Solo resetea el puntaje actual
    //[SerializeField] private Button btnClearLeaderboard;     // Limpia la tabla (opcional)
    [SerializeField] private Button btnSave;

    [Header("UI Inputs/Outputs")]
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private TMP_Text playerListText;        // Texto del leaderboard (NO en scoreText[])
    [SerializeField] private TextMeshProUGUI[] scoreText;    // Labels que muestran el puntaje actual

    public static ScoreCounter Instance { get; private set; }

    private int _score;
    private readonly List<PlayerScore> playerScores = new();

    private readonly struct PlayerScore : IComparable<PlayerScore>
    {
        public readonly string playerName;
        public readonly int score;

        public PlayerScore(string name, int score)
        {
            playerName = name;
            this.score = score;
        }

        public int CompareTo(PlayerScore other) => other.score.CompareTo(score);
    }

    public int Score
    {
        get => _score;
        set
        {
            if (_score == value) return;
            _score = value;

            // Actualiza SOLO los labels de puntaje actual
            for (int i = 0; i < scoreText.Length; i++)
                scoreText[i].SetText($"Puntaje = {_score}");
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Ya existe un ScoreCounter en escena. Asegúrate de tener SOLO UNO.");
        }
        Instance = this;
    }

    private void Start()
    {
        // Enlaza botones
        foreach (Button button in btnResetScore)
            button.onClick.AddListener(ResetScoreOnly);

        //if (btnClearLeaderboard != null)
        //    btnClearLeaderboard.onClick.AddListener(ClearLeaderboard);

        btnSave.onClick.AddListener(GuardarNombreJugador);

        // Verificación defensiva: que el leaderboard no esté en scoreText[]
        foreach (var t in scoreText)
        {
            if (t == playerListText)
                Debug.LogError("playerListText NO debe estar en scoreText[]. Corrígelo en el Inspector.");
        }

        // Inicializa UI
        Score = 0;
        ActualizarListaJugadores();
    }

    // Resetea SOLO el puntaje de la partida actual
    private void ResetScoreOnly() => Score = 0;

    // Limpia explícitamente el leaderboard (si quieres esa opción)
    private void ClearLeaderboard()
    {
        playerScores.Clear();
        ActualizarListaJugadores();
    }

    private void GuardarNombreJugador()
    {
        string playerName = playerNameInput.text;

        if (string.IsNullOrWhiteSpace(playerName))
            return;

        // Guarda el puntaje ACTUAL (no lo modifica)
        playerScores.Add(new PlayerScore(playerName.Trim(), _score));

        // Ordena y refresca
        playerScores.Sort();
        ActualizarListaJugadores();

        // Limpia input (no el score)
        playerNameInput.text = "";
    }

    private void ActualizarListaJugadores()
    {
        playerListText.text = "Top 5 Jugadores:\n";
        int rank = 1;

        foreach (var ps in playerScores.Take(5))
            playerListText.text += $"{rank++}. {ps.playerName} - Puntaje: {ps.score}\n";
    }
}
