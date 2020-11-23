using UnityEngine;

[CreateAssetMenu(fileName = "Phase", menuName = "ScriptableObjects/Phase", order = 1)]
public class ScriptablePhase : ScriptableObject
{
    [Tooltip("Nombre de la capa")]
    public string atmosphereLayer;

    [TextArea, Tooltip("Descripción de la capa")]
    public string atmosphereLayerDescription;

    [Tooltip("Tiempo minimo y maximo para cmabiar el colelctable")]
    public float minTimeCollectableChange, maxTimeCollectableChange;

    [Tooltip("Velocidad de los collectables")]
    public float collectableVelocity;

    [Tooltip("Tiempo entre cada spawn")]
    public float timeToSpawn;

    [Tooltip("Puntuación para pasar a la siguiente fase")]
    public float scoreToChangePhase;
    public Color color;
}
