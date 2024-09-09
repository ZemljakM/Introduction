﻿using Introduction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Introduction.Service.Common
{
    public interface IClubPresidentService
    {
        public Task<bool> InsertClubPresidentAsync(ClubPresident clubPresident);
    }
}
