using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 210;
    [SerializeField] private LayerMask horseLayerMask;
    [SerializeField] private Player player;

    private void Awake()
    {
        if (!player)
            player = FindAnyObjectByType<Player>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(((1 << other.gameObject.layer) & horseLayerMask) != 0)
        {
            //increase counter by 1
            player.AddCoin();
            //add coin sound
            AudioSource audioSource = GetComponent<AudioSource>();
            if (audioSource != null)
            {
                //adjust audio volume

                //play the sound
                audioSource.Play();
            }

            // 3. إخفاء الكائن (Disable)
            // نقوم بإخفاء الشكل (Mesh) لكي يختفي من أمام اللاعب فوراً
            GetComponent<MeshRenderer>().enabled = false;
            // ونعطل الـ Collider لكي لا يصطدم به الحصان مرة أخرى
            GetComponent<Collider>().enabled = false;

            // 4. تدمير الكائن بعد 3 ثوانٍ
            // ننتظر قليلاً لكي ينتهي صوت العملة من العمل قبل الحذف النهائي
            Destroy(gameObject, 3f);
        }

    }

}
