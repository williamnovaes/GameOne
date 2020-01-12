using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tilling : MonoBehaviour
{
    public int offsetX;
    public bool hasARightBuddy = false;
    public bool hasALeftBuddy = false;
    public bool reverseScale = false;

    private float spriteWidth = 0f;
    private Camera cam;
    private Transform myTransform;

    private void Awake() {
        cam = Camera.main;
        myTransform = transform;    
    }
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.bounds.size.x;   
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasALeftBuddy || !hasARightBuddy) {
            float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;

            float edgeVisiblePositionRight = (myTransform.position.x + Screen.width/2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (myTransform.position.x - Screen.width/2) + camHorizontalExtend;
        
            if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && !hasARightBuddy) {
                MakeNewBuddy(1);
                hasARightBuddy = true;
            } else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && !hasALeftBuddy) {
                MakeNewBuddy(-1);
                hasALeftBuddy = true;
            }
        }
    }

    void MakeNewBuddy(int rightOrLeft) {
        Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);
        Transform newBuddy = (Transform) Instantiate(myTransform, newPosition, myTransform.rotation);

        if (reverseScale) {
            newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
        }

        newBuddy.parent = myTransform.parent;
        if (rightOrLeft > 0) {
            newBuddy.GetComponent<Tilling>().hasALeftBuddy = true;
        } else {
             newBuddy.GetComponent<Tilling>().hasARightBuddy = true;
        }
    }
}
