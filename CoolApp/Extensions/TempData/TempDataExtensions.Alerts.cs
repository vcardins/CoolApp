#region credits
// ***********************************************************************
// Assembly	: EEJ
// Author	: Rod Johnson
// Created	: 03-17-2013
// 
// Last Modified By : Rod Johnson
// Last Modified On : 03-21-2013
// ***********************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace CoolApp.Extensions.TempData
{
    #region



    #endregion

    public static partial class TempDataDictionaryExtensions
    {
        public static Dictionary<AlertLocation, Dictionary<AlertType, List<string>>> InitializeAlertData(this TempDataDictionary tempData)
        {
            var dataValue = tempData["AlertData"] as Dictionary<AlertLocation, Dictionary<AlertType, List<string>>>;
            if (dataValue == null)
            {
                dataValue = new Dictionary<AlertLocation, Dictionary<AlertType, List<string>>>();
                foreach (AlertLocation loc in Enum.GetValues(typeof(AlertLocation)))
                {
                    var locValues = new Dictionary<AlertType, List<string>>();

                    foreach (AlertType alertType in Enum.GetValues(typeof(AlertType)))
                    {
                        locValues[alertType] = new List<string>();
                    }
                    dataValue.Add(loc, locValues);
                }
                tempData["AlertData"] = dataValue;
            }
            return dataValue;
        }

        public static void AddSuccessMessage(this TempDataDictionary tempData, string message,
            AlertLocation location = AlertLocation.Top)
        {
            AddMessage(tempData, AlertType.Success, message, location);
        }

        public static void AddErrorMessage(this TempDataDictionary tempData, string message,
            AlertLocation location = AlertLocation.Top)
        {
            AddMessage(tempData, AlertType.Error, message, location);
        }

        public static void AddInfoMessage(this TempDataDictionary tempData, string message,
            AlertLocation location = AlertLocation.Top)
        {
            AddMessage(tempData, AlertType.Info, message, location);
        }

        private static void AddMessage(TempDataDictionary tempData, AlertType type, string message,
            AlertLocation location)
        {
            var alertData = tempData.InitializeAlertData();

            alertData[location][type].Add(message);

            tempData["AlertData"] = alertData;
            tempData[type.ToString()] = message;
        }

        public static bool HasError(this TempDataDictionary tempData)
        {
            return tempData.ContainsKey("Error");
        }
    }

    public enum AlertType
    {
        [Description("alert-info")]
        Info,

        [Description("alert-success")]
        Success,

        [Description("alert-error")]
        Error
    }

    public enum AlertLocation
    {
        Top
    }
}