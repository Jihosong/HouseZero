using UnityEngine;
using System.Collections;

public class windowOpenScript : MonoBehaviour
{

    public bool open = false;
    public float doorOpenAngle = 45f;
    public float doorCloseAngle = 0f;
    public float smooth = 2f;

    AudioSource doorSound;

    // Use this for initialization
    void Start()
    {
        doorSound = GetComponent<AudioSource>();
    }

    public void ChangeDoorState()
    {
        open = !open;
        doorSound.Play();

    }
    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            Quaternion targetRotation = Quaternion.Euler(doorOpenAngle, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }

        else
        {
            Quaternion targetRotation2 = Quaternion.Euler(doorCloseAngle, 0, 0);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation2, smooth * Time.deltaTime);
        }
    }
}
