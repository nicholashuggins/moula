using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.EntityConfiguration;
using Persistence.Models;
using System;
using System.Collections.Generic;
using System.IO;
using static Persistence.Enums.Enumerations;
using Persistence.Enums;
using Persistence.QueryModels;

namespace Persistence
{
    public class MoulaContext : DbContext
    {

        public DbSet<StatusReason> StatusReasons { get; set; }
        public DbSet<RequestStatus> RequestStatuses { get; set; }
        public DbSet<PaymentRequest> PaymentRequests { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<PaymentRequestView> PaymentRequestView { get; set; }
        public MoulaContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dir = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true,
                             reloadOnChange: true);
            var cfg = builder.Build();

            var conn = cfg.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(conn);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentRequestConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new StatusReasonConfiguration());
            modelBuilder.ApplyConfiguration(new RequestStatusConfiguration());
            SeedStatusData(modelBuilder);
            SeedReasonData(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        private void SeedStatusData(ModelBuilder modelBuilder)
        {
            var data = new List<RequestStatus>();
            data.Add(new RequestStatus { Id = (int)StatusOfRequest.Pending,  Description =  StatusOfRequest.Pending.GetDescription() });
            data.Add(new RequestStatus { Id = (int)StatusOfRequest.Processed, Description = "Processed" });
            data.Add(new RequestStatus { Id = (int)StatusOfRequest.Closed, Description = "Closed" });
            modelBuilder.Entity<RequestStatus>().HasData(data);
        }
        private void SeedReasonData(ModelBuilder modelBuilder)
        {
            var data = new List<StatusReason>();
            data.Add(new StatusReason { Id = (int)RequestReason.ValidRequest, Description = RequestReason.ValidRequest.GetDescription() });
            data.Add(new StatusReason { Id = (int)RequestReason.InsufficientFunds, Description = RequestReason.InsufficientFunds.GetDescription() });
            data.Add(new StatusReason { Id = (int)RequestReason.InvalidCancelRequestProcessed, Description = RequestReason.InvalidCancelRequestProcessed.GetDescription() }); 
            modelBuilder.Entity<StatusReason>().HasData(data);
        }
    }
}
