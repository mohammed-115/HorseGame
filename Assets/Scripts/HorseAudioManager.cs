using UnityEngine;

public class HorseAudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [Tooltip("تأكد من تفعيل خيار Loop وتعطيل Play On Awake لهذا المصدر")]
    public AudioSource runningSource;
    [Tooltip("تعطيل Loop و Play On Awake لهذا المصدر")]
    public AudioSource effectsSource;

    [Header("Audio Clips")]
    public AudioClip runningClip;
    public AudioClip jumpClip;
    public AudioClip landClip;

    [Header("Ground Check Settings")]
    public LayerMask groundLayer;      // اختر طبقة الأرض (Ground) من المفتش
    public Transform groundCheck;     // الكائن الفارغ الموجود عند أقدام الحصان
    public float checkRadius = 0.2f;  // حجم دائرة التحقق
    public float landToRunDelay = 0.25f; // وقت الانتظار بعد الهبوط قبل بدء الركض

    private bool isGrounded;
    private bool wasGrounded;

    void Start()
    {
        // إعداد صوت الركض الأولي
        if (runningSource != null)
        {
            runningSource.clip = runningClip;
            runningSource.loop = true;
            runningSource.playOnAwake = false;
        }

        // فحص الحالة الأولية للأرض لمنع تشغيل صوت الهبوط عند أول فريم
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);
        wasGrounded = isGrounded;
    }

    void Update()
    {
        // التحقق المستمر من حالة الأرض
        isGrounded = Physics.CheckSphere(groundCheck.position, checkRadius, groundLayer);

        // إذا لم تبدأ اللعبة بعد، نحدث الحالة فقط ونوقف الأصوات
        if (!GameManager.gameHasStarted)
        {
            if (runningSource.isPlaying) runningSource.Stop();
            wasGrounded = isGrounded;
            return;
        }

        // منطق اكتشاف الهبوط (Landing)
        // لا يشغل الصوت إلا إذا كان في الهواء (wasGrounded = false) وأصبح على الأرض
        if (isGrounded && !wasGrounded)
        {
            HandleLanding();
        }

        // إدارة صوت الركض المستمر
        HandleRunningLoop();

        // تحديث الحالة السابقة للفريم القادم
        wasGrounded = isGrounded;
    }

    private void HandleRunningLoop()
    {
        // يشغل صوت الركض فقط إذا كان على الأرض، اللعبة بدأت، ولا يوجد تأخير "Invoke" حالي
        if (isGrounded && !runningSource.isPlaying && !IsInvoking("StartRunningSound"))
        {
            runningSource.Play();
        }
        // يوقف الصوت فوراً إذا طار الحصان في الهواء
        else if (!isGrounded && runningSource.isPlaying)
        {
            runningSource.Stop();
        }
    }

    private void HandleLanding()
    {
        // نوقف الركض ونلغي أي طلبات تشغيل سابقة لضمان التزامن
        CancelInvoke("StartRunningSound");
        runningSource.Stop();

        // تشغيل صوت الهبوط لمرة واحدة
        if (landClip != null)
            effectsSource.PlayOneShot(landClip);

        // جدولة العودة لصوت الركض بعد انتهاء تأثير الهبوط
        Invoke("StartRunningSound", landToRunDelay);
    }

    private void StartRunningSound()
    {
        // التأكد من أن الحصان لا يزال على الأرض قبل إعادة تشغيل الصوت
        if (isGrounded && GameManager.gameHasStarted)
        {
            runningSource.Play();
        }
    }

    // يتم استدعاء هذه الدالة من سكريبت الحركة (Jump Logic)
    public void PlayJumpSound()
    {
        CancelInvoke("StartRunningSound");
        if (runningSource.isPlaying) runningSource.Stop();

        if (jumpClip != null)
            effectsSource.PlayOneShot(jumpClip);
    }
}