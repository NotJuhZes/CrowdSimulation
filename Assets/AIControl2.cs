using UnityEngine;

public class AIControl2 : MonoBehaviour
{
    GameObject[] goal;
    UnityEngine.AI.NavMeshAgent agent;
    Animator anim;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        goal = GameObject.FindGameObjectsWithTag("goal");
        int i = Random.Range(0, goal.Length);
        agent.SetDestination(goal[i].transform.position);
        anim = GetComponent<Animator>();
        anim.SetTrigger("isWalking");
        anim.SetFloat("wOffset", Random.Range(0f, 1f));
        float sm = Random.Range(0.5f, 2f);
        anim.SetFloat("speedMult", sm);
        agent.speed *= sm;
    }

    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            int i = Random.Range(0, goal.Length);
            agent.SetDestination(goal[i].transform.position);
        }
    }
}
