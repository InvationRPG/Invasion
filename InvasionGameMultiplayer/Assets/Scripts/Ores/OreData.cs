using UnityEngine;

[CreateAssetMenu(fileName = "New Ore", menuName = "Ore Data", order = 52)]
public class OreData : ScriptableObject
{
    [SerializeField]
    private string _nameOre;

    [SerializeField]
    private float _digTimeSec;

    [SerializeField]
    private float _minLevelPick;

    public string OreName
    {
        get
        {
            return _nameOre;
        }
    }

    public float DigTimeSec
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

}
