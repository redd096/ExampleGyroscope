using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PcOrMobile : MonoBehaviour
{
    [SerializeField] GameObject[] objectsPc = default;
    [SerializeField] GameObject[] objectsMobile = default;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        ActiveOrDeactive(false);
#else
        ActiveOrDeactive(true);
#endif
    }

    void ActiveOrDeactive(bool pc)
    {
        //active pc or mobile
        bool activePc = pc ? true : false;
        bool activeMobile = !activePc;

        //every pc object
        foreach(GameObject go in objectsPc)
        {
            go.SetActive(activePc);
        }

        //every mobile object
        foreach(GameObject go in objectsMobile)
        {
            go.SetActive(activeMobile);
        }
    }
}
