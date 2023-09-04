using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dache.DAL
{
    public class Filter
    {
        bool  beginfilter;
        private string FString { get; set; }
        public Filter()
        {
            this.FString = "";
            beginfilter = false;
        }

        public Filter byuserId(int id)
        {
            this.FString += $"WHERE SupplierID={id}";
            return this;
        }
        public Filter byId(int id)
        {
            this.FString += $"WHERE Id = {id}";
            return this;
        }
        
        public Filter byarea(string areacolection)
        {
            if (areacolection != "-allarea")
            {
                String[] areaoptions = areacolection.Split('-');
                StringBuilder parameters = new StringBuilder();
                parameters.Append($"Area = N'{areaoptions[1]}' ");
                for (int i = 2; i < areaoptions.Length; i++)
                {
                    parameters.Append($"OR Area = N'{areaoptions[i]}' ");
                }
                this.FString += ((beginfilter == false) ? "WHERE " : "AND ") + $"({parameters})";
                beginfilter = true;            
            }
            return this;
        }

        public Filter bycategory(string catcolection)
        {

            if (catcolection != "-allcat")
            {
                String[] areaoptions = catcolection.Split('-');
                StringBuilder parameters = new StringBuilder();
                parameters.Append($"Catrgory = N'{areaoptions[1]}' ");
                for (int i = 2; i < areaoptions.Length; i++)
                {
                    parameters.Append($"OR Catrgory = N'{areaoptions[i]}' ");
                }
                this.FString += ((beginfilter == false) ? "WHERE " : "AND ") + $"({parameters})";
                beginfilter = true;
            }
            return this;
        }

        public Filter byarsearchstr(string serchstring)
        {
            List<string> relevantfilds = new List<string>() { "Name" , "Head" , "Description", "Addrese", "MorDetiles" , "Catrgory", "Area" };
            StringBuilder parameters = new StringBuilder();
            parameters.Append($"{relevantfilds[0]} LIKE N'%{serchstring}%'");
            for (int i = 1; i < relevantfilds.Count(); i++)
            {
                parameters.Append($"OR {relevantfilds[i]} LIKE N'%{serchstring}%'");
            }
            this.FString += ((beginfilter == false) ? "WHERE " : "AND ") + $"({parameters})";
            beginfilter  = true;
            return this;
        }

        public override string ToString()
        {
            return this.FString;
        }
    }
}
