using UnityEngine;

public static class Util
{
    /// <summary>
    /// Encuentra el valor de un angulo a partir de un Vector
    /// </summary>
    /// <param name="lAngle"></param>
    /// <returns></returns>
    public static Vector3 GetVectorFromAngle(float lAngle)
    {
        var angleRad = lAngle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    /// <summary>
    /// Retorna el angulo de un vector
    /// </summary>
    /// <param name="lVector"></param>
    /// <returns></returns>
    public static float GetAngleFromVector(Vector3 lVector)
    {
        lVector = lVector.normalized;
        var n = Mathf.Atan2(lVector.y, lVector.x) * Mathf.Rad2Deg;
        n += n < 0 ? 360 : 0;
        return n;
    }
}