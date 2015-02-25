using UnityEngine;
using System.Collections;

public class TrailRenderedFix : MonoBehaviour {

    private TrailRenderer trail;

    void Start()
    {
        trail = GetComponent<TrailRenderer>();
    }

	void OnEnable()
    {
        if(trail != null)
        StartCoroutine(ResetTrail());
    }

    private IEnumerator ResetTrail()
    {
        float time = trail.time;
        trail.time = 0;
        yield return new WaitForEndOfFrame();
        trail.time = time;
    }
}
