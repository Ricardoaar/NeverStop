using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class OnCollectableEnter : UnityEvent<GameObject>
{
}

[System.Serializable]
public class OnGameDecorationEnter : UnityEvent<SpriteRenderer>
{
}

public class KillZone : MonoBehaviour
{
    public OnCollectableEnter onCollectableEnter = new OnCollectableEnter();

    public OnGameDecorationEnter onGameDecorationEnter = new OnGameDecorationEnter();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Collectable"))
            onCollectableEnter.Invoke(other.gameObject);
        if (other.gameObject.layer == LayerMask.NameToLayer("Decoration"))
        {
            onGameDecorationEnter.Invoke(other.GetComponent<SpriteRenderer>());
        }
    }
}