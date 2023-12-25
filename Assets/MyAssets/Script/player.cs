using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class player_class
{
    //玩家數據
    public int now_loc;  //現在所在的格子位置(格子編號)
    public int next_loc;  //下一個位置(透過累計格子編號)
    public int player_money;  //持有金額
    public int can_player_move_round;  //禁止移動的次數
    public bool can_player_move = true;  //能不能移動
    public GameObject play_er;  //取得自己的物件

    public int totalAmount;
    public int playerId;
    public int roleId;
    public string roleName;
    public player_class(int now_loc, int next_loc, int player_money, int can_player_move_round, bool can_player_move)
    {
        this.now_loc = now_loc;
        this.next_loc = next_loc;
        this.player_money = player_money;
        this.can_player_move_round = can_player_move_round;
        this.can_player_move = can_player_move;
    }

    public virtual void set_object(GameObject play_er)
    {
        this.play_er = play_er;
    }
    public virtual void player_skill(int round)
    {

    }
}

public class Fahai_class : player_class
{
    public static bool Fahai_shiled_on = false;
    public Fahai_class(int now_loc, int next_loc, int player_money, int can_player_move_round, bool can_player_move) : base(now_loc, next_loc, player_money, can_player_move_round, can_player_move)
    {

    }
    public override void set_object(GameObject play_er)
    {
        this.play_er = play_er;
    }
    public override void player_skill(int round)
    {
        move.well = "法海發動技能:防禦";
        Fahai_shiled_on = true;
    }
}

public class Xiaoqing_class : player_class
{
    string[] _player = { "Fahai", "Xiaoqing", "Xu_Xian", "Bai_Suzhen" };
    public Xiaoqing_class(int now_loc, int next_loc, int player_money, int can_player_move_round, bool can_player_move) : base(now_loc, next_loc, player_money, can_player_move_round, can_player_move)
    {

    }
    public override void set_object(GameObject play_er)
    {
        this.play_er = play_er;
    }
    public override void player_skill(int round)
    {
        move.well = "小青發動技能:燒錢";
        for (int i = 0; i < 4; i++)
        {
            if (move.player[round].now_loc - move.player[i].now_loc <= 5 && move.player[round].now_loc - move.player[i].now_loc >= 0 && round != i)
            {
                move.player[i].player_money -= 1000;
                if (_player[i] == "Fahai" && Fahai_class.Fahai_shiled_on)
                {
                    move.player[i].player_money += 500;
                    Fahai_class.Fahai_shiled_on = false;
                }
            }

        }
    }
}

public class Xu_Xian_class : player_class
{

    public Xu_Xian_class(int now_loc, int next_loc, int player_money, int can_player_move_round, bool can_player_move) : base(now_loc, next_loc, player_money, can_player_move_round, can_player_move)
    {

    }
    public override void set_object(GameObject play_er)
    {
        this.play_er = play_er;
    }
    public override void player_skill(int round)
    {
        move.well = "此技能為被動技能:\n升級/收購為70%金額，過路費為80%";
    }
}

public class Bai_Suzhen_class : player_class
{
    string[] _player = { "Fahai", "Xiaoqing", "Xu_Xian", "Bai_Suzhen" };
    public Bai_Suzhen_class(int now_loc, int next_loc, int player_money, int can_player_move_round, bool can_player_move) : base(now_loc, next_loc, player_money, can_player_move_round, can_player_move)
    {
        player_money = (int)(player_money * 1.15f);
    }
    public override void set_object(GameObject play_er)
    {
        this.play_er = play_er;
    }
    public override void player_skill(int round)
    {
        move.well = "白素貞發動技能:偷錢";
        for (int i = 0; i < 4; i++)
        {
            if (move.player[i].now_loc - move.player[round].now_loc <= 5 && move.player[i].now_loc - move.player[round].now_loc >= 0 && round != i)
            {
                move.player[i].player_money -= 500;
                move.player[round].player_money += 500;
                if (_player[i] == "Fahai" && Fahai_class.Fahai_shiled_on)
                {
                    move.player[i].player_money += 250;
                    move.player[round].player_money -= 250;
                    Fahai_class.Fahai_shiled_on = false;
                }

            }
        }
    }
}

public class player : MonoBehaviour
{
    
}