using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Tile : MonoBehaviour
{
    // Posición del tile en la cuadrícula
    public int x;
    public int y;


    private Item _item;

    // Propiedad para acceder y establecer el ítem asociado al azulejo
    public Item Item
    {
        get => _item;

        set
        {
            // Verificar si el nuevo ítem es diferente al ítem actual
            if (_item == value) return;
            _item = value;

            // Actualizar la imagen del ícono del tile con el ícono del nuevo ítem
            icon.sprite = _item.sprite;
        }
    }

    // Referencia a la imagen del ícono del azulejo en la interfaz de usuario
    public Image icon;

    // Referencia al botón asociado al azulejo en la interfaz de usuario
    public Button button;

    // Propiedades para obtener los tiles adyacentes en la cuadrícula
    public Tile Left => x > 0 ? Board.Instance.Tiles[x - 1, y] : null;
    public Tile Top => y > 0 ? Board.Instance.Tiles[x, y - 1] : null;
    public Tile Right => x < Board.Instance.Width - 1 ? Board.Instance.Tiles[x + 1, y] : null;
    public Tile Bottom => y < Board.Instance.Height - 1 ? Board.Instance.Tiles[x, y + 1] : null;

    // Propiedad que devuelve una lista de los tiles adyacentes 
    //Permite acceder a todos los tiles cercanos a un tile dado en una cuadrícula.
    public Tile[] Neighbours => new[]
    {
        Left,
        Top,
        Right,
        Bottom,
    };

    // Método que devuelve una lista de tiles conectados a este azulejo
    public List<Tile> GetConnetedTiles(List<Tile> exclude = null)
    {
        var result = new List<Tile> { this, };

        // Si la lista de exclusión es nula, se inicializa con este azulejo
        if (exclude == null)
        {
            exclude = new List<Tile> { this, };
        }
        else
        {
            // Si no es nula, se agrega este azulejo a la lista de exclusión
            exclude.Add(this);
        }

        // Recorre los azulejos adyacentes
        foreach (var neighbour in Neighbours)
        {
            // Si el azulejo adyacente es nulo, está en la lista de exclusión o tiene un ítem diferente, se ignora
            if (neighbour == null || exclude.Contains(neighbour) || neighbour.Item != Item) continue;

            // Si no cumple ninguna de las condiciones anteriores, se agrega a la lista de azulejos conectados
            result.AddRange(neighbour.GetConnetedTiles(exclude));
        }

        return result;
    }

    // Método que se llama al iniciar el juego
    void Start()
    {
        // Se agrega un listener al botón asociado al azulejo para manejar la selección del azulejo
        button.onClick.AddListener(() => Board.Instance.Select(this));
    }

    // Método que se llama una vez por cuadro
    void Update()
    {
        // No hay ninguna lógica de actualización en este momento
    }
}
