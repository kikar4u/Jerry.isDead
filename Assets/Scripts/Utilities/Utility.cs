using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utility
{
    public static T ChangeAlpha<T>(this T g, float newAlpha) where T : Graphic
     {
         var color = g.color;
         color.a = newAlpha;
         g.color = color;
         return g;
     }


    /// <summary>
     /// Convert a float to a string
     /// </summary>
     /// <param name="num">the time you want to display</param>
     /// <returns>Returns a string like that : 00:15:51</returns>
    public static string TransformFloatToMMssmmTime(float num)
    {
        //num *= 10f;
        string[] finalStr = {"00","00","00"};
        string realFinalStr = "";
        string[] nums = num.ToString().Split(',');

        if(nums[1].Length >= 2)
        {
            finalStr[2] = nums[1] != null ? nums[1]?[0].ToString() + nums[1]?[1].ToString()+"" : "00";
        }

        if(nums[0].Length >= 2)
        {
            finalStr[1] = nums[0][nums[0].Length - 2].ToString() + nums[0][nums[0].Length - 1].ToString(); // 99,99
            if(nums[0].Length == 3)
                finalStr[0] = "0" + nums[0][nums[0].Length - 3];
            else if (nums[0].Length > 3)
                finalStr[0] = nums[0][nums[0].Length - 4].ToString() + nums[0][nums[0].Length - 3].ToString() ;
        }
        else if(nums[0].Length == 1)
            finalStr[1] = "0"+nums[0][0].ToString();
        else if (nums[0].Length < 1)
             finalStr[1] = "00";

        realFinalStr = finalStr[0] +" : "+ finalStr[1] +" : "+ finalStr[2];

        return realFinalStr;
    }
}
