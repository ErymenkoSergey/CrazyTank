using CrazyTank.Interface;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class AIModule : MonoBehaviour, ISimulated
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Vector3 _target;
    public bool _attackActivated { get; private set; } = false;

    private void FixedUpdate()
    {
        if (!_attackActivated)
            return;

        FollowTarget();
    }

    public void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    public void SetStatusAtack(bool isOn)
    {
        _attackActivated = isOn;
    }

    private void FollowTarget()
    {
        if (_target == null)
        {
            Debug.LogError($"Not target! ");
            return;
        }

        _agent.SetDestination(_target);
    }
}
