public class StatModifier
{
    public readonly float value;
    public readonly object source;

    public StatModifier(float value, object source)
    {
        this.value = value;
        this.source = source;
    }

    public StatModifier(float value) : this (value, null) { }
}
