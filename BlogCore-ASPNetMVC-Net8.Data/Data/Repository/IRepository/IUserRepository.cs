﻿using BlogCore_ASPNetMVC_Net8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore_ASPNetMVC_Net8.Data.Repository.IRepository
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        void DeactivateUser(string userId);
        void ActivateUser(string userId);
    }
}