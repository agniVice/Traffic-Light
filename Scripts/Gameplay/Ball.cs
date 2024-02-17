using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public ColorType Type;

    public float Speed = 1.2f;

    private Rigidbody2D _rigidBody;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private GameObject _particlePrefab;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
    }
    private void FixedUpdate()
    {
        if (GameState.Instance.CurrentState != GameState.State.InGame)
        {
            _rigidBody.velocity = Vector3.zero;
            return;
        }

        _rigidBody.velocity = -Vector2.up * Speed;
    }
    public void Initialize(ColorType type, float speed, Sprite sprite, GameObject particlePrefab)
    { 
        Type = type;
        Speed = speed;

        _spriteRenderer.sprite = sprite;
        _particlePrefab = particlePrefab;
    }
    private void SpawnParticle()
    {
        var particle = Instantiate(_particlePrefab).GetComponent<ParticleSystem>();

        particle.transform.position = new Vector2(transform.position.x, transform.position.y + 0.2f);
        particle.Play();

        Destroy(particle.gameObject, 2f);
    }
    public void OnBallCorrect()
    {
        BallSpawner.Instance.DeleteMe(this);

        SpawnParticle();
        Destroy(_collider);

        transform.DOScale(0, 0.2f).SetLink(gameObject);

        Destroy(gameObject, 0.3f);
    }
}
