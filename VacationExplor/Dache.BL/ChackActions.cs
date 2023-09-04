using Dache.DAL;
using Dache.DP;
using System;
using System.Collections.Generic;

namespace Dache.BL
{
    public class ChackActions
    {
        public static void Insert <T>(T RegisterData, string Table)
        {
            SqueryDB.Insert<T>(RegisterData, Table);
        }
        public static UserHeader GetUserHeder(string UserName , string Password)
        {
            UserHeader userHeader = SqueryDB.FindUser(UserName, Password);
            return userHeader;
        }

        public static void Update<T>(T RegisterData, string Table ,int Id)
        {
            string filter = new Filter().byId(Id).ToString();
            SqueryDB.Update<T>(RegisterData, Table, filter);
        }
        public static bool CheckUserName(Login login)
        {
            return SqueryDB.CheckUsername(login);
        }

        public static List<Service> GetInfo(Dictionary<string,string> routevals)
        {
            List<Service> result;
            string filter = string.Empty; 
            switch (routevals.Count)
            {
                case 2:
                    result = SqueryDB.GetInfo(filter);
                    return result;
                case 4:
                    filter = new Filter().byarea(routevals["areas"]).bycategory(routevals["categorys"]).ToString();
                    result = SqueryDB.GetInfo(filter);
                    return result;
                case 5:
                    filter = new Filter().byarea(routevals["areas"]).bycategory(routevals["categorys"]).byarsearchstr(routevals["searchStr"]).ToString();
                    result = SqueryDB.GetInfo(filter);
                    return result;
                default:
                    result = SqueryDB.GetInfo(filter);
                    return result;
            }

        }

        public static List<Service> GetUserInfo(int Id)
        {
            List<Service> result;
            string filter = new Filter().byuserId(Id).ToString();
            result = SqueryDB.GetInfo(filter);
            return result;     
        }
        public static List<Service> Getinfobyid(int Id)
        {
            List<Service> result;
            string filter = new Filter().byId(Id).ToString();
            result = SqueryDB.GetInfo(filter);
            return result;

        }
        public static string Delete(string Table , int Id)
        {
            string response = SqueryDB.Delete(Table, Id);    
            return response;
        }
    }
}
