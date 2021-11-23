﻿using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TekkenApp.Models;

namespace TekkenApp.Data
{
    public class HitTypeService : BaseService<HitType, HitType_name>
    {

        public HitTypeService(TekkenDbContext tekkenDbContext) : base(tekkenDbContext, tekkenDbContext.hitType, tekkenDbContext.hitType_name)
        {
            mainTable = TableName.HitType.ToString();
            nameTable = TableName.HitType_name.ToString();
            preUrl = "HitTypes";
        }

        public async Task<HitType> UpdateHitTypeAsync(HitType hitType)
        {
            //hitType.Description=
            _tekkenDBContext.Entry(hitType).State = EntityState.Modified;
            await _tekkenDBContext.SaveChangesAsync();
            return hitType;
        }
    }
}

