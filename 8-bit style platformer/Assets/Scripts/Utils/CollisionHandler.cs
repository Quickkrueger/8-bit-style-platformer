using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    const float SIDE_PADDING = 0.3f;

    public static string[] CheckForCollisions(Transform requester)
    {
        // {up, down, left, right}
        string[] directions = new string[4];
        directions[0] = CheckUp(requester);
        directions[1] = CheckDown(requester);
        directions[2] = CheckLeft(requester);
        directions[3] = CheckRight(requester);

        return directions;
    }

    private static string CheckUp(Transform requestor)
    {
        RaycastHit2D hitCenter = Physics2D.Raycast(requestor.position, requestor.up, 0.5f);
        RaycastHit2D hitLeft = Physics2D.Raycast(requestor.position - requestor.right * SIDE_PADDING, requestor.up, 0.5f);
        RaycastHit2D hitRight = Physics2D.Raycast(requestor.position + requestor.right * SIDE_PADDING, requestor.up, 0.5f);

        if (hitCenter.transform != null)
        {
            return hitCenter.transform.tag;
        }
        else if (hitLeft.transform != null)
        {
            return hitLeft.transform.tag;
        }
        else if (hitRight.transform != null)
        {
            return hitRight.transform.tag;
        }

        return "";
    }

    private static string CheckDown(Transform requestor)
    {
        RaycastHit2D hitCenter = Physics2D.Raycast(requestor.position, requestor.up * -1, 0.5f);
        RaycastHit2D hitLeft = Physics2D.Raycast(requestor.position - requestor.right * SIDE_PADDING, requestor.up * -1, 0.5f);
        RaycastHit2D hitRight = Physics2D.Raycast(requestor.position + requestor.right * SIDE_PADDING, requestor.up * -1, 0.5f);

        if (hitCenter.transform != null)
        {
            return hitCenter.transform.tag;
        }
        else if (hitLeft.transform != null)
        {
            return hitLeft.transform.tag;
        }
        else if (hitRight.transform != null)
        {
            return hitRight.transform.tag;
        }

        return "";
    }

    private static string CheckLeft(Transform requestor)
    {
        RaycastHit2D hitCenter = Physics2D.Raycast(requestor.position, requestor.up * -1, 0.5f);
        RaycastHit2D hitBottom = Physics2D.Raycast(requestor.position - requestor.up * SIDE_PADDING, requestor.right * -1, 0.5f);
        RaycastHit2D hitTop = Physics2D.Raycast(requestor.position + requestor.up * SIDE_PADDING, requestor.right * -1, 0.5f);

        if (hitCenter.transform != null)
        {
            return hitCenter.transform.tag;
        }
        else if (hitBottom.transform != null)
        {
            return hitBottom.transform.tag;
        }
        else if (hitTop.transform != null)
        {
            return hitTop.transform.tag;
        }

        return "";
    }

    private static string CheckRight(Transform requestor)
    {
        RaycastHit2D hitCenter = Physics2D.Raycast(requestor.position, requestor.up, 0.5f);
        RaycastHit2D hitBottom = Physics2D.Raycast(requestor.position - requestor.up * SIDE_PADDING, requestor.right, 0.5f);
        RaycastHit2D hitTop = Physics2D.Raycast(requestor.position + requestor.up * SIDE_PADDING, requestor.right, 0.5f);

        if (hitCenter.transform != null)
        {
            return hitCenter.transform.tag;
        }
        else if (hitBottom.transform != null)
        {
            return hitBottom.transform.tag;
        }
        else if (hitTop.transform != null)
        {
            return hitTop.transform.tag;
        }

        return "";
    }
}
