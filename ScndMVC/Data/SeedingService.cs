using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScndMVC.Models;
using ScndMVC.Models.Enums;

namespace ScndMVC.Data
{
    public class SeedingService
    {
        private MainContext _context;

        public SeedingService(MainContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if(_context.Department.Any() ||
               _context.Seller.Any() ||
               _context.SalesRecord.Any())
            {
                return; //DB has been seeded already
            }

            Department d1 = new Department(1, "Computers");
            Department d2 = new Department(2, "Electronics");
            Department d3 = new Department(3, "Fashion");
            Department d4 = new Department(4, "Books");

            Seller s1 = new Seller(1, "Bob Brown", "bob@gmail.com", new DateTime(1998, 4, 21), 10000, d3);
            Seller s2 = new Seller(2, "maria Green", "MarryG@gmail.com", new DateTime(1979, 12, 31), 9500, d4);
            Seller s3 = new Seller(3, "Alex Grey", "Alex.Gray@gmail.com", new DateTime(1993, 12, 10), 13370, d1);
            Seller s4 = new Seller(4, "Martha Red", "DarkMarthaRed@gmail.com", new DateTime(1988, 1, 10), 10250, d4);
            Seller s5 = new Seller(5, "Donald Blue", "DonaldNotTrump@gmail.com", new DateTime(1970, 6, 16), 23600, d2);
            Seller s6 = new Seller(6, "Alex Pink", "WalterWhiteFriend@gmail.com", new DateTime(1997, 4, 21), 9200, d2);

            SalesRecord sr1  = new SalesRecord(1, new DateTime(2024, 7, 14), 11000.0, SaleStatus.Billed, s1);
            SalesRecord sr2  = new SalesRecord(2, new DateTime(2024, 7, 16), 12000.0, SaleStatus.Billed, s3);
            SalesRecord sr3  = new SalesRecord(3, new DateTime(2024, 7, 21), 9000.0, SaleStatus.Billed, s1);
            SalesRecord sr4  = new SalesRecord(4, new DateTime(2024, 8, 1), 1110500.0, SaleStatus.Canceled, s5);
            SalesRecord sr5  = new SalesRecord(5, new DateTime(2024, 8, 3), 13000.0, SaleStatus.Billed, s6);
            SalesRecord sr6  = new SalesRecord(6, new DateTime(2024, 8, 5), 6000.0, SaleStatus.Billed, s2);
            SalesRecord sr7  = new SalesRecord(7, new DateTime(2024, 8, 6), 7000.0, SaleStatus.Billed, s4);
            SalesRecord sr8  = new SalesRecord(8, new DateTime(2024, 8, 30), 110000.0, SaleStatus.Billed, s5);
            SalesRecord sr9  = new SalesRecord(9, new DateTime(2024, 9, 11), 200109.11, SaleStatus.Pending, s1);
            SalesRecord sr10 = new SalesRecord(10, new DateTime(2024, 9, 13), 2256.25, SaleStatus.Billed, s4);
            SalesRecord sr11 = new SalesRecord(11, new DateTime(2024, 9, 15), 1000.0, SaleStatus.Billed, s1);
            SalesRecord sr12 = new SalesRecord(12, new DateTime(2024, 9, 17), 17500.0, SaleStatus.Canceled, s2);
            SalesRecord sr13 = new SalesRecord(13, new DateTime(2024, 9, 17), 12350.0, SaleStatus.Billed, s2);
            SalesRecord sr14 = new SalesRecord(14, new DateTime(2024, 9, 20), 4762.5, SaleStatus.Billed, s4);
            SalesRecord sr15 = new SalesRecord(15, new DateTime(2024, 9, 23), 1500.0, SaleStatus.Canceled, s2);
            SalesRecord sr16 = new SalesRecord(16, new DateTime(2024, 9, 30), 5375.25, SaleStatus.Billed, s3);
            SalesRecord sr17 = new SalesRecord(17, new DateTime(2024, 10, 2), 3366.99, SaleStatus.Billed, s6);
            SalesRecord sr18 = new SalesRecord(18, new DateTime(2024, 10, 6), 3100.0, SaleStatus.Billed, s5);
            SalesRecord sr19 = new SalesRecord(19, new DateTime(2024, 10, 14), 4620.0, SaleStatus.Billed, s6);
            SalesRecord sr20 = new SalesRecord(20, new DateTime(2024, 10, 16), 8921.0, SaleStatus.Billed, s1);
            SalesRecord sr21 = new SalesRecord(21, new DateTime(2024, 10, 18), 9230.0, SaleStatus.Billed, s2);
            SalesRecord sr22 = new SalesRecord(22, new DateTime(2024, 10, 24), 10560.0, SaleStatus.Billed, s4);
            SalesRecord sr23 = new SalesRecord(23, new DateTime(2024, 10, 25), 8100.0, SaleStatus.Billed, s3);
            SalesRecord sr24 = new SalesRecord(24, new DateTime(2024, 11, 1), 19110.0, SaleStatus.Pending, s5);
            SalesRecord sr25 = new SalesRecord(25, new DateTime(2024, 11, 11), 115.0, SaleStatus.Canceled, s4);
            SalesRecord sr26 = new SalesRecord(26, new DateTime(2024, 11, 22), 34500.0, SaleStatus.Billed, s3);
            SalesRecord sr27 = new SalesRecord(27, new DateTime(2024, 11, 29), 600.0, SaleStatus.Billed, s1);
            SalesRecord sr28 = new SalesRecord(28, new DateTime(2024, 12, 1), 950.0, SaleStatus.Billed, s2);
            SalesRecord sr29 = new SalesRecord(29, new DateTime(2024, 12, 2), 1290.0, SaleStatus.Billed, s1);
            SalesRecord sr30 = new SalesRecord(30, new DateTime(2024, 12, 12), 14500.0, SaleStatus.Pending, s6);

            _context.Department.AddRange(d1, d2, d3, d4);
            _context.Seller.AddRange(s1, s2, s3, s4, s5, s6);
            _context.SalesRecord.AddRange( sr1,  sr2,  sr3,  sr4,  sr5,  sr6,  sr7,  sr8,  sr9, sr10, 
                                          sr11, sr12, sr13, sr14, sr15, sr16, sr17, sr18, sr19, sr20,
                                          sr21, sr22, sr23, sr24, sr25, sr26, sr27, sr28, sr29, sr30);

            _context.SaveChanges();
        }
    }
}
