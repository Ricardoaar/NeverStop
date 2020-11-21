using UnityEngine;

public class FactoryCollectable
{
    public GameObject CollectableBase { get; set; }

    public FactoryCollectable(GameObject collectableBase) => CollectableBase = collectableBase;

    public GameObject CreateCollectable()
    {
        GameObject collectable = GameObject.Instantiate(CollectableBase);
        collectable.SetActive(false);
        return collectable;
    }
}
