using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class block : MonoBehaviour
{
    public GameObject[] _test_block = new GameObject[53];
    public int[,] house_money_buy = new int[53, 3];  //(屬於誰, 價格, 等級)
    public Button Yes;
    public Button No;
    public static bool is_block_over = false;
    public int test_house_round = 0;  //玩家編號 > 土地所有人
    public int test_house_loc = 0;  //玩家當前位置 > 土地編號
    public Text game_event;
    public string[] _player = { "Fahai", "Xiaoqing", "Xu_Xian", "Bai_Suzhen" };


    // Start is called before the first frame update
    void Start()
    {
        //house_money_buy[0,0]=5;
        //print(house_money_buy[0,0]);
        _test_block[0] = GameObject.Find("House");
        for (int i = 1; i < 53; i++)
        {
            _test_block[i] = GameObject.Find("House (" + i + ")");
            house_money_buy[i, 0] = -1;
            house_money_buy[i, 1] = Random.Range(100, 1000);
            house_money_buy[i, 2] = 0;
        }
        //_test_block[1].transform.Find("Level_1").gameObject.SetActive(true);
    }

    //根據格子編號執行不同效果
    public void block_cheak(int round,int now_loc)
    {
        switch(now_loc)
        {
            case 0:
                get_1000_money(round);
                is_block_over = true;
                break;
            case 19:
            case 20:
                losing_money(round);
                is_block_over = true;
                break;
            case 9:
            case 33:
                tp(round);
                is_block_over = true;
                break;
            case 7:
            case 49:
                tp_point_2(round);
                is_block_over = true;
                break;
            case 29:
            case 38:
                tp_point_1(round);
                is_block_over = true;
                break;
            case 12:
            case 32:
            case 45:
                losing_random_money(round);
                is_block_over = true;
                break;
            case 39:
                cant_move(round);
                is_block_over = true;
                break;
            case 2:
            case 8:
            case 14:
            case 21:
            case 27:
            case 35:
            case 40:
            case 48:
                move.well += "\n事件格\n";
                random_block(round);
                is_block_over = true;
                break;
            default:
                move.well += "\n土地格\n";
                house(round, now_loc);
                break;
        }
    }

    public void house(int round, int now_loc)
    {
        test_house_round = round;
        test_house_loc = now_loc;
        Yes.gameObject.SetActive(true);
        No.gameObject.SetActive(true);
        if (house_money_buy[now_loc, 0] < 0)
        {
            move.well += "是否買下房子/土地: $" + house_money_buy[now_loc, 1];
        }
        else if (house_money_buy[now_loc, 0] == round)
        {
            move.well += "是否升級房子/土地: $" + house_money_buy[now_loc, 1];
        }
        else
        {
            move.well += "是否搶奪房子/土地: $" + house_money_buy[now_loc, 1] * 5 + "(為原價格五倍)";
        }
        game_event.text = move.well.ToString();
    }

    public void Yes_button()
    {
        if (house_money_buy[test_house_loc, 0] < 0)
        {
            if (_player[test_house_round] == "Xu_Xian")
            {
                double skill = (double)house_money_buy[test_house_loc, 1] * 0.7;
                move.player[test_house_round].player_money -= (int)skill;
            }
            else
            {
                move.player[test_house_round].player_money -= house_money_buy[test_house_loc, 1];
            }
            house_money_buy[test_house_loc, 0] = test_house_round;
            _test_block[test_house_loc].transform.Find("Belong").GetComponent<MeshRenderer>().material = MyGameManager.instance.BelongMark[test_house_round];
            _test_block[test_house_loc].transform.Find("Level_0").gameObject.SetActive(true);
            move.well = "已買下";
        }
        else if (house_money_buy[test_house_loc, 0] == test_house_round)
        {
            if (_player[test_house_round] == "Xu_Xian")
            {
                double skill = (double)house_money_buy[test_house_loc, 1] * 0.7;
                move.player[test_house_round].player_money -= (int)skill;
            }
            else
            {
                move.player[test_house_round].player_money -= house_money_buy[test_house_loc, 1];
            }
            move.well = "已升級";
            house_money_buy[test_house_loc, 2]++;
            house_money_buy[test_house_loc, 1] = Random.Range((house_money_buy[test_house_loc, 2] * 100), (house_money_buy[test_house_loc, 2] + 1) * 100);
            switch (house_money_buy[test_house_loc, 2])
            {
                case 1:
                    _test_block[test_house_loc].transform.Find("Level_0").gameObject.SetActive(false);
                    _test_block[test_house_loc].transform.Find("Level_1").gameObject.SetActive(true);
                    break;
                case 2:
                    _test_block[test_house_loc].transform.Find("Level_1").gameObject.SetActive(false);
                    _test_block[test_house_loc].transform.Find("Level_2").gameObject.SetActive(true);
                    break;
                case 3:
                    _test_block[test_house_loc].transform.Find("Level_2").gameObject.SetActive(false);
                    _test_block[test_house_loc].transform.Find("Level_3").gameObject.SetActive(true);
                    break;
                default:
                    move.well = "已升級到最高級";
                    break;
            }
        }
        else
        {
            move.well = "已搶下，回合結束";
            move.player[test_house_round].player_money -= house_money_buy[test_house_loc, 1] * 5;
            move.player[house_money_buy[test_house_loc, 0]].player_money += house_money_buy[test_house_loc, 1] * 5;
            house_money_buy[test_house_loc, 0] = test_house_round;
            _test_block[test_house_loc].transform.Find("Belong").GetComponent<MeshRenderer>().material = MyGameManager.instance.BelongMark[test_house_round];
        }
        Yes.gameObject.SetActive(false);
        No.gameObject.SetActive(false);
        is_block_over = true;
    }

    public void No_button()
    {
        if (house_money_buy[test_house_loc, 0] < 0)
        {
            move.well = "不買，回合結束";
        }
        else if (house_money_buy[test_house_loc, 0] == test_house_round)
        {
            move.well = "不升，回合結束";
        }
        else
        {
            move.well = "不搶，交過路費，回合結束";
            if (_player[test_house_round] == "Xu_Xian")
            {
                double skill = (double)house_money_buy[test_house_loc, 1] * 0.8;
                move.player[test_house_round].player_money -= (int)skill;
                move.player[house_money_buy[test_house_loc, 0]].player_money += (int)skill;
            }
            else
            {
                move.player[test_house_round].player_money -= house_money_buy[test_house_loc, 1];
                move.player[house_money_buy[test_house_loc, 0]].player_money += house_money_buy[test_house_loc, 1];
            }
        }
        Yes.gameObject.SetActive(false);
        No.gameObject.SetActive(false);
        is_block_over = true;
    }

    //鐵樹效果
    public void tp(int round)
    {
        Vector3 Vec;

        {
            move.player[round].next_loc = Random.Range(1, 52);
            move.player[round].now_loc = move.player[round].next_loc;
        }
        Vec = move.cell[move.player[round].now_loc].transform.localPosition;
        move.player[round].play_er.transform.localPosition = new Vector3(Vec.x, Vec.y+4, Vec.z);
        move.well += "\n格子:鐵樹 隨機傳送\n傳送後位置為第" + move.player[round].next_loc + "格";
    }

    //金山寺效果
    void get_1000_money(int round)
    {
        move.player[round].player_money += 1000;
        move.player[round].totalAmount += 1000;

        move.well += "\n格子:金山寺 獲得1000";
    }

    //強盜領地效果
    void losing_money(int round)
    {
        move.player[round].player_money -= 500;
        move.player[round].totalAmount -= 500;
        move.well += "\n格子:強盜領地 失去500";
    }

    //船港1效果
    void tp_point_1(int round)
    {
        Vector3 Vec;

        {
            if (move.player[round].next_loc == 38)
            {
                move.player[round].next_loc = 29;
            }
            else
            {
                move.player[round].next_loc = 38;
            }
            move.player[round].now_loc = move.player[round].next_loc;
        }
        Vec = move.cell[move.player[round].now_loc].transform.localPosition;
        move.player[round].play_er.transform.localPosition = new Vector3(Vec.x, Vec.y+4, Vec.z);
        move.well += "\n格子:船港1 指定移動\n移動後位置為第" + move.player[round].next_loc + "格";
    }

    //船港2效果
    void tp_point_2(int round)
    {
        Vector3 Vec;

        {
            if (move.player[round].next_loc == 7)
            {
                move.player[round].next_loc = 49;
            }
            else
            {
                move.player[round].next_loc = 7;
            }
            move.player[round].now_loc = move.player[round].next_loc;
        }
        Vec = move.cell[move.player[round].now_loc].transform.localPosition;
        move.player[round].play_er.transform.localPosition = new Vector3(Vec.x, Vec.y+4, Vec.z);
        move.well += "\n格子:船港2 指定移動\n移動後位置為第" + move.player[round].next_loc + "格";
    }

    //河坊街效果
    void losing_random_money(int round)
    {
        int t = Random.Range(0, 9999);
        move.player[round].player_money -= t;
        move.player[round].totalAmount -= t;
        move.well += "\n格子:河坊街 失去" + t;
    }

    //雷鋒塔效果
    void cant_move(int round)
    {
        move.player[round].can_player_move = false;
        move.player[round].can_player_move_round = 1;
        move.well += "\n格子:雷峰塔\n停止一回合";
    }

    //事件格效果
    void random_block(int round)
    {
        int i = Random.Range(0, 4);
        switch (i)
        {
            case 0:
                Vector3 Vec;
                move.player[round].next_loc = 39;
                move.player[round].now_loc = move.player[round].next_loc;
                Vec = move.cell[move.player[round].now_loc].transform.localPosition;
                move.player[round].play_er.transform.localPosition = new Vector3(Vec.x, Vec.y+4, Vec.z);
                move.player[round].can_player_move = false;
                move.player[round].can_player_move_round = 2;
                move.well += "誤觸機關被封印至雷峰塔兩回合";
                break;
            case 1:
                for (int a = 0; a < MyGameManager.instance.people; a++)
                {
                    int random = Random.Range(0, 9999);
                    move.player[a].player_money += random;
                    move.player[round].totalAmount += random;
                }
                move.well += "金山寺住持下山除妖\n全體玩家獲得隨機金額";
                break;
            case 2:
                move.player[round].player_money += 500;
                move.player[round].totalAmount += 500;
                move.well += "金山寺住持設下羅漢大陣\n獲得500";
                break;
            case 3:
                double t = (double)move.player[round].player_money * 0.2;
                move.player[round].player_money -= (int)t;
                move.player[round].totalAmount -= (int)t;
                move.well += "玩家受到萬蛇蝕身，為了治療需要大筆費用\n失去20%(" + t + ")金錢";
                break;
            case 4:
                for (int a = 0; a < 4; a++)
                {
                    move.player[a].can_player_move = true;
                    move.player[a].can_player_move_round = 0;
                }
                move.well += "神明指示撐起雷峰塔\n全員解放";
                break;

        }
    }
}
