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
        _pointsPFill = 1 / _maxValue;
        fill.fillAmount = 1;
    }

    public void SetMaxValue(float value)
    {
        _maxValue = value;
    }

    public void SetFillValue(float value)
    {
        fill.fillAmount = value * _pointsPFill;
    }
}