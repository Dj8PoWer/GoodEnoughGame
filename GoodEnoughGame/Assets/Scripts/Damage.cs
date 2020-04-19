using UnityEngine;

public class Damage : MonoBehaviour
{
    public float physical = 5f;
    public float fire = 5f;
    public float water = 5f;
    public float air = 5f;

    public string target;

    public float[] Damages()
    {
        return new[] {physical, fire, water, air};
    }
}
