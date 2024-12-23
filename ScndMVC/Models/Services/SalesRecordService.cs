﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ScndMVC.Models.Services
{
    public class SalesRecordService
    {
        private readonly MainContext _context;

        public SalesRecordService (MainContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj; //transformando de tipo <DbSet> para <IQueryable>
            if (minDate.HasValue)
                result = result.Where(x => x.Date >= minDate.Value);

            if(maxDate.HasValue)
                result = result.Where(x => x.Date <= maxDate.Value);

            return await result
                  .Include(x => x.Seller)
                  .Include(x => x.Seller.Department)
                  .OrderByDescending(x => x.Date)
                  .ToListAsync();
        }

        public async Task <List <IGrouping <Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj; //transformando de tipo <DbSet> para <IQueryable>
            if (minDate.HasValue)
                result = result.Where(x => x.Date >= minDate.Value);

            if (maxDate.HasValue)
                result = result.Where(x => x.Date <= maxDate.Value);

            var data = await result
                       .Include(x => x.Seller)
                       .Include(x => x.Seller.Department)
                       .OrderByDescending(x => x.Date)
                       .ToListAsync();

            return data.GroupBy(x => x.Seller.Department).ToList();
        }
    }
}
