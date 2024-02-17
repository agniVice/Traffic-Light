using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;

    [SerializeField] private GameObject _prefab;
    [SerializeField] private GameObject[] _particlePrefabs;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Sprite[] _sprites;

    private float _targetSpeed = 1.2f;

    private GameObject _lastBall;

    private List<Ball> _balls = new List<Ball>();

    private void OnEnable()
    {
        GameState.Instance.ScoreAdded += ChangeSpeed;
    }
    private void OnDisable()
    {
        GameState.Instance.ScoreAdded -= ChangeSpeed;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    private void FixedUpdate()
    {
        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;

        if (_lastBall != null)
        {
            if (Vector2.Distance(_spawnPosition.position, _lastBall.transform.position) >= 1f)
                SpawnRandomBall();
        }
        else
            SpawnRandomBall();

    }
    public void SpawnRandomBall()
    {
        var type = GetRandomType();

        var obj = Instantiate(_prefab, _spawnPosition.position, Quaternion.identity);
        obj.GetComponent<Ball>().Initialize(type, _targetSpeed, _sprites[(int)type], _particlePrefabs[(int)type]);

        _balls.Add(obj.GetComponent<Ball>());

        _lastBall = obj;
    }
    private void ChangeSpeed()
    {
        if (_targetSpeed <= 3f)
        {
            foreach (Ball ball in _balls)
            {
                ball.Speed += 0.03f;
            }
            _targetSpeed += 0.03f;
        }
    }
    public void DeleteMe(Ball ball)
    {
        _balls.Remove(ball);
    }
    private ColorType GetRandomType()
    {
        return (ColorType)Random.Range(0, 3);
    }
}
