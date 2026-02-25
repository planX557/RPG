using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private Transform backGround;
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private float imageWidthOffset = 10;

    private float imageFullWidth;
    private float imageHalfWidth;

    public void CalculateImageWidth()
    {
        imageFullWidth = backGround.GetComponent<SpriteRenderer>().bounds.size.x;
        imageHalfWidth = imageFullWidth / 2;
    }

    public void Move(float distanceToMove)
    {
        backGround.position += Vector3.right * (distanceToMove * parallaxMultiplier);
    }

    public void LoopBackground(float cameraLeftEdge,float cameraRightEdge)
    {
        float imageRightEdge = (backGround.position.x + imageHalfWidth) - 10;
        float imageLeftEdge = (backGround.position.x - imageHalfWidth) + 10;

        if (imageRightEdge < cameraLeftEdge)
            backGround.position += Vector3.right * imageFullWidth;
        else if (imageLeftEdge > cameraRightEdge)
            backGround.position += Vector3.right * -imageFullWidth;
    }
}
