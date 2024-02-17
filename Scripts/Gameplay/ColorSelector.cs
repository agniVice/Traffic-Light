using UnityEngine;

public class ColorSelector : MonoBehaviour
{
    public ColorType Type;

    [SerializeField] private ColorWheel _wheel;

    private void OnMouseDown()
    {
        if (GameState.Instance.CurrentState != GameState.State.InGame)
            return;

        _wheel.ChangeColorType(Type);
    }
}
