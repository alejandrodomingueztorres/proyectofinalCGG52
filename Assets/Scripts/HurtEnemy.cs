using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detecta colisiones con enemigos y les aplica daño.
/// Este script debe estar en un objeto con un collider configurado como trigger.
/// </summary>
public class HurtEnemy : MonoBehaviour
{
    /// <summary>
    /// Se ejecuta cuando otro collider entra en el trigger de este objeto.
    /// Si el objeto tiene la etiqueta "Enemy", se le aplica daño mediante <c>EnemyHealthmanager.TakeDamage()</c>.
    /// </summary>
    /// <param name="other">El collider que entra en el trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthmanager>().TakeDamage();
        }
    }
}
