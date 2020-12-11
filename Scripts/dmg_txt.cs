using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dmg_txt : MonoBehaviour
{
    public float moveSpeed;
    public float destroyTime;
    TextMeshProUGUI dmgText;
    public int dmg;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10.0f;
        destroyTime = 1.0f;

        dmgText = GetComponent<TextMeshProUGUI>();
        dmgText.text = dmg.ToString();
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
