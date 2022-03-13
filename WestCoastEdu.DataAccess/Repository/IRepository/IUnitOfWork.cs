﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WestCoastEdu.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ILocationRepository Location { get; }
        IStatusRepository Status { get; }
        IProductRepository Product { get; }

        void Save();
    }
}