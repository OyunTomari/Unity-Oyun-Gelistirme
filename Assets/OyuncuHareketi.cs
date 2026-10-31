using UnityEngine.UI;
using UnityEngine;

public class OyuncuHareketi : MonoBehaviour
{
    public int can = 100;

    public Slider canCubugu;
    public LayerMask zeminKatmani;

    public float hareketHizi = 2f;
    public float ziplamaGucu = 5f;
    public Transform zeminKontrolNoktasi;
    public float kontrolYaricapi = 0.15f;

    private Rigidbody2D rb;
    private Vector2 yon;
    private bool yerdeMi = false;
    private Vector2 yenidenDogmaKonumu;

    void Start()
    {
        canCubugu.value = can;
        rb = GetComponent<Rigidbody2D>();
        yenidenDogmaKonumu = rb.position;
        Debug.Log("Oyuncu maceraya hazır");
    }

    void Update()
    {
        yon = Vector2.zero;

        if (Input.GetKey(KeyCode.D))
            yon += Vector2.right;

        if (Input.GetKey(KeyCode.A))
            yon += Vector2.left;

        if (Input.GetKeyDown(KeyCode.Space) && yerdeMi)
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                ziplamaGucu
            );

            yerdeMi = false;
        }

        yon = yon.normalized;
    }

    void FixedUpdate()
    {
        yerdeMi = Physics2D.OverlapCircle(
            zeminKontrolNoktasi.position,
            kontrolYaricapi,
            zeminKatmani
        ) != null;

        rb.linearVelocity = new Vector2(
        yon.x * hareketHizi,
        rb.linearVelocity.y
    );
    }

    void YenidenDog()
    {
        can = 100;
        canCubugu.value = can;
        rb.linearVelocity = Vector2.zero;
        rb.position = yenidenDogmaKonumu;
        
        Debug.Log("Yeniden doğdun.");
    }

    void OnCollisionEnter2D(Collision2D carpisma)
    {
        if (carpisma.gameObject.name == "Duvar")
        {

            can -= 10;
            canCubugu.value = can;
            Debug.Log("Duvara çarptın Kalan Canın: " + can);

            if (can <= 0)
            {
                can = 100;
                Debug.Log("OYUN BİTTİ!");
                YenidenDog();
            }
        }
            if (carpisma.gameObject.CompareTag("Tehlike"))
            {
                Debug.Log("Dikkat tehlike!");
                can -= 20;
                canCubugu.value = can;
                Debug.Log("Kalan canın" + can);

                if (can <= 0)
                {
                    Debug.Log("OYUN BİTTİ!");
                    YenidenDog();
                }
            }
    }

    void OnTriggerEnter2D(Collider2D diger)
    {
        if (diger.gameObject.CompareTag("KontrolNoktasi"))
        {
            yenidenDogmaKonumu = rb.position;
            Debug.Log("Checkpoint aktif!");

        }
    }
}
