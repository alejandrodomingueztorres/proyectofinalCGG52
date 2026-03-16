using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarMeshPorDistancia : MonoBehaviour
{
    public Transform jugador;     // referencia al jugador
    public float distanciaMax = 5f; // distancia para activar
    private MeshRenderer mesh;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false; // empieza oculto
    }

    void Update()
    {
        float distancia = Vector3.Distance(jugador.position, transform.position);

        if (distancia <= distanciaMax)
        {
            mesh.enabled = true;
        }
        else
        {
            mesh.enabled = false;
        }
    }
}
