using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AdvancedBrain : MonoBehaviour, IEntity {
    [SerializeField] private bool _isMiniBoss;
    [SerializeField] private bool _isMegaBoss;
    private bool _shieldOn, _shieldChanged;

    [SerializeField] private NavMeshAgent _agent;

    [SerializeField] private Transform _playerTransform;

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _playerLayer;

    [SerializeField] private float _health;

    //Patrolling
    [SerializeField] private Vector3 _walkPoint;
    bool walkPointSet;
    [SerializeField] private float _walkPointRange;

    //Attacking
    [SerializeField] private float _timeBetweenAttacks;
    bool alreadyAttacked;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _bulletSpawn;

    //States
    [SerializeField] private float _sightRange, _attackRange;
    [SerializeField] private bool _playerInSightRange, _playerInAttackRange;

    [SerializeField] private int _enemyWeight;
    private bool _isDamageableBoss =  false;

    private GameManager _gameManager;

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();

        Player.AllOrbsCollected += Player_AllOrbsCollected;
    }

    private void Player_AllOrbsCollected() {
        _isDamageableBoss = true;
    }

    private void Update()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //Check for sight and attack range
        _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _playerLayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _playerLayer);

        if (_playerInAttackRange) {
            AttackPlayer();
        } else if (_playerInSightRange) {
            ChasePlayer();
        } else {
            Patrolling();
        }

        if (_isMiniBoss) {
            if (!_shieldChanged) {
                foreach (Transform gameObj in transform.GetChild(1)) {
                    if (_shieldOn) {
                        gameObj.GetComponent<Renderer>().material.color = new Color(0, 0, 200);
                    } else {
                        gameObj.GetComponent<Renderer>().material.color = new Color(200, 0, 0);
                    }
                }
                _shieldOn = !_shieldOn;

                _shieldChanged = true;
                Invoke(nameof(ResetShield), 2f);
            }
        } 
    }

    private void ResetShield() {
        _shieldChanged = false;
    }

    private void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            _agent.SetDestination(_walkPoint);

        Vector3 distanceToWalkPoint = transform.position - _walkPoint;

        //Walk point reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);
        float randomX = UnityEngine.Random.Range(-_walkPointRange, _walkPointRange);

        _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(_walkPoint, -transform.up, 2f, _groundLayer))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        _agent.SetDestination(_playerTransform.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        _agent.SetDestination(transform.position);

        transform.LookAt(_playerTransform);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Instantiate(_projectile, _bulletSpawn.position, this.transform.rotation);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Bullet")) {
            TakeDamage(other.GetComponent<ProjectileController>().ProjectileDamage);
        }
    }


    
    public void TakeDamage(float damage)
    {
        if ((!_isMiniBoss && !_isMegaBoss) || (_isMiniBoss && !_shieldOn) || (_isMegaBoss && _isDamageableBoss)) {
            _health -= damage;
        } 

        if (_health <= 0) Invoke(nameof(DestroyEnemy), 0.1f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
        GameManager.Instance.AddToScore(_enemyWeight);
    }

}
