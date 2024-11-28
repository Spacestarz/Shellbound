using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private void Awake()
    {
        //StartCoroutine(shockwave());
        transform.localEulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 90);
    }
    public IEnumerator shockwave(float duration, float scale, float range, Transform target)
    {
        //parent.stop();
        //transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
        //still = true;
        Vector3 startscale = transform.localScale;
        Vector3 endscale = Vector3.one;
        endscale.y = endscale.y * scale;
        Vector3 startlocation = transform.localPosition;
        Vector3 endlocation = new Vector3(0, -1.5f, 0);
        endlocation.z = endlocation.z + range;
        float elapsed = 0;
        //Debug.Log(endlocation);

        while (elapsed < duration)
        {
            var t = elapsed / duration;
            transform.localScale = Vector3.Lerp(startscale, endscale, t);
            transform.localPosition = Vector3.Lerp(startlocation, endlocation, t);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
    void shockwavereturn(GameObject wave)
    {
        //parent.start();
        //still = false;
        wave.transform.localPosition = new Vector3(0, -1.5f, 0);
        wave.transform.localScale = new Vector3(1, 0.5f, 1);
        //parent.attacking();
    }
}
