using UnityEngine;

public class Player : MonoBehaviour
{
    Snake script_ref;
    public GameObject gameOverPopup;

    // Start is called before the first frame update
    void Awake()
    {
        script_ref = transform.GetComponent<Snake>();
    }

    void OnTriggerEnter(Collider target){
        if(target.tag == "food"){
            target.gameObject.SetActive(false);
            Food.instance.MoveFood(target.gameObject.name);
            script_ref.AddBody();
        }
        if(target.tag == "wall" || target.tag == "body") {
            Debug.Log("gameover");
            script_ref.StopMove();
            Invoke("GameOver", 0.5f);
        }
    }

    void GameOver(){
            gameOverPopup.SetActive(true);
    }
}
