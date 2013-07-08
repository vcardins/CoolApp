using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Image = System.Web.UI.WebControls.Image;

namespace CoolApp.Infraestructure.Helpers
{
    public static class Common
    {
        private const string BingMapsApiKey = "Atk_aRF91kdsKA5s3iAn2RdT50GtNme0elFx7x3tAxWJBQ-gK5N90Y5WJG_hjXA6";

        public static double? TimeZoneOffset
        {
            get
            {
                if (HttpContext.Current.Session[CacheConstants.SessionCurrentUserTimeZoneOffset] != null)
                    return (double)HttpContext.Current.Session[CacheConstants.SessionCurrentUserTimeZoneOffset];
               
                if (HttpContext.Current.Request.Cookies[CacheConstants.SessionCurrentUserTimeZoneOffset] != null)
                    return Double.Parse(HttpContext.Current.Request.Cookies[CacheConstants.SessionCurrentUserTimeZoneOffset].Value);

                return null;
            }
            set
            {
                HttpContext.Current.Session[CacheConstants.SessionCurrentUserTimeZoneOffset] = value;
                HttpContext.Current.Response.Cookies.Add(new HttpCookie(CacheConstants.SessionCurrentUserTimeZoneOffset, value.ToString()));
            }
        }

        public static string FormatDateTime(DateTime date)
        {
            var custom = date.ToString("MMM dd, h:mmtt");

            return custom;
        }

        public static string FormatDateTimeShort(DateTime date)
        {
            var custom = date.ToString("MMMM dd");

            return custom;
        }

        public static void SetMultipleChoiceSelected(int selectedId, List<SelectListItem> items)
        {
            items.ForEach(c => c.Selected = false);

            if (selectedId > 0)
            {
                var item = items.FirstOrDefault(c => c.Value == selectedId.ToString());

                if (item != null)
                    item.Selected = true;
            }
            else
            {
                if (items.Count > 0)
                {
                    items.ForEach(c => c.Selected = false);
                    items[0].Selected = true;
                }
            }
        }

       
        /// <summary>
        /// Converts a comma separated list values into Array collection
        /// </summary>
        /// <param name="csl">Comma separated list</param>
        /// <returns>returns an IEnumerable collection</returns>
        public static T[] CslToCollection<T>(string csl)
        {
            return CslToCollection<T>(csl, ",");
        }

