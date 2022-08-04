using UnityEngine;

[CreateAssetMenu(fileName = "New Ore", menuName = "Data/Ore Data", order = 52)]
public class OreData : ScriptableObject
{
    [SerializeField]
    private string _nameOre;

    [SerializeField]
    private int _digTimeSec;

    [SerializeField]
    private float _minLevelPick;

    [SerializeField]
    private GameObject _prefabNugget;

    public string OreName
    {
        get
        {
            return _nameOre;
        }
    }

    public int DigTimeSec
    {
        get
        {
            return _digTimeSec;
        }
    }

    public float MinLevelPick
    {
        get
        {
            return _minLevelPick;
        }
    }

    public GameObject PrefabNugget
    {
        get
        {
            return _prefabNugget;
        }
    }


}
