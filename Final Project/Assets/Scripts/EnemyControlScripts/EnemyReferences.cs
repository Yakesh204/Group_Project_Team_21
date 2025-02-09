using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;
[DisallowMultipleComponent]
public class EnemyReferences : MonoBehaviour
{
    // Start is called before the first frame update
    [HideInInspector]public NavMeshAgent agent;
    [HideInInspector] public Animator animator;

    [Header("Stats")]
    public float pathUpdateDelay = 0.2f;

    private void Awake()
    {
       agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

}
