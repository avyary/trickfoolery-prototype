using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    bool isCharging = false;
    bool isAggro = false;
    bool isAttacking = false;
    public float chargeTime = 3f;
    public float attackTime = 1.5f;
    float flashInterval;
    Color originalColor;
    public LineRenderer lineRend;
    public GameObject player;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        originalColor = GetComponent<MeshRenderer>().material.color;
        flashInterval = chargeTime / 6f;
        lineRend.positionCount = 2;
        player = GameObject.Find("Capsule");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))  // replace with trigger
        {
            if (!isAggro)
            {
                StartCoroutine(aggro());
            }
        }

        if (isAttacking)
        {
            gameObject.transform.position += direction * Time.deltaTime; 
        }
    }

    IEnumerator aggro() {
        isAggro = true;
        StartCoroutine(charge());
        yield return new WaitForSeconds(chargeTime);
        StartCoroutine(attack());
    }

    IEnumerator attack()
    {
        print("starting attack");
        isAttacking = true;
        yield return new WaitForSeconds(attackTime);
        GetComponent<MeshRenderer>().material.color = originalColor;
        isAttacking = false;
        print("finishing attack");
        isAggro = false;
    }

    IEnumerator charge()
    {   
        lineRend.SetPosition(0, gameObject.transform.position);
        direction = player.transform.position - gameObject.transform.position;
        lineRend.SetPosition(1, gameObject.transform.position + direction);
        lineRend.enabled = true;
        isCharging = true;
        print("starting charge");
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(flashInterval);
        GetComponent<MeshRenderer>().material.color = originalColor;
        yield return new WaitForSeconds(flashInterval);
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(flashInterval);
        GetComponent<MeshRenderer>().material.color = originalColor;
        yield return new WaitForSeconds(flashInterval);
        GetComponent<MeshRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(flashInterval * 2f);
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        isCharging = false;
        lineRend.enabled = false;
        print("finishing charge");
    }
}
