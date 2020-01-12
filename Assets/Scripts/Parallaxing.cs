using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    [SerializeField]
    private Transform[] backgrounds;
    private float[] parallaxScales;
    [SerializeField]
    private float smoothing = 1;
    private Transform cam;
    private Vector3 previousCamPosition; //store positon of the camera  at the previous frame

    void Awake() {
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        //the previous frame had the current frames camera position
        previousCamPosition = cam.position;
        parallaxScales = new float[backgrounds.Length];

        for (int i = 0; i < backgrounds.Length; i++) {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for each backgrounds
        for (int i = 0; i < backgrounds.Length; i++) {
            //parallax oposite o the camera movement because the previous frame multiplied by the scale
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            float backgroundsTargetPosX = backgrounds[i].position.x + parallax;

            //create a target position which is the backgrounds current position with its target x position
            Vector3 backgroundsTargetPosition = new Vector3(backgroundsTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundsTargetPosition, smoothing * Time.deltaTime);
        }

        previousCamPosition = cam.position;
    }
}
