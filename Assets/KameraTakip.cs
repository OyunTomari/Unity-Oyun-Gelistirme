using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    public Transform hedef;
    public float yumusaklik = 5f;
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    void LateUpdate()
    {
        if (hedef == null) return;

        Vector3 hedefPozisyon = hedef.position + offset;
        transform.position = Vector3.Lerp(transform.position, hedefPozisyon, yumusaklik * Time.deltaTime);
    }
}