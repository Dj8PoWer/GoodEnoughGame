using UnityEngine;

public class Damage : MonoBehaviour
{
    public float physical = 0;
    public float fire = 0;
    public float water = 0;
    public float air = 0;

    public string target;

    public float[] Damages()
    {
        return new[] {physical, fire, water, air};
    }
}
