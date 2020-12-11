using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExerciseData : MonoBehaviour
{
    private int day;
    private int steps;
    private int distance;
    private int sleepTime;
    public Text TestText;

    // Start is called before the first frame update
    void Start()
    {
        day = 0;
        steps = 0;
        distance = 0;
        sleepTime = 0;
        TestText.text = "운동 데이터 입력 대기 중";
    }

    // Update is called once per frame
    void Update()
    {
        TestText.text = "걸음 수."+steps.ToString() + " | 달린거리." + distance.ToString() + " | 수면시간." + sleepTime.ToString();
        
        //TestText.text = "Update";
    }

    public void dataUpdate(string testData)
    {
        if (testData == "1")
        {
            steps += 1;
        }
        else if (testData == "2")
            distance += 1;
    }
}
