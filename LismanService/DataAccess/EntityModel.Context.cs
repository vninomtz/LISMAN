﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EntityModelContainer : DbContext
    {
        public EntityModelContainer()
            : base("name=EntityModelContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Player> PlayerSet { get; set; }
        public virtual DbSet<Account> AccountSet { get; set; }
        public virtual DbSet<Record> RecordSet { get; set; }
        public virtual DbSet<Game> GameSet { get; set; }
    }
}
