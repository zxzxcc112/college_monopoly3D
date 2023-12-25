using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OldMove : MonoBehaviour
{
    public int[] now_loc = new int[4];
    public int[] next_loc = new int[4];
    public int move_int = 0;
    public Vector3 Vec1;
    public Vector3 Vec2;
    public float _Time = 0;
    public GameObject[] Test = new GameObject[53];
    public GameObject[] player = new GameObject[4];
    public int round = 0;
    public Text[] money= new Text[4];
    public int[] player_money = new int[4];
    public bool[] can_player_move = new bool[4];
    public bool block_cheak = false;
    int[] can_player_move_round = new int[4];
    public Button button;
    public GameObject[] cam = new GameObject[4];
    public GameObject map;
    public GameObject mini_map;
    public GameObject round_block;
    public Text game_event;
    string well;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            can_player_move[i] = true;
        }
        for (int i = 0; i < 4; i++)
        {
            player_money[i] = 5000;
        }
        for (int i = 0; i < 4; i++)
        {
            money[i].text = player_money[i].ToString();
        }
        Test[0] = GameObject.Find("floor");
        for (int i = 1; i < 53; i++)
        {
            Test[i] = GameObject.Find("floor (" + i + ")");
        }
        for (int i = 0; i < 4; i++)
        {
            player[i].transform.localPosition = new Vector3(Test[0].transform.localPosition.x, player[i].transform.localPosition.y, Test[0].transform.localPosition.z);
        }
        map.transform.localPosition = cam[round].transform.position;
        round_block.transform.localPosition = GameObject.Find("PlayerState").GetComponent<Transform>().transform.localPosition;
        round_block.transform.localPosition = new Vector3(round_block.transform.localPosition.x, round_block.transform.localPosition.y + 150, round_block.transform.localPosition.z);
        well = "遊玩的玩家:player" + (round+1);
        game_event.text = well.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 v = mini_map.transform.localPosition;

            if (Input.GetAxis("Mouse X") > 0)
            {
                mini_map.transform.localPosition = new Vector3(v.x + 10, v.y, v.z);
            }
            if (Input.GetAxis("Mouse X") < 0)
            {
                mini_map.transform.localPosition = new Vector3(v.x - 10, v.y, v.z);
            }
            if (Input.GetAxis("Mouse Y") > 0)
            {
                mini_map.transform.localPosition = new Vector3(v.x, v.y, v.z + 10);
            }
            if (Input.GetAxis("Mouse Y") < 0)
            {
                mini_map.transform.localPosition = new Vector3(v.x, v.y, v.z - 10);
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && mini_map.GetComponent<Camera>().orthographicSize < 120)
            {
                mini_map.GetComponent<Camera>().orthographicSize += 5;
            }
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && mini_map.GetComponent<Camera>().orthographicSize > 5)
            {
                mini_map.GetComponent<Camera>().orthographicSize -= 5;
            }
        }

        if (_Time > 0)
            _Time -= Time.deltaTime;

        if (next_loc[round] != now_loc[round] && _Time <= 0&& can_player_move[round] == true)
        {
            if (now_loc[round] >= 52)
            {
                now_loc[round] -= 53;
            }
            Vec2 = Test[now_loc[round] + 1].transform.localPosition;
            player[round].transform.localPosition += new Vector3((Vec2.x - Vec1.x) / 5, 0, (Vec2.z - Vec1.z) / 5);
            move_int += 1;
            _Time += 0.1f;
            map.transform.localPosition = cam[round].transform.position;
            if (move_int == 5)
            {
                player[round].transform.localPosition = new Vector3(Vec2.x, Vec1.y, Vec2.z);
                now_loc[round] += 1;
                Vec1 = player[round].transform.localPosition;

                move_int = 0;
                block_cheak = true;
            }
        }
        else if (block_cheak && now_loc[round] == next_loc[round] && can_player_move[round] == true)
        {
            switch (now_loc[round])
            {
                case 0:
                    get_1000_money();
                    break;
                case 19:
                case 20:
                    losing_money();
                    break;
                case 9:
                case 33:
                    tp();
                    break;
                case 7:
                case 49:
                    tp_point_2();
                    break;
                case 29:
                case 38:
                    tp_point_1();
                    break;
                case 12:
                case 32:
                case 45:
                    losing_random_money();
                    break;
                case 39:
                    cant_move();
                    break;
                case 2:
                case 8:
                case 14:
                case 21:
                case 27:
                case 35:
                case 40:
                case 48:
                    well += "\n事件格\n";
                    random_block();
                    break;
                default:
                    break;
            }
            map.transform.localPosition = cam[round].transform.position;
            block_cheak = false;
            for(int i=0;i<4;i++)
            {
                money[i].text = player_money[i].ToString();
            }
            game_event.text = well.ToString();
        }
    }
    public void mini_map_big()
    {
        mini_map.GetComponent<Camera>().orthographicSize -= 10;
    }
    public void mini_map_small()
    {
        mini_map.GetComponent<Camera>().orthographicSize += 10;
    }
    public void mini_map_up()
    {
        Vector3 v = mini_map.transform.localPosition;
        mini_map.transform.localPosition=new Vector3(v.x,v.y,v.z+10);
    }
    public void mini_map_left()
    {
        Vector3 v = mini_map.transform.localPosition;
        mini_map.transform.localPosition = new Vector3(v.x-10, v.y, v.z );
    }
    public void mini_map_light()
    {
        Vector3 v = mini_map.transform.localPosition;
        mini_map.transform.localPosition = new Vector3(v.x+10, v.y, v.z );
    }
    public void mini_map_down()
    {
        Vector3 v = mini_map.transform.localPosition;
        mini_map.transform.localPosition = new Vector3(v.x, v.y, v.z - 10);
    }

    public void button_on()
    {
        button.enabled = false;
        
        if (can_player_move[round] == false)
        {
            well += "\n由於玩家被壓在雷峰塔\n移動不能";
            game_event.text = well.ToString();
            button.enabled = false;
            can_player_move_round[round]--;
            if (can_player_move_round[round] == 0)
            {
                can_player_move[round] = true;
            }
        }
        else
        {
            next_loc[round] += Random.Range(1, 6);
        }
        if (next_loc[round] >= 53)
        {
            next_loc[round] -= 53;
        }

        Vec1 = player[round].transform.localPosition;

        // Debug.Log(next_loc[round]);
    }
    public void round_over()
    {
        round++;
        round = round % 4;
        button.enabled = true;
        map.transform.localPosition = cam[round].transform.position;
        switch(round)
        {
            case 0:
                round_block.transform.localPosition = GameObject.Find("player1_money").GetComponent<Text>().transform.localPosition;
                break;
            case 1:
                round_block.transform.localPosition = GameObject.Find("player2_money").GetComponent<Text>().transform.localPosition;
                break;
            case 2:
                round_block.transform.localPosition = GameObject.Find("player3_money").GetComponent<Text>().transform.localPosition;
                break;
            case 3:
                round_block.transform.localPosition = GameObject.Find("player4_money").GetComponent<Text>().transform.localPosition;
                break;
            default:
                break;
        }
        round_block.transform.localPosition = new Vector3(round_block.transform.localPosition.x, round_block.transform.localPosition.y + 75, round_block.transform.localPosition.z);
        well = "遊玩的玩家:player" + (round + 1);
        game_event.text = well.ToString();
    }
    void tp()
    {
        Vector3 Vec;

        {
            next_loc[round] = Random.Range(1, 52);
            now_loc[round] = next_loc[round];
        }
        Vec = Test[now_loc[round]].transform.localPosition;
        player[round].transform.localPosition = new Vector3(Vec.x, Vec1.y, Vec.z);
        well += "\n格子:鐵樹 隨機傳送\n傳送後位置為第"+ next_loc[round]+"格";
    }
    void get_1000_money()
    {
        player_money[round] += 1000;
        well += "\n格子:金山寺 獲得1000";
    }
    void losing_money()
    {
        player_money[round] -= 500;
        well += "\n格子:強盜領地 失去500";
    }
    void tp_point_1()
    {
        Vector3 Vec;

        {
            if (next_loc[round] == 38)
            {
                next_loc[round] = 29;
            }
            else
            {
                next_loc[round] = 38;
            }
            now_loc[round] = next_loc[round];
        }
        Vec = Test[now_loc[round]].transform.localPosition;
        player[round].transform.localPosition = new Vector3(Vec.x, Vec1.y, Vec.z);
        well += "\n格子:船港1 指定移動\n移動後位置為第" + next_loc[round] + "格";
    }
    void tp_point_2()
    {
        Vector3 Vec;

        {
            if (next_loc[round] == 7)
            {
                next_loc[round] = 49;
            }
            else
            {
                next_loc[round] = 7;
            }
            now_loc[round] = next_loc[round];
        }
        Vec = Test[now_loc[round]].transform.localPosition;
        player[round].transform.localPosition = new Vector3(Vec.x, Vec1.y, Vec.z);
        well += "\n格子:船港2 指定移動\n移動後位置為第" + next_loc[round] + "格";
    }
    void losing_random_money()
    {
        int t= Random.Range(0, 9999);
        player_money[round] -= t;
        well += "\n格子:河坊街 失去"+t;
    }
    void cant_move()
    {
        can_player_move[round] = false;
        can_player_move_round[round] = 1;
        well += "\n格子:雷峰塔\n停止一回合";
    }
    void random_block()
    {
        int i= Random.Range(0, 4);
        switch(i)
        {
            case 0:
                Vector3 Vec;
                next_loc[round] = 39;
                now_loc[round] = next_loc[round];
                Vec = Test[now_loc[round]].transform.localPosition;
                player[round].transform.localPosition = new Vector3(Vec.x, Vec1.y, Vec.z);
                can_player_move[round] = false;
                can_player_move_round[round] = 2;
                well += "誤觸機關被封印至雷峰塔兩回合";
                break;
            case 1:
                for(int a=0;a<4;a++)
                {
                    player_money[a] += Random.Range(0, 9999);
                }
                well += "金山寺住持下山除妖\n全體玩家獲得隨機金額";
                break;
            case 2:
                player_money[round] += 500;
                well += "金山寺住持設下羅漢大陣\n獲得500";
                break;
            case 3:
                double t = (double)player_money[round] * 0.2;
                player_money[round] -= (int)t;
                well += "玩家受到萬蛇蝕身，為了治療需要大筆費用\n失去20%("+t+")金錢";
                break;
            case 4:
                for (int a = 0; a < 4; a++)
                {
                    can_player_move[a] = true;
                    can_player_move_round[a] = 0; 
                }
                well += "神明指示撐起雷峰塔\n全員解放";
                    break;

        }
    }
}
