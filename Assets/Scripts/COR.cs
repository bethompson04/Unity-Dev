using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COR : MonoBehaviour
{
    [SerializeField] float time = 3;
    [SerializeField] bool go = false;
    // Start is called before the first frame update
    Coroutine timerCoroutine;

    void Start()
    {
        timerCoroutine = StartCoroutine(Timer(time));
        StartCoroutine(WaitAction());
        StartCoroutine("StoryTime");
    }

    // Update is called once per frame
    void Update()
    {
        // time -= Time.deltaTime;
        // if(time <= 0)
        // {
        //     time = 3;
        //     print("goodbye");
        // }
    }

    IEnumerator Timer(float time)
    {
        while(true)
        {
            yield return new WaitForSeconds(time);
            print("tick");
        }
        yield return null;
    }

    IEnumerator StoryTime()
    {
        print("Hello!");
        yield return new WaitForSeconds(3);
        print("Welcome to HELL!");
        yield return new WaitForSeconds(3);
        print("TIME TO DIE!!! >:)");

        StopCoroutine(timerCoroutine);

        yield return null;
    }

    IEnumerator WaitAction()
    {
        yield return new WaitWhile(() => go);
        print("GO!");
        yield return null;
    }
}
