using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_battle : MonoBehaviour
{
    string battle_scene_name = "newBattle";
    //バトルステージに移動するイベント
    public void Go()
    {
        SceneManager.LoadScene(battle_scene_name);
    }
}
