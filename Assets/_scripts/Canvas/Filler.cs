using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(500)]
public class Filler : MonoBehaviour
{
    [SerializeField] private Image fill;

    private float _maxValue;
    private float _pointsPFill;

    private void Start()
    {
        fill.fillAmount = 1;
    }

    public void SetMaxValue(float value)
    {
        _maxValue = value;
        _pointsPFill = 1 / value;
    }

    public void SetFillValue(float value)
    {
        fill.fillAmount = value * _pointsPFill;
    }
}