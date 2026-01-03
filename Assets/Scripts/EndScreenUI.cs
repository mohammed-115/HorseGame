using TMPro;
using UnityEngine;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestRecordText;

    private const string BEST_SCORE_KEY = "BestScore"; // اسم المفتاح الذي سنخزن به القيمة

    private void OnEnable()
    {
        if (PlayerDieManager.instance != null)
        {
            PlayerDieManager.instance.onEndScreenShow += PlayerDieManager_onEndScreenShow;
        }
    }

    private void OnDisable()
    {
        if (PlayerDieManager.instance != null)
        {
            PlayerDieManager.instance.onEndScreenShow -= PlayerDieManager_onEndScreenShow;
        }
    }

    private void PlayerDieManager_onEndScreenShow(object sender, System.EventArgs e)
    {
        if (Player.instance != null)
        {
            // 1. حساب السكور الحالي
            int finalScore = Player.instance.coinsCounter * 10;
            scoreText.text = finalScore.ToString("N0");

            // 2. جلب أعلى سكور مسجل سابقاً (إذا لم يوجد سيسجل 0)
            int previousBestScore = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);

            // 3. عرض أعلى سكور (القديم) للاعب
            bestRecordText.text = previousBestScore.ToString("N0");

            // 4. التحقق إذا كان السكور الحالي أعلى من المسجل
            if (finalScore > previousBestScore)
            {
                // تخزين السكور الجديد كأعلى سكور
                PlayerPrefs.SetInt(BEST_SCORE_KEY, finalScore);
                PlayerPrefs.Save(); // حفظ التغييرات فوراً

            }

        }
    }
}