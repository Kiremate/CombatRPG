using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatRegisterController : MonoBehaviour
{

    public float letterPause = 0.1f;
   // public AudioClip typeSound1;
    //public AudioClip typeSound2;

    string message;
    Text textComp;
   public bool finished;
    // Use this for initialization
    void Start()
    {
        textComp = GetComponent<Text>();
        finished = false;
    }

    public void Commence()
    {
        StartCoroutine(TypeText());
    }
    public string Message
    {
        get { return this.message; }
        set { this.message = value; }
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            textComp.text += letter;
            //if (typeSound1 && typeSound2)
            // SoundManager.instance.RandomizeSfx(typeSound1, typeSound2);
            if (letter == message[message.Length-1])
            {
                finished = true;
            }
            yield return 0;
            yield return new WaitForSeconds(letterPause);
        }
    }
}