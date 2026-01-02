using TMPro;
using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    // يُفضل استخدام OnEnable لضمان تجديد الاشتراك دائماً
    private void OnEnable()
    {
        // نتحقق من وجود الـ instance أولاً
        if (PlayerDieManager.instance != null)
        {
            PlayerDieManager.instance.onEndScreenShow += PlayerDieManager_onEndScreenShow;
        }
    }

    // ضروري جداً لإلغاء الاشتراك عند تحميل مشهد جديد أو تدمير الكائن
    private void OnDisable()
    {
        if (PlayerDieManager.instance != null)
        {
            PlayerDieManager.instance.onEndScreenShow -= PlayerDieManager_onEndScreenShow;
        }
    }

    private void PlayerDieManager_onEndScreenShow(object sender, System.EventArgs e)
    {
        // تأكد من أن Player.instance موجود ولم يتم تدميره بعد
        if (Player.instance != null)
        {
            int finalScore = Player.instance.coinsCounter * 10;
            scoreText.text = finalScore.ToString();
            Debug.Log("Score Updated: " + finalScore);
        }
    }
}