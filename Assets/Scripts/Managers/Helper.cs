using UnityEngine;
using System.Collections;

    public static class Helper
    {
        public static float ClampAngle( float angle, float min, float max)
        {
            do
            {
                if (angle < -360)
                    angle += 360;
                if (angle > 360)
                    angle -= 360;
            }
            while (angle < -360 || angle > 360);

            return Mathf.Clamp(angle, min, max);
        } 
        
        /**************Moved From TitanPlayerController for nostalgia purposes and potential reference. will refactor at some point***/
       public static float ClampedAngle(float theAngle)
        {
            if (theAngle < -360.0f)
            {
                theAngle += 360.0f;
            }
            else if (theAngle > 360.0f)
            {
                theAngle -= 360.0f;
            }

            return theAngle;
        }
    }

    
        