using UnityEngine;
using System.Collections;

public class BlockMove : MonoBehaviour
{
    public int channel;
    public float moveWidth;

    void Start ()
    {
    
    }
    
    void Update ()
    {
        transform.localPosition = Vector3.up * (moveWidth * (MidiInput.GetController(channel) - 0.5f));
    }
}