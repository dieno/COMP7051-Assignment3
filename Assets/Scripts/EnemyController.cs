using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform[] points;
    private int destPoint = 0;
    private Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        // Disabling auto-braking allows for continuous movement
        // between points (ie, the agent doesn't slow down as it
        // approaches a destination point).
        agent.autoBraking = false;

        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        // Choose the next destination point when the agent gets
        // close to the current one.
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();

        if (agent.velocity.x != 0f || agent.velocity.z != 0f)
        {
            anim.SetBool("move", true);
        }
        else
        {
            anim.SetBool("move", false);
        }
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

}