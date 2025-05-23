using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controla el comportamiento de un enemigo mediante un sistema de estados (IA): patrullar, perseguir, atacar o esperar.
/// </summary>
/// <remarks>
/// Utiliza <see cref="NavMeshAgent"/> para movimiento y <see cref="Animator"/> para controlar animaciones.
/// Cambia de estado en función de la distancia al jugador.
/// </remarks>
public class EnemyController : MonoBehaviour
{
    #region Variables Públicas
    /// <summary>
    /// Puntos por los que el enemigo patrulla.
    /// </summary>
    public Transform[] patrolPoints;

    /// <summary>
    /// Índice del punto de patrulla actual.
    /// </summary>
    public int currentPatrolPoint;

    /// <summary>
    /// Agente de navegación utilizado para el movimiento automático.
    /// </summary>
    public NavMeshAgent agent;

    /// <summary>
    /// Componente de animación del enemigo.
    /// </summary>
    public Animator animator;

    /// <summary>
    /// Tiempo que el enemigo espera al llegar a un punto de patrulla.
    /// </summary>
    public float WaitAtPoint = 2f;

    /// <summary>
    /// Contador interno para controlar el tiempo de espera.
    /// </summary>
    private float waitCounter;

    /// <summary>
    /// Distancia mínima desde el jugador para comenzar a perseguirlo.
    /// </summary>
    public float chaseRange;

    /// <summary>
    /// Distancia mínima desde el jugador para iniciar un ataque.
    /// </summary>
    public float attackRange = 1f;

    /// <summary>
    /// Tiempo entre ataques consecutivos.
    /// </summary>
    public float timeBetweenAttacks = 2f;

    /// <summary>
    /// Contador interno que determina cuándo puede volver a atacar.
    /// </summary>
    private float attackCounter;

    /// <summary>
    /// Estados posibles de la IA del enemigo.
    /// </summary>
    public enum AIState
    {
        Idle,
        Patrolling,
        Chasing,
        Attacking
    };

    /// <summary>
    /// Estado actual del enemigo.
    /// </summary>
    public AIState currentState;
    #endregion

    #region Métodos Unity
    /// <summary>
    /// Inicializa el estado de espera al comenzar.
    /// </summary>
    void Start()
    {
        waitCounter = WaitAtPoint;
    }

    /// <summary>
    /// Controla el comportamiento del enemigo según su estado actual y la distancia al jugador.
    /// </summary>
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        switch (currentState)
        {
            case AIState.Idle:

                animator.SetBool("IsMoving", false);

                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = AIState.Patrolling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
                    animator.SetBool("IsMoving", true);
                }

                break;

            case AIState.Patrolling:

                //agent.SetDestination(patrolPoints[currentPatrolPoint].position);

                if (agent.remainingDistance <= .2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }

                    //agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                    currentState = AIState.Idle;
                    waitCounter = WaitAtPoint;
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = AIState.Chasing;
                }

                animator.SetBool("IsMoving", true);

                break;

            case AIState.Chasing:

                agent.SetDestination(PlayerController.instance.transform.position);

                if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.Attacking;
                    animator.SetTrigger("Attack");
                    animator.SetBool("IsMoving", false);

                    agent.velocity = Vector3.zero;
                    agent.isStopped = true;

                    attackCounter = timeBetweenAttacks;

                }

                if (distanceToPlayer > chaseRange)
                {
                    currentState = AIState.Idle;
                    waitCounter = WaitAtPoint;
                    
                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }

                break;

            case AIState.Attacking:

                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;
                if (attackCounter <= 0)
                {
                    if (distanceToPlayer < attackRange)
                    {
                        animator.SetTrigger("Attack");
                        attackCounter = timeBetweenAttacks;
                    }
                    else
                    {
                        currentState = AIState.Idle;
                        waitCounter = WaitAtPoint;

                        agent.isStopped = false;
                    }
                }

                break;
        }
        
    }
    #endregion

}
