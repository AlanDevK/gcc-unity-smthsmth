using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] int health = 100;
    NavMeshAgent agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }
    
    public void TakeDamage(){
        health -= 15;
        Debug.Log($"Enemy's health is {health}");
        if (health <= 0){
            Destroy(gameObject);
            Debug.Log("Enemy is dead");
        }
    }
}
