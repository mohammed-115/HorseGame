using UnityEngine;

public class HorseJump : MonoBehaviour
{
    public Rigidbody rb;
    public Animator horseAnimator;
    public float jumpForce = 7f;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    private bool isGrounded;
    private float jumpDelay = 0.1f; // وقت انتظار بسيط لمنع التكرار
    private float nextJumpTime;

    void Update()
    {
        // 1. التحقق من ملامسة الأرض
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // 2. تحديث الأنيميشن: نستخدم التحقق من الأرض مباشرة
        // لضمان عدم تعليق الأنيميشن، نتأكد من أننا لسنا في "وقت الانتظار"
        if (Time.time > nextJumpTime)
        {
            horseAnimator.SetBool("isJumping", !isGrounded);
        }

        // 3. مدخلات القفز
        bool jumpInput = Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);

        // 4. الشرط المعدل: إضافة شرط الوقت (Time.time > nextJumpTime)
        if (isGrounded && IsRunning() && jumpInput && Time.time > nextJumpTime)
        {
            Jump();
        }
    }

    bool IsRunning()
    {
        return horseAnimator.GetBool("IsGameStarting");
    }

    void Jump()
    {
        // تحديد موعد القفزة القادمة لمنع التكرار العشوائي
        nextJumpTime = Time.time + jumpDelay;

        // تصفير السرعة لضمان قفزة نظيفة
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // تفعيل الأنيميشن فوراً عند القفز
        horseAnimator.SetBool("isJumping", true);
    }
}