using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public EventVector3 OnClickEnviroment;

    [SerializeField] private bool _doorOpen = false;
    [SerializeField] private LayerMask _clickableLayer;
    [SerializeField] private Texture2D _pointer;
    [SerializeField] private Texture2D _target;
    [SerializeField] private Texture2D _doorway;
    [SerializeField] private Texture2D _combat;
    
    private CursorMode _cursorMode = CursorMode.Auto;
    private Vector2 _hotSpot = Vector2.zero;
    private float _doorOpeningMultiplier = 4.2f;

    void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit _hit, 50, _clickableLayer.value))
        {
            bool _door = false;
            if(_hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(_doorway, new Vector2(_doorway.width/2, _doorway.height/2), _cursorMode);
                _door = true;
            }
            else
            {
                Cursor.SetCursor(_target, new Vector2(_target.width/2, _target.height/2), _cursorMode);
            }

            if(Input.GetMouseButtonDown(0))
            {
                if (_door)
                {
                    //Transform _doorway = _hit.collider.gameObject.transform;
                    //OnClickEnviroment.Invoke(_doorway.position);
                    if (!_doorOpen)
                    {
                        _hit.collider.gameObject.transform.Translate(Vector3.up * _doorOpeningMultiplier);
                        _doorOpen = true;
                    }                    
                    else
                    {
                        _hit.collider.gameObject.transform.Translate(Vector3.down * _doorOpeningMultiplier);
                        _doorOpen = false;
                    }
                }
                else
                {
                    OnClickEnviroment.Invoke(_hit.point);
                }
            }
        }
        else
        {
            Cursor.SetCursor(_pointer, _hotSpot, _cursorMode);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> { }