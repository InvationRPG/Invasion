using UnityEngine;

[CreateAssetMenu(fileName = "New Pick", menuName = "Pick Data", order = 51)]
public class PickData : ScriptableObject
{
    [SerializeField]
    private string _pickName;
    [SerializeField]
    private string _description;
    [SerializeField]
    private Sprite _icon;
    [SerializeField]
    private int _attackDamage;

    [SerializeField]
    private int _level;

    [SerializeField]
    private int _speed;

    public string PickName
    {
        get
        {
            return _pickName;
        }
    }

    public string Description
    {
        get
        {
            return _description;
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
