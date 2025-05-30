using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private float lookAheadDistance = 1f;
    private Bounds cameraBounds;
    private Vector3 velocity = Vector3.zero; // SmoothDamp에 사용할 초기 속도
    private PlayerMove playerMove;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        GameObject boundaryObj = GameObject.Find("CameraBoundary");
        playerMove = target.GetComponent<PlayerMove>();

        BoxCollider2D boundaryCollider = boundaryObj.GetComponent<BoxCollider2D>();
        cameraBounds = boundaryCollider.bounds;
    }

    private void LateUpdate()
    {
        float lookAheadDir = target.localScale.x * playerMove.inputX;
        Vector3 targetPosition = new Vector3(
            target.position.x + lookAheadDir * lookAheadDistance,
            target.position.y,
            transform.position.z
        );

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);

        float cameraHalfHeight = Camera.main.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

        smoothPosition.x = Mathf.Clamp(smoothPosition.x,
            cameraBounds.min.x + cameraHalfWidth,
            cameraBounds.max.x - cameraHalfWidth);

        smoothPosition.y = Mathf.Clamp(smoothPosition.y,
            cameraBounds.min.y + cameraHalfHeight,
            cameraBounds.max.y - cameraHalfHeight);

        transform.position = smoothPosition;
    }
}
