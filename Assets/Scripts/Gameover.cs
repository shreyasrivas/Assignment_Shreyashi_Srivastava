using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Gameover : MonoBehaviour
{
    public Text score_text; 
    // Start is called before the first frame update
    void Awake()
    {
        score_text.text += PlayerPrefs.GetInt("TotalScore").ToString();
    }

    public void GoHome() {
        SceneManager.LoadScene("Main");
    }


}
