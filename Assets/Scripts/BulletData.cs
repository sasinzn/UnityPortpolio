using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData",
    menuName = "Create BulletData", order = 1)]
public class BulletData : ScriptableObject
{
    public string key = "Player";
    public Sprite sprite;
    public int level = 1;
    public float interval = 1.0f;
    public float power = 2.0f;
    public float speed = 10.0f;
    public float scale = 1.0f;
    public float distance = 2.0f;
    public float pushPower = 0.0f;
    public int emitterAmount = 1;
    public bool isTrigger = false;
    public bool isMove = true;
}
