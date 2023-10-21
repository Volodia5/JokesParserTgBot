using System;
using System.Collections.Generic;
using JokesParserTgBot.Models;
using Microsoft.EntityFrameworkCore;

namespace JokesParserTgBot.DbConnector;

public partial class JokesParserTgBotVododyaDbContext : DbContext
{
    public JokesParserTgBotVododyaDbContext()
    {
    }

    public JokesParserTgBotVododyaDbContext(DbContextOptions<JokesParserTgBotVododyaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Joke> Jokes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=194.67.105.79:5432;Database=jokes_parser_tg_bot_vododya_db;Username=jokes_parser_tg_bot_vododya_user;Password=12345");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Joke>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jokes_pk");

            entity.ToTable("jokes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.QuoteText)
                .IsRequired()
                .HasMaxLength(8000)
                .HasColumnName("quote_text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
