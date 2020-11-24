using UnityEngine;

public class RedLed : MonoBehaviour
{
    private Filler _filler;

    private void Awake()
    {
        _filler = GetComponent<Filler>();
        _filler.SetMaxValue(PlayerStats.SingleInstance.GetShootTime());
    }

    void Update()
    {
        if (PlayerStats.SingleInstance.GetLasserState()) return;
        _filler.SetFillValue(PlayerStats.SingleInstance.GetCurrentShootTime());
    }
}