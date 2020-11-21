using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnCollectableEnter : UnityEvent<GameObject>
{
}

public class KillZone : MonoBehaviour
{
    public OnCollectableEnter onCollectableEnter = new OnCollectableEnter();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Collectable"))
            onCollectableEnter.Invoke(other.gameObject);
    }
}
