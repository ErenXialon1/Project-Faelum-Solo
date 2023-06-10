using UnityEngine;

public class KnifeSkill : MonoBehaviour
{
    public Transform characterTransform;
    public GameObject objectPrefab;
    public float rotationSpeed = 100f;
    public float radius = 2f;
    public GameObject object1;
    public GameObject object2;
    public bool increaseSize = false;
    private float angle = 0f;
    public bool twoblade = false;
    public int rotSpeed;
    private bool isSizeIncreased = false;
    public int knifedamage = 35;
    public bool knifeSkillActive;
    private void Start()
    {
        // Nesneleri oluþtur
       
    }

    private void Update()
    {
        if (knifeSkillActive)
        {
            object1.transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
            object2.transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
            // Karakterin pozisyonunu al
            Vector3 characterPosition = characterTransform.position;

            // Nesneleri karakterin etrafýnda döndür
            float x1 = characterPosition.x + Mathf.Cos(angle) * radius;
            float y1 = characterPosition.y + Mathf.Sin(angle) * radius;
            Vector3 position1 = new Vector3(x1, y1, characterPosition.z);
            object1.transform.position = position1;

            if (twoblade)
            {
                object2.SetActive(true);
                float x2 = characterPosition.x + Mathf.Cos(angle + Mathf.PI) * radius;
                float y2 = characterPosition.y + Mathf.Sin(angle + Mathf.PI) * radius;
                Vector3 position2 = new Vector3(x2, y2, characterPosition.z);
                object2.transform.position = position2;
            }
            else
            {
                object2.SetActive(false);
            }

            if (increaseSize && !isSizeIncreased)
            {
                object1.transform.localScale *= 2f;
                object2.transform.localScale *= 2f;
                isSizeIncreased = true;
            }

            // Nesneleri döndür
            object1.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            object2.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

            // Açýyý güncelle
            angle += rotationSpeed * Time.deltaTime;
        }
    }
    public void AddKnives()
    {
        object1 = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
        object2 = Instantiate(objectPrefab, Vector3.zero, Quaternion.identity);
    }
    public void ToggleObject2()
    {
        twoblade = !twoblade;
        object2.SetActive(twoblade);
    }
    public void ToggleSizeIncrease()
    {
        increaseSize = !increaseSize;
    }
}