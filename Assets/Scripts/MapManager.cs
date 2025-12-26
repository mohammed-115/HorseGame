using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{

    public Speed_LVL speed_LVL;

    [SerializeField] private List<Transform> levelOneMapOwner;
    [SerializeField] private List<Transform> levelTwoMapOwner;
    [SerializeField] private List<Transform> levelThreeMapOwner;
    [SerializeField] private List<Transform> levelFourMapOwner;
    [SerializeField] private List<Transform> levelFiveMapOwner;

    public static float mapSpeed;

    public enum Speed_LVL
    {
        LVL_ONE,
        LVL_TWO, 
        LVL_THREE,
        LVL_FOUR,
        LVL_FIVE,
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        speed_LVL = Speed_LVL.LVL_ONE;
        mapSpeed = 10;
    }

    


}
