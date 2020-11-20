/* Agregar para darle descripcion a un gameobject que pueda llegar a ser confuso*/

using UnityEngine;

public class HowToUse : MonoBehaviour
{
    [TextArea(5, 10)] public string HowToUseThis;
}