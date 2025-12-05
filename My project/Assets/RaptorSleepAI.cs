using UnityEngine;
using UnityEngine.AI;

public class RaptorSleepAI : MonoBehaviour
{
    public enum State { Sleeping, FollowingPlayer, ReturningToSleep }
    public State currentState = State.Sleeping;

    public Transform player;
    public float wakeDistance = 8f;
    public float stopFollowDistance = 15f;

    private NavMeshAgent agent;
    private Animator animator;

    private Vector3 sleepPosition; // Donde duerme originalmente

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        sleepPosition = transform.position; // Guardamos la posición para volver

        EnterSleepState();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case State.Sleeping:
                if (distanceToPlayer < wakeDistance)
                {
                    // Wake up
                    currentState = State.FollowingPlayer;
                    animator.Play("walk");
                }
                break;

            case State.FollowingPlayer:
                agent.isStopped = false;
                agent.SetDestination(player.position);
                animator.Play("run");

                if (distanceToPlayer > stopFollowDistance)
                {
                    currentState = State.ReturningToSleep;
                    agent.SetDestination(sleepPosition);
                    animator.Play("walk");
                }
                break;

            case State.ReturningToSleep:
                float dist = Vector3.Distance(transform.position, sleepPosition);

                if (dist < 1f)
                {
                    EnterSleepState();
                }
                break;
        }
    }

    void EnterSleepState()
    {
        currentState = State.Sleeping;
        agent.isStopped = true;
        transform.position = sleepPosition; // Asegurar que esté bien colocado
        animator.Play("sleep");
    }
}
