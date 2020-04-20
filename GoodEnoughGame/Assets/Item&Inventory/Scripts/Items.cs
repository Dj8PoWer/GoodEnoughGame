using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class Items : ScriptableObject
{
    [SerializeField]
    public string id;
    public string ID { get { return id; } }
    [Range(1,99)]
    public int maxStack = 1;
    public string ItemName;
    public Sprite Icon;


#if UNITY_EDITOR
    protected virtual void OnValidate()
    {
        string path = AssetDatabase.GetAssetPath(this);
        id = AssetDatabase.AssetPathToGUID(path);
    }
#endif

    public virtual Items GetCopy()
    {
        Items item = Instantiate(this);
        item.id = this.id;
        return item;
    }

    public virtual void Destroy()
    {

    }
}
