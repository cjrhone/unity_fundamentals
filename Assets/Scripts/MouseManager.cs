using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    //Know what objects are clickable
    public LayerMask clickableLayer;

    //Swap Cursors per object
    public Texture2D pointer; //Normal Pointer
    public Texture2D target; //Cursor for clickable objects like the world
    public Texture2D doorway; //Cursor for doorways 
    public Texture2D combat; //Cursor combat actions

    public EventVector3 OnClickEnvironment;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value)) //if user clicks something with a value
        {
            bool door = false; // door becomes false
            bool item = false;


             
            if(hit.collider.gameObject.tag == "Doorway") // if doorway
            {
                Cursor.SetCursor(doorway, new Vector2 (16, 16), CursorMode.Auto); // change cursor
                door = true; // door becomes true
            }

            else if(hit.collider.gameObject.tag == "Item") 
            {
                Cursor.SetCursor(combat, new Vector2(16, 16), CursorMode.Auto);
                item = true;
            }

            else
            {
                Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto); // else change cursor to target
            }

            if(Input.GetMouseButtonDown(0))
            {
                if(door)
                {
                    Transform doorway = hit.collider.gameObject.transform; // stores transform data to doorway 

                    OnClickEnvironment.Invoke(doorway.position);
                    Debug.Log("DOOR");
                }

                else if(item)
                {
                    Transform itemPosition = hit.collider.gameObject.transform;

                    OnClickEnvironment.Invoke(itemPosition.position);
                    Debug.Log("ITEM");
                }

                else
                {
                   OnClickEnvironment.Invoke(hit.point); 
                }
               
            }
        
        }

        else 
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto); // default cursor(?)
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3>{}