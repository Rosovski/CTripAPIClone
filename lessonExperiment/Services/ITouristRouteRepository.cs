﻿using lessonExperiment.Models;
using System.Collections.Generic;
using System;

namespace lessonExperiment.Services
{
    public interface ITouristRouteRepository
    {
        IEnumerable<TouristRoute> GetTouristRoutes();
        TouristRoute GetTouristRoute(Guid touristRouteId);
    }
}