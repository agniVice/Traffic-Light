using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorWheel : MonoBehaviour
{
    public ColorType CurrentType;

    [SerializeField] private Sprite[] _sprites;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void UpdateSprite()
    {
        _spriteRenderer.sprite = _sprites[(int)CurrentType];
    }
    public void ChangeColorType(ColorType type)
    { 
        CurrentType = type;

        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f).SetLink(gameObject).SetEase(Ease.OutBack);

        UpdateSprite();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;

        var ball = collision.gameObject.GetComponent<Ball>();

        if (ball == null)
            return;

        if (ball.Type == CurrentType)
        {
            PlayerScore.Instance.AddScore();
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.ScoreAdd, Random.Range(0.9f, 1.1f));

            ball.OnBallCorrect();
        }
        else
        {
            AudioVibrationManager.Instance.PlaySound(AudioVibrationManager.Instance.Win, 1f);
            GameState.Instance.FinishGame();
        }
    }
}
