using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MidiInput : MonoBehaviour
{
    static MidiInput instance;
    MidiReceiver receiver;
    Dictionary<int, float> controllers;
    bool toLearn;
    int learnt;

    public static int LearntChannel {
        get { return instance.learnt; }
    }

    public static float GetController (int channel)
    {
        if (instance.controllers.ContainsKey (channel)) {
            return instance.controllers [channel];
        } else {
            return -1.0f;
        }
    }

    public static void StartLearn ()
    {
        instance.learnt = -1;
        instance.toLearn = true;
    }

    void Awake ()
    {
        instance = this;
        learnt = -1;
    }

    void Start ()
    {
        receiver = FindObjectOfType (typeof(MidiReceiver)) as MidiReceiver;
        controllers = new Dictionary<int, float> ();
    }

    void Update ()
    {
        while (!receiver.IsEmpty) {
            var message = receiver.PopMessage ();
            if (message.status == 0xb0) {
                controllers [message.data1] = 1.0f / 127 * message.data2;
                if (toLearn) {
                    learnt = message.data1;
                    toLearn = false;
                }
            }
        }
    }
}