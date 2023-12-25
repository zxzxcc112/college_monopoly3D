using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    public static player_class[] player = new player_class[4];  //角色數據
    public static GameObject[] player_ = new GameObject[4];   //抓人物物件

    public static string well;  //顯示當前玩家(文字)
    public static GameObject[] cell = new GameObject[53];  //取得格子物件

    //以下為移動機制用
    public bool blockChecked = false; //是否移動到下一格
    //public float _Time = 0;  //延遲移動速度
    public Vector3 Vec1;  //角色的座標
    public Vector3 Vec2;  //next_loc的座標
    public int move_int = 0;  //移動格子分五次

    //public GameObject round_block; //標示當前玩家的方塊
    public static int round = 0;  //當前玩家編號
    
    public Button button;//擲骰按鈕
    public Button button_over; //結束回合按鈕
    public Button button_skill;  //技能按鈕

    public GameObject[] cam = new GameObject[4];//攝影機照角色的位置
    public GameObject map;  //主攝影機

    //顯示數據
    public Text game_event;//事件
    //public Text[] money = new Text[4]; //顯示持有金錢

    public GameObject block_test;//遊戲移動路線

    public GameStatusView GSV;  //UI介面
    public DiceController diceController;  //骰子控制器

    void Start()
    {

        //抓格子物件
        cell[0] = GameObject.Find("floor");
        for (int i = 1; i < 53; i++)
        {
            cell[i] = GameObject.Find("floor (" + i + ")");
        }

        for (int i = 0; i < MyGameManager.instance.people; i++)
        {
            //抓人物
            //player_[i] = GameObject.Find(MyGameManager.instance.roleObjectName[i]);
            string roleName = MyGameManager.instance.roleObjectName[MyGameManager.instance.playerSelectedRole[i]];
            GameObject playerObject = Resources.Load("Prefab/" + roleName) as GameObject;
            player_[i] = GameObject.Instantiate(playerObject);
            player_[i].transform.parent = GameObject.Find("Player").transform;
            //初始化人物
            switch (roleName)
            {
                case "Fahai":
                    player[i] = new Fahai_class(0, 0, MyGameManager.instance.startingAmount, 0, true);
                    player[i].set_object(player_[i]);
                    break;
                case "Xiaoqing":
                    player[i] = new Xiaoqing_class(0, 0, MyGameManager.instance.startingAmount, 0, true);
                    player[i].set_object(player_[i]);
                    break;
                case "Xu_Xian":
                    player[i] = new Xu_Xian_class(0, 0, MyGameManager.instance.startingAmount, 0, true);
                    player[i].set_object(player_[i]);
                    break;
                case "Bai_Suzhen":
                    player[i] = new Bai_Suzhen_class(0, 0, MyGameManager.instance.startingAmount, 0, true);
                    player[i].set_object(player_[i]);
                    break;
            }
            player[i].totalAmount = player[i].player_money;
            player[i].playerId = i;
            player[i].roleId = MyGameManager.instance.playerSelectedRole[i];
            player[i].roleName = MyGameManager.instance.roleName[player[i].roleId];
            player_[i].AddComponent<CharacterLook>();
            player_[i].GetComponent<CharacterLook>().Id = i;
            player_[i].transform.GetChild(3).GetComponent<MeshRenderer>().material = MyGameManager.instance.MiniMapMark[i];
            
            //cam[i] = player_[i].transform.GetChild(1).gameObject;
            //顯示持有金錢
            //money[i].text = player[i].player_money.ToString();

            //將角色設置在起點
            player[i].play_er.transform.localPosition = new Vector3(cell[0].transform.localPosition.x, player[i].play_er.transform.localPosition.y, cell[0].transform.localPosition.z);
        }

        main_map.target = player_[round];

        //主攝影機移動到攝影機照角色的位置
        //map.transform.localPosition = cam[round].transform.position;
        //紅色方塊位置
        //round_block.transform.localPosition = GameObject.Find("player1_money").GetComponent<Text>().transform.localPosition;
        //round_block.transform.localPosition = new Vector3(round_block.transform.localPosition.x, round_block.transform.localPosition.y + 75, round_block.transform.localPosition.z);
        //事件
        well = "遊玩的玩家:player" + (round+1);
        game_event.text = well.ToString();
        //禁用結數回合
        button_over.interactable = false;
        button_skill.interactable = false;
        GSV = GetComponent<GameStatusView>();
        diceController = GetComponent<DiceController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (_Time > 0)
        //    _Time -= Time.deltaTime;

        for (int i = 0; i < MyGameManager.instance.people; i++)
        {
            if (player[i].player_money >= 100000)
            {
                ScenesManager sm = transform.GetComponent<ScenesManager>();
                {
                    sm.ChangeScene("GameOver");
                }
            }
        }

        if (!button.interactable)
        {
            player[round].now_loc %= 53;
            player[round].next_loc %= 53;

            if (player[round].next_loc != player[round].now_loc && player[round].can_player_move == true)
            {
                Vec1 = player[round].play_er.transform.localPosition;
                int pos = (player[round].now_loc + 1) % 53;
                Vec2 = cell[pos].transform.localPosition;
                Vec2 = new Vector3(Vec2.x, Vec1.y, Vec2.z);
                if (Vector3.Distance(Vec2, Vec1) > 0.01f)
                {
                    float speed = Time.deltaTime * UnityEngine.Mathf.Abs(player[round].next_loc - player[round].now_loc) * 20;
                    if (speed > 1.0f) speed = 1.0f;
                    player[round].play_er.transform.localPosition = Vector3.MoveTowards(player[round].play_er.transform.localPosition, Vec2, speed);
                }
                else
                {
                    ++player[round].now_loc;
                    player[round].now_loc %= 53;
                }

                if (player[round].next_loc == player[round].now_loc)
                {
                    blockChecked = true;
                }
            }
            else if (blockChecked && player[round].now_loc == player[round].next_loc && player[round].can_player_move == true)
            {
                block_test.GetComponent<block>().block_cheak(round, player[round].now_loc);
                blockChecked = false;
            }
        }


        //if (player[round].next_loc != player[round].now_loc /*&& _Time <= 0*/ && player[round].can_player_move == true)
        //{
        //    if (player[round].now_loc >= 52)
        //    {
        //        player[round].now_loc -= 53;
        //    }
        //    if (player[round].next_loc >= 52)
        //    {
        //        player[round].next_loc -= 53;
        //    }

        //    Vec2 = cell[player[round].now_loc + 1].transform.localPosition;
        //    player[round].play_er.transform.localPosition += new Vector3((Vec2.x - Vec1.x) / 5, 0, (Vec2.z - Vec1.z) / 5);
        //    move_int += 1;
        //    //_Time += 0.1f;

        //    //主攝影機移動到攝影機照角色的位置
        //    //map.transform.localPosition = cam[round].transform.position;
        //    //main_map.target = player_[round];
        //    if (move_int == 5)
        //    {
        //        player[round].play_er.transform.localPosition = new Vector3(Vec2.x, Vec1.y, Vec2.z);
        //        player[round].now_loc += 1;
        //        Vec1 = player[round].play_er.transform.localPosition;

        //        move_int = 0;
        //        blockChecked = true;
        //    }
        //}
        //else if (blockChecked && player[round].now_loc == player[round].next_loc && player[round].can_player_move == true)
        //{
        //    block_test.GetComponent<block>().block_cheak(round, player[round].now_loc);
        //    blockChecked = false;
        //}
        if (block.is_block_over == true)
        {

            //map.transform.localPosition = cam[round].transform.position;
            main_map.target = player_[round];
            //for (int i = 0; i < 4; i++)
            //{
            //    money[i].text = player[i].player_money.ToString();
            //}
            game_event.text = well.ToString();
            button_over.interactable = true;
            block.is_block_over = false;
        }
    }


    public void button_on()
    {
        //禁用擲骰
        button.interactable = false;
        button_skill.interactable = true;


        if (player[round].can_player_move == false)  //禁止移動
        {
            well += "\n由於玩家被壓在雷峰塔\n移動不能";
            game_event.text = well.ToString();
            button.interactable = false;
            button_skill.interactable = false;
            player[round].can_player_move_round--;
            if (player[round].can_player_move_round == 0)
            {
                player[round].can_player_move = true;
            }
            button_over.interactable = true;
        }
        else  //擲骰移動
        {
            diceController.SpawnDice();
            //player[round].next_loc += Random.Range(1, 6);
        }

        //格子編號不超過53
        //player[round].next_loc %= 53;

        //Vec1 = player[round].play_er.transform.localPosition;
        // Debug.Log(player[round].next_loc);
    }

    //下一位玩家
    public void round_over()
    {
        button.interactable = true;
        button_over.interactable = false;
        button_skill.interactable = false;

        main_map.target = player_[round];

        round = (round + 1) % MyGameManager.instance.people;
        //map.transform.localPosition = cam[round].transform.position;

        //紅色方塊
        //switch(round)
        //{
        //    case 0:
        //        round_block.transform.localPosition = GameObject.Find("player1_money").GetComponent<Text>().transform.localPosition;
        //        break;
        //    case 1:
        //        round_block.transform.localPosition = GameObject.Find("player2_money").GetComponent<Text>().transform.localPosition;
        //        break;
        //    case 2:
        //        round_block.transform.localPosition = GameObject.Find("player3_money").GetComponent<Text>().transform.localPosition;
        //        break;
        //    case 3:
        //        round_block.transform.localPosition = GameObject.Find("player4_money").GetComponent<Text>().transform.localPosition;
        //        break;
        //    default:
        //        break;
        //}
        //round_block.transform.localPosition = new Vector3(round_block.transform.localPosition.x, round_block.transform.localPosition.y + 75, round_block.transform.localPosition.z);

        GSV.AddRound();

        //事件
        well = "遊玩的玩家:player" + (round + 1);
        game_event.text = well.ToString();
    }
    public void skill_on()
    {
        button_skill.interactable = false;
        player[round].player_skill(round);
        game_event.text = well.ToString();
        //for (int i = 0; i < 4; i++)
        //{
        //    money[i].text = player[i].player_money.ToString();
        //}
    }
}
