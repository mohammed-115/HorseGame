using UnityEngine;

public class HorseJump : MonoBehaviour
{
    public Rigidbody rb;
    public Animator horseAnimator;
    public float jumpForce = 7f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;      // نقطة فارغة توضع أسفل حوافر الحصان
    public float groundDistance = 0.05f; // نصف قطر دائرة التحقق
    public LayerMask groundMask;       // تختار منها طبقة الأرض (Ground Layer)

    private bool isGrounded;
    private bool gameStarted = false;

    void Update()
    {
        // 1. التحقق إذا كان الحصان يلمس الأرض باستخدام الـ Layer
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // تحديث الأنيميشن بناءً على حالة الأرض
        horseAnimator.SetBool("isJumping", !isGrounded);

        // 2. التحقق من الضغط للقفز
        bool jumpInput = Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);

        // التحقق من أن اللعبة بدأت (عن طريق الباراميتر اللي عملناه سابقاً)
        if (horseAnimator.GetBool("IsGameStarting") && jumpInput && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // استخدام Impulse يعطي قوة دفع فورية مناسبة للقفز
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // لرؤية دائرة التحقق في نافذة الـ Scene للتأكد من مكانها
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}