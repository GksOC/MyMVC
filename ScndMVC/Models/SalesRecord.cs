using Microsoft.VisualBasic;
using ScndMVC.Models.Enums;
using System;

namespace ScndMVC.Models
{
    public class SalesRecord
    {
        public int ID { get; set; } 
        public DateTime Date {  get; set; }
        public double Amout { get; set; }

        public SalesStatus Status { get; set; }
        public Seller Seller { get; set; }

        public SalesRecord()
        {

        }

        public SalesRecord(int iD, DateTime date, double amout, SalesStatus status, Seller seller)
        {
            ID = iD;
            Date = date;
            Amout = amout;
            Status = status;
            Seller = seller;
        }
    }
}
