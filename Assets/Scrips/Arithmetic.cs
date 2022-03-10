using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arithmetic : MonoBehaviour
{
    public Text Text1;
    public Text Text2;
    public Text Text3;

    public InputField myInputField;
    public InputField firstNumField;
    public InputField seconfNumField;

    public GameObject dmgTxt;

    private float time_start;
    private float time_current;
    private float time_game;

    private int num1;
    private int num2;

    private int gameLevelConut = 1;
    private int gameConut = 0;

    // Start is called before the first frame update
    void Start()
    {
        Text2.text = "+";

        randomNum();

        myInputField.onEndEdit.AddListener(delegate {
            ValueChanged(myInputField);
        });

        //textPrefab = Resources.Load<Text>("TextPrefab");

        time_start = Time.time;
        time_game = time_start;
        time_current = 0;

        Debug.Log("time_start : " + time_start);

        addText(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("escape"))
        {
            ExitGame();
        }
            //time_current = Time.time;
            //Debug.Log("time_current : " + time_current);

        if (gameConut >= 10)
        {
            gameConut = 0;

            time_current = Time.time - time_start;
            time_start += time_current;
            addTextBlank();

            //GameObject damageText = Instantiate(dmgTxt, new Vector3(0, 0, 0), Quaternion.identity);
            //damageText.GetComponent<Text>().text = gameLevelConut + " 레벨 완료. (총 게임 플레이 시간 : " + time_start.ToString("F2") + ")";
            //damageText.GetComponent<Text>().transform.SetParent(GameObject.Find("Content").transform);

            addText(0, 1);

            addTextBlank();
            addTextBlank();
            gameLevelConut++;
            time_game = time_start;

        }

    }

    void addText(int value, int complte)
    {
        GameObject damageText = Instantiate(dmgTxt, new Vector3(0, 0, 0), Quaternion.identity);
        if(complte == 0)
        {
            if (time_current > 0)
            {
                damageText.GetComponent<Text>().text = Text1.text + " " + Text2.text + " " + Text3.text + " = " + value + " (" + time_current.ToString("F2") + ")";
            }
            else 
            {
                damageText.GetComponent<Text>().text = time_current + "";
            }
        }
        else 
        {
            damageText.GetComponent<Text>().text = gameLevelConut + " 레벨 완료. (총 게임 플레이 시간 : " + (time_start - time_game).ToString("F2") + ")";
        }
        damageText.GetComponent<Text>().transform.SetParent(GameObject.Find("Content").transform);
    }

    void addTextBlank() 
    {
        GameObject damageText = Instantiate(dmgTxt, new Vector3(0, 0, 0), Quaternion.identity);
        damageText.GetComponent<Text>().text = "";
        damageText.GetComponent<Text>().transform.SetParent(GameObject.Find("Content").transform);
    }

    public void randomNum() 
    {
        Debug.Log("gameConut : " + gameConut);

        num1 = Random.Range(1, int.Parse(firstNumField.text));
        num2 = Random.Range(1, int.Parse(seconfNumField.text));

        switch (Text2.text)
        {
            case "+":
                break;
            case "×":
                break;
            case "-":
            case "÷":
                if (num1 < num2)
                {
                    reRandomNum();
                }
                break;
        }

        Text1.text = num1.ToString();
        //Text2.text = "+";
        Text3.text = num2.ToString();
    }

    void reRandomNum()
    {
        if (num1 < num2)
        {
            num2 = Random.Range(1, num1);
        }
    }

    void ValueChanged(InputField input) //이때는 원하는 매개변수를 사용할 수 있다
    {
        //int num1 = int.Parse(Text1.text);
        //int num2 = int.Parse(Text3.text);
        int value = 0;
        switch(Text2.text)
        {
            case "+":
                value = num1 + num2;
                break;
            case "-":
                value = num1 - num2;
                break;
            case "×":
                value = num1 * num2;
                break;
            case "÷":
                value = num1 / num2;
                break;
        }

        //Debug.Log("text : " + input.text + ", value : " + value);
        int solution = int.Parse(input.text);
        if (solution == value)
        {
            //Debug.Log("같음");
            time_current = Time.time - (time_start);
            time_start += time_current;
            //Debug.Log("같음 : time_current : " + time_current);
            addText(value, 0);

            randomNum();

            input.text = "";
            gameConut++;
        }
        else
        {
            Debug.Log("다름");
        }
        
    }

    void OnApplicationQuit()
    {
        time_current = Time.time - time_start;
        time_start += time_current;
        Debug.Log("time_game : " + time_start);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit(); // 어플리케이션 종료
        #endif
    }
}
