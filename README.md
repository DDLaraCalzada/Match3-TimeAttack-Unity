# Match3-TimeAttack-Unity
A vertical match-3 puzzle game built with Unity. Score as many points as possible before the timer runs out! Includes leaderboard system to save and rank scores from best to worst.

## Características principales

- Match-3 clásico  
  Sistema de tablero dinámico con fichas intercambiables y detección de combinaciones.

- Modo contrarreloj  
  Un temporizador controla la partida. Al llegar a cero, se muestra la pantalla de resultados.

- Leaderboard integrado  
  Permite ingresar el nombre del jugador, guardar su puntaje y mostrar un Top 5 ordenado de mayor a menor.

- Feedback audiovisual  
  Animaciones con DOTween para los swaps, efectos al hacer matches y cambios en la música al avanzar el tiempo.

- Assets configurables  
  Fichas (Item) definidas como ScriptableObject, fáciles de extender y modificar desde el editor.

- Formato vertical  
  Diseñado para dispositivos móviles, con interfaz adaptable y botones para reiniciar/volver al menú.

## Estructura del proyecto

- Board.cs → Controla la cuadrícula, intercambios de fichas y validación de combinaciones.  
- Tile.cs → Representa cada celda, maneja los ítems y vecinos adyacentes.  
- Item / ItemDatabase.cs → Base de datos de ítems como ScriptableObject.  
- ScoreCounter.cs → Sistema de puntaje y ranking con Top 5.  
- CountdownTimer.cs → Temporizador con control de música y animaciones de transición.  
- Menu.cs → Control del menú principal, tutorial y salida.  
- Row.cs / Table.cs → Soporte estructural para la organización del tablero.

## Gameplay

### Eliminación de combinaciones
![](docs/gifs/playing 1.1.gif)

### Pantalla de puntajes y ranking
![](docs/gifs/playing 2.gif)

## Cómo jugar

1. Inicia la partida desde el menú.  
2. Intercambia fichas adyacentes para formar combinaciones de 3 o más.  
3. Suma puntos y compite contra el tiempo.  
4. Al finalizar, guarda tu nombre y revisa el ranking.  

## Tecnologías

- Unity (versión recomendada XX.X)  
- C#  
- DOTween para animaciones  

## Licencia

Este proyecto está bajo la licencia MIT.  
Los assets gráficos pueden estar bajo CC BY 4.0 si quieres diferenciarlos.  

## Contribuciones

Sugerencias, issues y pull requests son bienvenidos.  
Este proyecto fue creado como ejemplo de desarrollo de un juego Match-3 con Unity.

