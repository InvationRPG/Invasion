using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Data/Item Data", order = 51)]
public class ItemData : ScriptableObject
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private string _description;

    [SerializeField]
    private Sprite _icon;

    [SerializeField]
    private GameObject _prefabItem;

    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private int _level;

    [SerializeField]
    private int _speed;

    [SerializeField]
    private Typeobjects _type;

    public string Name
    {
        get
        {
            return _name;
        }
    }


    public string Description
    {
        get
        {
            return _description;
        }
    }

    public GameObject PrefabItem
    {
        get
        {
            return _prefabItem;
        }
    }

    public Typeobjects Type
    {
        get
        {
            return _type;
        }
    }

    public Sprite Icon
    {
        get
        {
            return _icon;
        }
    }


    public int AttackDamage
    {
        get
        {
            return _attackDamage;
        }
    }

    public int Level
    {
        get
        {
            return _level;
        }
    }

    public int Speed
    {
        get
        {
            return _speed;
        }
    }
}
