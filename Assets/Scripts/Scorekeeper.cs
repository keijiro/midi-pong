using UnityEngine;
using System.Collections;

public class Scorekeeper : MonoBehaviour
{
    public GameObject ballPrefab;
    public GUIStyle labelStyle;
    int[] scores;
    int lastWon;

    void Awake ()
    {
        scores = new int[2];
    }

    IEnumerator Start ()
    {
        while (true) {
            yield return new WaitForSeconds (1.0f);

            Ball.initialDirection = (lastWon == 0) ? Vector3.right : -Vector3.right;
            var ball = Instantiate (ballPrefab) as GameObject;

            while (ball != null) {
                yield return null;
            }

            yield return new WaitForSeconds (1.0f);
        }
    }

    void OnGUI ()
    {
        var w = Screen.width / 2;
        var h = Screen.height;
        GUI.Label (new Rect (0, 0, w, h), scores [0].ToString (), labelStyle);
        GUI.Label (new Rect (w, 0, w, h), scores [1].ToString (), labelStyle);
    }

    void Increment (int index)
    {
        scores [index]++;
        lastWon = index;
    }
}