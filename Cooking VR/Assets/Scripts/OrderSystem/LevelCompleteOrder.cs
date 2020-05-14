using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteOrder : MonoBehaviour
{

    [SerializeField]
    private UnityEngine.UI.Text TimeTaken;

    [SerializeField]
    public UnityEngine.UI.Text BodyLeft;

    [SerializeField]
    private UnityEngine.UI.Text BodyRight;


    private int NextSceneToLoad;








    public void SetUp( int NSTL)
    {


        NextSceneToLoad = NSTL;
        string LeftText = "";
        string RightText = "";
        foreach (OrderManager.FinishedOrder a in OrderManager.finishedOrders)
        {
            LeftText += "Order#: " + a.OriginalOrder.OrderNum+"\n";
            RightText +=  a.Score+"/"+(a.OriginalOrder.Ingredents.Count - 2) * 100+"\n";
        }
        BodyLeft.text = LeftText;
        BodyRight.text = RightText;

        int IntTime = 0;
        IntTime = (int)Time.fixedTime;
        TimeTaken.text = "Time Ordered: " + IntTime / 60 + ":" + IntTime % 60;
    }



    public void NextLevel()
    {
        FadeSceneChanger.FadeToScene(NextSceneToLoad);

       // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
       // SceneManager.LoadScene(NextSceneToLoad);
    }
}
