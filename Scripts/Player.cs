using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [HideInInspector] public static int playerHP;
    [HideInInspector] public static int playerPower;
    [HideInInspector] public static int playerArmor;
    [HideInInspector] public static int playerMP;
    [HideInInspector] public static int initialPlayerHP;
    [HideInInspector] public static int initialPlayerMP;
    [HideInInspector] public int initialPlayerPower;
    [HideInInspector] public int initialPlayerArmor;
    public Enemy enemyObject;

    RectTransform HP_BAR;
    RectTransform MP_BAR;

    public Image NowHP_BAR;
    public Image NowMP_BAR;
    public GameObject canvas;
    public Text NowHP_Text;
    public Text NowMP_Text;

    private Animator animator;
    private float dieDelay = 1f;

    public GameObject hudDamageText;

    // 운동 데이터. 시간은 분 단위로 저장
    public int day, steps, sleepTime;
    // 거리 = 걸음 수 * 0.0008km
    public double distance;
   void Start()
    {
        if (GameManager.instance.initialPlayerStats == true)
        {
            playerHP = GameManager.instance.playerHP + steps;
            playerMP = GameManager.instance.playerMP + steps;
            initialPlayerHP = GameManager.instance.playerHP + steps;
            initialPlayerMP = GameManager.instance.playerMP + steps;
            GameManager.instance.initialPlayerHP = initialPlayerHP;
            GameManager.instance.initialPlayerMP = initialPlayerMP;

            playerPower = GameManager.instance.playerPower + (int)distance;
            playerArmor = GameManager.instance.playerArmor + sleepTime;
            initialPlayerPower = GameManager.instance.playerPower + (int)distance;
            initialPlayerArmor = GameManager.instance.playerArmor + sleepTime;
            GameManager.instance.initialPlayerPower = initialPlayerPower;
            GameManager.instance.initialPlayerArmor = initialPlayerArmor;


            GameManager.instance.initialPlayerStats = false;
        }
        else
        {
            playerHP = GameManager.instance.playerHP;
            playerMP = GameManager.instance.playerMP;
            initialPlayerHP = GameManager.instance.initialPlayerHP;
            initialPlayerMP = GameManager.instance.initialPlayerMP;

            playerPower = GameManager.instance.playerPower;
            playerArmor = GameManager.instance.playerArmor;
        }

        if (GameManager.stage == 1 && GameManager.instance.level == 1)   // main
            gameObject.transform.position = new Vector3(3f, 4.8f, 0f);
        else if (GameManager.stage == 1 && GameManager.instance.level > 1)  // tutorial
            gameObject.transform.position = new Vector3(21f, 6.2f, 0f);
        else if (GameManager.stage == 2) // forest
            gameObject.transform.position = new Vector3(-14.3f, -10f, 0f);
        else if (GameManager.stage == 3) // cave
            gameObject.transform.position = new Vector3(20.5f, -8.43f, 0f);
        /*else if (GameManager.instance.level == 5) // village
            gameObject.transform.position = new Vector3(-15, 1, 0);*/
        else
            gameObject.transform.position = new Vector3(3f, 4.8f, 0f);




        animator = GetComponent<Animator>();

        HP_BAR = GameObject.Find("BGHP_BAR").GetComponent<RectTransform>();
        NowHP_BAR = GameObject.Find("BGHP_BAR").transform.Find("HP_BAR").GetComponent<Image>();
        MP_BAR = GameObject.Find("BGMP_BAR").GetComponent<RectTransform>();
        NowMP_BAR = GameObject.Find("BGMP_BAR").transform.Find("MP_BAR").GetComponent<Image>();
        NowHP_Text = GameObject.Find("BGHP_BAR").transform.Find("HP_Text").GetComponent<Text>();
        NowMP_Text = GameObject.Find("BGMP_BAR").transform.Find("MP_Text").GetComponent<Text>();

    }

    void Update()
    {
        NowHP_BAR.fillAmount = (float)playerHP / initialPlayerHP;
        NowMP_BAR.fillAmount = (float)playerMP / initialPlayerMP;
        NowHP_Text.text = string.Format("{0}/" + initialPlayerHP.ToString(), playerHP);
        NowMP_Text.text = string.Format("{0}/" + initialPlayerMP.ToString(), playerMP);

    }

    public void FindEnemyObject()
    {
        enemyObject = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
    }

    public void Attack()
    {
        int damage = playerPower * 1;
        //GameObject.FindWithTag("Enemy").GetComponent<Enemy>().Hit(damage);
        enemyObject.Hit(damage);
        PassPlayerStats();
        animator.SetTrigger("PlayerAttack");
        Debug.Log("Player Attack!");
    }

    public void JumpAttack()
    {
        int damage = playerPower * 2;
        playerMP -= 10;
        //GameObject.FindWithTag("Enemy").GetComponent<Enemy>().Hit(damage);
        enemyObject.Hit(damage);
        PassPlayerStats();
        animator.SetTrigger("PlayerJumpAttack");
        Debug.Log("Player JumpAttack!");
    }

    public void Strike()
    {
        int damage = playerPower * 3;
        playerMP -= 20;
        //GameObject.FindWithTag("Enemy").GetComponent<Enemy>().Hit(damage);
        enemyObject.Hit(damage);
        PassPlayerStats();
        animator.SetTrigger("PlayerStrike");
        Debug.Log("Player Strike!");
    }

    public void Hit(int damage)
    {
        int decreaseDamage = damage - playerArmor;
        if (decreaseDamage <= 0) decreaseDamage = 1;
        GameObject hudText = Instantiate(hudDamageText,canvas.transform);
        hudText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x - 2, transform.position.y +1.7f, 0));
        hudText.GetComponent<dmg_txt>().dmg = decreaseDamage;
        playerHP -= decreaseDamage;
        PassPlayerStats();
        animator.SetTrigger("PlayerDizzy");

        //if (playerHP <= 0) Die();
        if (playerHP <= 0)
        {
            playerHP = 0;
            StartCoroutine(WaitDie());
        }
    }

    IEnumerator WaitDie()
    {
        animator.SetTrigger("PlayerDie");
        yield return new WaitForSeconds(dieDelay);
        Die();
    }

    void Die()
    {
        //animator.SetTrigger("PlayerDie");
        Destroy(gameObject);
        //GameManager.instance.GameOver();
        SceneManager.LoadScene("Main_display");
    }

    private void PassPlayerStats()
    {
        GameManager.instance.playerHP = playerHP;
        GameManager.instance.playerMP = playerMP;
        GameManager.instance.playerPower = playerPower;
        GameManager.instance.playerArmor = playerArmor;
    }

    // native app으로 부터 데이터 수신
    public void getExData(string exerciseData)
    {
        string[] sliceData;
        if (exerciseData.IndexOf('_') > 0)
        {
            sliceData = exerciseData.Split('_');
            day = int.Parse(sliceData[0]);
            steps = int.Parse(sliceData[1]);
            // 거리 = 걸음 수 * 보폭 평균치(cm)
            distance = int.Parse(sliceData[2]) * 0.0008;
            // 수면 시간은 분 단위로 저장됨 2시간 -> 120분
            sleepTime = int.Parse(sliceData[3]);
        }
    }
}
