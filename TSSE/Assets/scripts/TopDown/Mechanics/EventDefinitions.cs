using UnityEngine;
using System.Collections;

public class EventDefinitions {
    public struct Event
    {
        public string uniqueId;
        public string desc;
        public Event(string description, string unique)
        {
            uniqueId = unique;
            desc = formatDate(System.DateTime.Now) + "\n\n" + description;
        }
    }

    public static string formatDate(System.DateTime date)
    {
        string ret = "";

        switch(date.Month)
        {
            case 1:
                ret = "JANUARY";
                break;
            case 2:
                ret = "FEBRUARY";
                break;
            case 3:
                ret = "MARCH";
                break;
            case 4:
                ret = "APRIL";
                break;
            case 5:
                ret = "MAY";
                break;
            case 6:
                ret = "JUNE";
                break;
            case 7:
                ret = "JULY";
                break;
            case 8:
                ret = "AUGUST";
                break;
            case 9:
                ret = "SEPTEMBER";
                break;
            case 10:
                ret = "OCTOBER";
                break;
            case 11:
                ret = "NOVEMBER";
                break;
            case 12:
                ret = "DECEMBER";
                break;
        }

        int day = date.Day;

        string daySuffix = "";

        switch(day)
        {
            case 1:
                daySuffix = "ST";
                break;
            case 21:
                daySuffix = "ST";
                break;
            case 31:
                daySuffix = "ST";
                break;
            case 2:
                daySuffix = "ND";
                break;
            case 3:
                daySuffix = "RD";
                break;
            case 23:
                daySuffix = "RD";
                break;
            default:
                daySuffix = "TH";
                break;
        }

        ret = ret + " " + day + daySuffix;

        return ret;
    }
}
