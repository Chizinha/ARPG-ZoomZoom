using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public delegate void OnDisableCallback(Enemy Instance);
    public OnDisableCallback Disable;

    private NavMeshTriangulation Triangulation;
    private NavMeshAgent agent;
    private EnemyMovement movement;

    private int health = 2;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        movement = GetComponent<EnemyMovement>();
    }
    private void Start()
    {
        Triangulation = NavMesh.CalculateTriangulation();
    }

    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0f)
        {
            Disable?.Invoke(this);
        }
    }

    public void Setup()
    {
        Triangulation = NavMesh.CalculateTriangulation();
        int VertexIndex = Random.Range(0, Triangulation.vertices.Length);

        NavMeshHit Hit;
        if (NavMesh.SamplePosition(Triangulation.vertices[VertexIndex], out Hit, 2f, -1))
        {
            this.agent.Warp(Hit.position);
            this.movement.StartFollowTarget();
        }
        else
        {
            Debug.LogError($"Could not place NavMeshAgent on NavMesh.");
        }

        health = 2;
    }

}
