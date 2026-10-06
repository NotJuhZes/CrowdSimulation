using UnityEngine;

public class AIControl2 : MonoBehaviour
{
    GameObject[] goal;
    UnityEngine.AI.NavMeshAgent agent;
    Animator anim;
    float speedMult;
    float detectionRadius = 5f;
    float fleeRadius = 10f;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        goal = GameObject.FindGameObjectsWithTag("goal");
        int i = Random.Range(0, goal.Length);
        agent.SetDestination(goal[i].transform.position);
        anim = GetComponent<Animator>();
        anim.SetTrigger("isWalking");
        anim.SetFloat("wOffset", Random.Range(0f, 1f));

        ResetAgent();
    }

    void ResetAgent()
    {
        speedMult = Random.Range(0.1f, 1.5f);
        anim.SetFloat("speedMult", speedMult);
        anim.SetTrigger("isWalking");
        agent.speed *= speedMult;
        agent.angularSpeed = 120f;
        agent.ResetPath();
    }

    public void DetectNewObstacle(Vector3 position)
    {
        if (Vector3.Distance(position, transform.position) < detectionRadius)
        {
            Vector3 fleeDirection = (transform.position - position).normalized;
            Vector3 newGoal = transform.position + fleeDirection * fleeRadius;

            UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
            agent.CalculatePath(newGoal, path);

            if (path.status != UnityEngine.AI.NavMeshPathStatus.PathInvalid)
            {
                agent.SetDestination(path.corners[path.corners.Length - 1]);
                anim.SetTrigger("isRunning");
                agent.speed = 10f;
                agent.angularSpeed = 500f;
            }
        }
    }

    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            ResetAgent();
            int i = Random.Range(0, goal.Length);
            agent.SetDestination(goal[i].transform.position);
        }
    }
}
