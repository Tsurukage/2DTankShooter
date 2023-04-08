using UnityEngine;
using UnityEngine.UI;

public class SimpleGame : MonoBehaviour
{
    public int shootingCount = 3;
    public int tankCount;
    public Text valueText, valueText2;
    public Transform tankLt;
    public Transform stageClearPanel, gameOverPanel;
    // Start is called before the first frame update
    void Awake()
    {
        tankCount = tankLt.childCount;
        if(valueText != null)
            valueText.text = tankCount.ToString();
        if (valueText2 != null)
            valueText2.text = shootingCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootingCD()
    {
        if(tankCount > 0)
            tankCount--;
        valueText.text = tankCount.ToString();
        CheckGame();
        
    }
    public void Count()
    {
        if (shootingCount > 0)
            shootingCount--;
        valueText2.text = shootingCount.ToString();
        CheckGame();
    }
    public void CheckGame()
    {
        if (tankCount == 0)
        {
            Debug.Log("Stage Complete");
            var gL = Instantiate(stageClearPanel, transform.parent);
            gL.gameObject.SetActive(true);
        }
        else if (tankCount > 0 && shootingCount == 0)
        {
            Debug.Log("Game Over!");
            var gL = Instantiate(gameOverPanel, transform.parent);
            gL.gameObject.SetActive(true);
        }
    }
}
