using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerInteractionController : MonoBehaviour
{
    public Image crosshairDefault;
    public Image crosshairSelected;
    public GraphicRaycaster graphicRaycaster;

    void Awake()
    {
        ToggleSelectedCursor(false);
    }

    void Update()
    {
        PhysicsRaycasts();
        GraphicsRaycasts();
    }

    void PhysicsRaycasts()
    {
        // store the centre of the screen 
        Vector3 centreOfScreen = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0);

        // distance to fire the ray, in metres
        float distanceToFireRay = 20;

        // create the ray from the centre of the screen (centre of the camera) 
        Ray centreOfScreenRay = Camera.main.ScreenPointToRay(centreOfScreen);

        // variable to populate with collision data if we interesect a collider
        RaycastHit hit;
        int layerMask;

        layerMask = LayerMask.GetMask(new[] {"InteractiveObject"});
        // fire the ray, storing any collision data in the "hit" variable
        if (Physics.Raycast(centreOfScreenRay, out hit, distanceToFireRay, layerMask))
        {
            ToggleSelectedCursor(true);

            // if the user has clicked the left mouse button THIS FRAME
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Raycast hit: " + hit.transform.name);
                hit.transform.GetComponent<InteractiveObjectBase>().OnInteraction();
            }
        }
        else // raycast didn't hit anything...
        {
            ToggleSelectedCursor(false);
        }

    }

    public void ToggleSelectedCursor(bool showSelectedCursor)
    {
        crosshairSelected.enabled = showSelectedCursor;
        crosshairDefault.enabled = !showSelectedCursor;
    }

    void GraphicsRaycasts()
    {
        // retrieve event data
        PointerEventData eventData = new PointerEventData(EventSystem.current);

        // set pointer position to the centre of the screen (reuse centreOfScreen.x / .y)
        eventData.position = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        // list to populate with results from raycast
        List<RaycastResult> results = new List<RaycastResult>();

        // the graphics raycast
        graphicRaycaster.Raycast(eventData, results);

        // boolean to track whether we hit a button
        bool hitButton = false;

        // if there are results from the raycast
        if (results.Count > 0)
        {
            // loop those results
            for (int i = 0; i < results.Count; i++)
            {
                // retrieve the Button Component from result list index and store
                Button button = results[i].gameObject.GetComponent<Button>();

                // if there is a Button Component, i.e. not null
                if (button != null)
                {
                    // we hit a button so flag the boolean true
                    hitButton = true;

                    // if the mouse is down, then Invoke() the .onClick property of the button
                    if (Input.GetMouseButtonDown(0)) button.onClick.Invoke();
                }
            }

            // if out boolean is flagged, toggle the selected cursor
            if (hitButton) ToggleSelectedCursor(true);
        }
    }

}
