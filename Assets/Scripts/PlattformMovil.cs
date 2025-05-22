using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Plataforma móvil que se desplaza entre puntos predefinidos y mueve al jugador junto con ella.
/// Detecta cuando el jugador está encima y lo desplaza siguiendo el movimiento de la plataforma.
/// </summary>
public class PlattformMovil : MonoBehaviour
{
    /// <summary>
    /// Array de puntos entre los cuales se moverá la plataforma
    /// </summary>
    [SerializeField] private Transform[] puntosMovimiento;
    /// <summary>
    /// Velocidad de movimiento de la plataforma en unidades por segundo
    /// </summary>
    [SerializeField] private float velocidadMovimiento = 3f;
    /// <summary>
    /// Tag del jugador para identificarlo en las detecciones
    /// </summary>
    [SerializeField] private string playerTag = "Player";
    /// <summary>
    /// Layer del jugador para la detección mediante OverlapBox
    /// </summary>
    [SerializeField] private LayerMask playerLayer;
    /// <summary>
    /// Tamaño del área de detección encima de la plataforma
    /// </summary>
    [SerializeField] private Vector3 areaSize = new Vector3(2.5f, 0.1f, 2.5f);

    /// <summary>
    /// Índice del punto de movimiento actual hacia el cual se dirige la plataforma
    /// </summary>
    private int puntoActual = 0;
    /// <summary>
    /// Posición de la plataforma en el frame anterior, usada para calcular el desplazamiento
    /// </summary>
    private Vector3 ultimaPosicion;
    /// <summary>
    /// Referencia al GameObject del jugador cuando está encima de la plataforma
    /// </summary>
    private GameObject jugadorEncima;
    /// <summary>
    /// Inicialización del script. Valida que existan suficientes puntos de movimiento
    /// y guarda la posición inicial.
    /// </summary>
    void Start()
    {
        if (puntosMovimiento.Length <= 1)
        {
            Debug.LogError("La plataforma necesita al menos 2 puntos de movimiento para funcionar");
        }

        ultimaPosicion = transform.position;
    }
    /// <summary>
    /// Actualización por frame que maneja el movimiento de la plataforma y del jugador.
    /// Mueve la plataforma, calcula el desplazamiento y mueve al jugador si está encima.
    /// </summary>
    void Update()
    {
        // Mover la plataforma
        MoverPlataforma();

        // Calcular el desplazamiento
        Vector3 deltaMovimiento = transform.position - ultimaPosicion;

        // Detectar jugador encima y moverlo con la plataforma
        DetectarYDesplazarJugador(deltaMovimiento);

        // Guardar posición actual para el próximo frame
        ultimaPosicion = transform.position;
    }

    /// <summary>
    /// Mueve la plataforma hacia el punto de movimiento actual.
    /// Cuando llega al punto, cambia al siguiente punto en el array de forma cíclica.
    /// </summary>
    void MoverPlataforma()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            puntosMovimiento[puntoActual].position,
            velocidadMovimiento * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, puntosMovimiento[puntoActual].position) < 0.1f)
        {
            puntoActual = (puntoActual + 1) % puntosMovimiento.Length;
        }
    }
    /// <summary>
    /// Detecta si el jugador está encima de la plataforma y lo mueve junto con ella.
    /// Usa OverlapBox para detectar al jugador y CharacterController.Move para desplazarlo.
    /// </summary>
    /// <param name="deltaMovimiento">El desplazamiento que ha tenido la plataforma en este frame</param>
    void DetectarYDesplazarJugador(Vector3 deltaMovimiento)
    {
        Collider[] hits = Physics.OverlapBox(transform.position + Vector3.up * 0.55f, areaSize / 2f, Quaternion.identity, playerLayer);

        if (hits.Length > 0)
        {
            foreach (Collider col in hits)
            {
                if (col.CompareTag(playerTag))
                {
                    CharacterController cc = col.GetComponent<CharacterController>();
                    if (cc != null)
                    {
                        cc.Move(deltaMovimiento);
                        jugadorEncima = col.gameObject;
                    }
                }
            }
        }
    }
    // Visualización en escena del área de detección
    /// <summary>
    /// Visualización en escena del área de detección cuando la plataforma está seleccionada.
    /// Dibuja un cubo verde que representa el área donde se detecta al jugador.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 0.55f, areaSize);
    }
}