﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CitcWeb.Domain
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CitcEntities : DbContext
    {
        public CitcEntities()
            : base("name=CitcEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AnnualCourse> AnnualCourse { get; set; }
        public virtual DbSet<ClassInfo> ClassInfo { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<LifePicture> LifePicture { get; set; }
        public virtual DbSet<PayRank> PayRank { get; set; }
        public virtual DbSet<StudentReport> StudentReport { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<BookInfo> BookInfo { get; set; }
        public virtual DbSet<BorrowHistory> BorrowHistory { get; set; }
    }
}
