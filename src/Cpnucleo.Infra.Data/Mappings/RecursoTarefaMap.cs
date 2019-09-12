﻿using Cpnucleo.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cpnucleo.Infra.Data.Mappings
{
    class RecursoTarefaMap : IEntityTypeConfiguration<RecursoTarefa>
    {
        public void Configure(EntityTypeBuilder<RecursoTarefa> builder)
        {
            builder.ToTable("CPN_TB_RECURSO_TAREFA");

            builder.Property(c => c.Id)
                .HasColumnName("RTAR_ID");

            builder.Property(c => c.PercentualTarefa)
                .HasColumnName("RTAR_PERCENTUAL")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(c => c.IdRecurso)
                .HasColumnName("REC_ID")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(c => c.IdTarefa)
                .HasColumnName("TAR_ID")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(c => c.DataInclusao)
                .HasColumnName("RTAR_DATA_INCLUSAO")
                .HasColumnType("datetime");

            builder.Property(c => c.DataAlteracao)
                .HasColumnName("RTAR_DATA_ALTERACAO")
                .HasColumnType("datetime");

            builder
                .HasOne(p => p.Recurso)
                .WithMany()
                .HasForeignKey(f => f.IdRecurso);

            builder
                .HasOne(p => p.Tarefa)
                .WithMany()
                .HasForeignKey(f => f.IdTarefa);
        }
    }
}