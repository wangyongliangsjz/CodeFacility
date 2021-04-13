using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 坐标系转换
    /// </summary>
    public class MapTransform
    {

        public const double x_PI = 3.14159265358979324 * 3000.0 / 180.0;
        public const double PI = 3.1415926535897932384626;
        public const double a = 6378245.0;
        public const double ee = 0.00669342162296594323;

        /// <summary>
        /// 百度坐标系 (BD-09) 与 火星坐标系 (GCJ-02)的转换
        /// 即 百度 转 谷歌、高德
        /// </summary>
        /// <param name="mapx"></param>
        /// <param name="mapy"></param>
        /// <returns></returns>
        public static double[] BD09ToGCJ02(double mapx, double mapy)
        {
            double bd_lon = mapx;
            double bd_lat = mapy;
            double x = bd_lon - 0.0065;
            double y = bd_lat - 0.006;
            double z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * x_PI);
            double theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * x_PI);
            double gg_lng = z * Math.Cos(theta);
            double gg_lat = z * Math.Sin(theta);
            return new double[] { gg_lng, gg_lat };
        }

       /// <summary>
       /// 火星坐标系 (GCJ-02) 与百度坐标系 (BD-09) 的转换
       /// 即谷歌、高德 转 百度
       /// </summary>
       /// <param name="mapx"></param>
       /// <param name="mapy"></param>
       /// <returns></returns>
        public static double[] GCJ02ToBD09(double mapx, double mapy)
        {
            double lat = mapy;
            double lng = mapx;
            double z = Math.Sqrt(lng * lng + lat * lat) + 0.00002 * Math.Sin(lat * x_PI);
            double theta = Math.Atan2(lat, lng) + 0.000003 * Math.Cos(lng * x_PI);
            double bd_lng = z * Math.Cos(theta) + 0.0065;
            double bd_lat = z * Math.Sin(theta) + 0.006;
            return new double[] { bd_lng, bd_lat };
        }

        /// <summary>
        /// WGS84转GCj02
        /// </summary>
        /// <param name="mapx"></param>
        /// <param name="mapy"></param>
        /// <returns></returns>
        public static double[] WGS84ToGCJ02(double mapx, double mapy)
        {
            double lat = mapy;
            double lng = mapx;
            if (out_of_china(lng, lat))
            {
                return new double[] { lng, lat };
            }
            else
            {
                double dlat = transformlat(lng - 105.0, lat - 35.0);
                double dlng = transformlng(lng - 105.0, lat - 35.0);
                double radlat = lat / 180.0 * PI;
                double magic = Math.Sin(radlat);
                magic = 1 - ee * magic * magic;
                double sqrtmagic = Math.Sqrt(magic);
                dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
                dlng = (dlng * 180.0) / (a / sqrtmagic * Math.Cos(radlat) * PI);
                var mglat = lat + dlat;
                var mglng = lng + dlng;
                return new double[] { mglng, mglat };
            }
        }

        /// <summary>
        /// GCJ02 转换为 WGS84
        /// </summary>
        /// <param name="mapx"></param>
        /// <param name="mapy"></param>
        /// <returns></returns>
        public static double[] GCJ02ToWGS84(double mapx, double mapy)
        {
            double lat = mapy;
            double lng = mapx;
            if (out_of_china(lng, lat))
            {
                return new double[] { lng, lat };
            }
            else
            {
                double dlat = transformlat(lng - 105.0, lat - 35.0);
                double dlng = transformlng(lng - 105.0, lat - 35.0);
                double radlat = lat / 180.0 * PI;
                double magic = Math.Sin(radlat);
                magic = 1 - ee * magic * magic;
                double sqrtmagic = Math.Sqrt(magic);
                dlat = (dlat * 180.0) / ((a * (1 - ee)) / (magic * sqrtmagic) * PI);
                dlng = (dlng * 180.0) / (a / sqrtmagic * Math.Cos(radlat) * PI);
                double mglat = lat + dlat;
                double mglng = lng + dlng;
                return new double[] { lng * 2 - mglng, lat * 2 - mglat };
            }
        }

        private static double transformlat(double mapx, double mapy)
        {
            double lat = mapy;
            double lng = mapx;
            double ret = -100.0 + 2.0 * lng + 3.0 * lat + 0.2 * lat * lat + 0.1 * lng * lat + 0.2 * Math.Sqrt(Math.Abs(lng));
            ret += (20.0 * Math.Sin(6.0 * lng * PI) + 20.0 * Math.Sin(2.0 * lng * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lat * PI) + 40.0 * Math.Sin(lat / 3.0 * PI)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(lat / 12.0 * PI) + 320 * Math.Sin(lat * PI / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private static double transformlng(double mapx, double mapy)
        {
            double lat = mapy;
            double lng = mapx;
            double ret = 300.0 + lng + 2.0 * lat + 0.1 * lng * lng + 0.1 * lng * lat + 0.1 * Math.Sqrt(Math.Abs(lng));
            ret += (20.0 * Math.Sin(6.0 * lng * PI) + 20.0 * Math.Sin(2.0 * lng * PI)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lng * PI) + 40.0 * Math.Sin(lng / 3.0 * PI)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(lng / 12.0 * PI) + 300.0 * Math.Sin(lng / 30.0 * PI)) * 2.0 / 3.0;
            return ret;
        }

        /// <summary>
        /// 判断是否在国内，不在国内则不做偏移
        /// </summary>
        /// <param name="mapx"></param>
        /// <param name="mapy"></param>
        /// <returns></returns>
        public static bool out_of_china(double mapx, double mapy)
        {
            var lat = mapy;
            var lng = mapx;
            // 纬度3.86~53.55,经度73.66~135.05 
            return !(lng > 73.66 && lng < 135.05 && lat > 3.86 && lat < 53.55);
        }
    }
}
