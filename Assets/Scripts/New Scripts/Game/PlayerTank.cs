using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            GameManager.Instance.UpdateGameState(GameState.StageFailUI,2f);
            GameManager.Instance.HandleStageClear(false);
            Destroy(gameObject.transform.parent.parent.gameObject);
        }
    }
}
