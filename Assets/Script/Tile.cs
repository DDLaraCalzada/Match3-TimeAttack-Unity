using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Tile : MonoBehaviour
{
    // Posici�n del tile en la cuadr�cula
    public int x;
    public int y;


    private Item _item;

    // Propiedad para acceder y establecer el �tem asociado al azulejo
    public Item Item
    {
        get => _item;

        set
        {
            // Verificar si el nuevo �tem es diferente al �tem actual
            if (_item == value) return;
            _item = value;

            // Actualizar la imagen del �cono del tile con el �cono del nuevo �tem
            icon.sprite = _item.sprite;
        }
    }

    // Referencia a la imagen del �cono del azulejo en la interfaz de usuario
    public Image icon;

    // Referencia al bot�n asociado al azulejo en la interfaz de usuario
    public Button button;

    // Propiedades para obtener los tiles adyacentes en la cuadr�cula
    public Tile Left => x > 0 ? Board.Instance.Tiles[x - 1, y] : null;
    public Tile Top => y > 0 ? Board.Instance.Tiles[x, y - 1] : null;
    public Tile Right => x < Board.Instance.Width - 1 ? Board.Instance.Tiles[x + 1, y] : null;
    public Tile Bottom => y < Board.Instance.Height - 1 ? Board.Instance.Tiles[x, y + 1] : null;

    // Propiedad que devuelve una lista de los tiles adyacentes 
    //Permite acceder a todos los tiles cercanos a un tile dado en una cuadr�cula.
    public Tile[] Neighbours => new[]
    {
        Left,
        Top,
        Right,
        Bottom,
    };

    // M�todo que devuelve una lista de tiles conectados a este azulejo
    public List<Tile> GetConnetedTiles(List<Tile> exclude = null)
    {
        var result = new List<Tile> { this, };

        // Si la lista de exclusi�n es nula, se inicializa con este azulejo
        if (exclude == null)
        {
            exclude = new List<Tile> { this, };
        }
        else
        {
            // Si no es nula, se agrega este azulejo a la lista de exclusi�n
            exclude.Add(this);
        }

        // Recorre los azulejos adyacentes
        foreach (var neighbour in Neighbours)
        {
            // Si el azulejo adyacente es nulo, est� en la lista de exclusi�n o tiene un �tem diferente, se ignora
            if (neighbour == null || exclude.Contains(neighbour) || neighbour.Item != Item) continue;

            // Si no cumple ninguna de las condiciones anteriores, se agrega a la lista de azulejos conectados
            result.AddRange(neighbour.GetConnetedTiles(exclude));
        }

        return result;
    }

    // M�todo que se llama al iniciar el juego
    void Start()
    {
        // Se agrega un listener al bot�n asociado al azulejo para manejar la selecci�n del azulejo
        button.onClick.AddListener(() => Board.Instance.Select(this));
    }

    // M�todo que se llama una vez por cuadro
    void Update()
    {
        // No hay ninguna l�gica de actualizaci�n en este momento
    }
}
