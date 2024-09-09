using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    [SerializeField] private float _speed = 2f;

    private int _currentWaypoint = 0;
    private Vector3 _currentPosition;

    private void Update()
    {        
        _currentPosition = transform.position;

        Patrol();
        TurnInMovingDirection();
    }

    private void Patrol()
    {
        if (transform.position == _waypoints[_currentWaypoint].position)
            _currentWaypoint = ++_currentWaypoint % _waypoints.Length;

        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].position, _speed * Time.deltaTime);
    }

    private void TurnInMovingDirection()
    {
        float TurnLeftYAxisDegreese = 180;
        float TurnRightYAxisDegreese = 0;

        if (transform.position.x < _currentPosition.x)
            transform.rotation = Quaternion.Euler(0, TurnLeftYAxisDegreese, 0);
        else if (transform.position.x > _currentPosition.x)
            transform.rotation = Quaternion.Euler(0, TurnRightYAxisDegreese, 0);
    }
}
