using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField] Light l;
    public Color[] colour;
    int last;
    float i;
    float transitionLength = 2;

    private void OnEnable()
    {
        colour[0] = l.color;
    }
    public void ChangeColour(int to)
    {
        StartCoroutine(ColourChange(to));
    }

    IEnumerator ColourChange(int to)
    {
        i = 0;
        while (i <= 1)
        {
            yield return new WaitForSeconds(0.05f);
            i += transitionLength*0.05f;
            l.color = Color.Lerp(colour[last], colour[to], i);
        }
        l.color = colour[to];
        last = to;

    }


}
