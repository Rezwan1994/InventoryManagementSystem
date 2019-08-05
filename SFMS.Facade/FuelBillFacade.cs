﻿using SFMS.Entity;
using SFMS.Repository;
using System;
using System.Collections.Generic;
using System.IO;

namespace SFMS.Facade
{
    public class FuelBillFacade : Facade<FuelBill>
    {
        FuelBillRepository fuelrepo = null;
        public FuelBillFacade(DataContext dataContext) : base(dataContext)
        {
            fuelrepo = new FuelBillRepository(dataContext);
        }

        public FuelModel GetFuels(FuelFilter filter)
        {

            return fuelrepo.GetFuels(filter);
        }

        public FuelBillVM GetFuelsById(int id)
        {
            return fuelrepo.GetFuelById(id);
        }
        public List<FuelBill> GetAllFuelBillbyIdList(List<string> IdList)
        {

            return fuelrepo.GetAllBillsbyIdList(IdList);
        }
        public void WriteCSVFile(string data, string tempFolderPath)
        {
            try
            {
                File.WriteAllText(tempFolderPath, data);
            }
            catch (Exception ex) {  /*TODO: You must process this exception.*/}
        }
    }
}
