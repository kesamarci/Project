﻿using Microsoft.EntityFrameworkCore;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Project.Data
{
    public class DataDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DataDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connStr = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Datas;Integrated Security=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connStr);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
        .HasMany(e => e.Departments)
        .WithOne(d => d.Employees)
        .HasForeignKey(e => e.EmployeeID);

            modelBuilder.Entity<Manager>();
            modelBuilder.Entity<Department>().HasKey(d => d.DepartmentCode);


            base.OnModelCreating(modelBuilder);
        }
    }
}