using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            player.anim.SetTrigger("swordAttack");
        }
    }
}
