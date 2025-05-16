using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlattformMovil : MonoBehaviour
{
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float velocidadMovimiento = 3f;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector3 areaSize = new Vector3(2.5f, 0.1f, 2.5f);

    private int puntoActual = 0;
    private Vector3 ultimaPosicion;
    private GameObject jugadorEncima;

    void Start()
    {
        if (puntosMovimiento.Length <= 1)
        {
            Debug.LogError("La plataforma necesita al menos 2 puntos de movimiento para funcionar");
        }

        ultimaPosicion = transform.position;
    }

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
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 0.55f, areaSize);
    }
}