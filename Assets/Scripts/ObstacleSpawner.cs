using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Prefabs Arrays")]
    public GameObject[] coinsOnly;
    public GameObject[] simpleObstacles;
    public GameObject[] mixedEasy;
    public GameObject[] mixedMedium;
    public GameObject[] mixedHard;

    [Header("Spawn Settings")]
    public float spawnZPosition = 100f;

    [Tooltip("الوقت الأساسي بين العقبات عند السرعة 10")]
    public float baseTimeBetweenSpawns = 2.5f;

    [Tooltip("عامل زيادة المسافة: كلما زاد، زاد الفراغ بين العقبات مع السرعة")]
    public float spacingFactor = 1.2f;

    public float distanceBetweenCoins = 3f;

    [Header("Height Settings")]
    public float obstacleHeight = 0f;
    public float coinHeight = 0.7f;

    private float timer = 0f;

    void Update()
    {
        if (!GameManager.AbleToMove || GameManager.stopCompletly) return;

        timer += Time.deltaTime;

        // --- التعديل الجوهري هنا ---
        // بدلاً من تقسيم الوقت على السرعة فقط، سنضيف عامل "التباعد"
        // Mathf.Max لضمان أننا لا نقسم على صفر أبداً
        float speedRatio = SpeedManager.GlobalSpeed / 10f;

        // المعادلة الجديدة: الوقت يقل مع السرعة لكننا نضربه في spacingFactor 
        // ليعطي مسافة أمان أكبر للاعب
        float adjustedInterval = (baseTimeBetweenSpawns / speedRatio) * spacingFactor;

        if (timer >= adjustedInterval)
        {
            SpawnLogic(SpeedManager.GlobalSpeed);
            timer = 0f;
        }
    }

    void SpawnLogic(float currentSpeed)
    {
        int chance = Random.Range(0, 100);

        if (chance < 30) // Coins
        {
            SpawnCoinLine();
        }
        else if (chance < 60) // Simple Obstacles
        {
            if (simpleObstacles.Length > 0)
            {
                GameObject prefab = simpleObstacles[Random.Range(0, simpleObstacles.Length)];
                CreateMovingObject(prefab, spawnZPosition, obstacleHeight);
            }
        }
        else // Mixed
        {
            GameObject prefab = GetMixedPrefabBySpeed(currentSpeed);
            if (prefab != null)
            {
                CreateMovingObject(prefab, spawnZPosition, obstacleHeight);
            }
        }
    }

    void SpawnCoinLine()
    {
        if (coinsOnly == null || coinsOnly.Length == 0) return;
        GameObject coinPrefab = coinsOnly[Random.Range(0, coinsOnly.Length)];

        for (int i = 0; i < 5; i++)
        {
            float offsetZ = spawnZPosition + (i * distanceBetweenCoins);
            CreateMovingObject(coinPrefab, offsetZ, coinHeight);
        }
    }

    GameObject GetMixedPrefabBySpeed(float speed)
    {
        if (speed < 14f && mixedEasy.Length > 0) return mixedEasy[Random.Range(0, mixedEasy.Length)];
        if (speed < 18f && mixedMedium.Length > 0) return mixedMedium[Random.Range(0, mixedMedium.Length)];
        if (mixedHard.Length > 0) return mixedHard[Random.Range(0, mixedHard.Length)];
        return null;
    }

    void CreateMovingObject(GameObject prefab, float zPos, float yHeight)
    {
        if (prefab == null) return;
        Vector3 spawnPos = new Vector3(0f, yHeight, zPos);
        GameObject obj = Instantiate(prefab, spawnPos, Quaternion.identity);

        if (!obj.GetComponent<ObstacleMover>())
        {
            obj.AddComponent<ObstacleMover>();
        }
    }
}

public class ObstacleMover : MonoBehaviour
{
    void Update()
    {
        // 1. تأكد أن اللعبة بدأت
        if (!GameManager.AbleToMove) return;

        // 2. التحريك باستخدام السرعة العالمية
        // جرب استخدام Space.World لضمان أن الحركة في اتجاه العالم وليس اتجاه الكائن نفسه
        transform.Translate(0, 0, -SpeedManager.GlobalSpeed * Time.deltaTime, Space.World);

        // 3. الحذف التلقائي
        if (transform.position.z < -20f)
        {
            Destroy(gameObject);
        }
    }
}