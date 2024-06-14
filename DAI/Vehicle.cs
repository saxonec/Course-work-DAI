using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAI
{
    public class Vehicle
    {
        public string Brand { get; set; }
        public string Color { get; set; }
        public string FactoryNumber { get; set; }
        public string PlateNumber { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string ConstructionFeatures { get; set; }
        public string Painting { get; set; }
        public DateTime LastInspectionDate { get; set; }
        public string OwnerPassport { get; set; }



        public Vehicle(
            string brand,
            string color,
            string factoryNumber,
            string plateNumber,
            DateTime manufactureDate,
            string constructionFeatures,
            string painting,
            DateTime lastInspectionDate,
            string ownerPassport)
        {
            Brand = brand;
            Color = color;
            FactoryNumber = factoryNumber;
            PlateNumber = plateNumber;
            ManufactureDate = manufactureDate;
            ConstructionFeatures = constructionFeatures;
            Painting = painting;
            LastInspectionDate = lastInspectionDate;
            OwnerPassport = ownerPassport;
        }
    }

}
