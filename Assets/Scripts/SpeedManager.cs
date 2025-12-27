using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public static float GlobalSpeed = 10f; // سرعة عامة يمكن الوصول لها من أي مكان
    public float acceleration = 0.1f;      // مقدار الزيادة في السرعة
    public float maxSpeed = 25f;

    void Update()
    {
        if (!GameManager.gameHasStarted) return;

        // زيادة السرعة تدريجياً مع الوقت
        if (GlobalSpeed < maxSpeed)
        {
            GlobalSpeed += acceleration * Time.deltaTime;
        }
    }
}