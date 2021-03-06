﻿using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PortoGO.DB.Repositories
{
    public class RouteRepository : BaseRepository<Route, int>, IRouteRepository
    {
        public RouteRepository(DbContext context) : base(context)
        {
        }
    }
}
