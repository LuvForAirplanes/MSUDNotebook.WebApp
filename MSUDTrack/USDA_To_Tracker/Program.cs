using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using USDA_To_Tracker.Models;
using MSUDTrack.DataModels.Models;
using Humanizer;
using Microsoft.VisualBasic;
using ChoETL;

namespace USDA_To_Tracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var usda_nutrients = new List<USDA_Nutrient>();
            var usda_servingSizes = new List<USDA_ServingSize>();
            
            var line = string.Empty;
            using (var file = new StreamReader(Path.Combine(AppContext.BaseDirectory, @"C:\Users\Micah.abc\OneDrive - Pronect Software\Documents\NutriInfo\v1\Raw\CSV\Nutrients.csv")))
            {
                while ((line = file.ReadLine()) != null)
                {
                    var p = line.Split(',');
                    var nutrient = new USDA_Nutrient()
                    {
                        Id = Guid.NewGuid().ToString(),
                        NDB_Number = p[0].Trim().Trim('"'),
                        Name = p[2].Trim().Trim('"'),
                        Value = p[4].Trim().Trim('"'),
                        Value_UOM = p[5].Trim().Trim('"')
                    };

                    if (nutrient.Name == "Protein" || nutrient.Name == "Leucine")
                        usda_nutrients.Add(nutrient);
                }
            }

            line = string.Empty;
            using (var file = new StreamReader(Path.Combine(AppContext.BaseDirectory, @"C:\Users\Micah.abc\OneDrive - Pronect Software\Documents\NutriInfo\v1\Raw\CSV\Serving_Size.csv")))
            {
                while ((line = file.ReadLine()) != null)
                {
                    var p = line.Split(',');
                    usda_servingSizes.Add(new USDA_ServingSize()
                    {
                        Id = Guid.NewGuid().ToString(),
                        NDB_Number = p[0].Trim().Trim('"'),
                        ServingSize = p[1].Trim().Trim('"'),
                        ServingSize_UOM = p[2].Trim().Trim('"')
                    });
                }
            }

            using (var reader = new ChoCSVReader(@"C:\Users\Micah.abc\OneDrive - Pronect Software\Documents\NutriInfo\v1\Raw\CSV\Products.csv"))
            {
                foreach (dynamic chooitem in reader)
                {
                    var usda_food = new USDA_Food()
                    {
                        Id = Guid.NewGuid().ToString(),
                        NDB_Number = chooitem.ValuesArray[0].Replace('"', ' ').Trim().Trim('"'),
                        Name = chooitem.ValuesArray[1].Trim().Replace('"', ' ').Trim('"'),
                        UPC = chooitem.ValuesArray[3].Trim().Replace('"', ' ').Trim('"'),
                        Manufacturer = chooitem.ValuesArray[4].Trim().Replace('"', ' ').Trim('"')
                    };
                     
                    var leucineMg = usda_nutrients.FirstOrDefault(n => n.NDB_Number == usda_food.NDB_Number && n.Name == "Leucine");
                    var proteinMg = usda_nutrients.FirstOrDefault(n => n.NDB_Number == usda_food.NDB_Number && n.Name == "Protein");
                    var weightGm = usda_servingSizes.FirstOrDefault(n => n.NDB_Number == usda_food.NDB_Number);

                    if ((leucineMg != null || proteinMg != null) && weightGm != null && !string.IsNullOrEmpty(weightGm.ServingSize) && !string.IsNullOrEmpty(usda_food.Manufacturer))
                    {
                        var food = new Food()
                        {
                            Id = usda_food.Id,
                            Created = DateTime.Now,
                            Name = usda_food.Name.Replace(",", " ").ToLower().Transform(To.TitleCase),
                            WeightGrams = double.Parse(weightGm.ServingSize),
                            Manufacturer = usda_food.Manufacturer.Replace(",", " ").ToLower().Transform(To.TitleCase)
                        };

                        if (leucineMg != null)
                            food.LeucineMilligrams = double.Parse(leucineMg.Value);

                        if (proteinMg != null)
                            food.ProteinGrams = double.Parse(proteinMg.Value);

                        if (File.Exists(@"C:\Output\Output.csv"))
                        {
                            using (var tw = new StreamWriter(@"C:\Output\Output.csv", true))
                            {
                                tw.WriteLine(food.Id + "," + "2019-07-02 00:00:00" + "," + "2019-07-02 00:00:00" + "," + food.Name + "," + food.ProteinGrams + "," + food.LeucineMilligrams + "," + food.WeightGrams + "," + food.Manufacturer);
                            }
                        }
                    }
                }
            }
        }
    }
}
