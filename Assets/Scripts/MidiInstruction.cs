using UnityEngine;
using System.Collections;

public class MidiInstruction : MonoBehaviour
{
    public GUIStyle labelStyle;
    public Racket[] rackets;
    public GameObject manager;
    string message;

    IEnumerator Start ()
    {
        message = "MOVE THE 1ST\nCONTROLLER.";

        MidiInput.StartLearn ();
        while (MidiInput.LearntChannel < 0) {
            yield return null;
        }

        rackets [0].channel = MidiInput.LearntChannel;
        rackets [0].gameObject.SetActive (true);

        message = "THEN, THE 2ND\nONE PLEASE.";

        while (true) {
            MidiInput.StartLearn ();
            while (MidiInput.LearntChannel < 0) {
                yield return null;
            }
            if (MidiInput.LearntChannel != rackets [0].channel) {
                break;
            }
            yield return null;
        }

        rackets [1].channel = MidiInput.LearntChannel;
        rackets [1].gameObject.SetActive (true);

        manager.SetActive (true);
        enabled = false;
    }

    void OnGUI ()
    {
        GUI.Label (new Rect (0, 0, Screen.width, Screen.height), message, labelStyle);
    }
}