        /// <summary>
        /// Converts a list with a separator into Array collection
        /// </summary>
        /// <typeparam name="T">Value type</typeparam>
        /// <param name="input">string input</param>
        /// <param name="separator">string value by which the input will be seperated</param>
        /// <returns></returns>
        public static T[] CslToCollection<T>(string input, string separator)
        {
            List<T> result = new List<T>();

            if (!String.IsNullOrEmpty(input))
            {
                try
                {
                    var items = input.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);

                    result.AddRange(items.Select(s => (T)Convert.ChangeType(s, typeof(T))));
                }
                catch
                {
                    throw new InvalidCastException();
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// Gets the latitude and longitude values for a given location
        /// </summary>
        /// <param name="location">Location ex. Los Angeles, USA</param>
        /// <param name="latitude">Returns the location's latitude</param>
        /// <param name="longitude">Returns the location's longitude</param>
        /// <returns>Returns whether the location has been found</returns>
        public static bool GetCoordinates(string location, ref float latitude, ref float longitude)
        {
            string url = string.Format("http://dev.virtualearth.net/REST/v1/Locations?query={0}&key={1}&o=xml", location, BingMapsApiKey);
            return GetCoordinatesPrepared(url, ref  latitude, ref longitude);
        }

        public static bool GetCoordinates(string city, string address, ref float latitude, ref float longitude)
        {
            if (string.IsNullOrEmpty(city))
                return GetCoordinates(address, ref latitude, ref longitude);
            else
            {
                string url = string.Format("http://dev.virtualearth.net/REST/v1/Locations?locality={0}&addressLine={1}&key={2}&o=xml", city, address, BingMapsApiKey);
                return GetCoordinatesPrepared(url, ref  latitude, ref longitude);
            }
        }

        public static double? GetTimeZoneOffset(string location, ref DateTime localTime)
        {
            double? result = null;

            try
            {
                float latitude = 0;
                float longitude = 0;
                var isSuccess = GetCoordinates(location, ref latitude, ref longitude);

                if (isSuccess)
                {
                    result = GetTimeZoneOffsetByCoordinate(latitude, longitude, ref localTime);
                }
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public static double? GetTimeZoneOffsetByCoordinate(float latitude, float longitude, ref DateTime localTime)
        {
            const string apiQuery = "http://api.geonames.org/timezone?lat={0}&lng={1}&username=rsanjar";
            double? result = null;

            try
            {
                var query = String.Format(apiQuery, latitude, longitude);
                var xdoc = XDocument.Load(query);
                var dstOffsetNode = xdoc.Descendants("dstOffset").FirstOrDefault();
                var timeNode = xdoc.Descendants("time").FirstOrDefault();

                if (dstOffsetNode != null)
                {
                    result = double.Parse(dstOffsetNode.Value, CultureInfo.InvariantCulture);

                    if (timeNode != null)
                        DateTime.TryParse(timeNode.Value, out localTime);
                }
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public static string GetIPAddress()
        {
            HttpContext context = HttpContext.Current;

            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static int GetTimeZoneOffsetByIP()
        {
            if (HttpContext.Current.Session[CacheConstants.SessionCurrentUserTimeZoneOffset] != null)
                return (int)HttpContext.Current.Session[CacheConstants.SessionCurrentUserTimeZoneOffset];

            DateTime localTime = DateTime.Now;
            int offset = 0;

#if(!DEBUG)
			var ip = GetIPAddress();
            //var ip = "83.69.139.158";
#else
            var ip = "83.69.139.158";
#endif

            if (ip != null)
            {
                var locationInfo = GetLocationInfo(ip);

                if (locationInfo != null)
                {
                    offset = (int)GetTimeZoneOffsetByCoordinate(locationInfo.Latitude, locationInfo.Longitude, ref localTime).GetValueOrDefault();
                    HttpContext.Current.Session[CacheConstants.SessionCurrentUserTimeZoneOffset] = offset;
                }
            }

            return offset;
        }

        public static LocationInfo GetLocationInfo(string ipAddress)
        {
            IPAddress i = IPAddress.Parse(ipAddress);

            return GetLocationInfo(i);
        }

        public static LocationInfo GetLocationInfo(IPAddress ipAddress)
        {

            LocationInfo result = null;
            try
            {
                string ip = ipAddress.ToString();

                string r;
                using (var w = new WebClient())
                {
                    r = w.DownloadString(String.Format("http://freegeoip.net/xml/{0}", ip));
                }

                var xmlResponse = XDocument.Parse(r);


                result = (from x in xmlResponse.Descendants("Response")
                          select new LocationInfo
                          {
                              CountryCode = x.Element("CountryCode").Value,
                              CountryName = x.Element("CountryName").Value,
                              Latitude = float.Parse(x.Element("Latitude").Value),
                              Longitude = float.Parse(x.Element("Longitude").Value),
                              Name = x.Element("City").Value
                          }).SingleOrDefault();
            }
            catch
            {
                //Looks like we didn't get what we expected.
            }

            return result;
        }

        private static bool GetCoordinatesPrepared(string url, ref float latitude, ref float longitude)
        {
            bool result = false;

            try
            {
                XDocument xdoc = XDocument.Load(url);

                if (xdoc.Descendants().FirstOrDefault(c => c.Name.LocalName == "Latitude") != null)
                {
                    latitude = float.Parse(xdoc.Descendants().First(d => d.Name.LocalName == "Latitude").Value,
                                           CultureInfo.InvariantCulture);
                    longitude = float.Parse(xdoc.Descendants().First(d => d.Name.LocalName == "Longitude").Value,
                                            CultureInfo.InvariantCulture);

                    result = true;
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }

    public class LocationInfo
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Name { get; set; }
    }
}