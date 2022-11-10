using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// DATA
// just holding prices
// and related methods

public static class ComputerPartPrices
{
    public static int GetPrice(ComputerPart part)
    {
        int itemPrice = 999;    // if any wrong input, we see 999 just in case
        switch(part)
        {
            case ComputerPart.Body:
                itemPrice = 300;
                break;

            case ComputerPart.Keyboard_Frame:
                itemPrice = 25;
                break;

            case ComputerPart.Keyboard:
                itemPrice = 100;
                break;

            case ComputerPart.Mousepad_Frame:
                itemPrice = 25;
                break;

            case ComputerPart.Mousepad:
                itemPrice = 75;
                break;

            case ComputerPart.Screen_Frame:
                itemPrice = 100;
                break;

            case ComputerPart.Screen:
                itemPrice = 250;
                break;

            case ComputerPart.Graphic_Card:
                itemPrice = 500;
                break;

            case ComputerPart.Ram:
                itemPrice = 300;
                break;

            case ComputerPart.Camera:
                itemPrice = 100;
                break;

            case ComputerPart.Screen_Larger:
                itemPrice = 300;
                break;

            case ComputerPart.Style:
                itemPrice = 200;
                break;

            case ComputerPart.Protection_Sleeve:
                itemPrice = 200;
                break;

            case ComputerPart.Logo:
                itemPrice = 100;
                break;
        }
        return itemPrice;
    }
}
