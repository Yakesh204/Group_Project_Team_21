using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    public Transform Target;
    private EnemyReferences enemyReference;
    private float shootingDistance;
    private float pathUpdateDeadline;

    private void Awake()
    {
        enemyReference = GetComponent<EnemyReferences>();
    }
    // Start is called before the first frame update
    void Start()
    {
        shootingDistance = enemyReference.agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            bool inRange = Vector3.Distance(transform.position, Target.position) <= shootingDistance;

            if(inRange)
            {
                LookAtTarget();
            }
            else
            {
                UpdatePath();
            }
            enemyReference.animator.SetBool("Shooting", inRange);
            enemyReference.animator.SetFloat("Speed", enemyReference.agent.desiredVelocity.sqrMagnitude);
        }
       
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = Target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);

    }
    private void UpdatePath()
    {
        if(Time.time >= pathUpdateDeadline)
        {
            Debug.Log("Updating Path");
            pathUpdateDeadline = Time.time + enemyReference.pathUpdateDelay;
            enemyReference.agent.SetDestination(Target.position);
        }

    }
}
