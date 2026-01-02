using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player instance;

    public int coinsCounter { get; private set; }
    [SerializeField] private TextMeshProUGUI coinsCounterText;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coinsCounter = 0;
    }


    public void AddCoin()
    {
        coinsCounter++;
        coinsCounterText.text = coinsCounter.ToString();
    }

}
