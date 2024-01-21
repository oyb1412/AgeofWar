using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
   
    public string[] charName = { "clubman", "slingshotman", "dino", "sword", "archer", "knight", "dueler", "mouschete", "canoneer", "melee infantry", "gun infantry", "Tank", "godblade", "blaster", "war machie", "super soldier" };
    public int[] charCost = { 15, 25, 100, 50, 75, 500, 200, 400, 1000, 1500, 2000, 7000, 5000, 6000, 20000, 150000 };
    public int[] charTraningTime = { 40, 40, 100, 70, 50, 100, 100, 100, 200, 100, 100, 300, 100, 100, 300, 100 };
    public int[] charMaxHP = { 55, 42, 160, 100, 80, 300, 200, 160, 600, 350, 300, 1200, 1000, 800, 3000, 4000 };
    public int[] charDamGE = { 16, 8, 40, 35, 14, 60, 79, 20, 120, 100, 30, 300, 250, 80, 600, 400 };
    public float[] charMeleeAttackSpeed = { 1f, 1f, 1.12f, 2.47f / 2, 2.47f / 2, 1.30f, 1.05f, 1.15f, 1.95f, 0.75f, 0.52f, 1.57f, 0.92f, 0.7f, 2.25f, 0.7f };
    public float charAttackRange = 1f;
    public float charMoveSpeed = 1f;
    // ranged troops have different speeds when walking or stationating
    public float[] charRangeAttackSpeed = { 0f, 0.8f, 0f, 0f, 1f, 0f, 0f, 1.15f, 0f, 0f, 0.52f, 0f, 0f, 0.35f, 0f, 0.35f };
    public float[] tekiCharKillGold = { 20, 33, 180, 65, 98, 650, 260, 520, 1300, 1950, 2600, 9100, 6500, 7800, 26000 };


    public float[] base_hp = { 500, 1100, 2000, 3200, 4700 };

    public string[] turret_name = { "Rock slingshot", "egg automatic", "primitive catapult", "Catapult", "Fire Catapult", "oil", "Small Cannon", "Medium Cannon", "Big cannon", "gun", "rocket launcher", "double gun", "laser", "red pew pew", "blue pew pew" };
    public int[] turret_cost = { 100, 200, 500, 500, 750, 1000, 1500, 3000, 6000, 7000, 9000, 14000, 24000, 40000, 100000 };
    public int[] xp_cost = { 4000, 14000, 45000, 200000, 10000000 };
    public int[] slot_cost = { 1000, 3000, 7500 };


    public float[] turret_speed = { 0.8f, 0.25f, 1.37f, 2.47f, 2.47f, 1.92f, 1.12f, 2f, 2f, 1.12f, 1f, 0.5f, 1f, 0.25f, 0.25f };
    public int[] turret_damage = { 12, 5, 25, 40, 50, 125, 30, 70, 100, 70, 100, 60, 100, 40, 60 };
    public float[] turret_range = { 10f,10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };


}
