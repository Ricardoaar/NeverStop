using UnityEngine;

public class Shake : MonoBehaviour
{
    private Animator _animator;
    private static readonly int ShakeTrigger = Animator.StringToHash("Shake");


    private void Awake()
    {
        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        Collectable.OnCollectBad += () => _animator.SetTrigger(ShakeTrigger);
    }
}