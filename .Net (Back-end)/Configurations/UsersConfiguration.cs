﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using Practise;
using System;
using REST.Models;

namespace Practise.Configurations
{
    public partial class UsersConfiguration : IEntityTypeConfiguration<users>
    {
        public void Configure(EntityTypeBuilder<users> entity)
        {
            entity.HasKey(e => e.id_user)
                .HasName("PK__users__D2D14637CF5A22F7");

            entity.ToTable("users");

            entity.Property(e => e.id_user).HasColumnName("id_user");

            entity.Property(e => e.usern).HasColumnName("usern");

            entity.Property(e => e.passwordu)
                .HasColumnName("passwordu");

            entity.Property(e => e.imagepath)
                .HasColumnName("imagepath");

            OnConfigurePartial(entity);
        }

        partial void OnConfigurePartial(EntityTypeBuilder<users> entity);
    }
}
