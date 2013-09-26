using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MidiInput : MonoBehaviour
{
    static MidiInput instance;
    MidiReceiver receiver;
    Dictionary<int, float> controllers;

    public static float GetController (int channel)
    {
        if (instance.controllers.ContainsKey (channel)) {
            return instance.controllers [channel];
        } else {
            return -1.0f;
        }
    }

    void Awake ()
    {
        instance = this;
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
            }
        }
    }
}