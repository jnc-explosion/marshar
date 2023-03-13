using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaMove : MonoBehaviour
{
    // entire settings
    [SerializeField] Vector2 mouseSensitivity = new Vector2(1f, 1f);
    [SerializeField] Vector3 maxCamAngle = new Vector3(300, 0, 0);
    [SerializeField] Vector3 minCamAngle = new Vector3(10, 0, 0);
    [SerializeField] Vector2 mouseVelocity = new Vector2(0.6f, 0.6f);
    [SerializeField] float angleBias;
    [SerializeField] bool enableDebug;

    Transform playerTransform; // Just kidding

    Vector2 getInput(){
        var input = new Vector2();
        input.x = Input.GetAxis("Mouse X") * mouseSensitivity.x;
        input.y = Input.GetAxis("Mouse Y") * mouseSensitivity.y;
        return input;
    }
    
    // fix to Limited Vect3 value
    Vector3 fixIgnoreRegion(Vector3 value, Vector3 ceiling, Vector3 floor){
        var fixedValue = new Vector3();

        // In x ignore region?
        if(value.x < ceiling.x && value.x > floor.x){
            // upper or under of halfline?
            float halfline = ceiling.x - floor.x;
            if(value.x < halfline){   // under
                fixedValue.x = floor.x;
            }else{                  // upper
                fixedValue.x = ceiling.x;
            }
            if(enableDebug) Debug.LogWarning("FUCK OFF at x"); // debug
        }else{
            fixedValue.x = value.x;
        }
        
        // In y ignore region?
        if(value.y < ceiling.y && value.y > floor.y){
            // upper or under of halfline?
            float halfline = ceiling.y - floor.y;
            if(value.y < halfline){   // under
                fixedValue.y = floor.y;
            }else{                  // upper
                fixedValue.y = ceiling.y;
            }
            if(enableDebug) Debug.LogWarning("FUCK OFF at y"); // debug
        }else{
            fixedValue.y = value.y;
        }
        
        // In z ignore region?
        if(value.z < ceiling.z && value.z > floor.z){
            // upper or under of halfline?
            float halfline = ceiling.z - floor.z;
            if(value.z < halfline){   // under
                fixedValue.z = floor.z;
            }else{                  // upper
                fixedValue.z = ceiling.z;
            }
            if(enableDebug) Debug.LogWarning("FUCK OFF at z"); // debug
        }else{
            fixedValue.z = value.z;
        }

        return fixedValue;
    }

    Vector2 fixLength(Vector2 value, Vector2 maxLen){
        var fixedValue = new Vector2();
        var sign = new Vector2(Mathf.Sign(value.x), Mathf.Sign(value.y)); // sign store

        // maxim
        // x vect 
        if(Mathf.Abs(value.x) > maxLen.x) fixedValue.x = maxLen.x * sign.x;
        else fixedValue.x = value.x;
        // y vect 
        if(Mathf.Abs(value.y) > maxLen.y) fixedValue.y = maxLen.y * sign.y;
        else fixedValue.y = value.y;

        return fixedValue;
    }

    void Start()
    {
        // Get Parent transform
        playerTransform = this.transform.parent.transform;
    }

    void Update()
    {
        // get mouse input
        var move = fixLength(getInput(), mouseVelocity);

        // Horizontal behavior process
        playerTransform.Rotate(new Vector3(0f, move.x, 0f));
        
        // Get Barrel Horizontal Angle
        var localAngle = this.transform.localEulerAngles;

        // Vertical behavior process
        localAngle.x -= move.y; // after moved angle
        var bias = new Vector3(angleBias, 0f, 0f);
        if(enableDebug) Debug.Log("Angle: " + localAngle.x); //debug
        this.transform.localEulerAngles = fixIgnoreRegion(localAngle, maxCamAngle, minCamAngle);
    }
}
