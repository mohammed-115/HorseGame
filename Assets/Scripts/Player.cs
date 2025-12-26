using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    private int coinsCounter = 0;
    [SerializeField] private TextMeshProUGUI coinsCounterText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoin()
    {
        coinsCounter++;
        coinsCounterText.text = coinsCounter.ToString();
        
    }

}
