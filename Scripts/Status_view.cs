using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Status_view : MonoBehaviour
{
    public Text HP_Text;
    public Text MP_Text;
    public Text Power_Text;
    public Text Armor_Text;

    // Start is called before the first frame update

    void Start()
    {
        HP_Text = GameObject.Find("HP").transform.Find("HP_Index").GetComponent<Text>();
        MP_Text = GameObject.Find("MP").transform.Find("MP_Index").GetComponent<Text>();
        Power_Text = GameObject.Find("Power").transform.Find("Power_Index").GetComponent<Text>();
        Armor_Text = GameObject.Find("Armor").transform.Find("Armor_Index").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        HP_Text.text = string.Format(GameManager.instance.initialPlayerHP.ToString());
        MP_Text.text = string.Format(GameManager.instance.initialPlayerMP.ToString());
        Power_Text.text = string.Format(GameManager.instance.initialPlayerPower.ToString());
        Armor_Text.text = string.Format(GameManager.instance.initialPlayerArmor.ToString());
    }
}
